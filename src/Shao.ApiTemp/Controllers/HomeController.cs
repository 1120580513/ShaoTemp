namespace Shao.ApiTemp.Controllers;

/// <summary>
/// 默认控制器
/// </summary>
public class HomeController : ControllerBase
{
    /// <summary>
    /// 默认页
    /// </summary>
    /// <returns></returns>
    public IActionResult Index()
    {
        return new RedirectResult("~/swagger");
    }
}
