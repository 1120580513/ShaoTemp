using AutoMapper;
using Shao.ApiTemp.Domain.Store;
using Shao.ApiTemp.Domain.PromoteTask;
using Shao.ApiTemp.Domain.UserTask;
using Shao.ApiTemp.Domain.GiveGoods;
using Shao.ApiTemp.Domain.UserTaskRecord;

namespace Shao.ApiTemp.Repo.Mapper;

public class DoConvertPoPerfile : Profile
{
    public DoConvertPoPerfile()
    {
        CreateMap<GiveGoodsDo, GiveGoodsPo>().ReverseMap();

        CreateMap<StoreDo, StorePo>().ReverseMap();
        CreateMap<StoreConfigDo, StoreConfigPo>().ReverseMap();

        CreateMap<PromoteTaskDo, PromoteTaskPo>()
            .ForMember(x => x.StoreId, x => x.MapFrom(y => y.Store.StoreId))
            .ForMember(x => x.StoreName, x => x.MapFrom(y => y.Store.StoreName))
            .ReverseMap();
        CreateMap<PromoteTaskSpecDo, PromoteTaskSpecPo>().ReverseMap();

        CreateMap<UserTaskDo, UserTaskPo>()
            .ForMember(x => x.PromoteTaskId, x => x.MapFrom(y => y.PromoteTask.PromoteTaskId))
            .ForMember(x => x.PromoteTaskName, x => x.MapFrom(y => y.PromoteTask.PromoteTaskName))
            .ForMember(x => x.StoreId, x => x.MapFrom(y => y.PromoteTask.Store.StoreId))
            .ForMember(x => x.StoreName, x => x.MapFrom(y => y.PromoteTask.Store.StoreName))
            .ForMember(x => x.Mobile, x => x.MapFrom(y => y.ClaimUser.Mobile))
            .ReverseMap();

        CreateMap<UserTaskRecordDo, UserTaskRecordPo>()
            .ForMember(x => x.UserTaskId, x => x.MapFrom(y => y.UserTask.UserTaskId))
            .ForMember(x => x.PromoteTaskId, x => x.MapFrom(y => y.UserTask.PromoteTask.PromoteTaskId))
            .ForMember(x => x.PromoteTaskSpecId, x => x.MapFrom(y => y.MatchedSpec.PromoteTaskSpecId))
            .ForMember(x => x.StoreId, x => x.MapFrom(y => y.UserTask.PromoteTask.Store.StoreId))
            .ReverseMap();
    }
}
