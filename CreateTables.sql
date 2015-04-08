USE Charltone;

IF OBJECT_ID ('charltone.dbo.FK_Product_Photo') IS NOT NULL 
ALTER TABLE Photo DROP CONSTRAINT FK_Product_Photo; 

IF OBJECT_ID ('charltone.dbo.FK_Instrument_InstrumentType') IS NOT NULL 
ALTER TABLE Instrument DROP CONSTRAINT FK_Instrument_InstrumentType; 

IF OBJECT_ID ('charltone.dbo.FK_Instrument_Classification') IS NOT NULL 
ALTER TABLE Instrument DROP CONSTRAINT FK_Instrument_Classification; 

IF OBJECT_ID ('charltone.dbo.FK_Instrument_SubClassification') IS NOT NULL 
ALTER TABLE Instrument DROP CONSTRAINT FK_Instrument_SubClassification; 

IF OBJECT_ID ('charltone.dbo.FK_Product_Instrument') IS NOT NULL 
ALTER TABLE Product DROP CONSTRAINT FK_Product_Instrument; 

IF OBJECT_ID ('charltone.dbo.FK_Product_Status') IS NOT NULL 
ALTER TABLE Product DROP CONSTRAINT FK_Product_Status; 

IF OBJECT_ID ('charltone.dbo.FK_Product_ProductType') IS NOT NULL 
ALTER TABLE Product DROP CONSTRAINT FK_Product_ProductType; 

IF OBJECT_ID ('charltone.dbo.AdminUser','U') IS NOT NULL DROP TABLE AdminUser;
CREATE TABLE AdminUser
(
	Id INT PRIMARY KEY, 
	AdminPassword VARCHAR(15)
);

IF OBJECT_ID ('charltone.dbo.ProductType','U') IS NOT NULL DROP TABLE ProductType;
CREATE TABLE ProductType
(
	Id INT PRIMARY KEY, 
	ProductTypeDesc VARCHAR(100),
	SortOrder INT
);

IF OBJECT_ID ('charltone.dbo.InstrumentType','U') IS NOT NULL DROP TABLE InstrumentType;
CREATE TABLE InstrumentType
(
	Id INT PRIMARY KEY, 
	InstrumentTypeDesc VARCHAR(500),
	SortOrder INT
);

IF OBJECT_ID ('charltone.dbo.Classification','U') IS NOT NULL DROP TABLE Classification;
CREATE TABLE Classification
(
	Id INT PRIMARY KEY, 
	ClassificationDesc VARCHAR(500),
	SortOrder INT
);

IF OBJECT_ID ('charltone.dbo.SubClassification','U') IS NOT NULL DROP TABLE SubClassification;
CREATE TABLE SubClassification
(
	Id INT PRIMARY KEY, 
	SubClassificationDesc VARCHAR(500),
	SortOrder INT
);

IF OBJECT_ID ('charltone.dbo.ProductStatus','U') IS NOT NULL DROP TABLE ProductStatus;
CREATE TABLE ProductStatus
(
	Id INT PRIMARY KEY, 
	StatusDesc VARCHAR(500),
	SortOrder INT
);

IF OBJECT_ID ('charltone.dbo.Instrument','U') IS NOT NULL DROP TABLE Instrument;
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

ALTER TABLE [dbo].[Instrument]  WITH CHECK ADD  CONSTRAINT [FK_Instrument_Classification] FOREIGN KEY([ClassificationId])
REFERENCES [dbo].[Classification] ([Id])
GO

ALTER TABLE [dbo].[Instrument] CHECK CONSTRAINT [FK_Instrument_Classification]
GO

ALTER TABLE [dbo].[Instrument]  WITH CHECK ADD  CONSTRAINT [FK_Instrument_InstrumentType] FOREIGN KEY([InstrumentTypeId])
REFERENCES [dbo].[InstrumentType] ([Id])
GO

ALTER TABLE [dbo].[Instrument] CHECK CONSTRAINT [FK_Instrument_InstrumentType]
GO

ALTER TABLE [dbo].[Instrument]  WITH CHECK ADD  CONSTRAINT [FK_Instrument_SubClassification] FOREIGN KEY([SubClassificationId])
REFERENCES [dbo].[SubClassification] ([Id])
GO

ALTER TABLE [dbo].[Instrument] CHECK CONSTRAINT [FK_Instrument_SubClassification]
GO


IF OBJECT_ID ('charltone.dbo.Product','U') IS NOT NULL DROP TABLE Product;
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

ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Instrument] FOREIGN KEY([InstrumentId])
REFERENCES [dbo].[Instrument] ([Id])
GO

ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Instrument]
GO

ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_ProductType] FOREIGN KEY([ProductTypeId])
REFERENCES [dbo].[ProductType] ([Id])
GO

ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_ProductType]
GO

ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Status] FOREIGN KEY([StatusId])
REFERENCES [dbo].[ProductStatus] ([Id])
GO

ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Status]
GO


IF OBJECT_ID ('charltone.dbo.Photo','U') IS NOT NULL DROP TABLE Photo;
CREATE TABLE Photo
(
	Id INT IDENTITY(8,2) PRIMARY KEY,
	ProductId INT,
	IsDefault BIT,
	Data IMAGE,
	CONSTRAINT FK_Product_Photo FOREIGN KEY (ProductId) REFERENCES Product(Id)
);

GO

IF OBJECT_ID ('charltone.dbo.NoPhotoImage','U') IS NOT NULL DROP TABLE NoPhotoImage;
CREATE TABLE [dbo].[NoPhotoImage](
	[Id] [int] NULL,
	[Data] [image] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

IF OBJECT_ID ('charltone.dbo.HomePageImage','U') IS NOT NULL DROP TABLE HomePageImage;
CREATE TABLE [dbo].[HomePageImage](
	[Id] INT IDENTITY(8,2) PRIMARY KEY,
	[Data] IMAGE NOT NULL,
	[SortOrder] INT NOT NULL
)

GO

IF OBJECT_ID ('charltone.dbo.Ordering','U') IS NOT NULL DROP TABLE Ordering;
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

ALTER TABLE [dbo].[Ordering]  WITH CHECK ADD  CONSTRAINT [FK_Ordering_Classification] FOREIGN KEY([ClassificationId])
REFERENCES [dbo].[Classification] ([Id])
GO

ALTER TABLE [dbo].[Ordering] CHECK CONSTRAINT [FK_Ordering_Classification]
GO

ALTER TABLE [dbo].[Ordering]  WITH CHECK ADD  CONSTRAINT [FK_Ordering_InstrumentType] FOREIGN KEY([InstrumentTypeId])
REFERENCES [dbo].[InstrumentType] ([Id])
GO

ALTER TABLE [dbo].[Ordering] CHECK CONSTRAINT [FK_Ordering_InstrumentType]
GO

ALTER TABLE [dbo].[Ordering]  WITH CHECK ADD  CONSTRAINT [FK_Ordering_SubClassification] FOREIGN KEY([SubClassificationId])
REFERENCES [dbo].[SubClassification] ([Id])
GO

ALTER TABLE [dbo].[Ordering] CHECK CONSTRAINT [FK_Ordering_SubClassification]
GO

--------- admin user ------------
INSERT INTO AdminUser VALUES(1, 'guit@r1');

--------- product types -----------
INSERT INTO ProductType(Id, ProductTypeDesc, SortOrder) VALUES(1, 'Instrument', 1);
INSERT INTO ProductType(Id, ProductTypeDesc, SortOrder) VALUES(2, 'Box', 2);
INSERT INTO ProductType(Id, ProductTypeDesc, SortOrder) VALUES(3, 'Bridge Pin', 3);

--------- instrument types -----------
INSERT INTO InstrumentType(Id, InstrumentTypeDesc, SortOrder) VALUES(1, 'Guitar', 1);
INSERT INTO InstrumentType(Id, InstrumentTypeDesc, SortOrder) VALUES(2, 'Mandolin', 2);
INSERT INTO InstrumentType(Id, InstrumentTypeDesc, SortOrder) VALUES(3, 'Banjo', 3);
INSERT INTO InstrumentType(Id, InstrumentTypeDesc, SortOrder) VALUES(4, 'Dobro', 4);

--------- classifications -----------
INSERT INTO Classification(Id, ClassificationDesc, SortOrder) VALUES(1, 'Steel String', 1);
INSERT INTO Classification(Id, ClassificationDesc, SortOrder) VALUES(2, 'Nylon String', 2);
INSERT INTO Classification(Id, ClassificationDesc, SortOrder) VALUES(3, 'Solid Body', 3);
INSERT INTO Classification(Id, ClassificationDesc, SortOrder) VALUES(4, 'Electro-Acoustic', 4);

--------- subclassifications -----------
INSERT INTO SubClassification(Id, SubClassificationDesc, SortOrder) VALUES(1, 'Classical', 1);
INSERT INTO SubClassification(Id, SubClassificationDesc, SortOrder) VALUES(2, 'Solid Body Arched Top', 2);
INSERT INTO SubClassification(Id, SubClassificationDesc, SortOrder) VALUES(3, 'Solid Body Flat Top', 3);
INSERT INTO SubClassification(Id, SubClassificationDesc, SortOrder) VALUES(4, 'Solid Body "X"', 4);
INSERT INTO SubClassification(Id, SubClassificationDesc, SortOrder) VALUES(5, 'Dreadnought', 5);
INSERT INTO SubClassification(Id, SubClassificationDesc, SortOrder) VALUES(6, 'OM Variation', 6);
INSERT INTO SubClassification(Id, SubClassificationDesc, SortOrder) VALUES(7, 'Orchestra Model', 7);
INSERT INTO SubClassification(Id, SubClassificationDesc, SortOrder) VALUES(8, 'Parlour', 8);
INSERT INTO SubClassification(Id, SubClassificationDesc, SortOrder) VALUES(9, 'Flamenco', 9);
INSERT INTO SubClassification(Id, SubClassificationDesc, SortOrder) VALUES(10, 'Small Flamenco', 10);
INSERT INTO SubClassification(Id, SubClassificationDesc, SortOrder) VALUES(11, 'Semi-Hollow Body', 11);
INSERT INTO SubClassification(Id, SubClassificationDesc, SortOrder) VALUES(12, 'Folk', 12);
INSERT INTO SubClassification(Id, SubClassificationDesc, SortOrder) VALUES(13, 'Hollow Body Archtop', 13);

--------- add ItemStatus types -----------
INSERT INTO ProductStatus(Id, StatusDesc) VALUES(1, 'Available');
INSERT INTO ProductStatus(Id, StatusDesc) VALUES(2, 'Sold');
INSERT INTO ProductStatus(Id, StatusDesc) VALUES(3, 'Not For Sale');

--------- products / instruments -----------
INSERT INTO Instrument (InstrumentTypeId,  Model, SN, ClassificationId, SubClassificationId, [Top], BackAndSides, Body, [Binding], Neck, Faceplate, Fingerboard, FretMarkers, EdgeDots, Bridge, Finish, Tuners, Pickguard, Pickup, NutWidth, ScaleLength, FunFacts, CaseDetail, Comments, Strings)
VALUES (1, 'SS-DR', '003',	1, 5, 'Sitka Spruce', 'Honduras Mahogany', NULL, 'Santos Rosewood w/ black-white purfling', 'Honduras Mahogany', 'Unknown Hardwood (Redwood?)', 'Macassar Ebony', 'MOP 1/4" dots', '2mm White Plastic', 'Brazilian Rosewood', 'French polish', 'Schaller M6 Nickel', 'Black Plastic .035"', 'Fishman AG-125 Undersaddle (passive)', '43.5mm', '640mm', 'Faceplate is from unknown hardwood, given to me at my SAIT con-ed cabinet making course in 1999', 'Standard hard-shell', 'Very nice balance across all frequencies', 'D''Addario EJ16 "Lights"');

INSERT INTO Product(ProductTypeId, InstrumentId, ProductDesc, StatusId, IsPosted, DisplayPrice) VALUES(1, (SELECT MAX(Id) FROM Instrument), 'Instrument', 1, 0, '$1,000 Firm');

INSERT INTO Photo(ProductId, IsDefault, Data)
SELECT (SELECT MAX(Id) FROM Product), 1, * FROM
OPENROWSET(BULK N'C:\Users\John\Documents\GitHub\Charltone\photos\CL-RAM 003.jpg', SINGLE_BLOB) A;

INSERT INTO Photo(ProductId, IsDefault, Data)
SELECT (SELECT MAX(Id) FROM Product), 0, * FROM
OPENROWSET(BULK N'C:\Users\John\Documents\GitHub\Charltone\photos\CL-RAM 003 Peghead.jpg', SINGLE_BLOB) A;

--- Ordering ---
INSERT INTO Ordering (InstrumentTypeId, ClassificationId, SubClassificationId, Model, TypicalPrice, Comments, Photo)
SELECT 1, 1, 5, 'SS-DR', '$2,500 including a hardshell case', 'Based on the Bill Lewis dreadnought plans, so it has a slightly more pinched waist than a typical dreadnought, giving it a bit more brightness than others you might’ve heard. Otherwise, it has all the bass & balance you’d expect in this style.', * FROM
OPENROWSET(BULK N'C:\Users\John\Documents\GitHub\Charltone\photos\CL-RAM 003.jpg', SINGLE_BLOB) A;

--- No Photo Image ---
INSERT INTO NoPhotoImage(Id, Data)
SELECT 1, * FROM
OPENROWSET(BULK N'C:\Users\John\Documents\GitHub\Charltone\photos\CL-RAM 003 Peghead.jpg', SINGLE_BLOB) A;


--- Home Page Image ---
INSERT INTO HomePageImage(Data, SortOrder)
SELECT *, 1 FROM
OPENROWSET(BULK N'C:\Users\John\Documents\GitHub\Charltone\photos\home_page_sm.jpg', SINGLE_BLOB) A;

INSERT INTO HomePageImage(Data, SortOrder)
SELECT *, 2 FROM
OPENROWSET(BULK N'C:\Users\John\Documents\GitHub\Charltone\photos\Garage - Before (Ceiling)_sm.jpg', SINGLE_BLOB) A;

INSERT INTO HomePageImage(Data, SortOrder)
SELECT *, 3 FROM
OPENROWSET(BULK N'C:\Users\John\Documents\GitHub\Charltone\photos\Garage - Before (E. Wall)_1_sm.jpg', SINGLE_BLOB) A;

INSERT INTO HomePageImage(Data, SortOrder)
SELECT *, 4 FROM
OPENROWSET(BULK N'C:\Users\John\Documents\GitHub\Charltone\photos\Garage - Before (E. Wall)_2_sm.jpg', SINGLE_BLOB) A;

INSERT INTO HomePageImage(Data, SortOrder)
SELECT *, 5 FROM
OPENROWSET(BULK N'C:\Users\John\Documents\GitHub\Charltone\photos\Garage - Before (W wall)_1_sm.jpg', SINGLE_BLOB) A;

INSERT INTO HomePageImage(Data, SortOrder)
SELECT *, 6 FROM
OPENROWSET(BULK N'C:\Users\John\Documents\GitHub\Charltone\photos\Garage - Before (W wall)_2_sm.jpg', SINGLE_BLOB) A;

INSERT INTO HomePageImage(Data, SortOrder)
SELECT *, 7 FROM
OPENROWSET(BULK N'C:\Users\John\Documents\GitHub\Charltone\photos\IMG_2099_sm.jpg', SINGLE_BLOB) A;

INSERT INTO HomePageImage(Data, SortOrder)
SELECT *, 8 FROM
OPENROWSET(BULK N'C:\Users\John\Documents\GitHub\Charltone\photos\IMG_2113_sm.jpg', SINGLE_BLOB) A;

INSERT INTO HomePageImage(Data, SortOrder)
SELECT *, 9 FROM
OPENROWSET(BULK N'C:\Users\John\Documents\GitHub\Charltone\photos\IMG_2136_sm.jpg', SINGLE_BLOB) A;

INSERT INTO HomePageImage(Data, SortOrder)
SELECT *, 10 FROM
OPENROWSET(BULK N'C:\Users\John\Documents\GitHub\Charltone\photos\IMG_2144_sm.jpg', SINGLE_BLOB) A;

INSERT INTO HomePageImage(Data, SortOrder)
SELECT *, 11 FROM
OPENROWSET(BULK N'C:\Users\John\Documents\GitHub\Charltone\photos\IMG_2156_sm.jpg', SINGLE_BLOB) A;

INSERT INTO HomePageImage(Data, SortOrder)
SELECT *, 12 FROM
OPENROWSET(BULK N'C:\Users\John\Documents\GitHub\Charltone\photos\IMG_2190_sm.jpg', SINGLE_BLOB) A;

INSERT INTO HomePageImage(Data, SortOrder)
SELECT *, 13 FROM
OPENROWSET(BULK N'C:\Users\John\Documents\GitHub\Charltone\photos\IMG_2196_sm.jpg', SINGLE_BLOB) A;

INSERT INTO HomePageImage(Data, SortOrder)
SELECT *, 14 FROM
OPENROWSET(BULK N'C:\Users\John\Documents\GitHub\Charltone\photos\IMG_2197_sm.jpg', SINGLE_BLOB) A;

INSERT INTO HomePageImage(Data, SortOrder)
SELECT *, 15 FROM
OPENROWSET(BULK N'C:\Users\John\Documents\GitHub\Charltone\photos\IMG_2204_sm.jpg', SINGLE_BLOB) A;

INSERT INTO HomePageImage(Data, SortOrder)
SELECT *, 16 FROM
OPENROWSET(BULK N'C:\Users\John\Documents\GitHub\Charltone\photos\IMG_2212_sm.jpg', SINGLE_BLOB) A;

---

/*
SELECT * FROM Instrument
INSERT INTO Instrument (InstrumentTypeId, ClassificationId,SubClassificationId, Model, SN, Body, Neck, Fingerboard, Bridge, Pickguard, Pickup, NutWidth, ScaleLength, FunFacts, CaseDetail, Comments, Strings)
VALUES (1, 3, 6, 'EL-SBA 001', 'Quilted Maple top, African Mahogany body', 'Mahogany', 'Ebony', 'Metal', 'None', 'Dual Humbucker');

INSERT INTO Product(ProductTypeId, InstrumentId, ProductDesc, StatusId) VALUES(1, (SELECT MAX(Id) FROM Instrument), 'Instrument', 1);

INSERT INTO Photo(ProductId, IsDefault, Data)
SELECT (SELECT MAX(Id) FROM Product), 1, * FROM
OPENROWSET(BULK N'C:\Users\John\Documents\GitHub\Charltone\photos\EL-SBA 001 Side View.jpg', SINGLE_BLOB) A;

---


INSERT INTO Instrument (InstrumentTypeId, ClassificationId,SubClassificationId, Model, SN, Body, Neck, Fingerboard, Bridge, Pickguard, Pickup, NutWidth, ScaleLength, FunFacts, CaseDetail, Comments, Strings)
VALUES (1, 3, 5, 'EL-SBF 001', 'A strange and unusual strain of Alder found only near the shores of New Guinea', 'Chiseled from Bianco Marble', 'Maple', 'Metal', 'Optional', 'Dual Humbucker');

INSERT INTO Product(ProductTypeId, InstrumentId, ProductDesc, StatusId) VALUES(1, (SELECT MAX(Id) FROM Instrument), 'Instrument', 2);

INSERT INTO Photo(ProductId, IsDefault, Data)
SELECT (SELECT MAX(Id) FROM Product), 1, * FROM
OPENROWSET(BULK N'C:\Users\John\Documents\GitHub\Charltone\photos\EL-SBF 001.jpg', SINGLE_BLOB) A;

---

INSERT INTO Instrument (InstrumentTypeId, ClassificationId,SubClassificationId, Model, SN, Body, Neck, Fingerboard, Bridge, Pickguard, Pickup, NutWidth, ScaleLength, FunFacts, CaseDetail, Comments, Strings)
VALUES (1, 3, 5, 'EL-SBX 001', 'Maple top, Mahogany body', 'Mahogany', 'Ebony', 'Metal', 'None', 'Dual Humbucker');

INSERT INTO Product(ProductTypeId, InstrumentId, ProductDesc, StatusId) VALUES(1, (SELECT MAX(Id) FROM Instrument), 'Instrument', 3);

INSERT INTO Photo(ProductId, IsDefault, Data)
SELECT (SELECT MAX(Id) FROM Product), 1, * FROM
OPENROWSET(BULK N'C:\Users\John\Documents\GitHub\Charltone\photos\EL-SBX 001 Peghead.jpg', SINGLE_BLOB) A;

---

INSERT INTO Instrument (InstrumentTypeId, ClassificationId,SubClassificationId, Model, SN, Body, Neck, Fingerboard, Bridge, Pickguard, Pickup, NutWidth, ScaleLength, FunFacts, CaseDetail, Comments, Strings)
VALUES (1, 1, 1, 'SS-DR 001', 'Engelmann spruce top, Mahogany sides', 'Mahogany', 'Ebony', 'Honduran rosewood', 'Black plastic', NULL);

INSERT INTO Product(ProductTypeId, InstrumentId, ProductDesc, StatusId) VALUES(1, (SELECT MAX(Id) FROM Instrument), 'Instrument', 1);

INSERT INTO Photo(ProductId, IsDefault, Data)
SELECT (SELECT MAX(Id) FROM Product), 1, * FROM
OPENROWSET(BULK N'C:\Users\John\Documents\GitHub\Charltone\photos\SS-DR 001.jpg', SINGLE_BLOB) A;

INSERT INTO Photo(ProductId, IsDefault, Data)
SELECT (SELECT MAX(Id) FROM Product), 0, * FROM
OPENROWSET(BULK N'C:\Users\John\Documents\GitHub\Charltone\photos\SS-DR 001 Rosette.jpg', SINGLE_BLOB) A;

INSERT INTO Photo(ProductId, IsDefault, Data)
SELECT (SELECT MAX(Id) FROM Product), 0, * FROM
OPENROWSET(BULK N'C:\Users\John\Documents\GitHub\Charltone\photos\SS-DR 001 Peghead.jpg', SINGLE_BLOB) A;

---

INSERT INTO Instrument (InstrumentTypeId, ClassificationId,SubClassificationId, Model, SN, Body, Neck, Fingerboard, Bridge, Pickguard, Pickup, NutWidth, ScaleLength, FunFacts, CaseDetail, Comments, Strings)
VALUES (1, 1, 1, 'SS-DR 002', 'Engelmann spruce top, Mahogany sides', 'Mahogany', 'Ebony', 'Honduran rosewood', 'Black plastic',  NULL);

INSERT INTO Product(ProductTypeId, InstrumentId, ProductDesc, StatusId) VALUES(1, (SELECT MAX(Id) FROM Instrument), 'Instrument', 1);

INSERT INTO Photo(ProductId, IsDefault, Data)
SELECT (SELECT MAX(Id) FROM Product), 0, * FROM
OPENROWSET(BULK N'C:\Users\John\Documents\GitHub\Charltone\photos\SS-DR 002.jpg', SINGLE_BLOB) A;

INSERT INTO Photo(ProductId, IsDefault, Data)
SELECT (SELECT MAX(Id) FROM Product), 0, * FROM
OPENROWSET(BULK N'C:\Users\John\Documents\GitHub\Charltone\photos\SS-DR 002 Rosette.jpg', SINGLE_BLOB) A;

INSERT INTO Photo(ProductId, IsDefault, Data)
SELECT (SELECT MAX(Id) FROM Product), 1, * FROM
OPENROWSET(BULK N'C:\Users\John\Documents\GitHub\Charltone\photos\SS-DR 002 getting strung.jpg', SINGLE_BLOB) A;

---

INSERT INTO Instrument (InstrumentTypeId, ClassificationId,SubClassificationId, Model, SN, Body, Neck, Fingerboard, Bridge, Pickguard, Pickup, NutWidth, ScaleLength, FunFacts, CaseDetail, Comments, Strings)
VALUES (1, 1, 1, 'SS-DR 003', 'Engelmann spruce top, Mahogany sides', 'Mahogany', 'Ebony', 'Honduran rosewood', 'Black plastic', NULL);

INSERT INTO Product(ProductTypeId, InstrumentId, ProductDesc, StatusId) VALUES(1, (SELECT MAX(Id) FROM Instrument), 'Instrument', 1);

INSERT INTO Photo(ProductId, IsDefault, Data)
SELECT (SELECT MAX(Id) FROM Product), 1, * FROM
OPENROWSET(BULK N'C:\Users\John\Documents\GitHub\Charltone\photos\SS-DR 003.jpg', SINGLE_BLOB) A;

INSERT INTO Photo(ProductId, IsDefault, Data)
SELECT (SELECT MAX(Id) FROM Product), 0, * FROM
OPENROWSET(BULK N'C:\Users\John\Documents\GitHub\Charltone\photos\SS-DR 003 Pickup jack.jpg', SINGLE_BLOB) A;

INSERT INTO Photo(ProductId, IsDefault, Data)
SELECT (SELECT MAX(Id) FROM Product), 0, * FROM
OPENROWSET(BULK N'C:\Users\John\Documents\GitHub\Charltone\photos\SS-DR 003 Strings On_1.jpg', SINGLE_BLOB) A;

---

INSERT INTO Instrument (InstrumentTypeId, ClassificationId,SubClassificationId, Model, SN, Body, Neck, Fingerboard, Bridge, Pickguard, Pickup, NutWidth, ScaleLength, FunFacts, CaseDetail, Comments, Strings)
VALUES (1, 1, 1, 'SS-MAR6 002', 'Engelmann spruce top, Mahogany sides', 'Mahogany', 'Ebony', 'Honduran rosewood', 'Black plastic',  NULL);

INSERT INTO Product(ProductTypeId, InstrumentId, ProductDesc, StatusId) VALUES(1, (SELECT MAX(Id) FROM Instrument), 'Instrument', 1);

INSERT INTO Photo(ProductId, IsDefault, Data)
SELECT (SELECT MAX(Id) FROM Product), 1, * FROM
OPENROWSET(BULK N'C:\Users\John\Documents\GitHub\Charltone\photos\SS-MAR6 002 Front.jpg', SINGLE_BLOB) A;

INSERT INTO Photo(ProductId, IsDefault, Data)
SELECT (SELECT MAX(Id) FROM Product), 0, * FROM
OPENROWSET(BULK N'C:\Users\John\Documents\GitHub\Charltone\photos\SS-MAR6 002 Back.jpg', SINGLE_BLOB) A;

---

INSERT INTO Instrument (InstrumentTypeId, ClassificationId,SubClassificationId, Model, SN, Body, Neck, Fingerboard, Bridge, Pickguard, Pickup, NutWidth, ScaleLength, FunFacts, CaseDetail, Comments, Strings)
VALUES (1, 1, 8, 'SS-NI 001', 'Engelmann spruce top, Mahogany sides', 'Mahogany', 'Ebony', 'Honduran rosewood', 'Clear plastic',  NULL);

INSERT INTO Product(ProductTypeId, InstrumentId, ProductDesc, StatusId) VALUES(1, (SELECT MAX(Id) FROM Instrument), 'Instrument', 1);

INSERT INTO Photo(ProductId, IsDefault, Data)
SELECT (SELECT MAX(Id) FROM Product), 1, * FROM
OPENROWSET(BULK N'C:\Users\John\Documents\GitHub\Charltone\photos\SS-NI 001 Front.jpg', SINGLE_BLOB) A;

INSERT INTO Photo(ProductId, IsDefault, Data)
SELECT (SELECT MAX(Id) FROM Product), 0, * FROM
OPENROWSET(BULK N'C:\Users\John\Documents\GitHub\Charltone\photos\SS-NI 001 Back.jpg', SINGLE_BLOB) A;

---

INSERT INTO Instrument (InstrumentTypeId, ClassificationId,SubClassificationId, Model, SN, Body, Neck, Fingerboard, Bridge, Pickguard, Pickup, NutWidth, ScaleLength, FunFacts, CaseDetail, Comments, Strings)
VALUES (1, 1, 7, 'SS-ORC 001', 'Engelmann spruce top, Mahogany sides', 'Mahogany', 'Ebony', 'Honduran rosewood', 'Clear plastic',  NULL);

INSERT INTO Product(ProductTypeId, InstrumentId, ProductDesc, StatusId) VALUES(1, (SELECT MAX(Id) FROM Instrument), 'Instrument', 1);

INSERT INTO Photo(ProductId, IsDefault, Data)
SELECT (SELECT MAX(Id) FROM Product), 1, * FROM
OPENROWSET(BULK N'C:\Users\John\Documents\GitHub\Charltone\photos\SS-ORC 001  Front.jpg', SINGLE_BLOB) A;

INSERT INTO Photo(ProductId, IsDefault, Data)
SELECT (SELECT MAX(Id) FROM Product), 0, * FROM
OPENROWSET(BULK N'C:\Users\John\Documents\GitHub\Charltone\photos\SS-ORC 001 Back.jpg', SINGLE_BLOB) A;

---

INSERT INTO Instrument (InstrumentTypeId, ClassificationId,SubClassificationId, Model, SN, Body, Neck, Fingerboard, Bridge, Pickguard, Pickup, NutWidth, ScaleLength, FunFacts, CaseDetail, Comments, Strings)
VALUES (1, 1, 2, 'SS-SC 001', 'Engelmann spruce top, Mahogany sides', 'Mahogany', 'Ebony', 'Honduran rosewood', 'Black plastic',  NULL);

INSERT INTO Product(ProductTypeId, InstrumentId, ProductDesc, StatusId) VALUES(1, (SELECT MAX(Id) FROM Instrument), 'Instrument', 1);

INSERT INTO Photo(ProductId, IsDefault, Data)
SELECT (SELECT MAX(Id) FROM Product), 1, * FROM
OPENROWSET(BULK N'C:\Users\John\Documents\GitHub\Charltone\photos\SS-SC 001 Rosette.jpg', SINGLE_BLOB) A;

---

INSERT INTO Instrument (InstrumentTypeId, ClassificationId,SubClassificationId, Model, SN, Body, Neck, Fingerboard, Bridge, Pickguard, Pickup, NutWidth, ScaleLength, FunFacts, CaseDetail, Comments, Strings)
VALUES (1, 1, 2, 'SS-SC 003', 'Engelmann spruce top, Mahogany sides', 'Mahogany', 'Ebony', 'Honduran rosewood', 'Clear plastic', NULL);

INSERT INTO Product(ProductTypeId, InstrumentId, ProductDesc, StatusId) VALUES(1, (SELECT MAX(Id) FROM Instrument), 'Instrument', 1);

INSERT INTO Photo(ProductId, IsDefault, Data)
SELECT (SELECT MAX(Id) FROM Product), 1, * FROM
OPENROWSET(BULK N'C:\Users\John\Documents\GitHub\Charltone\photos\SS-SC 003.jpg', SINGLE_BLOB) A;

INSERT INTO Photo(ProductId, IsDefault, Data)
SELECT (SELECT MAX(Id) FROM Product), 0, * FROM
OPENROWSET(BULK N'C:\Users\John\Documents\GitHub\Charltone\photos\SS-SC 003 Peghead.jpg', SINGLE_BLOB) A;

---

INSERT INTO Instrument (InstrumentTypeId, ClassificationId,SubClassificationId, Model, SN, Body, Neck, Fingerboard, Bridge, Pickguard, Pickup, NutWidth, ScaleLength, FunFacts, CaseDetail, Comments, Strings)
VALUES (1, 1, 2, 'SS-SC 004', 'Engelmann spruce top, Mahogany sides', 'Mahogany', 'Ebony', 'Honduran rosewood', 'Black plastic', NULL);

INSERT INTO Product(ProductTypeId, InstrumentId, ProductDesc, StatusId) VALUES(1, (SELECT MAX(Id) FROM Instrument), 'Instrument', 1);

INSERT INTO Photo(ProductId, IsDefault, Data)
SELECT (SELECT MAX(Id) FROM Product), 1, * FROM
OPENROWSET(BULK N'C:\Users\John\Documents\GitHub\Charltone\photos\SS-SC 004  Front.jpg', SINGLE_BLOB) A;

INSERT INTO Photo(ProductId, IsDefault, Data)
SELECT (SELECT MAX(Id) FROM Product), 0, * FROM
OPENROWSET(BULK N'C:\Users\John\Documents\GitHub\Charltone\photos\SS-SC 004 Back.jpg', SINGLE_BLOB) A;

---

INSERT INTO Instrument (InstrumentTypeId, ClassificationId,SubClassificationId, Model, SN, Body, Neck, Fingerboard, Bridge, Pickguard, Pickup, NutWidth, ScaleLength, FunFacts, CaseDetail, Comments, Strings)
VALUES (1, 1, 2, 'SS-SC 005', 'Engelmann spruce top, Mahogany sides', 'Mahogany', 'Ebony', 'Honduran rosewood', 'Black plastic', NULL);

INSERT INTO Product(ProductTypeId, InstrumentId, ProductDesc, StatusId) VALUES(1, (SELECT MAX(Id) FROM Instrument), 'Instrument', 1);

INSERT INTO Photo(ProductId, IsDefault, Data)
SELECT (SELECT MAX(Id) FROM Product), 1, * FROM
OPENROWSET(BULK N'C:\Users\John\Documents\GitHub\Charltone\photos\SS-SC 005 No Neck.jpg', SINGLE_BLOB) A;

*/

SELECT 
	P.Id As ProductId,
	P.Id As InstrumentId,
	PHOTO.Id AS DefaultPhotoId,
	PT.ProductTypeDesc As ProductType,
	IT.InstrumentTypeDesc AS InstrumentType,
	C.ClassificationDesc AS Class, 
	SC.SubClassificationDesc AS SubClass, 
	I.Model, 
	I.Body, 
	I.Neck, 
	I.Fingerboard,
	I.Bridge,
	I.Pickguard, 
	I.Pickup,
	S.StatusDesc AS [Status]
FROM Product P
JOIN ProductType PT ON P.ProductTypeId = PT.Id
LEFT JOIN Instrument I ON P.InstrumentId = I.Id
JOIN ProductStatus S ON P.StatusId = S.Id
JOIN InstrumentType IT ON I.InstrumentTypeId = IT.Id
JOIN Classification C ON I.ClassificationId = C.Id
JOIN SubClassification SC ON I.SubClassificationId = SC.Id
JOIN Photo PHOTO ON I.Id = PHOTO.ProductId AND PHOTO.IsDefault = 1
ORDER BY IT.SortOrder, C.SortOrder, SC.SortOrder

