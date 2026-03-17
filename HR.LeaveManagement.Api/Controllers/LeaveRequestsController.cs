using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetail;
using HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;

namespace HR.LeaveManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LeaveRequestsController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeaveRequestsController(IMediator mediator)
    {
        this._mediator = mediator;
    }

    [HttpGet]
    public async Task<List<LeaveRequestListDTO>> Get()
    {
        var leaveRequests = await _mediator.Send(new GetLeaveRequestListQuery());
        return leaveRequests;
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<LeaveRequestDetailDTO>> GetById(int id)
    {
        var leaveRequest = await _mediator.Send(new GetLeaveRequestDetailQuery { Id = id });
        return Ok(leaveRequest);
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult> CreateLeaveRequest(CreateLeaveRequestCommand leaveRequest)
    {
        var response = await _mediator.Send(leaveRequest);

        return CreatedAtAction(nameof(GetById), new { id = response }, leaveRequest);

    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateLeaveRequest(int id, UpdateLeaveRequestCommand leaveRequest)
    {
        if (id != leaveRequest.Id)
        {
            return BadRequest("Id in URL must match Id in body.");
        }

        var response = await _mediator.Send(leaveRequest);
        return NoContent();
    }

    [HttpPut]
    [Route("CancelRequest")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> CancelLeaveRequest(CancelLeaveRequestCommand leaveRequest) 
    {
        await _mediator.Send(leaveRequest);
        return NoContent();
    }
    
    [HttpPut]
    [Route("UpdateApproval")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> ChangeLeaveRequest(ChangeLeaveRequestApprovalCommand leaveRequestApproval) {
        await _mediator.Send(leaveRequestApproval);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> DeleteLeaveRequest(int id)
    {
        var command = new DeleteLeaveRequestCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }
}
