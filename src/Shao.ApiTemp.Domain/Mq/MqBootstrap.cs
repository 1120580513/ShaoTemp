using Autofac;
using Shao.ApiTemp.Domain.Mq.Config;
using Shao.ApiTemp.Domain.Mq.Req.B2C;

namespace Shao.ApiTemp.Domain.Mq;

public static class MqBootstrap
{
    public static void Init()
    {
        App.Mq.Current.SetConfig(new B2cMqClientConfig(new List<Type>()
        {
            typeof(STextMqReq)
        }));
        //App.Mq.ERP.SetConfig<ErpMqClientConfig>();
    }
}