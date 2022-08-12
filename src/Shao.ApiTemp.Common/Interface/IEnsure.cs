namespace Shao.ApiTemp.Common.Interface;

public interface IEnsure
{
    /// <summary>
    /// 确保参数或业务的正常运行，否则会抛出异常
    /// </summary>
    /// <param name="condition">条件，当为 false 时抛出异常</param>
    /// <param name="message"></param>
    void AreEnsure(bool condition, string message, params object[] args);
}
