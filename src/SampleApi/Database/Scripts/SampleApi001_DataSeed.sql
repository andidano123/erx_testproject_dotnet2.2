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

Date: 2021-09-01 01:02:12
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
-- Records of AccountInfo
-- ----------------------------
SET IDENTITY_INSERT [dbo].[AccountInfo] ON
GO
INSERT INTO [dbo].[AccountInfo] ([UserID], [UserEmail], [AnswerStatus], [CreatedAt], [FinishAnswerAt]) VALUES (N'34', N'aba', N'1', N'2021-09-01 00:42:03.1064438', N'2021-09-01 00:42:24.7382231')
GO
GO
INSERT INTO [dbo].[AccountInfo] ([UserID], [UserEmail], [AnswerStatus], [CreatedAt], [FinishAnswerAt]) VALUES (N'35', N'ddd', N'1', N'2021-09-01 00:42:57.9041794', N'2021-09-01 00:43:44.1931051')
GO
GO
SET IDENTITY_INSERT [dbo].[AccountInfo] OFF
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
-- Records of AnswerInfo
-- ----------------------------
SET IDENTITY_INSERT [dbo].[AnswerInfo] ON
GO
SET IDENTITY_INSERT [dbo].[AnswerInfo] OFF
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
-- Records of QuestionCategoryInfo
-- ----------------------------
SET IDENTITY_INSERT [dbo].[QuestionCategoryInfo] ON
GO
INSERT INTO [dbo].[QuestionCategoryInfo] ([ID], [Title], [Sequence]) VALUES (N'1', N'Personal Information', N'1')
GO
GO
INSERT INTO [dbo].[QuestionCategoryInfo] ([ID], [Title], [Sequence]) VALUES (N'2', N'Address', N'2')
GO
GO
INSERT INTO [dbo].[QuestionCategoryInfo] ([ID], [Title], [Sequence]) VALUES (N'3', N'Occupation', N'3')
GO
GO
INSERT INTO [dbo].[QuestionCategoryInfo] ([ID], [Title], [Sequence]) VALUES (N'4', N'Bonos Category', N'4')
GO
GO
SET IDENTITY_INSERT [dbo].[QuestionCategoryInfo] OFF
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
-- Records of QuestionInfo
-- ----------------------------
SET IDENTITY_INSERT [dbo].[QuestionInfo] ON
GO
INSERT INTO [dbo].[QuestionInfo] ([ID], [Title], [CategoryID], [NotAllowed], [CreatedAt], [Sequence], [TypeID]) VALUES (N'1', N'Title', N'1', N'', N'2021-08-26 18:19:27.890', N'1', N'1')
GO
GO
INSERT INTO [dbo].[QuestionInfo] ([ID], [Title], [CategoryID], [NotAllowed], [CreatedAt], [Sequence], [TypeID]) VALUES (N'2', N'First name', N'1', N'', N'2021-08-26 18:19:58.647', N'2', N'2')
GO
GO
INSERT INTO [dbo].[QuestionInfo] ([ID], [Title], [CategoryID], [NotAllowed], [CreatedAt], [Sequence], [TypeID]) VALUES (N'3', N'Last name', N'1', N'', N'2021-08-26 18:20:16.580', N'3', N'2')
GO
GO
INSERT INTO [dbo].[QuestionInfo] ([ID], [Title], [CategoryID], [NotAllowed], [CreatedAt], [Sequence], [TypeID]) VALUES (N'4', N'Date of birth', N'1', N'', N'2021-08-26 18:23:21.993', N'4', N'2')
GO
GO
INSERT INTO [dbo].[QuestionInfo] ([ID], [Title], [CategoryID], [NotAllowed], [CreatedAt], [Sequence], [TypeID]) VALUES (N'5', N'Country of residence', N'1', N'["Cambodia", "Myanmar", "Pakistan"]', N'2021-08-26 18:24:10.300', N'5', N'3')
GO
GO
INSERT INTO [dbo].[QuestionInfo] ([ID], [Title], [CategoryID], [NotAllowed], [CreatedAt], [Sequence], [TypeID]) VALUES (N'6', N'House', N'2', N'', N'2021-08-26 18:24:36.940', N'6', N'2')
GO
GO
INSERT INTO [dbo].[QuestionInfo] ([ID], [Title], [CategoryID], [NotAllowed], [CreatedAt], [Sequence], [TypeID]) VALUES (N'7', N'Work', N'2', N'', N'2021-08-26 18:24:45.753', N'7', N'2')
GO
GO
INSERT INTO [dbo].[QuestionInfo] ([ID], [Title], [CategoryID], [NotAllowed], [CreatedAt], [Sequence], [TypeID]) VALUES (N'8', N'Occupation', N'3', N'', N'2021-08-26 18:28:20.340', N'8', N'5')
GO
GO
INSERT INTO [dbo].[QuestionInfo] ([ID], [Title], [CategoryID], [NotAllowed], [CreatedAt], [Sequence], [TypeID]) VALUES (N'9', N'Job Title', N'3', N'', N'2021-08-26 18:40:46.173', N'9', N'2')
GO
GO
INSERT INTO [dbo].[QuestionInfo] ([ID], [Title], [CategoryID], [NotAllowed], [CreatedAt], [Sequence], [TypeID]) VALUES (N'10', N'Business Type', N'3', N'', N'2021-08-26 18:41:05.240', N'10', N'2')
GO
GO
INSERT INTO [dbo].[QuestionInfo] ([ID], [Title], [CategoryID], [NotAllowed], [CreatedAt], [Sequence], [TypeID]) VALUES (N'11', N'Hobby', N'4', N'', N'2021-08-26 18:41:35.520', N'11', N'4')
GO
GO
SET IDENTITY_INSERT [dbo].[QuestionInfo] OFF
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
-- Records of QuestionTypeInfo
-- ----------------------------
SET IDENTITY_INSERT [dbo].[QuestionTypeInfo] ON
GO
INSERT INTO [dbo].[QuestionTypeInfo] ([ID], [Title], [Type], [Content]) VALUES (N'1', N'SelectQuestion1', N'2', N'["Mr","Ms","Mrs"]')
GO
GO
INSERT INTO [dbo].[QuestionTypeInfo] ([ID], [Title], [Type], [Content]) VALUES (N'2', N'NormalText', N'1', N'')
GO
GO
INSERT INTO [dbo].[QuestionTypeInfo] ([ID], [Title], [Type], [Content]) VALUES (N'3', N'Country', N'2', N'["Afghanistan","Aland Islands","Albania","Algeria","American Samoa","Andorra","Angola","Anguilla","Antarctica","Antigua And Barbuda","Argentina","Armenia","Aruba","Australia","Austria","Azerbaijan","Bahamas","Bahrain","Bangladesh","Barbados","Belarus","Belgium","Belize","Benin","Bermuda","Bhutan","Bolivia","Bosnia And Herzegovina","Botswana","Bouvet Island","Brazil","British Indian Ocean Territory","Brunei Darussalam","Bulgaria","Burkina Faso","Burundi","Cambodia","Cameroon","Canada","Cape Verde","Cayman Islands","Central African Republic","Chad","Chile","China","Christmas Island","Cocos (Keeling) Islands","Colombia","Comoros","Congo","Congo, The Democratic Republic Of The","Cook Islands","Costa Rica","Cote D''ivoire","Croatia","Cuba","Cyprus","Czechia","Denmark","Djibouti","Dominica","Dominican Republic","Ecuador","Egypt","El Salvador","Equatorial Guinea","Eritrea","Estonia","Ethiopia","Falkland Islands (Malvinas)","Faroe Islands","Fiji","Finland","France","French Guiana","French Polynesia","French Southern Territories","Gabon","Gambia","Georgia","Germany","Ghana","Gibraltar","Greece","Greenland","Grenada","Guadeloupe","Guam","Guatemala","Guernsey","Guinea","Guinea-bissau","Guyana","Haiti","Heard Island And Mcdonald Islands","Holy See (Vatican City State)","Honduras","Hong Kong","Hungary","Iceland","India","Indonesia","Iran, Islamic Republic Of","Iraq","Ireland","Isle Of Man","Israel","Italy","Jamaica","Japan","Jersey","Jordan","Kazakhstan","Kenya","Kiribati","Korea, Democratic People''s Republic Of","Korea, Republic Of","Kuwait","Kyrgyzstan","Lao People''s Democratic Republic","Latvia","Lebanon","Lesotho","Liberia","Libyan Arab Jamahiriya","Liechtenstein","Lithuania","Luxembourg","Macao","Macedonia, The Former Yugoslav Republic Of","Madagascar","Malawi","Malaysia","Maldives","Mali","Malta","Marshall Islands","Martinique","Mauritania","Mauritius","Mayotte","Mexico","Micronesia, Federated States Of","Moldova, Republic Of","Monaco","Mongolia","Montenegro","Montserrat","Morocco","Mozambique","Myanmar","Namibia","Nauru","Nepal","Netherlands","Netherlands Antilles","New Caledonia","New Zealand","Nicaragua","Niger","Nigeria","Niue","Norfolk Island","Northern Mariana Islands","Norway","Oman","Pakistan","Palau","Palestinian Territory, Occupied","Panama","Papua New Guinea","Paraguay","Peru","Philippines","Pitcairn","Poland","Portugal","Puerto Rico","Qatar","Reunion","Romania","Russian Federation","Rwanda","Saint Helena","Saint Kitts And Nevis","Saint Lucia","Saint Pierre And Miquelon","Saint Vincent And The Grenadines","Samoa","San Marino","Sao Tome And Principe","Saudi Arabia","Senegal","Serbia","Seychelles","Sierra Leone","Singapore","Slovakia","Slovenia","Solomon Islands","Somalia","South Africa","South Georgia And The South Sandwich Islands","Spain","Sri Lanka","Sudan","Suriname","Svalbard And Jan Mayen","Swaziland","Sweden","Switzerland","Syrian Arab Republic","Taiwan, Province Of China","Tajikistan","Tanzania, United Republic Of","Thailand","Timor-leste","Togo","Tokelau","Tonga","Trinidad And Tobago","Tunisia","Turkey","Turkmenistan","Turks And Caicos Islands","Tuvalu","Uganda","Ukraine","United Arab Emirates","United Kingdom","United States","United States Minor Outlying Islands","Uruguay","Uzbekistan","Vanuatu","Venezuela","Viet Nam","Virgin Islands, British","Virgin Islands, U.S.","Wallis And Futuna","Western Sahara","Yemen","Zambia","Zimbabwe"]')
GO
GO
INSERT INTO [dbo].[QuestionTypeInfo] ([ID], [Title], [Type], [Content]) VALUES (N'4', N'Hobby', N'3', N'["Football","Piano","Basketball","Computer Game","Tennis"]')
GO
GO
INSERT INTO [dbo].[QuestionTypeInfo] ([ID], [Title], [Type], [Content]) VALUES (N'5', N'Oppupation', N'2', N'["Developer","Manager","CTO","CEO"]')
GO
GO
SET IDENTITY_INSERT [dbo].[QuestionTypeInfo] OFF
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
