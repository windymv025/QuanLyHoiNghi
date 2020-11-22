/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2016                    */
/* Created on:     11/8/2020 3:35:55 PM                         */
/*==============================================================*/
USE MASTER
GO
CREATE DATABASE DBQuanLyHoiNghi
GO
USE DBQuanLyHoiNghi
GO

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CHITIETADMIN') and o.name = 'FK_CHITIETA_CO_HOINGHI')
alter table CHITIETADMIN
   drop constraint FK_CHITIETA_CO_HOINGHI
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CHITIETADMIN') and o.name = 'FK_CHITIETA_GOM_CO_USER')
alter table CHITIETADMIN
   drop constraint FK_CHITIETA_GOM_CO_USER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CHITIETHOINGHI') and o.name = 'FK_CHITIETH_TO_CHUC_T_DIADIEMT')
alter table CHITIETHOINGHI
   drop constraint FK_CHITIETH_TO_CHUC_T_DIADIEMT
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CHITIETHOINGHI') and o.name = 'FK_CHITIETH_TRONG2_HOINGHI')
alter table CHITIETHOINGHI
   drop constraint FK_CHITIETH_TRONG2_HOINGHI
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('DANGKITHAMGIA') and o.name = 'FK_DANGKITH_BAO_GOM_USER')
alter table DANGKITHAMGIA
   drop constraint FK_DANGKITH_BAO_GOM_USER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('DANGKITHAMGIA') and o.name = 'FK_DANGKITH_DUOC_DANG_HOINGHI')
alter table DANGKITHAMGIA
   drop constraint FK_DANGKITH_DUOC_DANG_HOINGHI
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CHITIETADMIN')
            and   name  = 'GOM_CO_FK'
            and   indid > 0
            and   indid < 255)
   drop index CHITIETADMIN.GOM_CO_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CHITIETADMIN')
            and   name  = 'CO_FK'
            and   indid > 0
            and   indid < 255)
   drop index CHITIETADMIN.CO_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CHITIETADMIN')
            and   type = 'U')
   drop table CHITIETADMIN
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CHITIETHOINGHI')
            and   name  = 'TO_CHUC_TAI_FK'
            and   indid > 0
            and   indid < 255)
   drop index CHITIETHOINGHI.TO_CHUC_TAI_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CHITIETHOINGHI')
            and   name  = 'TRONG2_FK'
            and   indid > 0
            and   indid < 255)
   drop index CHITIETHOINGHI.TRONG2_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CHITIETHOINGHI')
            and   type = 'U')
   drop table CHITIETHOINGHI
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('DANGKITHAMGIA')
            and   name  = 'BAO_GOM_FK'
            and   indid > 0
            and   indid < 255)
   drop index DANGKITHAMGIA.BAO_GOM_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('DANGKITHAMGIA')
            and   name  = 'DUOC_DANG_KI_FK'
            and   indid > 0
            and   indid < 255)
   drop index DANGKITHAMGIA.DUOC_DANG_KI_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('DANGKITHAMGIA')
            and   type = 'U')
   drop table DANGKITHAMGIA
go

if exists (select 1
            from  sysobjects
           where  id = object_id('DIADIEMTOCHUC')
            and   type = 'U')
   drop table DIADIEMTOCHUC
go

if exists (select 1
            from  sysobjects
           where  id = object_id('HOINGHI')
            and   type = 'U')
   drop table HOINGHI
go

if exists (select 1
            from  sysobjects
           where  id = object_id('"USER"')
            and   type = 'U')
   drop table "USER"
go

/*==============================================================*/
/* Table: CHITIETADMIN                                          */
/*==============================================================*/
create table CHITIETADMIN (
   IDHN                 int                  not null,
   IDUSER               int                  not null,
   LOAIADMIN            binary(1)            not null
)
go

/*==============================================================*/
/* Index: CO_FK                                                 */
/*==============================================================*/




create nonclustered index CO_FK on CHITIETADMIN (IDHN ASC)
go

/*==============================================================*/
/* Index: GOM_CO_FK                                             */
/*==============================================================*/




create nonclustered index GOM_CO_FK on CHITIETADMIN (IDUSER ASC)
go

/*==============================================================*/
/* Table: CHITIETHOINGHI                                        */
/*==============================================================*/
create table CHITIETHOINGHI (
   IDHN                 int                  not null,
   IDDD                 int                  not null,
   THOI_GIAN_BATDAU     datetime             not null,
   THOIGIANKETTHUC      datetime             not null
)
go

/*==============================================================*/
/* Index: TRONG2_FK                                             */
/*==============================================================*/




create nonclustered index TRONG2_FK on CHITIETHOINGHI (IDHN ASC)
go

/*==============================================================*/
/* Index: TO_CHUC_TAI_FK                                        */
/*==============================================================*/




create nonclustered index TO_CHUC_TAI_FK on CHITIETHOINGHI (IDDD ASC)
go

/*==============================================================*/
/* Table: DANGKITHAMGIA                                         */
/*==============================================================*/
create table DANGKITHAMGIA (
   IDUSER               int                  not null,
   IDHN                 int                  not null,
   TRANGTHAI            char(1)                  not null,
   THOIGIANDK           datetime             not null
)
go

/*==============================================================*/
/* Index: DUOC_DANG_KI_FK                                       */
/*==============================================================*/




create nonclustered index DUOC_DANG_KI_FK on DANGKITHAMGIA (IDHN ASC)
go

/*==============================================================*/
/* Index: BAO_GOM_FK                                            */
/*==============================================================*/




create nonclustered index BAO_GOM_FK on DANGKITHAMGIA (IDUSER ASC)
go

/*==============================================================*/
/* Table: DIADIEMTOCHUC                                         */
/*==============================================================*/
create table DIADIEMTOCHUC (
   IDDD                 int                  not null,
   TENDD                varchar(50)          not null,
   DIADIEM              varchar(100)         not null,
   SUCCHUA              int                  not null,
   constraint PK_DIADIEMTOCHUC primary key (IDDD)
)
go

/*==============================================================*/
/* Table: HOINGHI                                               */
/*==============================================================*/
create table HOINGHI (
   IDHN                 int                  not null,
   TENHN                varchar(50)          not null,
   MOTANGANHN           text                 not null,
   MOTACHITIETHN        text                 not null,
   HINHANH              image                not null,
   THOIGIAN             datetime             not null,
   SOLUONG              int                  not null,
   constraint PK_HOINGHI primary key (IDHN)
)
go

/*==============================================================*/
/* Table: "USER"                                                */
/*==============================================================*/
create table "USER" (
   IDUSER               int                  not null,
   TENUSER              varchar(50)          not null,
   PASSWORD             varchar(50)          not null,
   USERNAME             varchar(50)          not null,
   EMAIL                varchar(50)          not null,
   LOAIUSER             binary(1)            not null,
   constraint PK_USER primary key (IDUSER)
)
go

alter table CHITIETADMIN
   add constraint FK_CHITIETA_CO_HOINGHI foreign key (IDHN)
      references HOINGHI (IDHN)
go

alter table CHITIETADMIN
   add constraint FK_CHITIETA_GOM_CO_USER foreign key (IDUSER)
      references "USER" (IDUSER)
go

alter table CHITIETHOINGHI
   add constraint FK_CHITIETH_TO_CHUC_T_DIADIEMT foreign key (IDDD)
      references DIADIEMTOCHUC (IDDD)
go

alter table CHITIETHOINGHI
   add constraint FK_CHITIETH_TRONG2_HOINGHI foreign key (IDHN)
      references HOINGHI (IDHN)
go

alter table DANGKITHAMGIA
   add constraint FK_DANGKITH_BAO_GOM_USER foreign key (IDUSER)
      references "USER" (IDUSER)
go

alter table DANGKITHAMGIA
   add constraint FK_DANGKITH_DUOC_DANG_HOINGHI foreign key (IDHN)
      references HOINGHI (IDHN)
go


ALTER TABLE dbo.CHITIETADMIN
ADD CONSTRAINT CHK_CTADMIN CHECK (LOAIADMIN = '0' OR LOAIADMIN = '1') --- 0 admin được phân quyền, 1 admin tạo ra hội nghị

ALTER TABLE dbo.[USER]
ADD CONSTRAINT CHK_USER CHECK (LOAIUSER= '0' OR LOAIUSER = '1') --- 0 user thường, 1 user là admin

ALTER TABLE dbo.DANGKITHAMGIA
ADD CONSTRAINT CHK_DKTG CHECK (TRANGTHAI= '0' OR TRANGTHAI = '1' ) --- 0 chờ xác nhận, 1 đã tham gia hội nghị

GO
---Khi thêm user tham gia hội nghị thì cập nhật số lượng người tham gia trong bảng HoiNghi với điều kiện phải nhỏ hơn sức chứa của địa điểm tổ chức hội nghị

CREATE TRIGGER InsertUser
ON dbo.DANGKITHAMGIA
AFTER INSERT
AS
BEGIN
	DECLARE @SucChua INT;
	DECLARE @sltd INT;
	DECLARE @tt INT;
	SELECT @SucChua = dd.SUCCHUA FROM dbo.DIADIEMTOCHUC dd JOIN dbo.CHITIETHOINGHI ct ON dd.IDDD = ct.IDDD JOIN dbo.HOINGHI hn ON ct.IDHN = hn.IDHN;
	SELECT @sltd = SOLUONG FROM dbo.HOINGHI;
	SELECT @tt = Inserted.TRANGTHAI FROM Inserted;
	IF @sltd>=@SucChua
	BEGIN
		ROLLBACK TRAN;
		RAISERROR(N'Hội nghị đã đủ chỗ!!Cám ơn bạn!!!!!',16,1);
	END
	ELSE
	---Sau khi được accept từ admin thi mới update số lượng người tham gia hội nghị, ngược lại vẫn được thêm vào nhưng ở trạng thái chờ xác nhận
		IF @tt = 1
		BEGIN
			UPDATE dbo.HOINGHI SET SOLUONG = hn.SOLUONG + 1 FROM Inserted ins, dbo.HOINGHI hn
			WHERE ins.IDHN = hn.IDHN 
		END
END
