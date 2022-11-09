using RabbitMQ.Client;

namespace Shao.ApiTemp.Common.Mq.RabbitMq;

public class PublishOptions
{
    public Exchange Exchange { get; set; }
    public string RoutingKey { get; set; }
    /// <summary>
    /// 当为 true 时如果找不到对应的 Queue 则返还给生产者，为 false 时直接丢弃该消息
    /// </summary>
    public bool Mandatory { get; set; } = false;
    public IBasicProperties BasicProperties { get; set; }
    public ReadOnlyMemory<byte> Body { get; set; }
}