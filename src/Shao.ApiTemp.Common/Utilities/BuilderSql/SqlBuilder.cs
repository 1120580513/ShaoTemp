namespace Shao.ApiTemp.Common.Utilities.BuilderSql;

public class SqlBuilder
{
    private readonly string _body;

    public SqlBuilder(string body)
    {
        _body = body;
        Fill = new SqlBuilderFillCollection(this);
    }
    internal SqlBuilder(string body, SqlBuilder parent) : this(body)
    {
        Parent = parent;
    }

    public SqlBuilderFillCollection Fill { get; init; }
    public SqlBuilder? Parent { get; init; }

    public string Build()
    {
        if (Parent is not null) return Parent.Build();
        return CurrentBuild();
    }
    internal string CurrentBuild()
    {
        var formatArgs = Fill.Build().ToArray();
        return string.Format(_body, formatArgs);
    }
}
