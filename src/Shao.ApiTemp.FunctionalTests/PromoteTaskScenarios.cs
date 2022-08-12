using Shao.ApiTemp.Domain.Dto.GiveGoods;
using Shao.ApiTemp.Domain.Dto.PromoteTask;
using Shao.ApiTemp.Domain.Dto.Store;

namespace Shao.ApiTemp.FunctionalTests;

[TestClass]
public class PromoteTaskScenarios : ScenariosBase
{
    [TestMethod]
    public async Task Main_flow_and_delete_response_ok()
    {
        using var server = CreateServer();
        using var client = server.CreateClient();

        var idReq = await InitUnpublishTask(client);

        var deleteR = await PostR(client, "PromoteTask/Delete", idReq);
        Assert.IsTrue(deleteR.IsSucc);
    }
    [TestMethod]
    public async Task Main_flow_and_close_response_ok()
    {
        using var server = CreateServer();
        using var client = server.CreateClient();

        var idReq = await InitUnpublishTask(client);

        var closeR = await PostR(client, "PromoteTask/Close", idReq);
        Assert.IsTrue(closeR.IsSucc);
    }
    [TestMethod]
    public async Task Main_flow_and_publich_response_ok()
    {
        using var server = CreateServer();
        using var client = server.CreateClient();

        var idReq = await InitUnpublishTask(client);

        var publiishR = await PostR(client, "PromoteTask/Publish", idReq);
        Assert.IsTrue(publiishR.IsSucc);
    }

    private async Task<PromoteTaskIdReq> InitUnpublishTask(HttpClient client)
    {
        var queryReq = new QueryPromoteTaskReq()
        {
            Page = 1,
            PageSize = 10
        };

        var queryRStore = await PostR<IEnumerable<QueryStoreDto>>(client, StoreScenarios.Url.Query, queryReq);
        Assert.IsTrue(queryRStore.IsSucc);
        var store = queryRStore.Data.FirstOrDefault(x => x.StoreStatus == Domain.Store.StoreStatus.On);
        Assert.IsNotNull(store, "未找到开启的店铺");

        var queryRGiveGoods =
            await PostR<IEnumerable<QueryGiveGoodsDto>>(client, GiveGoodsScenarios.Url.Query, queryReq);
        Assert.IsTrue(queryRGiveGoods.IsSucc);
        var giveGoods = queryRGiveGoods.Data
            .FirstOrDefault(x => x.GiveGoodsStatus == Domain.GiveGoods.GiveGoodsStatus.On);
        Assert.IsNotNull(giveGoods, "未找到开启的赠品");

        var queryR = await PostR<IEnumerable<QueryPromoteTaskDto>>(client, "PromoteTask/Query", queryReq);
        Assert.IsTrue(queryR.IsSucc);

        var spec = new SavePromoteTaskSpecReq()
        {
            PromoteTaskSpecId = default,
            SpecNum = 1,
            IsDelete = default,
            GiveGoodsId = giveGoods.GiveGoodsId,
            GiveGoodsName = giveGoods.GiveGoodsName,
            GiveGoodsCode = giveGoods.GiveGoodsCode,
            GiveGoodsNum = giveGoods.GiveGoodsNum,
        };
        var saveReq = new SavePromoteTaskReq()
        {
            PromoteTaskId = default,
            PromoteTaskName = "FakePromoteTaskName",
            StoreId = store.StoreId,
            StartTime = DateTime.Now.AddDays(-7).Date,
            EndTime = DateTime.Now.Date,
            Specs = new List<SavePromoteTaskSpecReq>() { spec },
        };
        var unpublishPromoteTask = queryR.Data
            .FirstOrDefault(x => x.PromoteTaskStatus == Domain.PromoteTask.PromoteTaskStatus.Unpublished);
        if (unpublishPromoteTask is not null)
        {
            var unpublishGetPromoteTask = await GetR<PromoteTaskDto>(
                client, $"PromoteTask/Get?promoteTaskId={unpublishPromoteTask.PromoteTaskId}");
            Assert.IsTrue(unpublishGetPromoteTask.IsSucc);

            saveReq.PromoteTaskId = unpublishPromoteTask.PromoteTaskId;
            saveReq.Specs.First().PromoteTaskSpecId = unpublishGetPromoteTask.Data.Specs.First().PromoteTaskSpecId;
        }
        var saveR = await PostR(client, "PromoteTask/Save", saveReq);
        Assert.IsTrue(saveR.IsSucc);

        queryR = await PostR<IEnumerable<QueryPromoteTaskDto>>(client, "PromoteTask/Query", queryReq);
        Assert.IsTrue(queryR.IsSucc);
        Assert.IsTrue(queryR.Data.Count() > 0);

        unpublishPromoteTask = queryR.Data
            .First(x => x.PromoteTaskStatus == Domain.PromoteTask.PromoteTaskStatus.Unpublished);
        var idReq = new PromoteTaskIdReq(unpublishPromoteTask.PromoteTaskId);
        var getR = await GetR<PromoteTaskDto>(client, $"PromoteTask/Get?promoteTaskId={idReq.PromoteTaskId}");
        Assert.IsTrue(queryR.IsSucc);

        return idReq;
    }
}