# Lab 3 - Bài 6: Chat Room TCP

## Chức năng
- Một Server nhận nhiều Client TCP.
- Người dùng nhập tên, không cần đăng ký/đăng nhập.
- Chat nhóm: tin nhắn được broadcast đến các Client đang kết nối.
- Chat riêng: chọn người dùng trong danh sách trực tuyến.
- Server cập nhật danh sách người dùng.

## Chạy
1. Mở `Lab3_Bai06.sln` bằng Visual Studio 2022.
2. Build project.
3. Chạy ứng dụng, chọn Mở Server và bấm Start Server.
4. Mở thêm nhiều instance ứng dụng để giả lập nhiều Client.
5. Mỗi Client nhập tên khác nhau, IP `127.0.0.1`, bấm Connect.
6. Chọn `Tất cả (Public)` để chat nhóm hoặc chọn tên người nhận để nhắn riêng.

## Cổng
Dùng TCP port 9000. Nếu đổi, sửa ở `FormServer.cs` (ChatServer(9000,...)) và `FormChat.cs` (ConnectAsync(...,9000)).

## Giới hạn
Bản này hoàn thành các chức năng chính. Phần mở rộng gửi JPG/PNG/TXT chưa được triển khai.
