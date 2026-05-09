# 📘 Thiết kế API — Hệ thống Quản lý Bán hàng (Mini ERP)

> **Database:** `MiniERP_Generic` | **Ngày cập nhật:** 2026-05-09  
> **Tổng số bảng:** 13 | **Tổng số API:** 49

---

## 1. Module: Xác thực (Auth)
> **Bảng liên quan:** `Users`, `Roles`, `UserRoles`

| STT | Method   | Endpoint                  | Mô tả                                                                 | Module |
|:---:|:---------|:--------------------------|:-----------------------------------------------------------------------|:-------|
| 1   | **POST** | `/api/auth/login`         | Đăng nhập hệ thống, xác thực bằng UserName & Password, trả về JWT Token | Auth   |
| 2   | **POST** | `/api/auth/register`      | Đăng ký tài khoản mới, mặc định gán Role "Customer"                    | Auth   |
| 3   | **POST** | `/api/auth/refresh-token` | Làm mới Access Token khi Token cũ hết hạn                              | Auth   |

---

## 2. Module: Quản lý Người dùng (Users)
> **Bảng liên quan:** `Users`, `UserRoles`

| STT | Method     | Endpoint                       | Mô tả                                                                     | Module |
|:---:|:-----------|:-------------------------------|:---------------------------------------------------------------------------|:-------|
| 4   | **GET**    | `/api/users`                   | Lấy danh sách tất cả người dùng (chỉ Admin), lọc `IsDeleted = 0`          | Users  |
| 5   | **GET**    | `/api/users/{id}`              | Lấy thông tin chi tiết một người dùng kèm danh sách Role                   | Users  |
| 6   | **PUT**    | `/api/users/{id}`              | Cập nhật thông tin cá nhân (FullName, Email), tự động ghi `UpdatedAt`      | Users  |
| 7   | **DELETE** | `/api/users/{id}`              | Xóa mềm người dùng (`IsDeleted = 1`), không cho phép đăng nhập lại        | Users  |
| 8   | **PATCH**  | `/api/users/{id}/password`     | Đổi mật khẩu, yêu cầu gửi kèm mật khẩu cũ để xác minh                    | Users  |

---

## 3. Module: Quản lý Vai trò (Roles)
> **Bảng liên quan:** `Roles`

| STT | Method     | Endpoint                       | Mô tả                                                                     | Module |
|:---:|:-----------|:-------------------------------|:---------------------------------------------------------------------------|:-------|
| 9   | **GET**    | `/api/roles`                   | Lấy danh sách tất cả các vai trò (Admin, Staff, Customer...)               | Roles  |
| 10  | **POST**   | `/api/roles`                   | Tạo một vai trò mới trong hệ thống                                         | Roles  |

---

## 4. Module: Thương hiệu (Brands)
> **Bảng liên quan:** `Brands`

| STT | Method     | Endpoint              | Mô tả                                                              | Module |
|:---:|:-----------|:----------------------|:--------------------------------------------------------------------|:-------|
| 11  | **GET**    | `/api/brands`         | Lấy danh sách tất cả thương hiệu đang hoạt động (`IsDeleted = 0`)  | Brands |
| 12  | **GET**    | `/api/brands/{id}`    | Lấy thông tin chi tiết một thương hiệu                              | Brands |
| 13  | **POST**   | `/api/brands`         | Thêm thương hiệu mới (Name, Description)                            | Brands |
| 14  | **PUT**    | `/api/brands/{id}`    | Cập nhật thông tin thương hiệu                                      | Brands |
| 15  | **DELETE** | `/api/brands/{id}`    | Xóa mềm thương hiệu                                                | Brands |

---

## 5. Module: Danh mục Sản phẩm (Categories)
> **Bảng liên quan:** `ProductCategories`

| STT | Method     | Endpoint                 | Mô tả                                                                       | Module     |
|:---:|:-----------|:-------------------------|:-----------------------------------------------------------------------------|:-----------|
| 16  | **GET**    | `/api/categories`        | Lấy danh sách danh mục, hỗ trợ cấu trúc phân cấp Cha-Con (ParentCategoryId) | Categories |
| 17  | **GET**    | `/api/categories/{id}`   | Lấy chi tiết một danh mục kèm danh sách danh mục con (nếu có)               | Categories |
| 18  | **POST**   | `/api/categories`        | Thêm danh mục mới, có thể gán ParentCategoryId để tạo phân cấp              | Categories |
| 19  | **PUT**    | `/api/categories/{id}`   | Cập nhật tên hoặc thay đổi danh mục cha                                     | Categories |
| 20  | **DELETE** | `/api/categories/{id}`   | Xóa mềm danh mục                                                            | Categories |

---

## 6. Module: Sản phẩm (Products)
> **Bảng liên quan:** `Products`, `ProductCategories`, `Brands`

| STT | Method     | Endpoint              | Mô tả                                                                                | Module   |
|:---:|:-----------|:----------------------|:--------------------------------------------------------------------------------------|:---------|
| 21  | **GET**    | `/api/products`       | Lấy danh sách sản phẩm, hỗ trợ lọc theo CategoryId, BrandId, tìm kiếm theo tên/SKU  | Products |
| 22  | **GET**    | `/api/products/{id}`  | Lấy chi tiết sản phẩm bao gồm thông tin giá vốn, giá bán, tồn kho                   | Products |
| 23  | **POST**   | `/api/products`       | Thêm sản phẩm mới với đầy đủ thông tin (SKU, Name, Unit, CostPrice, RetailPrice...)  | Products |
| 24  | **PUT**    | `/api/products/{id}`  | Cập nhật thông tin sản phẩm, tự động ghi nhận thời điểm sửa vào `UpdatedAt`          | Products |
| 25  | **DELETE** | `/api/products/{id}`  | Xóa mềm sản phẩm, sản phẩm sẽ không xuất hiện trong các danh sách và đơn hàng mới   | Products |

---

## 7. Module: Nhà cung cấp (Suppliers)
> **Bảng liên quan:** `Suppliers`

| STT | Method     | Endpoint               | Mô tả                                                                    | Module    |
|:---:|:-----------|:-----------------------|:--------------------------------------------------------------------------|:----------|
| 26  | **GET**    | `/api/suppliers`       | Lấy danh sách nhà cung cấp, phục vụ cho phân hệ Mua hàng (PO)           | Suppliers |
| 27  | **GET**    | `/api/suppliers/{id}`  | Lấy chi tiết một nhà cung cấp (thông tin liên hệ, địa chỉ)              | Suppliers |
| 28  | **POST**   | `/api/suppliers`       | Thêm nhà cung cấp mới                                                    | Suppliers |
| 29  | **PUT**    | `/api/suppliers/{id}`  | Cập nhật thông tin nhà cung cấp                                          | Suppliers |
| 30  | **DELETE** | `/api/suppliers/{id}`  | Xóa mềm nhà cung cấp                                                    | Suppliers |

---

## 8. Module: Khách hàng (Customers)
> **Bảng liên quan:** `Customers`, `Users`

| STT | Method     | Endpoint                | Mô tả                                                                         | Module    |
|:---:|:-----------|:------------------------|:-------------------------------------------------------------------------------|:----------|
| 31  | **GET**    | `/api/customers`        | Lấy danh sách khách hàng, hỗ trợ tìm kiếm theo tên, SĐT, email               | Customers |
| 32  | **GET**    | `/api/customers/{id}`   | Lấy chi tiết khách hàng kèm thông tin tài khoản liên kết (nếu có UserId)      | Customers |
| 33  | **POST**   | `/api/customers`        | Thêm khách hàng mới (có thể tạo nhanh khi khách mua lần đầu tại quầy)        | Customers |
| 34  | **PUT**    | `/api/customers/{id}`   | Cập nhật thông tin khách hàng (địa chỉ, SĐT, email)                           | Customers |
| 35  | **DELETE** | `/api/customers/{id}`   | Xóa mềm khách hàng                                                            | Customers |

---

## 9. Module: Mua hàng — Nhập kho (Purchase Orders)
> **Bảng liên quan:** `PurchaseOrders`, `PurchaseOrderItems`, `Products`, `Suppliers`

| STT | Method    | Endpoint                                  | Mô tả                                                                                                         | Module          |
|:---:|:----------|:------------------------------------------|:---------------------------------------------------------------------------------------------------------------|:----------------|
| 36  | **GET**   | `/api/purchase-orders`                    | Lấy danh sách đơn mua hàng, hỗ trợ lọc theo Status (PENDING, RECEIVED, CANCELLED)                            | PurchaseOrders  |
| 37  | **GET**   | `/api/purchase-orders/{id}`               | Lấy chi tiết đơn mua hàng kèm danh sách PurchaseOrderItems (sản phẩm, số lượng, đơn giá)                     | PurchaseOrders  |
| 38  | **POST**  | `/api/purchase-orders`                    | Tạo đơn mua hàng mới từ nhà cung cấp, trạng thái mặc định là PENDING                                         | PurchaseOrders  |
| 39  | **PATCH** | `/api/purchase-orders/{id}/receive`       | Xác nhận nhập kho: chuyển Status → RECEIVED, ghi ReceivedDate, **cộng tồn kho** và **ghi log StockMovements** | PurchaseOrders  |
| 40  | **PATCH** | `/api/purchase-orders/{id}/cancel`        | Hủy đơn mua hàng (chỉ được hủy khi Status = PENDING)                                                         | PurchaseOrders  |

---

## 10. Module: Bán hàng (Sales Orders)
> **Bảng liên quan:** `SalesOrders`, `SalesOrderItems`, `Products`, `Customers`

| STT | Method    | Endpoint                             | Mô tả                                                                                                                   | Module      |
|:---:|:----------|:-------------------------------------|:-------------------------------------------------------------------------------------------------------------------------|:------------|
| 41  | **GET**   | `/api/sales-orders`                  | Lấy danh sách đơn bán hàng, hỗ trợ lọc theo Status, CustomerId, khoảng thời gian                                       | SalesOrders |
| 42  | **GET**   | `/api/sales-orders/{id}`             | Lấy chi tiết đơn bán hàng kèm danh sách SalesOrderItems (sản phẩm, số lượng, đơn giá, chiết khấu)                      | SalesOrders |
| 43  | **POST**  | `/api/sales-orders`                  | Tạo đơn bán hàng mới: tự động **trừ tồn kho**, ghi log xuất kho vào StockMovements, tính TotalAmount                    | SalesOrders |
| 44  | **PATCH** | `/api/sales-orders/{id}/status`      | Cập nhật trạng thái đơn hàng (SHIPPING → COMPLETED hoặc CANCELLED). Nếu hủy đơn → **hoàn lại tồn kho**                  | SalesOrders |

---

## 11. Module: Kho hàng & Báo cáo (Inventory & Reports)
> **Bảng liên quan:** `StockMovements`, `Products`, `SalesOrders`, `SalesOrderItems`, `PurchaseOrders`

| STT | Method   | Endpoint                              | Mô tả                                                                                          | Module    |
|:---:|:---------|:--------------------------------------|:------------------------------------------------------------------------------------------------|:----------|
| 45  | **GET**  | `/api/inventory/movements`            | Xem nhật ký biến động kho (nhập/xuất/điều chỉnh), hỗ trợ lọc theo ProductId, MovementType      | Inventory |
| 46  | **POST** | `/api/inventory/adjust`               | Điều chỉnh tồn kho thủ công khi kiểm kê thực tế bị lệch, ghi log MovementType = ADJUSTMENT    | Inventory |
| 47  | **GET**  | `/api/reports/low-stock`              | Danh sách sản phẩm có StockQuantity dưới ngưỡng cảnh báo (query param: `threshold`)             | Reports   |
| 48  | **GET**  | `/api/reports/revenue`                | Báo cáo doanh thu tổng hợp từ SalesOrders theo khoảng thời gian (query param: `from`, `to`)    | Reports   |
| 49  | **GET**  | `/api/reports/top-selling`            | Danh sách sản phẩm bán chạy nhất, tổng hợp từ SalesOrderItems (query param: `top`, `from`)     | Reports   |

---

## 📋 Tổng hợp theo Module

| Module          | GET | POST | PUT | PATCH | DELETE | Tổng |
|:----------------|:---:|:----:|:---:|:-----:|:------:|:----:|
| Auth            |  0  |  3   |  0  |   0   |   0    | **3**  |
| Users           |  2  |  0   |  1  |   1   |   1    | **5**  |
| Roles           |  1  |  1   |  0  |   0   |   0    | **2**  |
| Brands          |  2  |  1   |  1  |   0   |   1    | **5**  |
| Categories      |  2  |  1   |  1  |   0   |   1    | **5**  |
| Products        |  2  |  1   |  1  |   0   |   1    | **5**  |
| Suppliers       |  2  |  1   |  1  |   0   |   1    | **5**  |
| Customers       |  2  |  1   |  1  |   0   |   1    | **5**  |
| PurchaseOrders  |  2  |  1   |  0  |   2   |   0    | **5**  |
| SalesOrders     |  2  |  1   |  0  |   1   |   0    | **4**  |
| Inventory       |  1  |  1   |  0  |   0   |   0    | **2**  |
| Reports         |  3  |  0   |  0  |   0   |   0    | **3**  |
| **Tổng cộng**   | **21** | **11** | **5** | **4** | **5** | **49** |

---

## 🛡 Nguyên tắc chung

1. **Soft Delete:** Tất cả `GET` danh sách mặc định lọc `WHERE IsDeleted = 0`.
2. **Audit Trail:** Mọi `POST` ghi `CreatedAt`, mọi `PUT/PATCH` ghi `UpdatedAt`.
3. **Transaction:** Các API tạo đơn hàng (PO/SO) và xác nhận nhập/xuất kho phải dùng `SQL Transaction`.
4. **Response Format:** Tất cả API trả về JSON thống nhất: `{ success, data, message }`.
