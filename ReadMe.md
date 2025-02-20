## Controller
- Là 1 lớp kế thừa từ lớp Controller
- Chứa đầy đủ thông tin của Request gửi đến : 
    - HttpContext, HttpRequest, ...
    - User, ModelState, ViewData, ...
- Các dịch vụ inject qua hàm khởi tạo

## Action trong Controller
- Có thể trả về bất kì kiểu dữ liệu gì(hoặc 0 trả về gì) và sẽ được convert thành chuỗi để trả về cho client
- Thường khai bao trả về IActionResult

## View
- Là file .cshtml
- View cho action lưu tại : /View/ControllerName/ActionName.cshtml
- Thêm thư mục lưu trữ View : 
{0} -> Tên Action
{1} -> Teên Controller
{2} -> Tên Area

## Truyền dữ liệu sang view
- Model
- ViewData 
- ViewBag
- TempData