USE [EasyShopping]
GO
/****** Object:  Table [dbo].[Comment]    Script Date: 6/17/2017 2:33:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comment](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[ProductID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[ParentCmt] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Country]    Script Date: 6/17/2017 2:33:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Country](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CountryCode] [nvarchar](100) NOT NULL,
	[CommonName] [nvarchar](100) NULL,
	[FormalName] [nvarchar](100) NULL,
	[CountryType] [nvarchar](100) NULL,
	[CountrySubType] [nvarchar](100) NULL,
	[Sovereignty] [nvarchar](100) NULL,
	[Capital] [nvarchar](100) NULL,
	[CurrencyCode] [nvarchar](100) NULL,
	[CurrencyName] [nvarchar](100) NULL,
	[TelephoneCode] [nvarchar](100) NULL,
	[CountryCode3] [nvarchar](100) NULL,
	[CountryNumber] [nvarchar](100) NULL,
	[InternetCountryCode] [nvarchar](100) NULL,
	[SortOrder] [int] NULL,
	[IsPublished] [bit] NULL,
	[Flags] [nvarchar](50) NULL,
	[IsDeleted] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[District]    Script Date: 6/17/2017 2:33:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[District](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NULL,
	[Type] [nvarchar](50) NULL,
	[LatiLongTude] [nvarchar](50) NULL,
	[ProvinceId] [int] NOT NULL,
	[SortOrder] [int] NULL,
	[IsPublished] [bit] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_District] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Image]    Script Date: 6/17/2017 2:33:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Image](
	[Link] [nvarchar](100) NOT NULL,
	[ProductID] [int] NOT NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Message]    Script Date: 6/17/2017 2:33:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Message](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[SentID] [int] NULL,
	[FromID] [int] NULL,
	[CreatedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Order]    Script Date: 6/17/2017 2:33:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Order](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OrderCode] [varchar](50) NULL,
	[Note] [nvarchar](100) NULL,
	[Address] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NULL,
	[UserID] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedID] [int] NULL,
	[StatusID] [int] NULL,
	[Total] [float] NULL,
	[CountryID] [int] NULL,
	[CityID] [int] NULL,
	[DistrictID] [int] NULL,
	[WardID] [int] NULL,
	[StoreId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 6/17/2017 2:33:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ProductID] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedID] [int] NULL,
	[Quantity] [int] NULL,
	[Price] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OrderStatus]    Script Date: 6/17/2017 2:33:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderStatus](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Partner]    Script Date: 6/17/2017 2:33:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Partner](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StoreID] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedID] [int] NULL,
	[UseID] [int] NULL,
	[isClosed] [bit] NULL,
	[isWorking] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Producer]    Script Date: 6/17/2017 2:33:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Producer](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](1000) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Product]    Script Date: 6/17/2017 2:33:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](2000) NOT NULL,
	[ProductID] [nvarchar](250) NULL,
	[ManufacturedCountryID] [int] NOT NULL,
	[ProducerID] [int] NOT NULL,
	[Weight] [float] NOT NULL,
	[StatusID] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[ProductTypeID] [int] NOT NULL,
	[StoreID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductStatus]    Script Date: 6/17/2017 2:33:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductStatus](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductType]    Script Date: 6/17/2017 2:33:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[CateID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Province]    Script Date: 6/17/2017 2:33:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Province](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Type] [nvarchar](20) NULL,
	[TelephoneCode] [int] NULL,
	[ZipCode] [nvarchar](20) NULL,
	[CountryId] [int] NOT NULL,
	[CountryCode] [nvarchar](2) NULL,
	[SortOrder] [int] NULL,
	[IsPublished] [bit] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Province] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Rating]    Script Date: 6/17/2017 2:33:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rating](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Rate] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Roles]    Script Date: 6/17/2017 2:33:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ShipperDetail]    Script Date: 6/17/2017 2:33:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ShipperDetail](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ShipperId] [int] NULL,
	[RegDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[StatusId] [int] NULL,
	[Deposit] [float] NULL,
	[Total] [float] NULL,
	[RecentBalance] [float] NULL,
	[BankAccount] [varchar](50) NULL,
	[LatX] [float] NULL,
	[LatY] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ShipperRating]    Script Date: 6/17/2017 2:33:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShipperRating](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[ShipperID] [int] NULL,
	[Rate] [int] NULL,
	[Comment] [nvarchar](250) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ShipperStatus]    Script Date: 6/17/2017 2:33:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShipperStatus](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ShippingDetail]    Script Date: 6/17/2017 2:33:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShippingDetail](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedID] [int] NULL,
	[ShipperID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StoreRating]    Script Date: 6/17/2017 2:33:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StoreRating](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[StoreID] [int] NULL,
	[Rate] [int] NULL,
	[Comment] [nvarchar](250) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Stores]    Script Date: 6/17/2017 2:33:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Stores](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[UserID] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[ModifiedByID] [int] NOT NULL,
	[Description] [nvarchar](1000) NOT NULL,
	[ImgLink] [nvarchar](100) NULL,
	[StatusID] [int] NULL,
	[BankAccount] [varchar](50) NULL,
	[TaxCode] [varchar](50) NULL,
	[Address] [nvarchar](100) NULL,
	[WardId] [int] NULL,
	[DistrictId] [int] NULL,
	[CountryId] [int] NULL,
	[CityId] [int] NULL,
	[LatX] [float] NULL,
	[LatY] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[StoreStatus]    Script Date: 6/17/2017 2:33:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StoreStatus](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Target]    Script Date: 6/17/2017 2:33:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Target](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductTypeId] [int] NULL,
	[Count] [int] NULL,
	[UserId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 6/17/2017 2:33:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](100) NOT NULL,
	[PassWord] [varchar](255) NULL,
	[FirstName] [nvarchar](100) NOT NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[DOB] [date] NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[RegDate] [datetime] NOT NULL,
	[StatusID] [int] NOT NULL,
	[Phone] [nvarchar](100) NULL,
	[Sex] [bit] NOT NULL,
	[CityID] [int] NOT NULL,
	[DistrictID] [int] NOT NULL,
	[Address] [nvarchar](255) NOT NULL,
	[ImgLink] [nvarchar](255) NULL,
	[RoleID] [int] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[CountryID] [int] NOT NULL,
	[isSocialLogin] [bit] NOT NULL,
	[WardID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserStatus]    Script Date: 6/17/2017 2:33:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserStatus](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Ward]    Script Date: 6/17/2017 2:33:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ward](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Type] [nvarchar](50) NULL,
	[LatiLongTude] [nvarchar](50) NULL,
	[DistrictID] [int] NOT NULL,
	[SortOrder] [int] NOT NULL,
	[IsPublished] [bit] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Ward] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Wishlist]    Script Date: 6/17/2017 2:33:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Wishlist](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductDetailID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
 CONSTRAINT [PK__Wishlish__3214EC276DE0949F] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Comment] ADD  DEFAULT (NULL) FOR [ParentCmt]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((1)) FOR [isSocialLogin]
GO
ALTER TABLE [dbo].[Ward] ADD  CONSTRAINT [DF_Ward_SortOrder]  DEFAULT ((1)) FOR [SortOrder]
GO
ALTER TABLE [dbo].[Ward] ADD  CONSTRAINT [DF_Ward_IsPublished]  DEFAULT ((1)) FOR [IsPublished]
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_ProductDetail] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ID])
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_ProductDetail]
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_Users]
GO
ALTER TABLE [dbo].[District]  WITH CHECK ADD  CONSTRAINT [FK_District_Province] FOREIGN KEY([ProvinceId])
REFERENCES [dbo].[Province] ([Id])
GO
ALTER TABLE [dbo].[District] CHECK CONSTRAINT [FK_District_Province]
GO
ALTER TABLE [dbo].[Image]  WITH CHECK ADD  CONSTRAINT [FK_Image_ProductDetail] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ID])
GO
ALTER TABLE [dbo].[Image] CHECK CONSTRAINT [FK_Image_ProductDetail]
GO
ALTER TABLE [dbo].[Message]  WITH CHECK ADD FOREIGN KEY([FromID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Message]  WITH CHECK ADD FOREIGN KEY([SentID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD FOREIGN KEY([CityID])
REFERENCES [dbo].[Province] ([Id])
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD FOREIGN KEY([CountryID])
REFERENCES [dbo].[Country] ([Id])
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD FOREIGN KEY([DistrictID])
REFERENCES [dbo].[District] ([Id])
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD FOREIGN KEY([StoreId])
REFERENCES [dbo].[Stores] ([ID])
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD FOREIGN KEY([WardID])
REFERENCES [dbo].[Ward] ([Id])
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_OrderStatus] FOREIGN KEY([StatusID])
REFERENCES [dbo].[OrderStatus] ([ID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_OrderStatus]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Users]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Order] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Order] ([ID])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_Order]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_ProductDetail] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ID])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_ProductDetail]
GO
ALTER TABLE [dbo].[Partner]  WITH CHECK ADD  CONSTRAINT [FK_Partner_Stores] FOREIGN KEY([StoreID])
REFERENCES [dbo].[Stores] ([ID])
GO
ALTER TABLE [dbo].[Partner] CHECK CONSTRAINT [FK_Partner_Stores]
GO
ALTER TABLE [dbo].[Partner]  WITH CHECK ADD  CONSTRAINT [FK_Partner_Users] FOREIGN KEY([UseID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Partner] CHECK CONSTRAINT [FK_Partner_Users]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_ProductDetail_Country] FOREIGN KEY([ManufacturedCountryID])
REFERENCES [dbo].[Country] ([Id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_ProductDetail_Country]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_ProductDetail_Producer] FOREIGN KEY([ProducerID])
REFERENCES [dbo].[Producer] ([ID])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_ProductDetail_Producer]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_ProductDetail_Product Type] FOREIGN KEY([ProductTypeID])
REFERENCES [dbo].[ProductType] ([ID])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_ProductDetail_Product Type]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_ProductDetail_ProductStatus] FOREIGN KEY([StatusID])
REFERENCES [dbo].[ProductStatus] ([ID])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_ProductDetail_ProductStatus]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_ProductDetail_Stores] FOREIGN KEY([StoreID])
REFERENCES [dbo].[Stores] ([ID])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_ProductDetail_Stores]
GO
ALTER TABLE [dbo].[Province]  WITH CHECK ADD  CONSTRAINT [FK_Province_Country] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Country] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Province] CHECK CONSTRAINT [FK_Province_Country]
GO
ALTER TABLE [dbo].[Rating]  WITH CHECK ADD  CONSTRAINT [FK_Rating_ProductDetail] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ID])
GO
ALTER TABLE [dbo].[Rating] CHECK CONSTRAINT [FK_Rating_ProductDetail]
GO
ALTER TABLE [dbo].[Rating]  WITH CHECK ADD  CONSTRAINT [FK_Rating_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Rating] CHECK CONSTRAINT [FK_Rating_Users]
GO
ALTER TABLE [dbo].[ShipperDetail]  WITH CHECK ADD  CONSTRAINT [FK_ShipperDetail_ShippStatus] FOREIGN KEY([StatusId])
REFERENCES [dbo].[ShipperStatus] ([ID])
GO
ALTER TABLE [dbo].[ShipperDetail] CHECK CONSTRAINT [FK_ShipperDetail_ShippStatus]
GO
ALTER TABLE [dbo].[ShipperDetail]  WITH CHECK ADD  CONSTRAINT [FK_ShipperDetail_Users] FOREIGN KEY([ShipperId])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[ShipperDetail] CHECK CONSTRAINT [FK_ShipperDetail_Users]
GO
ALTER TABLE [dbo].[ShipperRating]  WITH CHECK ADD  CONSTRAINT [FK_ShipperRating_ShipperDetail] FOREIGN KEY([ShipperID])
REFERENCES [dbo].[ShipperDetail] ([ID])
GO
ALTER TABLE [dbo].[ShipperRating] CHECK CONSTRAINT [FK_ShipperRating_ShipperDetail]
GO
ALTER TABLE [dbo].[ShipperRating]  WITH CHECK ADD  CONSTRAINT [FK_ShipperRating_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[ShipperRating] CHECK CONSTRAINT [FK_ShipperRating_Users]
GO
ALTER TABLE [dbo].[ShippingDetail]  WITH CHECK ADD  CONSTRAINT [FK_ShippingDetail_Order] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Order] ([ID])
GO
ALTER TABLE [dbo].[ShippingDetail] CHECK CONSTRAINT [FK_ShippingDetail_Order]
GO
ALTER TABLE [dbo].[ShippingDetail]  WITH CHECK ADD  CONSTRAINT [FK_ShippingDetail_ShipperDetail] FOREIGN KEY([ShipperID])
REFERENCES [dbo].[ShipperDetail] ([ID])
GO
ALTER TABLE [dbo].[ShippingDetail] CHECK CONSTRAINT [FK_ShippingDetail_ShipperDetail]
GO
ALTER TABLE [dbo].[StoreRating]  WITH CHECK ADD  CONSTRAINT [FK_StroreRating_Stores] FOREIGN KEY([StoreID])
REFERENCES [dbo].[Stores] ([ID])
GO
ALTER TABLE [dbo].[StoreRating] CHECK CONSTRAINT [FK_StroreRating_Stores]
GO
ALTER TABLE [dbo].[StoreRating]  WITH CHECK ADD  CONSTRAINT [FK_StroreRating_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[StoreRating] CHECK CONSTRAINT [FK_StroreRating_Users]
GO
ALTER TABLE [dbo].[Stores]  WITH CHECK ADD FOREIGN KEY([CityId])
REFERENCES [dbo].[Province] ([Id])
GO
ALTER TABLE [dbo].[Stores]  WITH CHECK ADD FOREIGN KEY([CountryId])
REFERENCES [dbo].[Country] ([Id])
GO
ALTER TABLE [dbo].[Stores]  WITH CHECK ADD FOREIGN KEY([DistrictId])
REFERENCES [dbo].[District] ([Id])
GO
ALTER TABLE [dbo].[Stores]  WITH CHECK ADD FOREIGN KEY([WardId])
REFERENCES [dbo].[Ward] ([Id])
GO
ALTER TABLE [dbo].[Stores]  WITH CHECK ADD  CONSTRAINT [FK_Stores_Store Status] FOREIGN KEY([StatusID])
REFERENCES [dbo].[StoreStatus] ([ID])
GO
ALTER TABLE [dbo].[Stores] CHECK CONSTRAINT [FK_Stores_Store Status]
GO
ALTER TABLE [dbo].[Stores]  WITH CHECK ADD  CONSTRAINT [FK_Stores_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Stores] CHECK CONSTRAINT [FK_Stores_Users]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD FOREIGN KEY([WardID])
REFERENCES [dbo].[Ward] ([Id])
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Country] FOREIGN KEY([CountryID])
REFERENCES [dbo].[Country] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Country]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_District] FOREIGN KEY([DistrictID])
REFERENCES [dbo].[District] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_District]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Province] FOREIGN KEY([CityID])
REFERENCES [dbo].[Province] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Province]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Roles] ([ID])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_User Status] FOREIGN KEY([StatusID])
REFERENCES [dbo].[UserStatus] ([ID])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_User Status]
GO
ALTER TABLE [dbo].[Ward]  WITH CHECK ADD  CONSTRAINT [FK_Ward_District] FOREIGN KEY([DistrictID])
REFERENCES [dbo].[District] ([Id])
GO
ALTER TABLE [dbo].[Ward] CHECK CONSTRAINT [FK_Ward_District]
GO
ALTER TABLE [dbo].[Wishlist]  WITH CHECK ADD  CONSTRAINT [FK_UserWishlist] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Wishlist] CHECK CONSTRAINT [FK_UserWishlist]
GO
ALTER TABLE [dbo].[Wishlist]  WITH CHECK ADD  CONSTRAINT [FK_Wish list_Product] FOREIGN KEY([ProductDetailID])
REFERENCES [dbo].[Product] ([ID])
GO
ALTER TABLE [dbo].[Wishlist] CHECK CONSTRAINT [FK_Wish list_Product]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Kinh độ, vĩ độ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'District', @level2type=N'COLUMN',@level2name=N'LatiLongTude'
GO
