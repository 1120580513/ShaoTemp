namespace Shao.ApiTemp.Common.Mq.RabbitMq;

public interface IRabbitMqClientConfig : IMqClientConfig
{
    /// <summary>
    /// 
    /// </summary>
    ///<remarks>amqp://user:pass@hostName:port/vhost</remarks>
    string Uri { get; }

    DeclareOptions GetDeclareOptions();
    PublishOptions GetPublishOptions(MqReq req);
    ReceiveOptions GetReceiveOptions(IMqReceived received);
}
