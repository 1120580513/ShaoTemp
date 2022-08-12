using Dapper.Contrib.Extensions;
using System.Data;
using System.Data.SqlClient;

namespace Shao.ApiTemp.Repo.Base;

public partial class BaseRepo
{
    protected async Task<R> InsertOrUpdate<TPersisent>(TPersisent persistent, string errMsg)
        where TPersisent : class, IPersistant
    {
        bool isSucc;
        if (persistent.IsInsert())
        {
            isSucc = await Template(async conn =>
             {
                 return (await conn.InsertAsync(persistent, commandTimeout: DefaultTimout)) > 0;
             }, nameof(InsertOrUpdate), errMsg, persistent);
        }
        else
        {
            isSucc = await Template(async conn =>
            {
                return await conn.UpdateAsync(persistent, commandTimeout: DefaultTimout);
            }, nameof(InsertOrUpdate), errMsg, persistent);
        }
        return R.Cond(isSucc, errMsg);
    }

    protected async Task TranInsertOrUpdateOrDelete<TPersisent>(
        bool isDelete, TPersisent persistent, IDbConnection conn, IDbTransaction tran)
        where TPersisent : class, IPersistant
    {
        if (isDelete)
        {
            var deleted = await conn.DeleteAsync(persistent, tran);
            AreEnsure(deleted, nameof(TranInsertOrUpdateOrDelete), persistent);
        }
        else
        {
            await TranInsertOrUpdate(persistent, conn, tran);
        }
    }
    protected async Task TranInsertOrUpdate<TPersisent>(
        TPersisent persistent, IDbConnection conn, IDbTransaction tran)
        where TPersisent : class, IPersistant
    {
        bool isSucc;
        if (persistent.IsInsert())
        {
            isSucc = (await conn.InsertAsync(persistent, tran, commandTimeout: DefaultTimout)) > 0;
        }
        else
        {
            isSucc = await conn.UpdateAsync(persistent, tran, commandTimeout: DefaultTimout);
        }
        AreEnsure(isSucc, nameof(TranInsertOrUpdate), persistent);
    }

    protected async Task<bool> Delete<TPersisent>(TPersisent persistent, string errMsg)
        where TPersisent : class, IPersistant
    {
        return await Template(
            async conn => await conn.DeleteAsync(persistent, commandTimeout: DefaultTimout),
            nameof(Delete), errMsg, persistent);
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