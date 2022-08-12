using Autofac;
using FluentValidation;

namespace Shao.ApiTemp.Common.Dto;

public interface IReq { }

public class Req : IReq, IEnsure
{
    public virtual void Ensure() { }

    public static void Ensure<TReq>(TReq? req) where TReq : Req
    {
        if (req is null) throw new ValidException("参数不能为空");

        req.Ensure();

        var validators = App.Resolve<IEnumerable<IValidator<TReq>>>();

        var failures = validators
             .Select(x => x.Validate(req))
             .SelectMany(x => x.Errors)
             .Where(error => error is not null)
             .ToList();
        if (failures.Any())
        {
            throw new ValidException("参数验证错误", failures);
        }
    }

    /// <inheritdoc />
    /// <exception cref="ValidException"/>
    public void AreEnsure(bool condition, string message, params object[] args)
    {
        if (!condition) throw new ValidException(message);
    }
}
public class ReqValidator<TReq> : AbstractValidator<TReq> where TReq : Req
{
    public ReqValidator() { }
}
