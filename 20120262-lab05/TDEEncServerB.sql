/*----------------------------------------------------------
MASV: 20120262
HO TEN: KHÚC KHÁNH ĐĂNG
LAB: 05
NGAY: 28/04/2023
----------------------------------------------------------*/
--CAC CAU LENH DE THUC HIEN MA HOA
USE MASTER;
go
drop certificate TDESERVERBCert
drop master key


USE MASTER;
GO
CREATE MASTER KEY ENCRYPTION 
BY PASSWORD = '20120262LAB05';  
GO


CREATE CERTIFICATE TDESERVERBCert
  FROM FILE = N'D:\LAB05_Certificate_Backup.cer'
  WITH PRIVATE KEY ( 
    FILE = N'D:\LAB05_PK_Backup.pvk',
 DECRYPTION BY PASSWORD = '123abc'
  );
GO
