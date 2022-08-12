using Microsoft.AspNetCore.Mvc.Filters;
using Shao.ApiTemp.Common.Exceptions;

namespace Shao.ApiTemp.Infrastructure.Filters;

public class GlobalExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.ExceptionHandled) return;

        var ex = context.Exception.GetBaseException();
        var isCustomException = ex is CustomException;
        var resultMsg = isCustomException ? ex.Message : "内部异常，请联系管理员";
        List<ErrorR>? errors = null;

        var validEx = ex as ValidException;
        if (validEx is not null)
        {
            errors = validEx.Errors.Select(x => new ErrorR(x.PropertyName, x.ErrorMessage)).ToList();
        }

        if (!isCustomException)
        {
            Debug.Assert(false);
            App.CreateLog<GlobalExceptionFilter>().Error(
                nameof(OnException), ex, context.ActionDescriptor.DisplayName ?? "Action 匹配失败");
        }

        context.Result = new ContentResult()
        {
            Content = R.Fail(resultMsg, errors).ToJson()
        };
        context.ExceptionHandled = true;
    }
}
