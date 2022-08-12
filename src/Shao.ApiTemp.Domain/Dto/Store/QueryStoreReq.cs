namespace Shao.ApiTemp.Domain.Dto.Store;

/// <summary>
///  查询店铺请求
/// </summary>
public class QueryStoreReq : PageReq
{
    /// <summary>
    /// 店铺名称 
    /// </summary>
    public string? StoreName { get; set; }
}
public class QueryStoreReqValitator : PageReqValidator<QueryStoreReq>
{
    public QueryStoreReqValitator()
    {
        RuleFor(x => x.StoreName).Length(1, 128).WithName("店铺名称")
            .When(x => !string.IsNullOrWhiteSpace(x.StoreName));
    }
}
