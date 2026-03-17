using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Email;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CancelLeaveRequest
{
    public class CancelLeaveRequestCommandHandler : IRequestHandler<CancelLeaveRequestCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IEmailSender _emailSender;

        public CancelLeaveRequestCommandHandler(
            IMapper mapper, 
            ILeaveRequestRepository leaveRequestRepository,
            IEmailSender emailSender)
        {
            this._mapper = mapper;
            this._leaveRequestRepository = leaveRequestRepository;
            this._emailSender = emailSender;
        }

        public async Task<Unit> Handle(CancelLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);
            
            if( leaveRequest == null )            
                throw new NotFoundException(nameof(leaveRequest), request.Id);

            leaveRequest.Cancelled = true;
            

            var email = new EmailMessage
            {
                To = string.Empty,
                Body = $"Your leave request has been cancelled successfully",
                Subject = "Leave Request Cancelled"
            };

            await _emailSender.SendEmail(email);

            return Unit.Value;
        }
    }
}
