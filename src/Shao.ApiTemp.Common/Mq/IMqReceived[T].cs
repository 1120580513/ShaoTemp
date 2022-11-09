namespace Shao.ApiTemp.Common.Mq;

public interface IMqReceived<T>
    where T : MqReq
{
    /// <summary>
    /// 消费消息
    /// </summary>
    /// <param name="req"></param>
    /// <returns>是否重新入队</returns>
    Task<bool> Received(T req);
}