using System.Data;
using System.Data.SqlClient;

namespace Shao.ApiTemp.Repo.Base;

public partial class BaseRepo
{
    protected async Task InsertOrUpdateOrDelete<TPersisent>(
        bool isDelete, TPersisent persistent, UnitOfWork unitOfWork, string errMsg)
        where TPersisent : class, IPersistant
    {
        await Template(async () =>
        {
            unitOfWork = EnsureUnitOfWork(unitOfWork);
            var r = await unitOfWork.InsertOrUpdateOrDelete(persistent.IsInsert(), isDelete, persistent, errMsg);
            AreEnsure(r.IsSucc, errMsg, persistent);
        }, nameof(InsertOrUpdateOrDelete), errMsg, persistent);
    }

    protected async Task InsertOrUpdate<TPersisent>(TPersisent persistent, UnitOfWork unitOfWork, string errMsg)
        where TPersisent : class, IPersistant
    {
        await Template(async () =>
        {
            unitOfWork = EnsureUnitOfWork(unitOfWork);
            var r = await unitOfWork.InsertOrUpdateOrDelete(persistent.IsInsert(), false, persistent, errMsg);
            AreEnsure(r.IsSucc, errMsg, persistent);
        }, nameof(InsertOrUpdate), errMsg, persistent);
    }

    protected async Task Delete<TPersisent>(TPersisent persistent, UnitOfWork unitOfWork, string errMsg)
        where TPersisent : class, IPersistant
    {
        await Template(async () =>
        {
            unitOfWork = EnsureUnitOfWork(unitOfWork);
            var r = await unitOfWork.InsertOrUpdateOrDelete(false, true, persistent, errMsg);
            AreEnsure(r.IsSucc, errMsg, persistent);
        }, nameof(Delete), errMsg, persistent);
    }

    protected async Task<R> BulkInsert(DataTable dataTable)
    {
        var batchSize = dataTable.Rows.Count;
        var tableName = dataTable.TableName;
        if (string.IsNullOrWhiteSpace(tableName))
        {
            return R.Fail($"{nameof(dataTable.TableName)} 不能为空");
        }
        try
        {
            using var conn = (SqlConnection)CreateDbConnection();
            conn.Open();
            using var sqlBlukCopy = new SqlBulkCopy(conn);
            sqlBlukCopy.DestinationTableName = tableName;
            sqlBlukCopy.BatchSize = batchSize;
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                var column = dataTable.Columns[i];
                sqlBlukCopy.ColumnMappings.Add(column.ColumnName, column.ColumnName);
            }
            await sqlBlukCopy.WriteToServerAsync(dataTable);
            return R.Succ();
        }
        catch (Exception ex)
        {
            Debug.Assert(false);
            Log.Error(nameof(BulkInsert), ex, tableName);
            return R.Fail($"{nameof(BulkInsert)}失败：{ex.Message}");
        }
    }
}