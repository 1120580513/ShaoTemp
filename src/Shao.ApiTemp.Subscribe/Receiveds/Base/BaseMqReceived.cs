using Shao.ApiTemp.Common.Exceptions;
using Shao.ApiTemp.Common.Extensions;
using Shao.ApiTemp.Common.Interface;
using Shao.ApiTemp.Common.Mq;
using System.Diagnostics;

namespace Shao.ApiTemp.Subscribe.Receiveds.Base;

public abstract class BaseMqReceived<TMqReq, TCurrent> : IMqReceived<TMqReq>
    where TMqReq : MqReq
{
    private readonly ICustomLog _log;

    protected BaseMqReceived()
    {
        _log = App.CreateLog<TCurrent>();
    }

    public async Task<bool> Received(string msg)
    {
        var req = msg.FromJson<TMqReq>();
        return await Received(req);
    }

    public async Task<bool> Received(TMqReq req)
    {
        try
        {
            await Handle(req);
            return false;
        }
        catch (CustomException customEx)
        {
            Debug.Assert(false);
            _log.Error(nameof(Received), customEx, customEx.Args);
        }
        catch (Exception ex)
        {
            Debug.Assert(false);
            _log.Error(nameof(Received), ex);
        }

        return true;
    }

    public abstract Task Handle(TMqReq req);
}