#nullable disable

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Shao.ApiTemp.Common.Mq.RabbitMq;

public class RabbitMqClient : IMqClient, IEnsure
{
    private IRabbitMqClientConfig _config;
    public void SetConfig(IMqClientConfig config)
    {
        _config = (IRabbitMqClientConfig)config;
    }

    public void Init()
    {
        var connFactory = new ConnectionFactory()
        {
            Uri = new Uri(_config.Uri)
        };
        Conn = connFactory.CreateConnection();

        DeclareOfInit();
    }

    private void DeclareOfInit()
    {
        var options = _config.GetDeclareOptions();
        if (!options.EnableDeclare) return;

        var channel = Channel;

        foreach (var exchange in options.Exchanges)
        {
            channel.ExchangeDeclare(
                exchange.Name, exchange.Type.ToString(), exchange.Durable, exchange.AutoDelete, exchange.Args);
        }
        foreach (var queue in options.Queues)
        {
            channel.QueueDeclare(queue.Name, queue.Durable, queue.Exclusive, queue.AutoDelete, queue.Args);
        }
        foreach (var binding in options.Bindings)
        {
            channel.QueueBind(binding.Queue.Name, binding.Exchange.Name, binding.RouteingKey, binding.Args);
        }
    }

    public void Publish(MqReq req)
    {
        var options = _config.GetPublishOptions(req);
        Channel.BasicPublish(
            options.Exchange.Name, options.RoutingKey, options.Mandatory, options.BasicProperties, options.Body);
    }

    public void RegistoryReceived(IMqReceived received)
    {
        var options = _config.GetReceiveOptions(received);

        var channel = Channel;
        var consumer = new EventingBasicConsumer(channel);
        options.SetConsumer(consumer);
        channel.BasicConsume(options.Queue.Name, options.AutoAck, options.ConsumerTag, options.NoLocal,
            options.Exclusive, options.Args, consumer);
    }

    public void UnLoad()
    {
        Conn.Close();
        Conn.Dispose();
    }

    private IModel _channel;

    private IModel Channel
    {
        get
        {
            if (_channel is null)
            {
                _channel = Conn.CreateModel();
            }
            AreEnsure(_channel.IsOpen, "MQ会话已关闭");
            return _channel;
        }
    }

    private IConnection _conn;

    private IConnection Conn
    {
        get
        {
            AreEnsure(_conn is not null, "MQ连接未初始化");
            AreEnsure(_conn.IsOpen, "MQ连接已关闭 ");
            return _conn;
        }
        set { _conn = value; }
    }

    /// <summary>
    /// 确保参数或业务的正常运行，否则会抛出异常
    /// </summary>
    /// <param name="condition">条件，当为 false 时抛出异常</param>
    /// <param name="message"></param>
    /// <exception cref="DomainException"/>
    private void AreEnsure(bool condition, string message, params object[] args)
    {
        if (!condition)
        {
            Debug.Assert(true);
            throw new CustomException(message, args);
        }
    }
}