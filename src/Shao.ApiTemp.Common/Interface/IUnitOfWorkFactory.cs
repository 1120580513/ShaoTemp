namespace Shao.ApiTemp.Common.Interface;

public interface IUnitOfWorkFactory 
{
    /// <summary>
    /// 创建一个有事务的工作单元，且可直接使用（连接已打开）
    /// </summary>
    /// <returns></returns>
    UnitOfWork CreateTranUnitOfWork();
}