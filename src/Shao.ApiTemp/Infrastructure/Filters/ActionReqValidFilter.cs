using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;

namespace Shao.ApiTemp.Infrastructure.Filters;

public class ActionReqValidFilter : IActionFilter
{

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.Controller is null) return;

        var actionDesc = context.ActionDescriptor as ControllerActionDescriptor;
        if (actionDesc is null) return;

        var actionParamLength = actionDesc.MethodInfo.GetParameters().Length;
        if (actionParamLength == default) return;

        var needValid = context.Controller.GetType()
             .GetCustomAttributes(typeof(ReqValidAttrbute), inherit: true).Length > 0;
        if (!needValid) return;

        if (context.ActionArguments.Values.Count == default)
        {// 说明转换成 Req 失败，直接抛出异常
            Req.Ensure((Req?)null);
        };

        foreach (var req in context.ActionArguments.Values)
        {
            if (req is null) continue;
            if (req is not Req) continue;

            // 需要使用反射传递 Req.Ensure<TReq> 的范型参数
            var method = typeof(Req).GetMethod(nameof(Req.Ensure), BindingFlags.Public | BindingFlags.Static)!;
            method = method.MakeGenericMethod(req.GetType());
            method.Invoke(null, new object[] { req });
        }
    }
    public void OnActionExecuted(ActionExecutedContext context) { }
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public class ReqValidAttrbute : Attribute { }