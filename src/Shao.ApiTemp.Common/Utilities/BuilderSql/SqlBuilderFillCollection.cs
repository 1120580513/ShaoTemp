namespace Shao.ApiTemp.Common.Utilities.BuilderSql;

public class SqlBuilderFillCollection
{
    private readonly SqlBuilder _sqlBuilder;
    private readonly Dictionary<int, SqlBuilderWrap> _params;
    public SqlBuilderFillCollection(SqlBuilder sqlBuilder)
    {
        _params = new Dictionary<int, SqlBuilderWrap>();
        _sqlBuilder = sqlBuilder;
    }

    public IEnumerable<string> Build()
    {
        var orderlyKeys = _params.Keys.OrderBy(x => x).ToList();
        foreach (var key in orderlyKeys)
        {
            var wrap = _params[key];
            yield return wrap.Build();
        }
    }

    public SqlBuilderParam this[int i] => InitWrap(i, null).Param!;
    public SqlBuilder this[int i, string body] => InitWrap(i, body).Builder!;

    private SqlBuilderWrap InitWrap(int i, string? body)
    {
        if (!_params.ContainsKey(i))
        {
            _params[i] = new SqlBuilderWrap(_sqlBuilder, body);
        }
        _params[i].Ensure(_sqlBuilder, body);
        return _params[i];
    }
}

