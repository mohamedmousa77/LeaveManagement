using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Queries.GetLeaveAllocations;

public record GetLeaveAllocationsQuery : IRequest<List<LeaveAllocationDTO>>;

