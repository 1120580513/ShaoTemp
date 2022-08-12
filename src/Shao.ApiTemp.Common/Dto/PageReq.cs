using FluentValidation;

namespace Shao.ApiTemp.Common.Dto;
public class PageReq : Req
{
    public int Page { get; set; }
    public int PageSize { get; set; }

    public int GetMinRowNo() => (Page - 1) * PageSize + 1;
    public int GetMaxRowNo() => Page * PageSize;
}
public class PageReqValidator<TReq> : ReqValidator<TReq> where TReq : PageReq
{
    public PageReqValidator()
    {
        const int MaxPage = 1000;
        const int MaxPageSize = 1000;
        RuleFor(x => x.Page)
            .GreaterThan(0).WithMessage("页码必须大于0")
            .LessThan(MaxPage).WithMessage($"页码必须小于{MaxPage}");
        RuleFor(x => x.PageSize)
            .GreaterThan(0).WithMessage("页数必须大于0")
            .LessThan(MaxPageSize).WithMessage($"页数必须小于{MaxPageSize}");
    }
}
