# TỔNG HỢP 21 AUTOMATED UNIT TESTS - MINI ERP SYSTEM


| STT | Module | TC ID | Tên Unit Test | Mục đích & Ý nghĩa kiểm thử |
|:---:|:---|:---|:---|:---|
| 1 | **Auth** | UT-AU-01 | `Login_Success` | Kiểm tra đăng nhập thành công và cấp JWT Token hợp lệ. |
| 2 | **Auth** | UT-AU-02 | `Login_WrongPassword` | Đảm bảo hệ thống từ chối truy cập khi sai mật khẩu. |
| 3 | **Auth** | UT-AU-03 | `Login_UserNotFound` | Kiểm tra thông báo lỗi khi tên đăng nhập không tồn tại. |
| 4 | **Products** | UT-PR-01 | `Create_DuplicateSKU` | Ràng buộc mã SKU duy nhất, ngăn chặn trùng lặp hàng hóa. |
| 5 | **Products** | UT-PR-02 | `Create_Success` | Xác nhận luồng thêm mới sản phẩm vào hệ thống. |
| 6 | **Products** | UT-PR-03 | `Delete_SoftDelete` | Xác minh cơ chế xóa mềm (Soft Delete) để bảo toàn dữ liệu. |
| 7 | **Sales Order** | UT-SO-01 | `PlaceOrder_Success` | Kiểm tra logic sinh mã đơn hàng (SO-) và tính tổng tiền. |
| 8 | **Sales Order** | UT-SO-02 | `PlaceOrder_EmptyItems` | Ngăn chặn tạo đơn bán hàng rác (không có sản phẩm). |
| 9 | **Purchase Order**| UT-PO-01 | `Receive_DoubleDip` | Ngăn chặn nhập kho 2 lần cho cùng một đơn mua hàng. |
| 10 | **Purchase Order**| UT-PO-02 | `Create_NegativePrice` | Ngăn chặn nhập đơn giá âm cho hàng hóa nhập kho. |
| 11 | **Purchase Order**| UT-PO-03 | `Create_CalcTotal` | Kiểm tra tính chính xác của tổng giá trị đơn nhập hàng. |
| 12 | **Inventory** | UT-IN-01 | `AdjustStock_ZeroQty` | Ngăn chặn thao tác điều chỉnh kho với số lượng bằng 0. |
| 13 | **Inventory** | UT-IN-02 | `AdjustStock_NoReason` | Bắt buộc phải nhập lý do khi điều chỉnh kho thủ công. |
| 14 | **Inventory** | UT-IN-03 | `AdjustStock_Success` | Xác nhận quy trình điều chỉnh tăng/giảm tồn kho. |
| 15 | **Users** | UT-US-01 | `ChangePass_WrongOld` | Kiểm tra bảo mật: Pass cũ phải đúng mới được đổi pass mới. |
| 16 | **Users** | UT-US-02 | `ChangePass_SameAsOld` | Ngăn chặn việc đổi mật khẩu mới trùng với mật khẩu cũ. |
| 17 | **Brands** | UT-BR-01 | `Create_DuplicateName` | Kiểm tra ràng buộc không trùng tên nhãn hàng. |
| 18 | **Brands** | UT-BR-02 | `Create_EmptyName` | Đảm bảo tên nhãn hàng không được để trống. |
| 19 | **Category** | UT-CA-01 | `Create_DuplicateName` | Kiểm tra ràng buộc không trùng tên danh mục sản phẩm. |
| 20 | **Customer** | UT-CU-01 | `Create_EmptyName` | Đảm bảo thông tin khách hàng phải có tên khi khởi tạo. |
| 21 | **Supplier** | UT-SP-01 | `Create_DuplicateName` | Ngăn chặn việc tạo trùng tên nhà cung cấp trong hệ thống. |

---
**Tổng kết:** Toàn bộ 21 kịch bản trên đã được thực thi và đạt kết quả **100% Passed**.
