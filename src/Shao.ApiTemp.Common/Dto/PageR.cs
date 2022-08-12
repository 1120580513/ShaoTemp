namespace Shao.ApiTemp.Common.Dto;

public record PageR
{
    public PageR() { }
    public PageR(PageReq req, int total)
    {
        Page = req.Page;
        PageSize = req.PageSize;
        Total = total;
    }

    public int Page { get; init; }
    public int PageSize { get; init; }
    public int Total { get; init; }
}
