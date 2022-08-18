using Shao.ApiTemp.Domain.PromoteTask;
using Shao.ApiTemp.Domain.ThirdOrder;
using Shao.ApiTemp.Domain.UserTask;

namespace Shao.ApiTemp.Domain.UserTaskRecord;

/// <summary>
///  
/// </summary>
public class UserTaskRecordDo : BaseDo, IEntity, IUserTaskRecordId
{
    /// <summary>
    ///  
    /// </summary>
    public long UserTaskRecordId { get; set; }
    /// <summary>
    /// 用户任务
    /// </summary>
    public UserTaskDo UserTask { get; set; }
    /// <summary>
    /// 匹配的规格
    /// </summary>
    public PromoteTaskSpecDo MatchedSpec { get; set; }
    /// <summary>
    ///  
    /// </summary>
    public decimal RefundAmount { get; set; }
    /// <summary>
    ///  
    /// </summary>
    public decimal RefundedAmount { get; set; }
    /// <summary>
    ///  
    /// </summary>
    public string GiveGoodsName { get; set; }
    /// <summary>
    ///  
    /// </summary>
    public string GiveGoodsCode { get; set; }
    /// <summary>
    ///  
    /// </summary>
    public int GiveGoodsNum { get; set; }
    /// <summary>
    ///  
    /// </summary>
    public DateTime? CreateOn { get; set; }

    /// <summary>
    /// 自动映射使用
    /// </summary>
    public UserTaskRecordDo() { }
    public static async Task<UserTaskRecordDo> Create(
        UserTaskDo userTask, ThirdOrderDo thirdOrder, PromoteTaskSpecDo promoteTaskSpec)
    {
        var userTaskRecordDo = new UserTaskRecordDo()
        {
            UserTaskRecordId = default,
            UserTask = userTask,
            MatchedSpec = promoteTaskSpec,
            RefundAmount = thirdOrder.PayAmount,
            RefundedAmount = default,
            GiveGoodsName = promoteTaskSpec.GiveGoodsName,
            GiveGoodsCode = promoteTaskSpec.GiveGoodsCode,
            GiveGoodsNum = promoteTaskSpec.GiveGoodsNum,
            CreateOn = DateTime.Now,
        };
        return await Task.FromResult(userTaskRecordDo);
    }

    private readonly IUserTaskRecordRepo _repo = new Lazy<IUserTaskRecordRepo>(
        () => App.Resolve<IUserTaskRecordRepo>()).Value;
}
