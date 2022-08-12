namespace Shao.ApiTemp.Domain.Dto.UserTask;

/// <summary>
///  查询用户任务请求
/// </summary>
public class QueryUserTaskReq : PageReq
{
    /// <summary>
    ///  
    /// </summary>
    public long UserTaskId {get;set;} 
    /// <summary>
    ///  
    /// </summary>
    public long PromoteTaskId {get;set;} 
    /// <summary>
    ///  
    /// </summary>
    public string PromoteTaskName {get;set;} 
    /// <summary>
    ///  
    /// </summary>
    public long StoreId {get;set;} 
    /// <summary>
    ///  
    /// </summary>
    public string StoreName {get;set;} 
    /// <summary>
    /// 用户任务状态 0 已领取 1 待审核 2 待匹配 4 待退款 8 退款失败 16 已完成 
    /// </summary>
    public int UserTaskStatus {get;set;} 
    /// <summary>
    /// 用户手机号 
    /// </summary>
    public string Mobile {get;set;} 
    /// <summary>
    /// 订单号 
    /// </summary>
    public string? OrderNo {get;set;} 
    /// <summary>
    /// 匹配时间 
    /// </summary>
    public DateTime? MatchOn {get;set;} 
    /// <summary>
    ///  
    /// </summary>
    public DateTime CreateOn {get;set;} 
    /// <summary>
    ///  
    /// </summary>
    public DateTime ModifyOn {get;set;} 
}
public class QueryUserTaskReqValitator : PageReqValidator<QueryUserTaskReq>
{
    public QueryUserTaskReqValitator()
    {
        RuleFor(x => x.UserTaskId).Must(x => x > 0).WithMessage("UserTaskId不能为空");
    }
}
