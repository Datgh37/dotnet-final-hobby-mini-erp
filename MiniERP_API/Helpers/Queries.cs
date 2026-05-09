namespace MiniERP_API.Helpers
{
    public static class Queries
    {
        #region Product Queries
        public const string GetAllProducts = "SELECT * FROM Products WHERE IsActive = 1 AND IsDeleted = 0";
        public const string GetProductById = "SELECT * FROM Products WHERE Id = @Id";
        public const string InsertProduct = @"
            INSERT INTO Products (CategoryId, BrandId, SKU, Name, Description, SeriesName, Scale, Grade, Weight, CostPrice, RetailPrice, StockQuantity, ImageUrl, IsActive, CreatedAt, IsDeleted)
            VALUES (@CategoryId, @BrandId, @SKU, @Name, @Description, @SeriesName, @Scale, @Grade, @Weight, @CostPrice, @RetailPrice, @StockQuantity, @ImageUrl, 1, SYSDATETIMEOFFSET(), 0);
            SELECT CAST(SCOPE_IDENTITY() as int);";
        public const string UpdateProduct = @"
            UPDATE Products SET 
                CategoryId = @CategoryId, BrandId = @BrandId, SKU = @SKU, Name = @Name, 
                Description = @Description, SeriesName = @SeriesName, Scale = @Scale, 
                Grade = @Grade, Weight = @Weight, CostPrice = @CostPrice, 
                RetailPrice = @RetailPrice, StockQuantity = @StockQuantity, 
                ImageUrl = @ImageUrl, UpdatedAt = SYSDATETIMEOFFSET()
            WHERE Id = @Id";
        public const string DeleteProduct = "UPDATE Products SET IsDeleted = 1 WHERE Id = @Id";
        #endregion

        #region Supplier Queries
        public const string GetAllSuppliers = "SELECT * FROM Suppliers WHERE IsActive = 1 AND IsDeleted = 0";
        public const string GetSupplierById = "SELECT * FROM Suppliers WHERE Id = @Id";
        public const string InsertSupplier = @"
            INSERT INTO Suppliers (Name, ContactPerson, Phone, Email, Address, IsActive, CreatedAt, IsDeleted)
            VALUES (@Name, @ContactPerson, @Phone, @Email, @Address, 1, SYSDATETIMEOFFSET(), 0);
            SELECT CAST(SCOPE_IDENTITY() as int);";
        public const string UpdateSupplier = @"
            UPDATE Suppliers SET 
                Name = @Name, ContactPerson = @ContactPerson, 
                Phone = @Phone, Email = @Email, Address = @Address,
                UpdatedAt = SYSDATETIMEOFFSET()
            WHERE Id = @Id";
        public const string DeleteSupplier = "UPDATE Suppliers SET IsDeleted = 1 WHERE Id = @Id";
        #endregion

        #region Sales Order Queries
        public const string InsertSalesOrder = @"
            INSERT INTO SalesOrders (OrderNumber, CustomerId, OrderDate, Status, PaymentMethod, PaymentStatus, TotalAmount, ShippingAddress, Notes, CreatedBy, CreatedAt)
            VALUES (@OrderNumber, @CustomerId, @OrderDate, @Status, @PaymentMethod, @PaymentStatus, @TotalAmount, @ShippingAddress, @Notes, @CreatedBy, SYSDATETIMEOFFSET());
            SELECT CAST(SCOPE_IDENTITY() as int);";

        public const string InsertSalesOrderItem = @"
            INSERT INTO SalesOrderItems (SalesOrderId, ProductId, Quantity, UnitPrice, Discount)
            VALUES (@SalesOrderId, @ProductId, @Quantity, @UnitPrice, @Discount);";

        public const string UpdateProductStock = @"
            UPDATE Products SET StockQuantity = StockQuantity - @Quantity 
            WHERE Id = @ProductId AND StockQuantity >= @Quantity;";
            
        public const string GetSalesOrderById = "SELECT * FROM SalesOrders WHERE Id = @Id";
        public const string GetSalesOrderItemsByOrderId = "SELECT * FROM SalesOrderItems WHERE SalesOrderId = @OrderId";
        #endregion
    }
}
