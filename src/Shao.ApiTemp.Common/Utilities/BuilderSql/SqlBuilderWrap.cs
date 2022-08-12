namespace Shao.ApiTemp.Common.Utilities.BuilderSql;

internal class SqlBuilderWrap
{
    public SqlBuilderWrap(SqlBuilder sqlBuilder, string? body)
    {
        Ensure(sqlBuilder, body);
    }
    public SqlBuilderParam? Param { get; set; }
    public SqlBuilder? Builder { get; set; }

    public void Ensure(SqlBuilder sqlBuilder, string? body)
    {
        if (body is null && Param is null)
        {
            Param = new SqlBuilderParam(sqlBuilder);
        }
        if (body is not null && Builder is null)
        {
            Builder = new SqlBuilder(body, sqlBuilder);
        }
    }
    public string Build() => Builder is null ? Param!.StringBuilder.ToString() : Builder!.CurrentBuild();
}
