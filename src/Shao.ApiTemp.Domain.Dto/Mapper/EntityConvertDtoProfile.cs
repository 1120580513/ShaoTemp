using AutoMapper;
using Shao.ApiTemp.Domain.Dto.PromoteTask;
using Shao.ApiTemp.Domain.Dto.UserTask;
using Shao.ApiTemp.Domain.PromoteTask;
using Shao.ApiTemp.Domain.UserTask;

namespace Shao.ApiTemp.Domain.Dto.Mapper;

public class EntityConvertDtoProfile : Profile
{
    public EntityConvertDtoProfile()
    {
        CreateMap<SavePromoteTaskReq, PromoteTaskDo>().ReverseMap();
        CreateMap<PromoteTaskDo, PromoteTaskDto>().ReverseMap();
    }
}
