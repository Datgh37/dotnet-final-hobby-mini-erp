/****** Object:  Database [HobbyMiniERP] ******/
CREATE DATABASE [HobbyMiniERP]
GO

USE [HobbyMiniERP]
GO

/****** 1. Phân hệ Phân quyền & Người dùng ******/
CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Name] [nvarchar](100) NOT NULL UNIQUE,
	[CreatedAt] [datetimeoffset](7) NOT NULL DEFAULT (sysutcdatetime()),
	[UpdatedAt] [datetimeoffset](7) NULL,
	[IsDeleted] [bit] NOT NULL DEFAULT (0)
)
GO

CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[UserName] [nvarchar](100) NOT NULL UNIQUE,
	[Email] [nvarchar](256) NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[FullName] [nvarchar](200) NULL,
	[IsActive] [bit] NOT NULL DEFAULT (1),
	[CreatedAt] [datetimeoffset](7) NOT NULL DEFAULT (sysutcdatetime()),
	[UpdatedAt] [datetimeoffset](7) NULL,
	[IsDeleted] [bit] NOT NULL DEFAULT (0)
)
GO

CREATE TABLE [dbo].[UserRoles](
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
	PRIMARY KEY ([UserId], [RoleId]),
	CONSTRAINT [FK_UserRoles_User] FOREIGN KEY([UserId]) REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE,
	CONSTRAINT [FK_UserRoles_Role] FOREIGN KEY([RoleId]) REFERENCES [dbo].[Roles] ([Id]) ON DELETE CASCADE
)
GO

/****** 2. Danh mục & Đối tác ******/
CREATE TABLE [dbo].[Brands](
	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Name] [nvarchar](200) NOT NULL UNIQUE,
	[Country] [nvarchar](100) NULL,
	[LogoUrl] [nvarchar](500) NULL,
	[CreatedAt] [datetimeoffset](7) NOT NULL DEFAULT (sysutcdatetime()),
	[UpdatedAt] [datetimeoffset](7) NULL,
	[IsDeleted] [bit] NOT NULL DEFAULT (0)
)
GO

CREATE TABLE [dbo].[ProductCategories](
	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Name] [nvarchar](200) NOT NULL,
	[ParentCategoryId] [int] NULL,
	[CreatedAt] [datetimeoffset](7) NOT NULL DEFAULT (sysutcdatetime()),
	[UpdatedAt] [datetimeoffset](7) NULL,
	[IsDeleted] [bit] NOT NULL DEFAULT (0),
	CONSTRAINT [FK_PC_Parent] FOREIGN KEY([ParentCategoryId]) REFERENCES [dbo].[ProductCategories] ([Id])
)
GO

CREATE TABLE [dbo].[Suppliers](
	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Name] [nvarchar](200) NOT NULL,
	[ContactPerson] [nvarchar](200) NULL,
	[Phone] [nvarchar](50) NULL,
	[Email] [nvarchar](256) NULL,
	[Address] [nvarchar](500) NULL,
	[IsActive] [bit] NOT NULL DEFAULT (1),
	[CreatedAt] [datetimeoffset](7) NOT NULL DEFAULT (sysutcdatetime()),
	[UpdatedAt] [datetimeoffset](7) NULL,
	[IsDeleted] [bit] NOT NULL DEFAULT (0)
)
GO

/****** 3. Sản phẩm (Giản lược: Gộp Variant và Inventory vào đây) ******/
CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[CategoryId] [int] NULL,
	[BrandId] [int] NULL,
	[SKU] [nvarchar](100) NOT NULL UNIQUE,
	[Name] [nvarchar](300) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[SeriesName] [nvarchar](300) NULL, -- Thay bảng Series bằng cột text
	[Scale] [nvarchar](50) NULL,
	[Grade] [nvarchar](50) NULL,
	[Weight] [decimal](10, 2) NULL,
	[CostPrice] [decimal](18, 2) NOT NULL DEFAULT (0),
	[RetailPrice] [decimal](18, 2) NOT NULL DEFAULT (0),
	[StockQuantity] [int] NOT NULL DEFAULT (0), -- Quản lý tồn kho trực tiếp tại đây (1 kho)
	[ImageUrl] [nvarchar](500) NULL,
	[IsActive] [bit] NOT NULL DEFAULT (1),
	[CreatedAt] [datetimeoffset](7) NOT NULL DEFAULT (sysutcdatetime()),
	[UpdatedAt] [datetimeoffset](7) NULL,
	[IsDeleted] [bit] NOT NULL DEFAULT (0),
	CONSTRAINT [FK_Product_Category] FOREIGN KEY([CategoryId]) REFERENCES [dbo].[ProductCategories] ([Id]),
	CONSTRAINT [FK_Product_Brand] FOREIGN KEY([BrandId]) REFERENCES [dbo].[Brands] ([Id])
)
GO

/****** 4. Khách hàng (Gộp địa chỉ vào đây) ******/
CREATE TABLE [dbo].[Customers](
	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[UserId] [int] NULL, -- Liên kết nếu khách hàng có tài khoản
	[Name] [nvarchar](200) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[Phone] [nvarchar](50) NULL,
	[Address] [nvarchar](500) NULL,
	[City] [nvarchar](100) NULL,
	[CreatedAt] [datetimeoffset](7) NOT NULL DEFAULT (sysutcdatetime()),
	[UpdatedAt] [datetimeoffset](7) NULL,
	[IsDeleted] [bit] NOT NULL DEFAULT (0),
	CONSTRAINT [FK_Cust_User] FOREIGN KEY([UserId]) REFERENCES [dbo].[Users] ([Id])
)
GO

/****** 5. Phân hệ Mua hàng (Purchase Orders) ******/
CREATE TABLE [dbo].[PurchaseOrders](
	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[PONumber] [nvarchar](50) NOT NULL UNIQUE,
	[SupplierId] [int] NULL,
	[OrderDate] [date] NULL,
	[ExpectedDate] [date] NULL,
	[ReceivedDate] [date] NULL,
	[Status] [nvarchar](50) NULL, -- PENDING, RECEIVED, CANCELLED
	[TotalAmount] [decimal](18, 2) DEFAULT (0),
	[Notes] [nvarchar](max) NULL,
	[CreatedBy] [int] NULL,
	[CreatedAt] [datetimeoffset](7) NOT NULL DEFAULT (sysutcdatetime()),
	[UpdatedAt] [datetimeoffset](7) NULL,
	[IsDeleted] [bit] NOT NULL DEFAULT (0),
	CONSTRAINT [FK_PO_Supplier] FOREIGN KEY([SupplierId]) REFERENCES [dbo].[Suppliers] ([Id]),
	CONSTRAINT [FK_PO_User] FOREIGN KEY([CreatedBy]) REFERENCES [dbo].[Users] ([Id])
)
GO

CREATE TABLE [dbo].[PurchaseOrderItems](
	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[PurchaseOrderId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[UnitPrice] [decimal](18, 2) NOT NULL,
	CONSTRAINT [FK_POI_Order] FOREIGN KEY([PurchaseOrderId]) REFERENCES [dbo].[PurchaseOrders] ([Id]) ON DELETE CASCADE,
	CONSTRAINT [FK_POI_Product] FOREIGN KEY([ProductId]) REFERENCES [dbo].[Products] ([Id])
)
GO

/****** 6. Phân hệ Bán hàng (Sales Orders) ******/
CREATE TABLE [dbo].[SalesOrders](
	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[OrderNumber] [nvarchar](50) NOT NULL UNIQUE,
	[CustomerId] [int] NULL,
	[OrderDate] [date] NULL,
	[Status] [nvarchar](50) NULL, -- NEW, SHIPPING, COMPLETED, CANCELLED
	[PaymentMethod] [nvarchar](50) NULL,
	[PaymentStatus] [nvarchar](50) NULL,
	[TotalAmount] [decimal](18, 2) DEFAULT (0),
	[ShippingAddress] [nvarchar](500) NULL,
	[Notes] [nvarchar](max) NULL,
	[CreatedBy] [int] NULL,
	[CreatedAt] [datetimeoffset](7) NOT NULL DEFAULT (sysutcdatetime()),
	[UpdatedAt] [datetimeoffset](7) NULL,
	[IsDeleted] [bit] NOT NULL DEFAULT (0),
	CONSTRAINT [FK_SO_Customer] FOREIGN KEY([CustomerId]) REFERENCES [dbo].[Customers] ([Id]),
	CONSTRAINT [FK_SO_User] FOREIGN KEY([CreatedBy]) REFERENCES [dbo].[Users] ([Id])
)
GO

CREATE TABLE [dbo].[SalesOrderItems](
	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[SalesOrderId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[UnitPrice] [decimal](18, 2) NOT NULL,
	[Discount] [decimal](18, 2) DEFAULT (0),
	CONSTRAINT [FK_SOI_Order] FOREIGN KEY([SalesOrderId]) REFERENCES [dbo].[SalesOrders] ([Id]) ON DELETE CASCADE,
	CONSTRAINT [FK_SOI_Product] FOREIGN KEY([ProductId]) REFERENCES [dbo].[Products] ([Id])
)
GO

/****** 7. Nhật ký kho (Stock Movements) ******/
CREATE TABLE [dbo].[StockMovements](
	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[ProductId] [int] NOT NULL,
	[MovementType] [nvarchar](50) NOT NULL, -- IN, OUT, ADJUSTMENT, RETURN
	[Quantity] [int] NOT NULL,
	[Reference] [nvarchar](200) NULL, -- Mã PO hoặc SO tương ứng
	[CreatedBy] [int] NULL,
	[CreatedAt] [datetimeoffset](7) NOT NULL DEFAULT (sysutcdatetime()),
	[UpdatedAt] [datetimeoffset](7) NULL,
	[IsDeleted] [bit] NOT NULL DEFAULT (0),
	CONSTRAINT [FK_SM_Product] FOREIGN KEY([ProductId]) REFERENCES [dbo].[Products] ([Id]),
	CONSTRAINT [FK_SM_User] FOREIGN KEY([CreatedBy]) REFERENCES [dbo].[Users] ([Id])
)
GO
