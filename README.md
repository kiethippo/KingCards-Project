# 🃏 Tiến Lên Miền Nam LAN – Nhóm 3 | NT106.O22

## 📌 Giới thiệu

Đây là dự án môn **Lập trình mạng căn bản**, phát triển trò chơi **Tiến Lên Miền Nam** hoạt động thông qua kết nối **mạng LAN**, giúp nhiều người có thể chơi với nhau từ các máy khác nhau.

Dự án được phát triển bằng **ngôn ngữ C#**, sử dụng **WinForms** cho giao diện và **Socket TCP/IP** cho kết nối mạng.

---

## 🎮 Tính năng nổi bật

- 🎲 Tạo phòng chơi (tối đa 4 người)
- 👥 Tham gia / Thoát phòng chơi
- 🃏 Đánh bài / Bỏ lượt
- ✅ Kiểm tra bài hợp lệ
- 🏆 Xác định người thắng / Tạo ván chơi mới
- ⚡ Xử lý các trường hợp đặc biệt: tới trắng, chặt, thúi, cháy bài...

---

## 🧩 Cách hoạt động (Kỹ thuật)

- Mỗi lá bài được định danh bằng string (ví dụ: `"2H"` cho heo cơ).
- Server gửi và kiểm soát lượt chơi qua `TURN`.
- Client kiểm tra lượt và gửi `DISCARD` hoặc `SKIP`.
- Giao tiếp Client ↔ Server sử dụng các gói tin với:
- `Control Message` (`CONNECT`, `DISCARD`, `TURN`, `END`, ...)
- Tên người chơi, danh sách bài, hành động, v.v.

---

## 🚀 Hướng dẫn sử dụng

1. Mở bằng **Visual Studio**.
2. Chạy file `Server.exe` để tạo phòng.
3. Chạy `Client.exe` trên các máy khác để tham gia (cùng mạng LAN).

> ⚠️ Lưu ý: Các thiết bị cần kết nối cùng một mạng LAN để hoạt động chính xác.

---

## 👥 Thành viên nhóm

| STT | Họ và tên               | MSSV      |
|-----|--------------------------|-----------|
| 1   | Huỳnh Ngọc Anh Kiệt     | 22520718  |
| 2   | Huỳnh Thanh Long        | 22520812  |
| 3   | Bùi Ngọc Khánh Linh     | 22520754  |

---

## 🔗 Demo & liên kết

- 📹 **Demo:** [Link Google Drive](https://drive.google.com/drive/folders/1n5nc2Q4P7gdJPBuCbeY1lmjuMhBgmFq4?usp=sharing)
- 💻 **Source Code:** [GitHub](https://github.com/kiethippo/KingCards-Project.git)

---

