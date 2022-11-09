using RabbitMQ.Client.Events;

namespace Shao.ApiTemp.Common.Mq.RabbitMq;

public class ReceiveOptions
{
    public Queue Queue { get; set; }
    public bool AutoAck { get; set; } = false;
    public string ConsumerTag { get; set; }
    /// <summary>
    /// 如果为 true 则不能将同一个 conn 的消息发送给此 conn 的消费者
    /// </summary>
    public bool NoLocal { get; set; } = false;
    /// <summary>
    /// 是否排它
    /// </summary>
    public bool Exclusive { get; set; } = false;
    public IDictionary<string, object> Args { get; set; }
    public Action<EventingBasicConsumer> SetConsumer { get; set; }
}