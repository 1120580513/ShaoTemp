CREATE DATABASE [Shao.ApiTemp_FunctionalTest]
GO
USE [Shao.ApiTemp_FunctionalTest]
GO
CREATE TABLE Store(
	StoreId BIGINT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	StoreName NVARCHAR(64) NOT NULL,
	StoreStatus INT NOT NULL,
	AuditQuota DECIMAL(18,2) NOT NULL,
	CreateOn DATETIME NOT NULL,
	ModifyOn DATETIME NOT NULL,
)
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'店铺' , 
	@level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Store'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'店铺名称' , @level0type=N'SCHEMA',@level0name=N'dbo', 
	@level1type=N'TABLE',@level1name=N'Store', 
	@level2type=N'COLUMN',@level2name=N'StoreName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'审核限额' , @level0type=N'SCHEMA',@level0name=N'dbo', 
	@level1type=N'TABLE',@level1name=N'Store', 
	@level2type=N'COLUMN',@level2name=N'AuditQuota'
GO
CREATE TABLE [dbo].[StoreConfig](
	[StoreConfigId] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[StoreId] [bigint] NOT NULL,
	[PromoteLimitOfDay] [int] NOT NULL,
	[PromoteLimitCount] [int] NOT NULL
)
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'购买限制天数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StoreConfig', @level2type=N'COLUMN',@level2name=N'PromoteLimitOfDay'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'购买限制次数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StoreConfig', @level2type=N'COLUMN',@level2name=N'PromoteLimitCount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'店铺配置' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StoreConfig'
GO
CREATE TABLE GiveGoods(
	GiveGoodsId	BIGINT IDENTITY(1,1) PRIMARY KEY,
	GiveGoodsName NVARCHAR(128) NOT NULL,
	GiveGoodsCode VARCHAR(32) NOT NULL,
	GiveGoodsNum INT NOT NULL,
	GiveGoodsStatus INT NOT NULL,
	CreateOn DATETIME NOT NULL
)
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'赠品' , 
	@level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GiveGoods'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'赠品名称' , @level0type=N'SCHEMA',@level0name=N'dbo', 
	@level1type=N'TABLE',@level1name=N'GiveGoods', 
	@level2type=N'COLUMN',@level2name=N'GiveGoodsName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'赠品编码' , @level0type=N'SCHEMA',@level0name=N'dbo', 
	@level1type=N'TABLE',@level1name=N'GiveGoods', 
	@level2type=N'COLUMN',@level2name=N'GiveGoodsCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'赠送数量' , @level0type=N'SCHEMA',@level0name=N'dbo', 
	@level1type=N'TABLE',@level1name=N'GiveGoods', 
	@level2type=N'COLUMN',@level2name=N'GiveGoodsNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'赠品状态' , @level0type=N'SCHEMA',@level0name=N'dbo', 
	@level1type=N'TABLE',@level1name=N'GiveGoods', 
	@level2type=N'COLUMN',@level2name=N'GiveGoodsStatus'
GO
CREATE TABLE PromoteTask(
	PromoteTaskId BIGINT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	StoreId BIGINT NOT NULL,
	StoreName NVARCHAR(64) NOT NULL,
	PromoteTaskName NVARCHAR(64) NOT NULL,
	PromoteTaskStatus INT NOT NULL,
	StartTime DATETIME NOT NULL,
	EndTime DATETIME NOT NULL,
	CreateOn DATETIME NOT NULL,
	ModifyOn DATETIME NOT NULL,
)
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'推广任务' , 
	@level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PromoteTask'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'推广任务名称' , @level0type=N'SCHEMA',@level0name=N'dbo', 
	@level1type=N'TABLE',@level1name=N'PromoteTask', 
	@level2type=N'COLUMN',@level2name=N'PromoteTaskName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'推广任务状态 0 待发布 1 已发布 2 已停止 4 已作废' , @level0type=N'SCHEMA',@level0name=N'dbo', 
	@level1type=N'TABLE',@level1name=N'PromoteTask', 
	@level2type=N'COLUMN',@level2name=N'PromoteTaskStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'推广任务开始时间' , @level0type=N'SCHEMA',@level0name=N'dbo', 
	@level1type=N'TABLE',@level1name=N'PromoteTask', 
	@level2type=N'COLUMN',@level2name=N'StartTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'推广任务结束时间' , @level0type=N'SCHEMA',@level0name=N'dbo', 
	@level1type=N'TABLE',@level1name=N'PromoteTask', 
	@level2type=N'COLUMN',@level2name=N'EndTime'
GO
CREATE TABLE PromoteTaskSpec(
	PromoteTaskSpecId BIGINT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	PromoteTaskId BIGINT NOT NULL,
	SpecNum INT NOT NULL,
	GiveGoodsId BIGINT NOT NULL,
	GiveGoodsName NVARCHAR(128) NOT NULL,
	GiveGoodsCode VARCHAR(32) NOT NULL,
	GiveGoodsNum INT NOT NULL
)
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'推广任务规格' , 
	@level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PromoteTaskSpec'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'推广任务规格数（订单商品数量）' , @level0type=N'SCHEMA',@level0name=N'dbo', 
	@level1type=N'TABLE',@level1name=N'PromoteTaskSpec', 
	@level2type=N'COLUMN',@level2name=N'SpecNum'
GO
CREATE TABLE UserTask(
	UserTaskId BIGINT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	PromoteTaskId BIGINT NOT NULL,
	PromoteTaskName NVARCHAR(64) NOT NULL,
	StoreId BIGINT NOT NULL,
	StoreName NVARCHAR(64) NOT NULL,
	UserTaskStatus INT NOT NULL,
	Mobile VARCHAR(11) NOT NULL,
	OrderNo VARCHAR(64) NULL,
	MatchOn DATETIME NULL,
	CreateOn DATETIME NOT NULL,
	ModifyOn DATETIME NOT NULL,
)
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户任务' , 
	@level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserTask'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户任务状态 0 已领取 1 待审核 2 待匹配 4 待退款 8 退款失败 16 已完成' , @level0type=N'SCHEMA',@level0name=N'dbo', 
	@level1type=N'TABLE',@level1name=N'UserTask', 
	@level2type=N'COLUMN',@level2name=N'UserTaskStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户手机号' , @level0type=N'SCHEMA',@level0name=N'dbo', 
	@level1type=N'TABLE',@level1name=N'UserTask', 
	@level2type=N'COLUMN',@level2name=N'Mobile'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单号' , @level0type=N'SCHEMA',@level0name=N'dbo', 
	@level1type=N'TABLE',@level1name=N'UserTask', 
	@level2type=N'COLUMN',@level2name=N'OrderNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'匹配时间' , @level0type=N'SCHEMA',@level0name=N'dbo', 
	@level1type=N'TABLE',@level1name=N'UserTask', 
	@level2type=N'COLUMN',@level2name=N'MatchOn'
