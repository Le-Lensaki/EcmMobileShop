use master
Drop database EcmMobileShop
go
CREATE DATABASE EcmMobileShop
go
use EcmMobileShop
go

CREATE TABLE tb_HANGSP (
IdHangSP int identity(1,1) primary key,
TenHangSP nvarchar(100),
TrangThai bit NOT NULL DEFAULT 1,
)
go
CREATE TABLE tb_LOAISP (
IdLoaiSP int identity(1,1) primary key,
TenLoaiSP nvarchar(100),
TrangThai bit NOT NULL DEFAULT 1,
)
go
CREATE TABLE tb_SANPHAM (
IdSP int identity(1,1) primary key,
IdHangSP int,
IdLoaiSP int,
TenSP nvarchar(200),
MoTaSP nvarchar(max),
Gia int,
ImagePathMain nvarchar(max), 
NgayNhap datetime,
TrangThai bit NOT NULL DEFAULT 1,
constraint fk_tb_SANPHAM_tb_HANGSP foreign key(IdHangSP) references tb_HANGSP(IdHangSP),
constraint fk_tb_SANPHAM_tb_LOAISP foreign key(IdLoaiSP) references tb_LOAISP(IdLoaiSP),
)
go
CREATE TABLE tb_CT_SANPHAM (
IdctSP int identity(1,1) primary key,
IdSP int,
SoLuongSP int,
MauSac nvarchar(50),
ImagePathDetail nvarchar,
constraint fk_tb_CT_SANPHAM_tb_SANPHAM foreign key(IdSP) references tb_SANPHAM(IdSP),
)
go
CREATE TABLE tb_KHACHHANG (
IdKH int identity(1,1) primary key,
TenKH nvarchar(50),
DiaChi nvarchar(100),
SDT nchar(11),
TrangThai bit NOT NULL DEFAULT 1,
)
create table tb_TINHTRANGDH(
IdTinhTrangDH int identity(1,1) primary key,
TenTinhTrang nvarchar(100),
)

go
CREATE TABLE tb_HOADON (
IdHD int identity(1,1) primary key,
IdTinhTrangDH int,
IdKH int,
DiaChiGiao nvarchar(100),
NgayLap datetime,
constraint fk_KH_HD foreign key(IdKH) references tb_KHACHHANG(IdKH),
constraint fk_TT_HD foreign key(IdTinhTrangDH) references tb_TinhTrangDH(IdTinhTrangDH)
)
go
CREATE TABLE tb_CHITIETHOADON (
IdctHD int identity(1,1) primary key,
IdHD int,
IdctSP int,
TenSP nvarchar(200),
SoLuongBan float,
GiaBan float,
constraint fk_HD_CTHD foreign key(IdHD) references tb_HOADON(IdHD),
constraint fk_ctSP_CTHD foreign key(IdctSP) references tb_CT_SANPHAM(IdctSP)
)
go
insert into tb_LOAISP(TenLoaiSP)
values
(N'Tai Nghe'),
(N'Loa'),
(N'Máy ảnh'),
(N'Máy tính và Laptop'),
(N'Điện thoại'),
(N'Máy phát nhạc'),
(N'Chuột máy tính'),
(N'Điều khiển game'),
(N'Pin dự phòng'),
(N'Đồng hồ'),
(N'Tablet')
go
insert into tb_HANGSP(TenHangSP)
values 
('Philips'),
('Huawei'),
('Apple'),
('Sony'),
('Unihertz'),
('Canon'),
('Samsung'),
('Lenovo'),
('ednet GmbH'),
('iRiver'),
('Transcend Information'),
('Xiaomi'),
('Rapoo Technology'),
('Nokia'),
('Gembird Europe BV'),
('Speedlink'),
('Bo')
go
insert into tb_TinhTrangDH(TenTinhTrang) values (N'Đã đặt hàng')
insert into tb_TinhTrangDH(TenTinhTrang) values (N'Đã xác nhận')
insert into tb_TinhTrangDH(TenTinhTrang) values (N'Đang được giao')
insert into tb_TinhTrangDH(TenTinhTrang) values (N'Hoàn thành')
insert into tb_TinhTrangDH(TenTinhTrang) values (N'Đã Huỷ')
go
select * from tb_LOAISP
select * from tb_HANGSP
select * from tb_SANPHAM
go
insert into tb_SANPHAM (TenSP,IdHangSP, IdLoaiSP, Gia, ImagePathMain, MoTaSP)
values 
('Philips BT6900A',1,2, 225, 'https://f8-zpcloud.zdn.vn/1498056500057927627/46b2e8397b81a6dfff90.jpg',N'Loa philips BT6900A dùng để phát nhạc'),
('Huawei MediaPad',2,6, 225, 'https://f9-zpcloud.zdn.vn/9089540525722719989/28406a71c2c91f9746d8.jpg',N'Huawei MediaPad dùng để nghe nhạc, chơi game và nhiều chức năng khác'),
('Apple iPod shuffle',3,6,379,'https://f8-zpcloud.zdn.vn/3905040690467303925/9cabc1cf6b77b629ef66.jpg',N'Máy Apple iPod shuffle dùng để nghe nhạc'),
('Sony MDRZX310W',4,1, 225, 'https://f9-zpcloud.zdn.vn/8850552338859793538/960b82f5294df413ad5c.jpg',N'Tai nghe Sony dùng để nghe nhạc'),
('LUNA Smartphone',5,5, 379, 'https://f8-zpcloud.zdn.vn/6890547260255790327/3e13931e3da6e0f8b9b7.jpg',N'Smartphone Luna dùng để gọi và các chức năng khác'),
('Canon IXUS 175',6,3,379, 'https://f9-zpcloud.zdn.vn/4331014760625145716/091ef71e56a68bf8d2b7.jpg',N'Canon IXUS 175 dùng để chụp hình'),
('Canon STM Kit',6,3,379, 'https://f9-zpcloud.zdn.vn/3516845691762163440/b4c835b890004d5e1411.jpg',N'Canon STM Kit dùng để chụp hình'),
('Samsung J330F',7,5, 225, 'https://f9-zpcloud.zdn.vn/4964687665886756019/b0de9b4e23f6fea8a7e7.jpg',N'Smartphone Samsung J330F dùng để để gọi và các chức năng khác'),
('Lenovo IdeaPad',8,11,225, 'https://f9-zpcloud.zdn.vn/1982473211109659255/dae1e85851e08cbed5f1.jpg',N'Laptop Lenovo thích hợp cho văn phòng'),
('Digitus EDNET',9,9,379, 'https://f8-zpcloud.zdn.vn/4698635953048460420/1bdcf7934c2b9175c83a.jpg',N'Pin dự phòng Ednet có thể chứa 5000mAh'),
('Astro M2 Black',10,6,225, 'https://f8-zpcloud.zdn.vn/3962378003029983750/bbe1c3707fc8a296fbd9.jpg',N'Máy Astro M2 Black dùng để nghe nhạc'),
('Transcend T.Sonic',11,6,225, 'https://f8-zpcloud.zdn.vn/4234069458252406000/ba417885c43d1963402c.jpg',N'Máy Astro M2 Black dùng để nghe nhạc'),
('Xiaomi Band 2',12,10,225, 'https://f9-zpcloud.zdn.vn/4788716100702118287/ed62405efee623b87af7.jpg',N'Đồng hồ Xiaomi dùng để theo dõi sức khỏe'),
('Rapoo T8 White',13,7,379, 'https://f9-zpcloud.zdn.vn/8889816080986219893/a831f0254f9d92c3cb8c.jpg',N'Chuột máy tính Rapoo di chuyển rất mượt'),
('Nokia 3310 (2017)',14,5, 379, 'https://f8-zpcloud.zdn.vn/251579608831398292/ea4e22a893104e4e1701.jpg',N'Nokia 3310 dùng để gọi và các chức năng khác'),
('Rapoo 7100p Gray',13,7, 225, 'https://f8-zpcloud.zdn.vn/4130890314513558525/6f1e8fe53d5de003b94c.jpg',N'Chuột máy tính Rapoo di chuyển rất mượt'),
('Canon EF',6,3,379, 'https://f8-zpcloud.zdn.vn/8054836897786462932/8e2c9e232a9bf7c5ae8a.jpg',N'Canon EF dùng để chụp hình'),
('Gembird SPK-103',15,2,225, 'https://f8-zpcloud.zdn.vn/357883875769593643/8a01236a96d24b8c12c3.jpg',N'Loa Gembird dùng để phát nhạc'),
('Sony PS4 Slim',4,8,300,'https://f9-zpcloud.zdn.vn/607005283512847213/c0af45b31387cfd99696.jpg',N'Máy PS4 dùng để giải trí'),
('Speedlink',16,8,375, 'https://f10-zpcloud.zdn.vn/6483442226326085064/b04ae57e8d4a5114085b.jpg',N'Điều khiển Speedlink dùng để giải trí'),
('Beoplay H7',17,1,300, 'https://f10-zpcloud.zdn.vn/7096629597012267425/fa44feed94d9488711c8.jpg',N'Tai nghe Beoplay dùng để nghe nhạc')
go
insert into tb_CT_SANPHAM(IdSP,SoLuongSP,MauSac)
values 
(1,100,N'Lục'),
(2,100,N'Trắng'),
(3,100,N'Vàng'),
(4,100,N'Trắng'),
(5,100,N'Xám'),
(6,100,N'Xám'),
(7,100,N'Xám'),
(8,100,N'Trắng'),
(9,100,N'Trắng'),
(10,100,N'Vàng'),
(11,100,N'Đen'),
(12,100,N'Đen'),
(13,100,N'Đen'),
(14,100,N'Trắng'),
(15,100,N'Đen'),
(16,100,N'Đen'),
(17,100,N'Trắng'),
(18,100,N'Đen'),
(19,100,N'Đen'),
(20,100,N'Đen'),
(21,100,N'Vàng')
go
