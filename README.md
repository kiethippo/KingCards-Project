📌 Tóm tắt nội dung chính
🔹 Tổng quan về trò chơi
Giới thiệu: Trò chơi sử dụng bộ bài Tây 52 lá, chơi theo luật "Tiến lên miền Nam".

Luật chơi: Bao gồm các kiểu bài hợp lệ (đôi, sảnh, tứ quý, đôi thông...), cách đè bài, chặt heo, và các tình huống đặc biệt như tới trắng, cháy bài, thúi, phạt...

🔹 Triển khai đồ án
Tính năng chính của hệ thống:

Tạo phòng chơi tối đa 4 người.

Tham gia/thoát phòng chơi.

Đánh bài/Skip lượt.

Xử lý các trường hợp kết thúc ván chơi, bắt đầu ván mới.

So sánh hợp lệ giữa bài người chơi và bài trên bàn.

Mô hình phân rã chức năng:

Bao gồm sơ đồ chức năng tổng quát và chi tiết, giúp minh họa luồng hoạt động của hệ thống.

Cấu trúc gói tin (packet):

Client → Server: Các lệnh như CONNECT, START, DISCARD, SKIP... kèm thông tin bài.

Server → Client: Các thông điệp như INIT, SETUP, UPDATE, TURN... dùng để chia bài, thông báo lượt, cập nhật bàn chơi.

Giao diện:

Có ba giao diện: màn hình vào game, lobby và bàn chơi chính.

Tổng quan lập trình:

Mỗi lá bài được định danh bằng string (VD: "2H" cho heo cơ).

Server quản lý và broadcast lượt đi, Client nhận và kiểm tra có phải lượt mình không.

Mọi hành động người chơi đều được gửi kèm header, Server phân tích và broadcast lại cho các client khác.

🔹 Phân công công việc
Mỗi thành viên chịu trách nhiệm về giao diện, logic, báo cáo, và luật chơi với tỉ lệ công việc rõ ràng.

✅ Ưu điểm
Triển khai đầy đủ luật chơi phức tạp của Tiến Lên Miền Nam.

Xử lý mạng socket giữa các client/server khá chi tiết.

Có tài liệu thiết kế (mô hình phân rã, cấu trúc gói tin).

Phân công nhóm rõ ràng và công bằng.
Nhóm 3 thực hiện đồ án gồm các thành viên:


Huỳnh Ngọc Anh Kiệt	22520718
Huỳnh Thanh Long	22520812
Bùi Ngọc Khánh Linh	22520754
