# Website đọc sách trực tuyến – Nhà sách Tiến Thọ

## 📌 Mô tả
Dự án đồ án tốt nghiệp xây dựng website đọc sách trực tuyến cho Nhà sách Tiến Thọ.  
Hệ thống cho phép người dùng đọc sách online và hỗ trợ quản trị nội dung sách thông qua trang quản trị.

Dự án được xây dựng nhằm mục tiêu áp dụng kiến thức về ASP.NET Core, Web API, Entity Framework và kiến trúc phân lớp trong thực tế.

---

## 🏗 Kiến trúc hệ thống
Dự án được tổ chức theo mô hình phân lớp:

- **TienThoBookStore.Domain**  
  Chứa các entity và logic nghiệp vụ cốt lõi

- **TienThoBookStore.Application**  
  Chứa các DTO, service và xử lý nghiệp vụ

- **TienThoBookStore.Infrastructure**  
  Làm việc với cơ sở dữ liệu, Entity Framework Core

- **TienThoBookStore.WebAPI**  
  Cung cấp các RESTful API cho hệ thống

- **TienThoBookStore.WebApp**  
  Ứng dụng web ASP.NET Core MVC tiêu thụ Web API

---

## 🛠 Công nghệ sử dụng
- ASP.NET Core MVC
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- LINQ
- Git / GitHub

---

## ⚙ Chức năng chính

### Người dùng
- Đăng ký, đăng nhập
- Xem danh sách sách
- Đọc sách trực tuyến
- Thêm sách vào giỏ hàng

### Quản trị
- Quản lý sách
- Quản lý danh mục
- Quản lý người dùng
- Quản lý doanh thu

---

## 🚀 Hướng dẫn chạy project

### Yêu cầu
- Đã cài đặt .NET SDK
- Đã cài đặt SQL Server

### Các bước chạy
1. Clone source code:
2. Mở file solution TienThoBookStore.sln bằng Visual Studio
2. Cấu hình connection string trong appsettings.json
3. Chạy project:
 - Set TienThoBookStore.WebAPI làm Startup Project
 - Sau đó chạy TienThoBookStore.WebApp
