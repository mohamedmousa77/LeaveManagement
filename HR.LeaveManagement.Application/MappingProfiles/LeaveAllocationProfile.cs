using AutoMapper;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Commands.CreateLeaveAllocatio;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Commands.UpdateLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Queries.GetLeaveAllocations;
using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.MappingProfiles;

public class LeaveAllocationProfile : Profile
{
    public LeaveAllocationProfile()
    { 
        CreateMap<LeaveAllocationDTO, LeaveAllocation>().ReverseMap();
        CreateMap<LeaveAllocation, LeaveAllocationDTO>();
        CreateMap<CreateLeaveAllocationCommand, LeaveAllocation>();
        CreateMap<UpdateLeaveAllocationCommand, LeaveAllocation>();

    }

}
