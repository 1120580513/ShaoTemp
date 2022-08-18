namespace Shao.ApiTemp.Common.Utilities.BuilderSql;

public class SqlBuilderWhere : SqlBuilderParam
{
    public SqlBuilderWhere(SqlBuilderParam param) : base(param.Builder)
    {
        _builder = param.StringBuilder;
    }

    public SqlBuilderWhere And(string colName, string val, bool isAppend)
    {
        if (!isAppend) return this;

        AppendPrefix();
        _builder.AppendFormat("{0} = '{1}'", colName, val);
        return this;
    }
    public SqlBuilderWhere ParamAndStart(string colName, string paramName, bool isAppend)
    {
        if (!isAppend) return this;

        AppendPrefix();
        _builder.AppendFormat("{0} >= @{1}", colName, paramName);
        return this;
    }
    public SqlBuilderWhere ParamAndEnd(string colName, string paramName, bool isAppend)
    {
        if (!isAppend) return this;

        AppendPrefix();
        _builder.AppendFormat("{0} < @{1}", colName, paramName);
        return this;
    }
    public SqlBuilderWhere ParamAndLike(string colName, bool isAppend)
    {
        if (!isAppend) return this;

        AppendPrefix();
        _builder.AppendFormat("{0} LIKE '%'+@{0}+'%'", colName);
        return this;
    }
    public SqlBuilderWhere ParamAnd(string colName, bool isAppend)
    {
        if (!isAppend) return this;

        AppendPrefix();
        _builder.AppendFormat("{0} = @{0}", colName);
        return this;
    }
    public override SqlBuilderWhere Append(string str, bool isAppend)
    {
        if (!isAppend) return this;

        AppendPrefix();
        _builder.Append(str);
        return this;
    }

    private void AppendPrefix()
    {
        _builder.Append(_builder.Length == default ? " WHERE " : " AND ");
    }
}
