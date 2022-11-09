using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shao.ApiTemp.Common.Mq;
using Shao.ApiTemp.Common.Mq.RabbitMq;
using System.Diagnostics;
using System.Text;

namespace Shao.ApiTemp.Domain.Mq.Config;

public class B2cMqClientConfig : IRabbitMqClientConfig, IEnsure
{
    private Encoding _encoding;
    private Exchange _defExchange;
    private List<Type> _mqReqs;
    private List<Queue> _queues;
    private List<IMqReceived> _mqReceived;

    private Dictionary<Type, Queue> _mqReqAndQueueDic;

    public B2cMqClientConfig(List<Type> mqReqs)
    {
        _mqReqs = mqReqs;
        _encoding = Encoding.UTF8;
        _defExchange = new Exchange()
        {
            Name = "B2C_EXCHANGE",
            Type = Common.Mq.RabbitMq.ExchangeType.direct,
        };

        _queues = new List<Queue>(_mqReqs.Count);
        _mqReqAndQueueDic = new Dictionary<Type, Queue>(_mqReqs.Count);
        _mqReqs.ForEach(reqType =>
        {
            var name = GetMqName(reqType);
            var queue = new Queue() { Name = name };
            _queues.Add(queue);
            _mqReqAndQueueDic.Add(reqType, queue);
        });
    }

    public string Uri => App.Config.Mq.Uri;
    public DeclareOptions GetDeclareOptions()
    {
        var exchanges = new List<Exchange>() { _defExchange };
        var bindings = _queues.Select(q => new Binding()
        {
            Queue = q,
            Exchange = _defExchange,
            RouteingKey = q.Name
        });
        return new DeclareOptions()
        {
            EnableDeclare = true,
            Exchanges = exchanges,
            Queues = _queues,
            Bindings = bindings,
        };
    }
    public PublishOptions GetPublishOptions(MqReq req)
    {
        return new PublishOptions()
        {
            RoutingKey = GetMqName(req),
            Body = _encoding.GetBytes(req.ToJson()).ToArray(),
            Exchange = _defExchange,
        };
    }
    public ReceiveOptions GetReceiveOptions(IMqReceived received)
    {
        return new ReceiveOptions()
        {
            Queue = GetQueue(received),
            SetConsumer = (consumer) => SetConsumer(consumer, received)
        };
    }

    private void SetConsumer(EventingBasicConsumer consumer, IMqReceived received)
    {
        consumer.Received += (sender, e) => Consumer_Received(sender, e, received);
    }

    private void Consumer_Received(object? sender, BasicDeliverEventArgs e, IMqReceived received)
    {
        var channel = (IModel)sender!;
        var msg = _encoding.GetString(e.Body.ToArray());
        var isRepublish = received.Received(msg).GetAwaiter().GetResult();
        channel.BasicAck(e.DeliveryTag, false);
    }

    private Queue GetQueue(IMqReceived received)
    {
        const string DefineErrorMsg = "Mq消费者定义错误";
        var genericTypes = received.GetType().GenericTypeArguments;
        AreEnsure(genericTypes.Length > 0, DefineErrorMsg);

        var mqReqType = genericTypes[0];
        AreEnsure(mqReqType.IsAssignableFrom(typeof(IMqReq)), DefineErrorMsg);
        AreEnsure(_mqReqAndQueueDic.ContainsKey(mqReqType), DefineErrorMsg);

        return _mqReqAndQueueDic[mqReqType];
    }
    private string GetMqName(MqReq req)
    {
        return GetMqName(req.GetType());
    }
    private string GetMqName(Type type)
    {
        return type.Name;
    }

    /// <inheritdoc />
    void AreEnsure(bool condition, string message, params object[] args)
    {
        if (!condition)
        {
            Debug.Assert(true);
            throw new CustomException(message, args);
        }
    }
}