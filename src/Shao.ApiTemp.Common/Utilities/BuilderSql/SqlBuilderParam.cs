using System.Text;

namespace Shao.ApiTemp.Common.Utilities.BuilderSql;

public class SqlBuilderParam
{
    private readonly SqlBuilder _sqlBuilder;
    protected StringBuilder _builder;
    private SqlBuilderWhere? _where;
    public SqlBuilderParam(SqlBuilder sqlBuilder)
    {
        _sqlBuilder = sqlBuilder;
        _builder = new StringBuilder();
    }

    public virtual SqlBuilderParam Append(string str, bool isAppend = true)
    {
        if (isAppend)
        {
            _builder.Append(str);
        }
        return this;
    }

    public SqlBuilder Builder => _sqlBuilder;
    public SqlBuilderWhere Where
    {
        get
        {
            _where ??= new SqlBuilderWhere(this);
            return _where;
        }
    }
    internal StringBuilder StringBuilder => _builder;
}
