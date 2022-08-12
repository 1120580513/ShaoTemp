using Shao.ApiTemp.Common.Dto;
using Shao.ApiTemp.Common.Extensions;
using Shao.ApiTemp.Domain.Dto.Store;

namespace Shao.ApiTemp.FunctionalTests;

[TestClass]
public class StoreScenarios : ScenariosBase
{
    [TestMethod]
    public async Task Main_flow_and_response_ok()
    {
        var queryReq = new QueryStoreReq()
        {
            Page = 1,
            PageSize = 10
        };
        var saveReq = new SaveStoreReq()
        {
            StoreId = default,
            StoreName = "FakeStoreName",
            AuditQuota = Random.Shared.Next(100),
        };

        using var server = CreateServer();
        using var client = server.CreateClient();

        var queryR = await PostR<IEnumerable<QueryStoreDto>>(client, Url.Query, queryReq);
        Assert.IsTrue(queryR.IsSucc);
        if (queryR.Data.Count() > 0)
        {
            saveReq.StoreId = queryR.Data.First().StoreId;
        }

        var saveR = await PostR(client, "Store/Save", saveReq);
        Assert.IsTrue(saveR.IsSucc);

        queryR = await PostR<IEnumerable<QueryStoreDto>>(client, Url.Query, queryReq);
        Assert.IsTrue(queryR.IsSucc);
        Assert.IsTrue(queryR.Data.Count() > 0);

        var idReq = new StoreIdReq(queryR.Data.First().StoreId);
        var idUrl = $"Store/Get?storeId={idReq.StoreId}";
        var getR = await GetR<StoreDto>(client, idUrl);
        Assert.IsTrue(queryR.IsSucc);

        if (getR.Data.StoreStatus == Domain.Store.StoreStatus.Off)
        {
            var openR = await PostR(client, "Store/Open", idReq);
            Assert.IsTrue(openR.IsSucc);

            var closeR = await PostR(client, "Store/Close", idReq);
            Assert.IsTrue(closeR.IsSucc);

            openR = await PostR(client, "Store/Open", idReq);
            Assert.IsTrue(openR.IsSucc);
        }
        else if (getR.Data.StoreStatus == Domain.Store.StoreStatus.On)
        {
            var closeR = await PostR(client, "Store/Close", idReq);
            Assert.IsTrue(closeR.IsSucc);

            var openR = await PostR(client, "Store/Open", idReq);
            Assert.IsTrue(openR.IsSucc);
        }

        var saveConfigReq = new SaveStoreConfigReq()
        {
            StoreId = idReq.StoreId,
            PromoteLimitCount = 1,
            PromoteLimitOfDay = 10,
        };
        var saveConfigR = await PostR(client, "Store/SaveConfig", saveConfigReq);
        Assert.IsTrue(saveConfigR.IsSucc);

        R<StoreConfigDto> getConfigR = await GetR<StoreConfigDto>(client, idUrl);
        Assert.IsTrue(getConfigR.IsSucc);
        Assert.IsNotNull(getConfigR.Data);
    }

    public static class Url
    {
        public const string Query = "Store/Query";
    }
}