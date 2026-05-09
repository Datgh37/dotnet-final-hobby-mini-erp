# Hobby Store Mini ERP API

## 📌 Tổng quan dự án (Overview)
**Hobby Store Mini ERP** là một hệ thống quản lý tài nguyên doanh nghiệp (ERP) phiên bản tinh gọn, được thiết kế dành riêng cho các mô hình kinh doanh đồ chơi, figure, gundam và các sản phẩm sưu tầm (hobby). 

Hệ thống cung cấp các giải pháp quản lý tập trung từ khâu nhập hàng, theo dõi tồn kho, quản lý danh mục sản phẩm đặc thù đến xử lý đơn hàng bán lẻ. Dự án được xây dựng dưới dạng **Web API** sử dụng nền tảng .NET Core, hướng tới sự linh hoạt, dễ dàng tích hợp với các nền tảng Frontend hoặc Mobile App.

---

## 🎯 Mục tiêu dự án (Project Goals)
1. **Quản lý chuyên sâu sản phẩm Hobby:** Hỗ trợ các thuộc tính đặc thù như Scale (tỉ lệ), Grade (phân loại mô hình), SKU, Series...
2. **Tối ưu quy trình vận hành:** Số hóa quy trình từ lúc đặt hàng nhà cung cấp (PO) đến khi xuất kho bán hàng (SO).
3. **Kiểm soát tồn kho thời gian thực:** Theo dõi biến động kho (Stock Movements) để tránh thất thoát và tối ưu hóa vòng quay hàng tồn.
4. **Kiến trúc bền vững:** Xây dựng codebase theo Layered Architecture (Repository/Service Pattern) để dễ dàng bảo trì và mở rộng.

---

## 🛠 Định hướng các Module (Module Direction)

Hệ thống được chia thành 4 phân hệ chính:

### 1. Phân hệ Quản trị & Danh mục (Core & Master Data)
- **User & RBAC:** Quản lý nhân viên và phân quyền truy cập.
- **Product Catalog:** Quản lý thông tin chi tiết sản phẩm, thương hiệu (Brands) và phân loại (Categories).
- **Partner Management:** Quản lý thông tin Nhà cung cấp (Suppliers) và Khách hàng (Customers).

### 2. Phân hệ Mua hàng (Procurement)
- **Purchase Orders (PO):** Tạo và quản lý các đơn đặt hàng từ nhà cung cấp.
- **Receiving:** Xác nhận nhập kho và cập nhật giá vốn (Cost Price).

### 3. Phân hệ Kho hàng (Inventory)
- **Stock Management:** Theo dõi số lượng tồn kho thực tế.
- **Stock Movements:** Ghi lại nhật ký mọi biến động nhập/xuất/điều chỉnh kho.

### 4. Phân hệ Bán hàng (Sales)
- **Sales Orders (SO):** Xử lý đơn hàng từ khách hàng, quản lý trạng thái thanh toán và giao hàng.
- **Revenue Tracking:** Theo dõi doanh thu trên từng đơn hàng.

---

## 🚀 Công nghệ sử dụng (Tech Stack)
- **Backend:** .NET 8.0 (ASP.NET Core Web API)
- **Database:** SQL Server
- **Data Access:** ADO.NET / Dapper (Tối ưu hiệu năng và kiểm soát SQL)
- **Documentation:** NSwag (Swagger UI)
- **Pattern:** Repository & Service Pattern

---

## 📋 Yêu cầu hệ thống
- .NET SDK 8.0+
- SQL Server 2019+
- Visual Studio 2022 hoặc VS Code
