namespace Shao.ApiTemp.Domain.Store;

public enum StoreStatus
{
    /// <summary>
    /// 关
    /// </summary>
    [Display(Name ="禁用")]
    Off = 0,
    /// <summary>
    /// 开
    /// </summary>
    [Display(Name ="开启")]
    On = 1,
}
