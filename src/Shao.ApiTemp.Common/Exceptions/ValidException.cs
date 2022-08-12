using FluentValidation.Results;

namespace Shao.ApiTemp.Common.Exceptions;

public class ValidException : CustomException
{
    private readonly List<ValidationFailure> _errors;
    public ValidException(string msg) : base(msg)
    {
        _errors = Enumerable.Empty<ValidationFailure>().ToList();
    }
    public ValidException(string msg, List<ValidationFailure> errors) : base(msg)
    {
        _errors = errors;
    }

    public List<ValidationFailure> Errors => _errors;
}
