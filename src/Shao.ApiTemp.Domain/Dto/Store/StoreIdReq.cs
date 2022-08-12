using Shao.ApiTemp.Domain.Store;

namespace Shao.ApiTemp.Domain.Dto.Store;

/// <summary>
///  唯一店铺
/// </summary>
public class StoreIdReq : Req, IStoreId
{
    /// <inheritdoc />
    public long StoreId { get; set; }

    public StoreIdReq() { }
    public StoreIdReq(long storeId)
    {
        StoreId = storeId;
    }
}
public class StoreIdReqValitator : ReqValidator<StoreIdReq>
{
    public StoreIdReqValitator()
    {
        RuleFor(x => x.StoreId).GreaterThan(0).WithName("店铺标识");
    }
}
public class StoreIdReqValitator<TReq> : ReqValidator<TReq>
    where TReq : StoreIdReq
{
    public StoreIdReqValitator()
    {
        RuleFor(x => x.StoreId).GreaterThan(0).WithName("店铺标识");
    }
}
