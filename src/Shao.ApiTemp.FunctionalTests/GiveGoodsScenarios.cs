using Shao.ApiTemp.Domain.Dto.GiveGoods;

namespace Shao.ApiTemp.FunctionalTests;

[TestClass]
public class GiveGoodsScenarios : ScenariosBase
{
    [TestMethod]
    public async Task Main_flow_and_response_ok()
    {
        var queryReq = new QueryGiveGoodsReq()
        {
            Page = 1,
            PageSize = 10
        };
        var saveReq = new SaveGiveGoodsReq()
        {
            GiveGoodsId = default,
            GiveGoodsCode = "FakeGiveGoodsCode",
            GiveGoodsName = "FakeGiveGoodsName",
            GiveGoodsNum = 1,
            GiveGoodsStatus = Domain.GiveGoods.GiveGoodsStatus.Off
        };

        using var server = CreateServer();
        using var client = server.CreateClient();

        var queryR = await PostR<IEnumerable<QueryGiveGoodsDto>>(client, Url.Query, queryReq);
        Assert.IsTrue(queryR.IsSucc);
        if (queryR.Data.Count() > 0)
        {
            saveReq.GiveGoodsId = queryR.Data.First().GiveGoodsId;
        }

        var saveR = await PostR(client, "GiveGoods/Save", saveReq);
        Assert.IsTrue(saveR.IsSucc);

        queryR = await PostR<IEnumerable<QueryGiveGoodsDto>>(client, Url.Query, queryReq);
        Assert.IsTrue(queryR.IsSucc);
        Assert.IsTrue(queryR.Data.Count() > 0);

        var idReq = new GiveGoodsIdReq(queryR.Data.First().GiveGoodsId);
        var giveGoods = queryR.Data.First();

        if (giveGoods.GiveGoodsStatus == Domain.GiveGoods.GiveGoodsStatus.Off)
        {
            var openR = await PostR(client, "GiveGoods/Open", idReq);
            Assert.IsTrue(openR.IsSucc);

            var closeR = await PostR(client, "GiveGoods/Close", idReq);
            Assert.IsTrue(closeR.IsSucc);

            openR = await PostR(client, "GiveGoods/Open", idReq);
            Assert.IsTrue(openR.IsSucc);
        }
        else if (giveGoods.GiveGoodsStatus == Domain.GiveGoods.GiveGoodsStatus.On)
        {
            var closeR = await PostR(client, "GiveGoods/Close", idReq);
            Assert.IsTrue(closeR.IsSucc);

            var openR = await PostR(client, "GiveGoods/Open", idReq);
            Assert.IsTrue(openR.IsSucc);
        }
    }
    public static class Url
    {
        public const string Query = "GiveGoods/Query";
    }
}