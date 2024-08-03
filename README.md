# Match3
## **Đánh giá dự án**
#### Ưu điểm:
- Code sạch, đọc dễ hiểu, dễ tối ưu, dễ mở rộng.
#### Nhược điểm:
- Cần tạo nhiều Objects khi vào màn chơi, Nếu kích thước bảng là 10x10 thì cần tạo 100 cell khi vào --> Load lâu
- Chưa có lớp quản lý data game, Ví dụ như dữ liệu các theme Items, điểm cao nhất, audio clip,...

#### Khác
- Nên dùng State Pattern hay FSM cho dễ mở rộng, thay vì action bị giới giạn
- Có thể dùng Factory Pattern để tạo Items

#### Video quay màn hình quá trình làm Part 2
https://drive.google.com/file/d/1Sr3t0qREAhmLwTQWmly8fW6Evxfl9-wj/view?usp=drive_link
