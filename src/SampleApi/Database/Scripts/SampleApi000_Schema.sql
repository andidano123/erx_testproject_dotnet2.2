/*
Navicat SQL Server Data Transfer

Source Server         : localhost,1433
Source Server Version : 110000
Source Host           : localhost,1433:1433
Source Database       : SampleApi
Source Schema         : dbo

Target Server Type    : SQL Server
Target Server Version : 110000
File Encoding         : 65001

Date: 2021-09-01 01:01:13
*/


-- ----------------------------
-- Table structure for AccountInfo
-- ----------------------------
DROP TABLE [dbo].[AccountInfo]
GO
CREATE TABLE [dbo].[AccountInfo] (
[UserID] int NOT NULL IDENTITY(1,1) ,
[UserEmail] nvarchar(255) NULL DEFAULT '' ,
[AnswerStatus] int NOT NULL DEFAULT ((0)) ,
[CreatedAt] datetime2(7) NOT NULL DEFAULT (getdate()) ,
[FinishAnswerAt] datetime2(7) NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[AccountInfo]', RESEED, 35)
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'AccountInfo', 
'COLUMN', N'AnswerStatus')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'1: Finished 0: Not Finished'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'AccountInfo'
, @level2type = 'COLUMN', @level2name = N'AnswerStatus'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'1: Finished 0: Not Finished'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'AccountInfo'
, @level2type = 'COLUMN', @level2name = N'AnswerStatus'
GO

-- ----------------------------
-- Table structure for AnswerInfo
-- ----------------------------
DROP TABLE [dbo].[AnswerInfo]
GO
CREATE TABLE [dbo].[AnswerInfo] (
[ID] int NOT NULL IDENTITY(1,1) ,
[UserID] int NOT NULL DEFAULT ((0)) ,
[QuestionID] int NOT NULL DEFAULT ((0)) ,
[QuestionSequence] int NOT NULL DEFAULT ((0)) ,
[QuestionType] int NOT NULL DEFAULT ((0)) ,
[Answer] nvarchar(255) NOT NULL DEFAULT '' ,
[CreatedAt] datetime NOT NULL DEFAULT (getdate()) 
)


GO
DBCC CHECKIDENT(N'[dbo].[AnswerInfo]', RESEED, 149)
GO

-- ----------------------------
-- Table structure for QuestionCategoryInfo
-- ----------------------------
DROP TABLE [dbo].[QuestionCategoryInfo]
GO
CREATE TABLE [dbo].[QuestionCategoryInfo] (
[ID] int NOT NULL IDENTITY(1,1) ,
[Title] varchar(255) NOT NULL DEFAULT '' ,
[Sequence] int NOT NULL DEFAULT ((0)) 
)


GO
DBCC CHECKIDENT(N'[dbo].[QuestionCategoryInfo]', RESEED, 4)
GO

-- ----------------------------
-- Table structure for QuestionInfo
-- ----------------------------
DROP TABLE [dbo].[QuestionInfo]
GO
CREATE TABLE [dbo].[QuestionInfo] (
[ID] int NOT NULL IDENTITY(1,1) ,
[Title] nvarchar(255) NULL DEFAULT '' ,
[CategoryID] int NOT NULL ,
[NotAllowed] nvarchar(255) NOT NULL ,
[CreatedAt] datetime NOT NULL DEFAULT (getdate()) ,
[Sequence] int NOT NULL DEFAULT ((0)) ,
[TypeID] int NOT NULL DEFAULT ((0)) 
)


GO
DBCC CHECKIDENT(N'[dbo].[QuestionInfo]', RESEED, 11)
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'QuestionInfo', 
'COLUMN', N'NotAllowed')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'Not Allowed Value Array'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'QuestionInfo'
, @level2type = 'COLUMN', @level2name = N'NotAllowed'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Not Allowed Value Array'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'QuestionInfo'
, @level2type = 'COLUMN', @level2name = N'NotAllowed'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'QuestionInfo', 
'COLUMN', N'Sequence')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'Must bigger than 0'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'QuestionInfo'
, @level2type = 'COLUMN', @level2name = N'Sequence'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Must bigger than 0'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'QuestionInfo'
, @level2type = 'COLUMN', @level2name = N'Sequence'
GO

-- ----------------------------
-- Table structure for QuestionTypeInfo
-- ----------------------------
DROP TABLE [dbo].[QuestionTypeInfo]
GO
CREATE TABLE [dbo].[QuestionTypeInfo] (
[ID] int NOT NULL IDENTITY(1,1) ,
[Title] nvarchar(200) NOT NULL ,
[Type] int NOT NULL DEFAULT ((1)) ,
[Content] text NOT NULL DEFAULT '' 
)


GO
DBCC CHECKIDENT(N'[dbo].[QuestionTypeInfo]', RESEED, 5)
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'QuestionTypeInfo', 
'COLUMN', N'Type')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'1：text 2：single select  3:multi select'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'QuestionTypeInfo'
, @level2type = 'COLUMN', @level2name = N'Type'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'1：text 2：single select  3:multi select'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'QuestionTypeInfo'
, @level2type = 'COLUMN', @level2name = N'Type'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'QuestionTypeInfo', 
'COLUMN', N'Content')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'if Type= 2, Content contains the all select values.  Items are sperated by comma(,)'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'QuestionTypeInfo'
, @level2type = 'COLUMN', @level2name = N'Content'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'if Type= 2, Content contains the all select values.  Items are sperated by comma(,)'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'QuestionTypeInfo'
, @level2type = 'COLUMN', @level2name = N'Content'
GO

-- ----------------------------
-- Indexes structure for table AccountInfo
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table AccountInfo
-- ----------------------------
ALTER TABLE [dbo].[AccountInfo] ADD PRIMARY KEY ([UserID])
GO

-- ----------------------------
-- Indexes structure for table AnswerInfo
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table AnswerInfo
-- ----------------------------
ALTER TABLE [dbo].[AnswerInfo] ADD PRIMARY KEY ([ID])
GO

-- ----------------------------
-- Indexes structure for table QuestionCategoryInfo
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table QuestionCategoryInfo
-- ----------------------------
ALTER TABLE [dbo].[QuestionCategoryInfo] ADD PRIMARY KEY ([ID])
GO

-- ----------------------------
-- Indexes structure for table QuestionInfo
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table QuestionInfo
-- ----------------------------
ALTER TABLE [dbo].[QuestionInfo] ADD PRIMARY KEY ([ID])
GO

-- ----------------------------
-- Indexes structure for table QuestionTypeInfo
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table QuestionTypeInfo
-- ----------------------------
ALTER TABLE [dbo].[QuestionTypeInfo] ADD PRIMARY KEY ([ID])
GO
