using Shao.ApiTemp.Domain.Mq.Req.B2C;

namespace Shao.ApiTemp.Subscribe.Receiveds;

public class SaveGiveGoodsReceived : BaseMqReceived<STextMqReq, SaveGiveGoodsReceived>
{
    public override Task Handle(STextMqReq req)
    {
        throw new NotImplementedException();
    }
}