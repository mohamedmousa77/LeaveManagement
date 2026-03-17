using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Email;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;

public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, Unit>
{
    private readonly IEmailSender _emailSender;
    private readonly IMapper _mapper;
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IAppLogger<UpdateLeaveRequestCommandHandler> _logger;

    public UpdateLeaveRequestCommandHandler(
        IEmailSender emailSender,
        IMapper mapper, 
        ILeaveRequestRepository leaveRequestRepository,
        ILeaveTypeRepository leaveTypeRepository,
        IAppLogger<UpdateLeaveRequestCommandHandler> logger)
    {
        _emailSender = emailSender;
        _mapper = mapper;
        _leaveRequestRepository = leaveRequestRepository;
        _leaveTypeRepository = leaveTypeRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateLeaveRequestCommandValidator(_leaveTypeRepository, _leaveRequestRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in update request for {0} - {1}", nameof(LeaveRequest), request.Id);
            throw new BadRequestException("Invalid leave request update", validationResult);
        }

        var leaveRequestToUpdate = await _leaveRequestRepository.GetByIdAsync(request.Id);

        if (leaveRequestToUpdate is null)
        {
            throw new NotFoundException(nameof(LeaveRequest), request.Id);
        }

        _mapper.Map(request, leaveRequestToUpdate);

        await _leaveRequestRepository.UpdateAsync(leaveRequestToUpdate);

        // Send confirmation email.
        try
        {
            var email = new EmailMessage
            {
                To = string.Empty,
                Body = $"You leave request for {request.StartDate:D} to {request.EndDate} has been updated successfully",
                Subject = "Leave request update"
            };
            await _emailSender.SendEmail(email);

        }catch (Exception ex)        
        {
            _logger.LogWarning($"Error: {ex}");
        }

            return Unit.Value;
        
    }
}
