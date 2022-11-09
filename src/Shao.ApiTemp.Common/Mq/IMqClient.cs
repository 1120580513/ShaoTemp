namespace Shao.ApiTemp.Common.Mq;

public interface IMqClient
{
    void SetConfig(IMqClientConfig config);

    void Init();

    void UnLoad();

    void Publish(MqReq req);

    void RegistoryReceived(IMqReceived received);
}