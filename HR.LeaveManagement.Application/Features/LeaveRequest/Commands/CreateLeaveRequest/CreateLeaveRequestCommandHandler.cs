using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Email;
using HR.LeaveManagement.Domain;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;

public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, int>
{
    private readonly IEmailSender _emailSender;
    private readonly IMapper _mapper;
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IAppLogger<CreateLeaveRequestCommandHandler> _logger;

    public CreateLeaveRequestCommandHandler(
        IEmailSender emailSender,
        IMapper mapper, 
        ILeaveRequestRepository leaveRequestRepository,
        ILeaveTypeRepository leaveTypeRepository,
        IAppLogger<CreateLeaveRequestCommandHandler> logger)
    {
        this._emailSender = emailSender;
        _mapper = mapper;
        _leaveRequestRepository = leaveRequestRepository;
        _leaveTypeRepository = leaveTypeRepository;
        _logger = logger;
    }

    public async Task<int> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveRequestCommandValidator(_leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in create request for {0}", nameof(LeaveRequest));
            throw new BadRequestException("Invalid leave request", validationResult);
        }

        var leaveRequest = _mapper.Map<Domain.LeaveRequest>(request);

        await _leaveRequestRepository.CreateAsync(leaveRequest);

        var email = new EmailMessage
        {
            To = string.Empty,
            Body = $"You leave request for {request.StartDate:D} to {request.EndDate} has been created successfully",
            Subject = "Leave request confirmation"
        };

        await _emailSender.SendEmail(email);

        return leaveRequest.Id;
    }
}
