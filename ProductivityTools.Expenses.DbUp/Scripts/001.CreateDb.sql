/****** Object:  Schema [ccw]    Script Date: 07.12.2023 19:44:19 ******/
CREATE SCHEMA [ccw]
GO
/****** Object:  Schema [gl]    Script Date: 07.12.2023 19:44:19 ******/
CREATE SCHEMA [gl]
GO
/****** Object:  Schema [kameralne7]    Script Date: 07.12.2023 19:44:19 ******/
CREATE SCHEMA [kameralne7]
GO
/****** Object:  Schema [me]    Script Date: 07.12.2023 19:44:19 ******/
CREATE SCHEMA [me]
GO


/****** Object:  Table [me].[Bag]    Script Date: 07.12.2023 19:44:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [me].[Bag](
	[BagID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](20) NULL,
	[Description] [varchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[BagID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [me].[Category]    Script Date: 07.12.2023 19:44:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [me].[Category](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [me].[Expense]    Script Date: 07.12.2023 19:44:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [me].[Expense](
	[ExpenseID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NULL,
	[CategoryID] [int] NULL,
	[BagID] [int] NULL,
	[Date] [datetime] NULL,
	[TimeStamp] [timestamp] NOT NULL,
	[CurrencyID] [int] NULL,
	[Value] [decimal](7, 2) NULL,
	[Free] [bit] NOT NULL,
	[Comment] [varchar](2000) NULL,
	[IncomeId] [int] NULL,
	[ExpectedValue] [decimal](7, 2) NULL,
	[Discount] [decimal](7, 2) NOT NULL,
	[ValueAfterDiscount]  AS ([Value]-[Discount]),
PRIMARY KEY CLUSTERED 
(
	[ExpenseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [me].[Raport]    Script Date: 07.12.2023 19:44:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
    CREATE VIEW  [me].[Raport] as
  SELECT B.BagID,B.[Name] as 'BagName',B.[Description],c.[Name] AS 'CategoryName',E.ExpenseID, E.[Name] as 'ExpenceName',E.[Date],E.ExpectedValue,E.[Value],E.Discount, E.ValueAfterDiscount,
  E.Free, E.Comment
  FROM [me].[Bag] b
  inner join me.Expense e ON b.BagID=e.BagID
  inner join me.Category c ON c.CategoryId = e.CategoryId
GO
/****** Object:  Table [me].[TagGroup]    Script Date: 07.12.2023 19:44:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [me].[TagGroup](
	[TagGroupID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[TagGroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [me].[Tag]    Script Date: 07.12.2023 19:44:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [me].[Tag](
	[TagID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](30) NULL,
	[ExpectedValue] [decimal](6, 2) NULL,
	[TagGroupID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[TagID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [me].[ExpenseTag]    Script Date: 07.12.2023 19:44:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [me].[ExpenseTag](
	[ExpenseTagID] [int] IDENTITY(1,1) NOT NULL,
	[ExpenseID] [int] NULL,
	[TagID] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  View [me].[TagExpense]    Script Date: 07.12.2023 19:44:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [me].[TagExpense] AS
  select r.BagName,r.CategoryName, r.ExpenseID,r.ExpenceName,tg.Name as 'TagGroup',t.Name as 'Tag',r.ExpectedValue, r.[Value]
  from me.Raport r
  left join me.ExpenseTag et ON r.ExpenseId=et.ExpenseId
  left join me.Tag t ON t.TagID=et.TagId
  left join me.TagGroup tg ON tg.TagGroupId=t.TagGroupId
  
Go
  
 CREATE VIEW [me].[Summary] as
with categories as
(
	select CategoryName,Free,SUM([value]) as 'CumulativeValue'
	FROM [me].[Raport]
	where BagName='MagdaSetup'
	group by CategoryName,Free 
)
select CategoryName, TotalSales = SUM(CumulativeValue )
from categories
group by  Rollup(CategoryName)
GO
/****** Object:  View [me].[ExpencesByMonth]    Script Date: 07.12.2023 19:44:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [me].[ExpencesByMonth] as
SELECT BagName, Year(Date) as 'Year', MONTH(Date) as 'Month',
      SUM([Value]) as MonthCost
  FROM [me].[Raport]
  --where BagName='MagdaSetup'
  group by BagName, Year(Date), MONTH(Date)
GO
/****** Object:  View [me].[Peugeot]    Script Date: 07.12.2023 19:44:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [me].[Peugeot] AS
SELECT 
      [Description]
      
      ,[Value]
     
  FROM [me].[Raport] where BagName='Peugeot308'
GO
/****** Object:  View [me].[rollupCost]    Script Date: 07.12.2023 19:44:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [me].[rollupCost] as
SELECT [BagName]
      ,[CategoryName]
      ,[ExpenceName]
       ,Sum([Value]) as aggr
  FROM [me].[Raport]
 group by rollup ([BagName],[Description],CategoryName,ExpenceName)
GO
/****** Object:  View [me].[rollupSpent]    Script Date: 07.12.2023 19:44:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [me].[rollupSpent] as
SELECT [BagName]
      ,[CategoryName]
      ,[ExpenceName]
       ,Sum([Value]) as aggr
  FROM [me].[Raport]
  where free<>1
 group by rollup ([BagName],[Description],CategoryName,ExpenceName)
 
GO
/****** Object:  View [misc].[GiftSummary]    Script Date: 07.12.2023 19:44:19 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [TEMP].[Kameralne7] as
SELECT [BagName]
      ,[CategoryName]
      ,[ExpenseID]
      ,[ExpenceName]
      ,[TagGroup]
      ,[Tag]
      ,[ExpectedValue]
      ,[Value]
  FROM [me].[TagExpense] where BagName='kameralne7' and CategoryName='InitialRemont'
GO
/****** Object:  Table [me].[BagCategory]    Script Date: 07.12.2023 19:44:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [me].[BagCategory](
	[BagCategoryID] [int] IDENTITY(1,1) NOT NULL,
	[BagId] [int] NULL,
	[CategoryId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[BagCategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [BagCategoryUnique] UNIQUE NONCLUSTERED 
(
	[BagId] ASC,
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [me].[BagCurrency]    Script Date: 07.12.2023 19:44:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [me].[BagCurrency](
	[BagCurrencyID] [int] IDENTITY(1,1) NOT NULL,
	[CurrencyID] [int] NULL,
	[BagID] [int] NULL,
	[Value] [decimal](5, 3) NULL,
PRIMARY KEY CLUSTERED 
(
	[BagCurrencyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [me].[Currency]    Script Date: 07.12.2023 19:44:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [me].[Currency](
	[CurrencyID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[CurrencyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [me].[Income]    Script Date: 07.12.2023 19:44:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [me].[Income](
	[IncomeID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](200) NULL,
	[CategoryID] [int] NULL,
	[BagID] [int] NULL,
	[Date] [datetime] NULL,
	[TimeStamp] [timestamp] NOT NULL,
	[CurrencyID] [int] NULL,
	[Value] [decimal](6, 2) NULL,
	[Cleared] [bit] NOT NULL,
	[Comment] [varchar](2000) NULL,
	[IncomeSourceId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IncomeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [me].[IncomeSource]    Script Date: 07.12.2023 19:44:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [me].[IncomeSource](
	[IncomeSourceId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[IncomeSourceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [me].[TagGroupCategory]    Script Date: 07.12.2023 19:44:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [me].[TagGroupCategory](
	[TagGroupCategoryID] [int] IDENTITY(1,1) NOT NULL,
	[TagGroupID] [int] NULL,
	[CategoryID] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [me].[TagTagGroup]    Script Date: 07.12.2023 19:44:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [me].[TagTagGroup](
	[TagTagGroupID] [int] IDENTITY(1,1) NOT NULL,
	[TagID] [int] NULL,
	[TagGroupID] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [me].[Expense] ADD  CONSTRAINT [def_false]  DEFAULT ((0)) FOR [Free]
GO
ALTER TABLE [me].[Expense] ADD  DEFAULT ((0)) FOR [Discount]
GO
ALTER TABLE [me].[Income] ADD  CONSTRAINT [incomeClearedFalse]  DEFAULT ((0)) FOR [Cleared]
GO
ALTER TABLE [me].[BagCategory]  WITH CHECK ADD  CONSTRAINT [FK_BagCategory_Bag] FOREIGN KEY([BagId])
REFERENCES [me].[Bag] ([BagID])
GO
ALTER TABLE [me].[BagCategory] CHECK CONSTRAINT [FK_BagCategory_Bag]
GO
ALTER TABLE [me].[BagCategory]  WITH CHECK ADD  CONSTRAINT [FK_BagCategory_Category] FOREIGN KEY([CategoryId])
REFERENCES [me].[Category] ([CategoryID])
GO
ALTER TABLE [me].[BagCategory] CHECK CONSTRAINT [FK_BagCategory_Category]
GO
ALTER TABLE [me].[BagCurrency]  WITH CHECK ADD  CONSTRAINT [FK_CategoryCurenncy_Category] FOREIGN KEY([BagID])
REFERENCES [me].[Bag] ([BagID])
GO
ALTER TABLE [me].[BagCurrency] CHECK CONSTRAINT [FK_CategoryCurenncy_Category]
GO
ALTER TABLE [me].[BagCurrency]  WITH CHECK ADD  CONSTRAINT [FK_CategoryCurenncy_Currency] FOREIGN KEY([CurrencyID])
REFERENCES [me].[Currency] ([CurrencyID])
GO
ALTER TABLE [me].[BagCurrency] CHECK CONSTRAINT [FK_CategoryCurenncy_Currency]
GO
ALTER TABLE [me].[Expense]  WITH CHECK ADD  CONSTRAINT [FK_Expence_Income] FOREIGN KEY([IncomeId])
REFERENCES [me].[Income] ([IncomeID])
GO
ALTER TABLE [me].[Expense] CHECK CONSTRAINT [FK_Expence_Income]
GO
ALTER TABLE [me].[Expense]  WITH CHECK ADD  CONSTRAINT [FK_Expense_Bag] FOREIGN KEY([BagID])
REFERENCES [me].[Bag] ([BagID])
GO
ALTER TABLE [me].[Expense] CHECK CONSTRAINT [FK_Expense_Bag]
GO
ALTER TABLE [me].[Expense]  WITH CHECK ADD  CONSTRAINT [FK_Expense_Category] FOREIGN KEY([CategoryID])
REFERENCES [me].[Category] ([CategoryID])
GO
ALTER TABLE [me].[Expense] CHECK CONSTRAINT [FK_Expense_Category]
GO
ALTER TABLE [me].[Expense]  WITH CHECK ADD  CONSTRAINT [FK_Expense_Currency] FOREIGN KEY([CurrencyID])
REFERENCES [me].[Currency] ([CurrencyID])
GO
ALTER TABLE [me].[Expense] CHECK CONSTRAINT [FK_Expense_Currency]
GO
ALTER TABLE [me].[ExpenseTag]  WITH CHECK ADD  CONSTRAINT [FK_ExpenseTag_Expense] FOREIGN KEY([ExpenseID])
REFERENCES [me].[Expense] ([ExpenseID])
GO
ALTER TABLE [me].[ExpenseTag] CHECK CONSTRAINT [FK_ExpenseTag_Expense]
GO
ALTER TABLE [me].[ExpenseTag]  WITH CHECK ADD  CONSTRAINT [FK_ExpenseTag_Tag] FOREIGN KEY([TagID])
REFERENCES [me].[Tag] ([TagID])
GO
ALTER TABLE [me].[ExpenseTag] CHECK CONSTRAINT [FK_ExpenseTag_Tag]
GO
ALTER TABLE [me].[Income]  WITH CHECK ADD  CONSTRAINT [FK_Income_Bag] FOREIGN KEY([BagID])
REFERENCES [me].[Bag] ([BagID])
GO
ALTER TABLE [me].[Income] CHECK CONSTRAINT [FK_Income_Bag]
GO
ALTER TABLE [me].[Income]  WITH CHECK ADD  CONSTRAINT [FK_Income_Category] FOREIGN KEY([CategoryID])
REFERENCES [me].[Category] ([CategoryID])
GO
ALTER TABLE [me].[Income] CHECK CONSTRAINT [FK_Income_Category]
GO
ALTER TABLE [me].[Income]  WITH CHECK ADD  CONSTRAINT [FK_Income_Currency] FOREIGN KEY([CurrencyID])
REFERENCES [me].[Currency] ([CurrencyID])
GO
ALTER TABLE [me].[Income] CHECK CONSTRAINT [FK_Income_Currency]
GO
ALTER TABLE [me].[Income]  WITH CHECK ADD  CONSTRAINT [FK_Income_IncomeSource] FOREIGN KEY([IncomeSourceId])
REFERENCES [me].[IncomeSource] ([IncomeSourceId])
GO
ALTER TABLE [me].[Income] CHECK CONSTRAINT [FK_Income_IncomeSource]
GO
ALTER TABLE [me].[Tag]  WITH CHECK ADD  CONSTRAINT [FK_Tag_TagGroup] FOREIGN KEY([TagGroupID])
REFERENCES [me].[TagGroup] ([TagGroupID])
GO
ALTER TABLE [me].[Tag] CHECK CONSTRAINT [FK_Tag_TagGroup]
GO
ALTER TABLE [me].[TagGroupCategory]  WITH CHECK ADD  CONSTRAINT [FK_CategoryTagGroup_Category] FOREIGN KEY([CategoryID])
REFERENCES [me].[Category] ([CategoryID])
GO
ALTER TABLE [me].[TagGroupCategory] CHECK CONSTRAINT [FK_CategoryTagGroup_Category]
GO
ALTER TABLE [me].[TagGroupCategory]  WITH CHECK ADD  CONSTRAINT [FK_CategoryTagGroup_TagGroup] FOREIGN KEY([TagGroupID])
REFERENCES [me].[TagGroup] ([TagGroupID])
GO
ALTER TABLE [me].[TagGroupCategory] CHECK CONSTRAINT [FK_CategoryTagGroup_TagGroup]
GO
ALTER TABLE [me].[TagTagGroup]  WITH CHECK ADD  CONSTRAINT [FK_TagTagGroup_Tag] FOREIGN KEY([TagID])
REFERENCES [me].[Tag] ([TagID])
GO
ALTER TABLE [me].[TagTagGroup] CHECK CONSTRAINT [FK_TagTagGroup_Tag]
GO
ALTER TABLE [me].[TagTagGroup]  WITH CHECK ADD  CONSTRAINT [FK_TagTagGroup_TagGroup] FOREIGN KEY([TagGroupID])
REFERENCES [me].[TagGroup] ([TagGroupID])
GO
ALTER TABLE [me].[TagTagGroup] CHECK CONSTRAINT [FK_TagTagGroup_TagGroup]
GO
USE [master]
GO
ALTER DATABASE [PTExpenses] SET  READ_WRITE 
GO
