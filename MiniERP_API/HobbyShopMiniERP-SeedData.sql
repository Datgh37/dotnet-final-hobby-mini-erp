USE [HobbyMiniERP]
GO

-- 1. Seed Roles
INSERT INTO [dbo].[Roles] ([Name]) VALUES ('Admin'), ('Staff'), ('Customer');
GO

-- 2. Seed Users (Password: Admin@123 - đây chỉ là ví dụ hash)
INSERT INTO [dbo].[Users] ([UserName], [Email], [PasswordHash], [FullName], [IsActive])
VALUES ('admin', 'admin@hobbystore.com', 'AQAAAAEAACcQAAAAEAD...', 'System Administrator', 1);
GO

-- 3. Seed UserRoles (Admin role cho user admin)
DECLARE @AdminId INT = (SELECT Id FROM Users WHERE UserName = 'admin');
DECLARE @RoleId INT = (SELECT Id FROM Roles WHERE Name = 'Admin');
INSERT INTO [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (@AdminId, @RoleId);
GO

-- 4. Seed Brands
INSERT INTO [dbo].[Brands] ([Name], [Country], [LogoUrl])
VALUES 
('Bandai Namco', 'Japan', 'https://upload.wikimedia.org/wikipedia/commons/b/b1/Bandai_Namco_Holdings_logo.svg'),
('Kotobukiya', 'Japan', 'https://example.com/kotobukiya.png'),
('Good Smile Company', 'Japan', 'https://example.com/gsc.png');
GO

-- 5. Seed Product Categories
INSERT INTO [dbo].[ProductCategories] ([Name], [ParentCategoryId]) VALUES ('Gunpla', NULL);
INSERT INTO [dbo].[ProductCategories] ([Name], [ParentCategoryId]) VALUES ('Action Figures', NULL);
INSERT INTO [dbo].[ProductCategories] ([Name], [ParentCategoryId]) VALUES ('Scale Figures', NULL);
GO

-- 6. Seed Suppliers
INSERT INTO [dbo].[Suppliers] ([Name], [ContactPerson], [Phone], [Email], [Address])
VALUES 
('Global Hobby Distro', 'John Doe', '0123456789', 'contact@globalhobby.com', 'Tokyo, Japan'),
('Asia Figure Wholesale', 'Chen Wei', '0987654321', 'sales@asiafigure.com', 'Hong Kong');
GO

-- 7. Seed Products (Gộp biến thể và tồn kho)
DECLARE @BandaiId INT = (SELECT Id FROM Brands WHERE Name = 'Bandai Namco');
DECLARE @GunplaId INT = (SELECT Id FROM ProductCategories WHERE Name = 'Gunpla');
DECLARE @FigureId INT = (SELECT Id FROM ProductCategories WHERE Name = 'Action Figures');

INSERT INTO [dbo].[Products] 
([CategoryId], [BrandId], [SKU], [Name], [Description], [SeriesName], [Scale], [Grade], [CostPrice], [RetailPrice], [StockQuantity], [ImageUrl])
VALUES 
(@GunplaId, @BandaiId, 'MG-RX78-2', 'MG RX-78-2 Gundam Ver.3.0', 'The classic Gundam in Master Grade detail.', 'Mobile Suit Gundam', '1/100', 'MG', 45.00, 65.00, 20, 'https://example.com/rx78.jpg'),
(@GunplaId, @BandaiId, 'HG-AERIAL', 'HG Gundam Aerial', 'Main suit from The Witch from Mercury.', 'The Witch from Mercury', '1/144', 'HG', 15.00, 25.00, 50, 'https://example.com/aerial.jpg'),
(@FigureId, @BandaiId, 'SHF-GOKU', 'S.H.Figuarts Son Goku - A New Adventure', 'Highly posable Goku figure.', 'Dragon Ball', NULL, 'SHF', 35.00, 55.00, 15, 'https://example.com/goku.jpg');
GO

-- 8. Seed Customers
INSERT INTO [dbo].[Customers] ([Name], [Email], [Phone], [Address], [City])
VALUES 
('Nguyen Van A', 'customerA@gmail.com', '0901234567', '123 Nguyen Trai', 'Ho Chi Minh'),
('Tran Thi B', 'customerB@yahoo.com', '0907654321', '456 Le Loi', 'Da Nang');
GO

-- 9. Seed 1 Sales Order mẫu
DECLARE @CustId INT = (SELECT TOP 1 Id FROM Customers);
DECLARE @ProdId INT = (SELECT TOP 1 Id FROM Products WHERE SKU = 'HG-AERIAL');

-- Thêm Header
INSERT INTO [dbo].[SalesOrders] ([OrderNumber], [CustomerId], [OrderDate], [Status], [PaymentMethod], [PaymentStatus], [TotalAmount], [ShippingAddress])
VALUES ('SO-2026050801', @CustId, GETDATE(), 'COMPLETED', 'COD', 'PAID', 50.00, '123 Nguyen Trai, Ho Chi Minh');

DECLARE @OrderId INT = SCOPE_IDENTITY();

-- Thêm Items cho đơn hàng trên
INSERT INTO [dbo].[SalesOrderItems] ([SalesOrderId], [ProductId], [Quantity], [UnitPrice], [Discount])
VALUES (@OrderId, @ProdId, 2, 25.00, 0);

-- Cập nhật tồn kho thủ công (vì seed không chạy qua API Service)
UPDATE [dbo].[Products] SET StockQuantity = StockQuantity - 2 WHERE Id = @ProdId;
GO
