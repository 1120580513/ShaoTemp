using Shao.ApiTemp.Infrastructure.Filters;

namespace Shao.ApiTemp.Controllers.Base;

/// <summary>
/// 
/// </summary>
[Route("[controller]/[action]")]
[ApiController]
[ReqValidAttrbute]
public class ApiController : ControllerBase
{
}
