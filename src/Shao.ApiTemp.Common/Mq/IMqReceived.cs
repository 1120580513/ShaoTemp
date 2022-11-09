namespace Shao.ApiTemp.Common.Mq;

public interface IMqReceived
{
    /// <summary>
    /// 消费消息
    /// </summary>
    /// <param name="msg"></param>
    /// <returns>是否重新入队</returns>
    Task<bool> Received(string msg);
}