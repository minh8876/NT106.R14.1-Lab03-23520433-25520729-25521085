# Bài 3 - 1 Server, 1 Client (TCP) - C#

## Nội dung
Ứng dụng Windows Forms gồm:
- FormMenu: mở Form Server hoặc Form Client.
- FormServer: dùng `TcpListener` lắng nghe cổng 8080, chấp nhận Client và nhận thông điệp.
- FormClient: dùng `TcpClient` kết nối, gửi thông điệp UTF-8 và ngắt kết nối.

## Yêu cầu
- Windows
- Visual Studio 2022 (hoặc IDE hỗ trợ .NET 8)
- Workload: .NET desktop development

## Cách mở và chạy
1. Mở `Bai03_TCP.csproj` bằng Visual Studio.
2. Chọn cấu hình Debug và nhấn F5.
3. Trong màn hình Menu, nhấn **Mở Server**.
4. Trong Form Server, nhấn **Listen**.
5. Quay lại Menu, nhấn **Mở Client**.
6. Để IP là `127.0.0.1`, Port là `8080`, nhấn **Connect**.
7. Nhập nội dung tiếng Việt và nhấn **Send**.
8. Kiểm tra Form Server để thấy nội dung nhận được.
9. Nhấn **Disconnect** ở Client. Server vẫn tiếp tục chờ Client tiếp theo.

## Lưu ý
- Đây là bản TCP một Client tại một thời điểm, phù hợp phạm vi Bài 3.
- Server dùng `IPAddress.Any` để lắng nghe trên các giao diện IPv4 của máy.
- Client và Server chạy cùng máy thì dùng `127.0.0.1`.
- Nếu cổng 8080 đang được chương trình khác sử dụng, hãy đóng chương trình đó trước khi chạy.
- Nếu muốn thử qua mạng LAN, nhập địa chỉ IPv4 nội bộ của máy chạy Server và cho phép ứng dụng qua Windows Firewall khi được yêu cầu.
- Không gửi chuỗi `quit` như nội dung chat thông thường vì đây là từ khóa kết thúc phiên trong ví dụ này.

## Các kiến thức thể hiện
`TcpListener`, `TcpClient`, `NetworkStream`, `StreamReader`, `StreamWriter`, `Thread`, UTF-8 và cập nhật giao diện an toàn bằng `InvokeRequired` / `BeginInvoke`.
