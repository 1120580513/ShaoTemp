namespace Shao.ApiTemp.Common.Mq.RabbitMq;

public class DeclareOptions
{
    public bool EnableDeclare { get; set; }
    public IEnumerable<Exchange> Exchanges { get; set; }
    public IEnumerable<Queue> Queues { get; set; }
    public IEnumerable<Binding> Bindings { get; set; }
}
public class Exchange
{
    public string Name { get; set; }
    public ExchangeType Type { get; set; } = ExchangeType.direct;
    /// <summary>
    /// 是否持久化
    /// </summary>
    ///<remarks>broker 重启时不持久化的将被删除 </remarks>
    public bool Durable { get; set; } = true;
    /// <summary>
    /// 是否自动删除
    /// </summary>
    ///<remarks>如果没有绑定队列则直接删除</remarks>
    public bool AutoDelete { get; set; } = false;
    public IDictionary<string, object> Args { get; set; }
}
public class Queue
{
    public string Name { get; set; }
    /// <summary>
    /// 是否持久化
    /// </summary>
    ///<remarks>broker 重启时不持久化的将被删除 </remarks>
    public bool Durable { get; set; } = true;
    /// <summary>
    /// 是否排它
    /// </summary>
    ///<remarks>为 true 时仅在创建的 conn 中有效且 conn 关闭后会自动删除</remarks>
    public bool Exclusive { get; set; } = false;
    /// <summary>
    /// 是否自动删除
    /// </summary>
    ///<remarks>如果没有绑定队列则直接删除</remarks>
    public bool AutoDelete { get; set; } = false;
    public IDictionary<string, object> Args { get; set; }
}
public class Binding
{
    public Queue Queue { get; set; }
    public Exchange Exchange { get; set; }
    public string RouteingKey { get; set; }
    public IDictionary<string, object> Args { get; set; }
}