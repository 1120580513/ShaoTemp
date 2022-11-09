SELECT * FROM INFORMATION_SCHEMA.TABLES
SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = ''
-----------------------------------------
USE [Shao.ApiTemp]
GO
-----------------------------------------
SELECT TOP 100 * FROM dbo.Store WITH(NOLOCK)
SELECT TOP 100 * FROM dbo.StoreConfig WITH(NOLOCK)
SELECT TOP 100 * FROM dbo.GiveGoods WITH(NOLOCK)
SELECT TOP 100 * FROM dbo.PromoteTask WITH(NOLOCK)
SELECT TOP 100 * FROM dbo.PromoteTaskSpec WITH(NOLOCK)
SELECT TOP 100 * FROM dbo.UserTask WITH(NOLOCK)
SELECT TOP 100 * FROM dbo.UserTaskRecord WITH(NOLOCK)
-----------------------------------------
DECLARE @StoreId BIGINT = 1
SELECT TOP 100 * FROM dbo.Store WITH(NOLOCK) WHERE StoreId = @StoreId
SELECT TOP 100 * FROM dbo.StoreConfig WITH(NOLOCK) WHERE StoreId = @StoreId
-----------------------------------------
DECLARE @PromoteTaskId BIGINT = 1
SELECT TOP 100 * FROM dbo.PromoteTask WITH(NOLOCK) WHERE PromoteTaskId = @PromoteTaskId
SELECT TOP 100 * FROM dbo.PromoteTaskSpec WITH(NOLOCK) WHERE PromoteTaskId = @PromoteTaskId
SELECT TOP 100 * FROM dbo.GiveGoods WITH(NOLOCK) WHERE GiveGoodsId IN(
	SELECT GiveGoodsId FROM dbo.PromoteTaskSpec WITH(NOLOCK) WHERE PromoteTaskId = @PromoteTaskId
)
-----------------------------------------

SELECT TOP 100 * FROM dbo.UserTaskRecord WITH(NOLOCK)
delete from UserTaskRecord where usertaskrecordid <> 1