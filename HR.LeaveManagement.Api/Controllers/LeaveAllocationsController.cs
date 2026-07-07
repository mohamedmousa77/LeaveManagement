using HR.LeaveManagement.Application.Features.LeaveAllocations.Commands.CreateLeaveAllocatio;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Commands.DeleteLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Commands.UpdateLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Queries.GetLeaveAllocations;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Queries.GetLeaveAllocationsDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace HR.LeaveManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LeaveAllocationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeaveAllocationsController(IMediator mediator)
    {
        this._mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<LeaveAllocationDTO>>> Get()
    {
        var leaveAllocations = await _mediator.Send(new GetLeaveAllocationsQuery());
        return Ok(leaveAllocations);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<LeaveAllocationDetailsDTO>> Get(int id)
    {
        var leaveAllocationDetails =await _mediator.Send(new LeaveAllocationDetailsQuery() { Id = id });
        return Ok(leaveAllocationDetails);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CreateLeaveAllocationCommand>> CreateLeaveAllocation
        (CreateLeaveAllocationCommand leaveAllocation)
    {
        var response = await _mediator.Send(leaveAllocation);
        return  CreatedAtAction(nameof(Get), new { id = response }, response);
    }


    [HttpPut]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<UpdateLeaveAllocationCommand>> UpdateLeaveAllocation (UpdateLeaveAllocationCommand leaveAllocation)
    {
        await _mediator.Send(leaveAllocation);
        return NoContent();
    }


    [HttpDelete("{id}")]
    public  async Task<ActionResult<DeleteLeaveAllocationCommand>> DeleteAllocation(int id)
    {
        var command = new DeleteLeaveAllocationCommand() { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }
}
