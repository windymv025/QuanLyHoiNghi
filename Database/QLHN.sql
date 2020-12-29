/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2016                    */
/* Created on:     12/24/2020 10:06:21 PM                       */
/*==============================================================*/
go
create database QuanLiHoiNghi
go
use QuanLiHoiNghi
go
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
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('HOINGHI') and o.name = 'FK_HOINGHI_DUOC_TO_C_DIADIEMT')
alter table HOINGHI
   drop constraint FK_HOINGHI_DUOC_TO_C_DIADIEMT
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
            from  sysindexes
           where  id    = object_id('HOINGHI')
            and   name  = 'DUOC_TO_CHUC_FK'
            and   indid > 0
            and   indid < 255)
   drop index HOINGHI.DUOC_TO_CHUC_FK
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
   LOAIADMIN            binary(1)            not null,
   constraint PK_CHITIETADMIN primary key nonclustered (IDHN, IDUSER)
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
/* Table: DANGKITHAMGIA                                         */
/*==============================================================*/
create table DANGKITHAMGIA (
   IDHN                 int                  not null,
   IDUSER               int                  not null,
   TRANGTHAI            int                  not null,
   THOIGIANDK           datetime             not null,
   constraint PK_DANGKITHAMGIA primary key nonclustered (IDHN, IDUSER)
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
   DIACHI               varchar(50)          not null,
   SUCCHUA              int                  not null,
   constraint PK_DIADIEMTOCHUC primary key (IDDD)
)
go

/*==============================================================*/
/* Table: HOINGHI                                               */
/*==============================================================*/
create table HOINGHI (
   IDHN                 int                  not null,
   IDDD                 int                  not null,
   TENHN                varchar(50)          not null,
   MOTANGANHN           text                 not null,
   MOTACHITIETHN        text                 not null,
   HINHANH              varchar(50)          not null,
   THOIGIANBATDAU       datetime             not null,
   THOIGIANKETTHUC      datetime             not null,
   SOLUONG              int                  not null,
   constraint PK_HOINGHI primary key (IDHN)
)
go

/*==============================================================*/
/* Index: DUOC_TO_CHUC_FK                                       */
/*==============================================================*/




create nonclustered index DUOC_TO_CHUC_FK on HOINGHI (IDDD ASC)
go

/*==============================================================*/
/* Table: "USER"                                                */
/*==============================================================*/
create table "USER" (
   IDUSER               int                  not null,
   TENUSER              varchar(50)          not null,
   PASSWORD             varchar(256)         not null,
   USERNAME             varchar(50)          not null,
   EMAIL                varchar(50)          not null,
   HINHANH              varchar(50)          not null,
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

alter table DANGKITHAMGIA
   add constraint FK_DANGKITH_BAO_GOM_USER foreign key (IDUSER)
      references "USER" (IDUSER)
go

alter table DANGKITHAMGIA
   add constraint FK_DANGKITH_DUOC_DANG_HOINGHI foreign key (IDHN)
      references HOINGHI (IDHN)
go

alter table HOINGHI
   add constraint FK_HOINGHI_DUOC_TO_C_DIADIEMT foreign key (IDDD)
      references DIADIEMTOCHUC (IDDD)
go

