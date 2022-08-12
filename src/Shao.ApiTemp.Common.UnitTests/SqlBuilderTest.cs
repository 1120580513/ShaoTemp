using Shao.ApiTemp.Common.Utilities.BuilderSql;

namespace Shao.ApiTemp.Common.UnitTests;

[TestClass]
public class SqlBuilderTest
{
    [TestMethod]
    public void Test_SqlBuilder_Build()
    {
        var exceptSql = @"
SELECT *,B.Id
FROM dbo.A

JOIN dbo.B ON A.Id = B.Id AND A.Name = B.Name
JOIN dbo.C ON B.Id = C.Id

 WHERE name = @name AND Status = '1'
";

        var req = new { name = "aaaaa" };
        var sql = new SqlBuilder(@"
SELECT *{0}
FROM dbo.A
{1}
{2}
")
            .Fill[0]
                .Append(",B.Id", true)
            .Builder.Fill[1, @"
JOIN dbo.B ON A.Id = B.Id{0}
{1}
"]
                .Fill[0]
                    .Append(" AND A.Name = B.Name", true)
                .Builder.Fill[1]
                    .Append("JOIN dbo.C ON B.Id = C.Id", true)
            .Builder.Parent!.Fill[2]
                .Where.ParamAnd(nameof(req.name), true)
                      .Append("", true)
                      .And("Status", "1", true)
            .Builder.Build();

        Assert.AreEqual(exceptSql, sql);
    }
}
