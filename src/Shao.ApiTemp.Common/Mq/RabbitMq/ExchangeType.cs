namespace Shao.ApiTemp.Common.Mq.RabbitMq;

public enum ExchangeType
{
    /// <summary>
    /// 路由键完全匹配
    /// </summary>
    direct,
    /// <summary>
    /// 路由键模式匹配
    /// </summary>
    topic,
    /// <summary>
    /// 广播
    /// </summary>
    fanout,
    /// <summary>
    /// 不处理路由键，仅根据消息的 headers 匹配
    /// </summary>
    headers
}