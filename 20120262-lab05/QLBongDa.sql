/*----------------------------------------------------------
MASV: 20120262	
HO TEN: KHÚC KHÁNH ĐĂNG 
LAB: 05
NGAY: 28/04/2023
----------------------------------------------------------*/
-- CAC CAU LENH DE THUC HIEN MA HOA

use master

drop certificate TDESERVERACert
drop master key
DROP DATABASE IF EXISTS QLBongDa
CREATE DATABASE QLBongDa
GO
USE QLBongDa
GO

----TAO MASTER KEY
USE MASTER
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<20120262LAB05>';
GO
-- TẠO CERTIFICATE
USE MASTER

CREATE CERTIFICATE TDESERVERACert WITH SUBJECT = 'Certificate to protect TDE key';
GO
----TAO KEY MA HOA DATABASE
USE QLBongDa;
GO
DROP DATABASE ENCRYPTION KEY
GO
CREATE DATABASE ENCRYPTION KEY
WITH ALGORITHM = AES_256
ENCRYPTION BY SERVER CERTIFICATE TDESERVERACert
GO
 --BẬT MÃ HÓA DATABASE
ALTER DATABASE QLBongDa
SET ENCRYPTION ON;
GO

----BACKUP CERTIFICATE

USE MASTER
GO
BACKUP CERTIFICATE TDESERVERACert to file = 'D:\LAB05_Certificate_Backup.cer'
with private key(
file = 'D:\LAB05_PK_Backup.pvk',
ENCRYPTION BY PASSWORD = '123abc')

---TAO TABLE VÀ DỮ LIỆU


USE QLBongDa
GO

--taotable

create table CAUTHU(
	MACT NUMERIC NOT NULL IDENTITY(1,1),
	HOTEN NVARCHAR(100) NOT NULL,
	VITRI NVARCHAR(20) NOT NULL,  
	NGAYSINH DATETIME,
	DIACHI NVARCHAR(200),
	MACLB VARCHAR(5) NOT NULL,  
	MAQG VARCHAR(5) NOT NULL,
	SO INT NOT NULL,

	constraint PK_CAUTHU primary key (MACT)
	
)
go

create table QUOCGIA(
	MAQG VARCHAR(5) NOT NULL,
	TENQG NVARCHAR(60) NOT NULL,

	constraint PK_QUOCGIA primary key (MAQG)
)
go

create table CAULACBO(
	MACLB VARCHAR(5) NOT NULL, 
	TENCLB NVARCHAR(100) NOT NULL, 
	MASAN VARCHAR(5) NOT NULL, 
	MATINH VARCHAR(5) NOT NULL,

	constraint PK_CAULACBO primary key(MACLB)
	
)
go

create table TINH(
	MATINH VARCHAR(5) NOT NULL,
	TENTINH NVARCHAR(100) NOT NULL,

	constraint PK_TINH primary key(MATINH)
)
go

create table SANVD(
	MASAN VARCHAR(5) NOT NULL,
	TENSAN NVARCHAR(100) NOT NULL,
	DIACHI NVARCHAR(200),

	constraint PK_SANVD primary key(MASAN)
)
go

create table HUANLUYENVIEN(
	MAHLV VARCHAR(5) NOT NULL, 
	TENHLV NVARCHAR(100) NOT NULL,
	NGAYSINH DATETIME,
	DIACHI NVARCHAR(200),
	DIENTHOAI NVARCHAR(20),
	MAQG VARCHAR(5) NOT NULL,

	constraint PK_HUANLUYENVIEN primary key(MAHLV)
	
)
go

create table HLV_CLB(
	MAHLV VARCHAR(5) NOT NULL, 
	MACLB VARCHAR(5) NOT NULL,
	VAITRO NVARCHAR(100) NOT NULL,

	constraint PK_HLV_CLB primary key(MAHLV, MACLB)
	
)
go

create table TRANDAU(
	MATRAN NUMERIC NOT NULL IDENTITY(1,1), 
	NAM INT NOT NULL,
	VONG INT NOT NULL, 
	NGAYTD DATETIME NOT NULL,
	MACLB1 VARCHAR(5) NOT NULL, 
	MACLB2 VARCHAR(5) NOT NULL,
	MASAN VARCHAR(5) NOT NULL,
	KETQUA VARCHAR(5) NOT NULL,

	constraint PK_TRANDAU primary key(MATRAN)
)
go

create table BANGXH(
	MACLB VARCHAR (5) NOT NULL,
	NAM INT NOT NULL,
	VONG INT NOT NULL,
	SOTRAN INT NOT NULL,
	THANG INT NOT NULL,
	HOA INT NOT NULL,
	THUA INT NOT NULL,
	HIEUSO VARCHAR (5) NOT NULL,
	DIEM INT NOT NULL,
	HANG INT NOT NULL,

	constraint PK_BANGXH primary key(MACLB, NAM, VONG)
	
)
go
--quanhe

--BANGXH
alter table BANGXH
add constraint FK_BXH_CLB foreign key(MACLB) references CAULACBO(MACLB)

--TRANDAU
alter table TRANDAU
add constraint FK_TD_SVD foreign key(MASAN) references SANVD(MASAN),
constraint FK_TD_CLB1 foreign key(MACLB1) references CAULACBO(MACLB),
constraint FK_TD_CLB2 foreign key(MACLB2) references CAULACBO(MACLB)

--CAUTHU
alter table CAUTHU
add constraint FK_CT_CLB foreign key(MACLB) references CAULACBO(MACLB),
constraint FK_CT_QG foreign key(MAQG) references QUOCGIA(MAQG)

--HLV_CLB
alter table HLV_CLB
add constraint FK_HC_HLV foreign key(MAHLV) references HUANLUYENVIEN(MAHLV),
constraint FK_HC_CLB foreign key(MACLB) references CAULACBO(MACLB)

--HUANLUYENVIEN
alter table HUANLUYENVIEN
add constraint FK_HLV_QG foreign key(MAQG) references QUOCGIA(MAQG)

--CAULACBO
alter table CAULACBO
add constraint FK_CLB_SVD foreign key(MASAN) references SANVD(MASAN),
constraint FK_CLB_T foreign key(MATINH) references TINH(MATINH)

--nhap data
INSERT INTO SANVD
    (MASAN,TENSAN,DIACHI)
VALUES('GD', N'Gò Đậu', N'123 QL1, TX Thủ Dầu Một, Bình Dương'),
    ('PL', N'Pleiku', N'22 Hồ Tùng Mậu, Thống Nhất, Thị xã Pleiku, Gia Lai'),
    ('CL', N'Chi Lăng', N'127 Võ Văn Tần, Đà Nẵng'),
    ('NT', N'Nha Trang', N'128 Phan Chu Trinh, Nha Trang, Khánh Hòa'),
    ('TH', N'Tuy Hòa', N'57 Trường Chinh, Tuy Hòa, Phú Yên'),
    ('LA', N'Long An', N'102 Hùng Vương, Tp Tân An, Long An')
INSERT INTO TINH
    (MATINH,TENTINH)
VALUES
    ('BD', N'Bình Dương'),
    ('GL', N'Giai Lai'),
    ('DN', N'Đà Nẵng'),
    ('KH', N'Khánh Hòa'),
    ('PY', N'Phú Yên'),
    ('LA', N'Long An')

INSERT INTO QUOCGIA
    (MAQG,TENQG)
VALUES
    ('VN', N'Việt Nam'),
    ('ANH', N'Anh Quốc'),
    ('TBN', N'Tây Ban Nha'),
    ('BDN', N'Bồ Đào Nha'),
    ('BRA', N'Brazil'),
    ('ITA', N'Ý'),
    ('THA', N'Thái Lan')
INSERT INTO HUANLUYENVIEN
    (MAHLV,TENHLV,NGAYSINH,DIACHI,DIENTHOAI,MAQG)
VALUES
    ('HLV01', N'Vital', '10/15/1955', NULL, N'0918011075', 'BDN'),
    ('HLV02', N'Lê Huỳnh Đức', '05/20/1972', NULL, N'01223456789', 'VN'),
    ('HLV03', N'Kiatisuk', '12/11/1970', NULL, N'01990123456', 'THA'),
    ('HLV04', N'Hoàng Anh Tuấn', '06/10/1970', NULL, N'0989112233', 'VN'),
    ('HLV05', N'Trần Công Minh', '07/07/1973', NULL, N'0909099990', 'VN'),
    ('HLV06', N'Trần Văn Phúc', '03/02/1965', NULL, N'01650101234', 'VN')


INSERT INTO CAULACBO
    (MACLB,TENCLB,MASAN,MATINH)
VALUES
    ('BBD', N'BECAMEX BÌNH DƯƠNG', 'GD', 'BD'),
    ('HAGL', N'HOÀNG ANH GIA LAI', 'PL', 'GL'),
    ('SDN', N'SHB ĐÀ NẴNG', 'CL', 'DN'),
    ('KKH', N'KHATOCO KHÁNH HÒA', 'NT', 'KH'),
    ('TPY', N'THÉP PHÚ YÊN', 'TH', 'PY'),
    ('GDT', N'GẠCH ĐỒNG TÂM LONG AN', 'LA', 'LA')
INSERT INTO HLV_CLB
    (MAHLV,MACLB,VAITRO)
VALUES
    ('HLV01', 'BBD', N'HLV Chính'),
    ('HLV02', 'SDN', N'HLV Chính'),
    ('HLV03', 'HAGL', N'HLV Chính'),
    ('HLV04', 'KKH', N'HLV Chính'),
    ('HLV05', 'GDT', N'HLV Chính'),
    ('HLV06', 'BBD', N'HLV Thủ môn')
INSERT INTO TRANDAU
    (NAM,VONG,NGAYTD,MACLB1,MACLB2,MASAN,KETQUA)
VALUES
    (2009, 1, '02/07/2009', 'BBD', 'SDN', 'GD', '3-0'),
    (2009, 1, '02/07/2009', 'KKH', 'GDT', 'NT', '1-1'),
    (2009, 2, '02/16/2009', 'SDN', 'KKH', 'CL', '2-2'),
    (2009, 2, '02/16/2009', 'TPY', 'BBD', 'TH', '5-0'),
    (2009, 3, '03/01/2009', 'TPY', 'GDT', 'TH', '0-2'),
    (2009, 3, '03/01/2009', 'KKH', 'BBD', 'NT', '0-1'),
    (2009, 4, '03/07/2009', 'KKH', 'TPY', 'NT', '1-0'),
    (2009, 4, '03/07/2009', 'BBD', 'GDT', 'GD', '2-2')

INSERT INTO BANGXH
    (MACLB,NAM,VONG,SOTRAN,THANG,HOA,THUA,HIEUSO,DIEM,HANG)
VALUES
    ('BBD', 2009, 1, 1, 1, 0, 0, '3-0', 3, 1),
    ('KKH', 2009, 1, 1, 0, 1, 0, '1-1', 1, 2),
    ('GDT', 2009, 1, 1, 0, 1, 0, '1-1', 1, 3),
    ('TPY', 2009, 1, 0, 0, 0, 0, '0-0', 0, 4),
    ('SDN', 2009, 1, 1, 0, 0, 1, '0-3', 0, 5),
    ('TPY', 2009, 2, 1, 1, 0, 0, '5-0', 3, 1),
    ('BBD', 2009, 2, 2, 1, 0, 1, '3-5', 3, 2),
    ('KKH', 2009, 2, 2, 0, 2, 0, '3-3', 2, 3),
    ('GDT', 2009, 2, 1, 0, 1, 0, '1-1', 1, 4),
    ('SDN', 2009, 2, 2, 1, 1, 0, '2-5', 1, 5),
    ('BBD', 2009, 3, 3, 2, 0, 1, '4-5', 6, 1),
    ('GDT', 2009, 3, 2, 1, 1, 0, '3-1', 4, 2),
    ('TPY', 2009, 3, 2, 1, 0, 1, '5-2', 3, 3),
    ('KKH', 2009, 3, 3, 0, 2, 1, '3-4', 2, 4),
    ('SDN', 2009, 3, 2, 1, 1, 0, '2-5', 1, 5),
    ('BBD', 2009, 4, 4, 2, 1, 1, '6-7', 7, 1),
    ('GDT', 2009, 4, 3, 1, 2, 0, '5-1', 5, 2),
    ('KKH', 2009, 4, 4, 1, 2, 1, '4-4', 5, 3),
    ('TPY', 2009, 4, 4, 1, 0, 2, '5-3', 3, 4),
    ('SDN', 2009, 4, 2, 1, 1, 0, '2-5', 1, 5)
INSERT INTO CAUTHU
    (HOTEN, VITRI, NGAYSINH, DIACHI, MACLB, MAQG, SO)
VALUES
    (N'Nguyễn Vũ Phong', N'Tiền vệ', '02/20/1990', NULL, 'BBD', 'VN', 17),
    (N'Nguyễn Công Vinh', N'Tiền đạo', '03/10/1992', NULL, 'HAGL', 'VN', 9),
    (N'Trần Tấn Tài', N'Tiền vệ', '11/12/1989', NULL, 'BBD', 'VN', 8),
    (N'Phan Hồng Sơn', N'Thủ môn', ' 06/10/1991', NULL, 'HAGL', 'VN', 1),
    (N'Ronaldo', N'Tiền vệ', '12/12/1989', NULL, 'SDN', 'BRA', 7),
    (N'Robinho', N'Tiền vệ', '10/12/1989', NULL, 'SDN', 'BRA', 8),
    (N'Vidic', N'Hậu vệ', '10/15/1987', NULL, 'HAGL', 'ANH', 3),
    (N'Trần Văn Santos', N'Thủ môn', '10/21/1990', NULL, 'BBD', 'BRA', 1),
    (N'Nguyễn Trường Sơn', N'Hậu vệ', '08/26/1993', NULL, 'BBD', 'VN', 4)






