using AutoMapper;
using Shao.ApiTemp.Domain.Dto.Store;
using Shao.ApiTemp.Domain.Store;
using Shao.ApiTemp.Domain.Dto.PromoteTask;
using Shao.ApiTemp.Domain.PromoteTask;
using Shao.ApiTemp.Domain.Dto.UserTask;
using Shao.ApiTemp.Domain.UserTask;
using Shao.ApiTemp.Domain.Dto.GiveGoods;
using Shao.ApiTemp.Domain.GiveGoods;

namespace Shao.ApiTemp.Domain.Dto.Mapper;

public class EntityConvertDtoProfile : Profile
{
    public EntityConvertDtoProfile()
    {
        CreateMap<SaveGiveGoodsReq, GiveGoodsDo>().ReverseMap();

        CreateMap<StoreDto, StoreDo>().ReverseMap();
        CreateMap<SaveStoreReq, StoreDo>().ReverseMap();
        CreateMap<StoreConfigDto, StoreConfigDo>().ReverseMap();
        CreateMap<SaveStoreConfigReq, StoreConfigDo>().ReverseMap();

        CreateMap<PromoteTaskDto, PromoteTaskDo>().ReverseMap();
        CreateMap<SavePromoteTaskReq, PromoteTaskDo>().ReverseMap();
        CreateMap<PromoteTaskSpecDto, PromoteTaskSpecDo>().ReverseMap();
        CreateMap<SavePromoteTaskSpecReq, PromoteTaskSpecDo>().ReverseMap();

        CreateMap<CliamUserTaskReq, UserTaskDo>().ReverseMap();
    }
}
