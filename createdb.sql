USE [Charltone]
GO
/****** Object:  User [john]    Script Date: 04/06/2015 12:42:47 ******/
CREATE USER [john] FOR LOGIN [john] WITH DEFAULT_SCHEMA=[john]
GO
/****** Object:  Schema [john]    Script Date: 04/06/2015 12:42:47 ******/
CREATE SCHEMA [john] AUTHORIZATION [john]
GO
/****** Object:  Table [dbo].[SubClassification]    Script Date: 04/06/2015 12:42:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SubClassification](
	[Id] [int] NOT NULL,
	[SubClassificationDesc] [varchar](500) NULL,
	[SortOrder] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProductType]    Script Date: 04/06/2015 12:42:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProductType](
	[Id] [int] NOT NULL,
	[ProductTypeDesc] [varchar](100) NULL,
	[SortOrder] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProductStatus]    Script Date: 04/06/2015 12:42:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProductStatus](
	[Id] [int] NOT NULL,
	[StatusDesc] [varchar](500) NULL,
	[SortOrder] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[HomePageImage]    Script Date: 04/06/2015 12:42:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HomePageImage](
	[Id] [int] IDENTITY(8,2) NOT NULL,
	[Data] [image] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Classification]    Script Date: 04/06/2015 12:42:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Classification](
	[Id] [int] NOT NULL,
	[ClassificationDesc] [varchar](500) NULL,
	[SortOrder] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AdminUser]    Script Date: 04/06/2015 12:42:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AdminUser](
	[Id] [int] NOT NULL,
	[AdminPassword] [varchar](15) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[NoPhotoImage]    Script Date: 04/06/2015 12:42:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NoPhotoImage](
	[Id] [int] NULL,
	[Data] [image] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InstrumentType]    Script Date: 04/06/2015 12:42:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[InstrumentType](
	[Id] [int] NOT NULL,
	[InstrumentTypeDesc] [varchar](500) NULL,
	[SortOrder] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Ordering]    Script Date: 04/06/2015 12:42:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Ordering](
	[Id] [int] IDENTITY(8,2) NOT NULL,
	[InstrumentTypeId] [int] NULL,
	[ClassificationId] [int] NULL,
	[SubClassificationId] [int] NULL,
	[Model] [varchar](25) NULL,
	[TypicalPrice] [varchar](200) NULL,
	[Comments] [varchar](1000) NULL,
	[Photo] [image] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Instrument]    Script Date: 04/06/2015 12:42:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Instrument](
	[Id] [int] IDENTITY(8,2) NOT NULL,
	[InstrumentTypeId] [int] NULL,
	[Model] [varchar](25) NULL,
	[SN] [varchar](10) NULL,
	[ClassificationId] [int] NULL,
	[SubClassificationId] [int] NULL,
	[Top] [varchar](200) NULL,
	[BackAndSides] [varchar](200) NULL,
	[Body] [varchar](200) NULL,
	[Binding] [varchar](200) NULL,
	[Neck] [varchar](200) NULL,
	[Faceplate] [varchar](200) NULL,
	[Fingerboard] [varchar](200) NULL,
	[FretMarkers] [varchar](200) NULL,
	[EdgeDots] [varchar](200) NULL,
	[Bridge] [varchar](200) NULL,
	[Finish] [varchar](200) NULL,
	[Tuners] [varchar](200) NULL,
	[PickGuard] [varchar](200) NULL,
	[Pickup] [varchar](200) NULL,
	[NutWidth] [varchar](200) NULL,
	[ScaleLength] [varchar](200) NULL,
	[FunFacts] [varchar](1000) NULL,
	[CaseDetail] [varchar](200) NULL,
	[Comments] [varchar](500) NULL,
	[Strings] [varchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Product]    Script Date: 04/06/2015 12:42:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [int] IDENTITY(8,2) NOT NULL,
	[ProductTypeId] [int] NULL,
	[InstrumentId] [int] NULL,
	[ProductDesc] [varchar](500) NULL,
	[Price] [numeric](12, 2) NULL,
	[StatusId] [int] NULL,
	[IsPosted] [bit] NULL,
	[DisplayPrice] [varchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Photo]    Script Date: 04/06/2015 12:42:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Photo](
	[Id] [int] IDENTITY(8,2) NOT NULL,
	[ProductId] [int] NULL,
	[IsDefault] [bit] NULL,
	[Data] [image] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_Instrument_Classification]    Script Date: 04/06/2015 12:42:47 ******/
ALTER TABLE [dbo].[Instrument]  WITH CHECK ADD  CONSTRAINT [FK_Instrument_Classification] FOREIGN KEY([ClassificationId])
REFERENCES [dbo].[Classification] ([Id])
GO
ALTER TABLE [dbo].[Instrument] CHECK CONSTRAINT [FK_Instrument_Classification]
GO
/****** Object:  ForeignKey [FK_Instrument_InstrumentType]    Script Date: 04/06/2015 12:42:47 ******/
ALTER TABLE [dbo].[Instrument]  WITH CHECK ADD  CONSTRAINT [FK_Instrument_InstrumentType] FOREIGN KEY([InstrumentTypeId])
REFERENCES [dbo].[InstrumentType] ([Id])
GO
ALTER TABLE [dbo].[Instrument] CHECK CONSTRAINT [FK_Instrument_InstrumentType]
GO
/****** Object:  ForeignKey [FK_Instrument_SubClassification]    Script Date: 04/06/2015 12:42:47 ******/
ALTER TABLE [dbo].[Instrument]  WITH CHECK ADD  CONSTRAINT [FK_Instrument_SubClassification] FOREIGN KEY([SubClassificationId])
REFERENCES [dbo].[SubClassification] ([Id])
GO
ALTER TABLE [dbo].[Instrument] CHECK CONSTRAINT [FK_Instrument_SubClassification]
GO
/****** Object:  ForeignKey [FK_Ordering_Classification]    Script Date: 04/06/2015 12:42:47 ******/
ALTER TABLE [dbo].[Ordering]  WITH CHECK ADD  CONSTRAINT [FK_Ordering_Classification] FOREIGN KEY([ClassificationId])
REFERENCES [dbo].[Classification] ([Id])
GO
ALTER TABLE [dbo].[Ordering] CHECK CONSTRAINT [FK_Ordering_Classification]
GO
/****** Object:  ForeignKey [FK_Ordering_InstrumentType]    Script Date: 04/06/2015 12:42:47 ******/
ALTER TABLE [dbo].[Ordering]  WITH CHECK ADD  CONSTRAINT [FK_Ordering_InstrumentType] FOREIGN KEY([InstrumentTypeId])
REFERENCES [dbo].[InstrumentType] ([Id])
GO
ALTER TABLE [dbo].[Ordering] CHECK CONSTRAINT [FK_Ordering_InstrumentType]
GO
/****** Object:  ForeignKey [FK_Ordering_SubClassification]    Script Date: 04/06/2015 12:42:47 ******/
ALTER TABLE [dbo].[Ordering]  WITH CHECK ADD  CONSTRAINT [FK_Ordering_SubClassification] FOREIGN KEY([SubClassificationId])
REFERENCES [dbo].[SubClassification] ([Id])
GO
ALTER TABLE [dbo].[Ordering] CHECK CONSTRAINT [FK_Ordering_SubClassification]
GO
/****** Object:  ForeignKey [FK_Product_Photo]    Script Date: 04/06/2015 12:42:47 ******/
ALTER TABLE [dbo].[Photo]  WITH CHECK ADD  CONSTRAINT [FK_Product_Photo] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
GO
ALTER TABLE [dbo].[Photo] CHECK CONSTRAINT [FK_Product_Photo]
GO
/****** Object:  ForeignKey [FK_Product_Instrument]    Script Date: 04/06/2015 12:42:47 ******/
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Instrument] FOREIGN KEY([InstrumentId])
REFERENCES [dbo].[Instrument] ([Id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Instrument]
GO
/****** Object:  ForeignKey [FK_Product_ProductType]    Script Date: 04/06/2015 12:42:47 ******/
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_ProductType] FOREIGN KEY([ProductTypeId])
REFERENCES [dbo].[ProductType] ([Id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_ProductType]
GO
/****** Object:  ForeignKey [FK_Product_Status]    Script Date: 04/06/2015 12:42:47 ******/
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Status] FOREIGN KEY([StatusId])
REFERENCES [dbo].[ProductStatus] ([Id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Status]
GO
