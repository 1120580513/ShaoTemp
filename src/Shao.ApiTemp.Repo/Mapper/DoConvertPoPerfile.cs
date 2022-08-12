using AutoMapper;
using Shao.ApiTemp.Domain.Store;
using Shao.ApiTemp.Domain.PromoteTask;
using Shao.ApiTemp.Domain.UserTask;
using Shao.ApiTemp.Domain.GiveGoods;

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

        CreateMap<UserTaskDo, UserTaskPo>().ReverseMap();
    }
}
