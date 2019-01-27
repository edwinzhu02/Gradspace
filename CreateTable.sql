if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Cart') and o.name = 'FK_CART_REFERENCE_CONTACT')
alter table Cart
   drop constraint FK_CART_REFERENCE_CONTACT
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CartProd') and o.name = 'FK_CARTPROD_REFERENCE_CART')
alter table CartProd
   drop constraint FK_CARTPROD_REFERENCE_CART
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CartProd') and o.name = 'FK_CARTPROD_REFERENCE_PRODUCT')
alter table CartProd
   drop constraint FK_CARTPROD_REFERENCE_PRODUCT
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Product') and o.name = 'FK_PRODUCT_REFERENCE_PRODUCTM')
alter table Product
   drop constraint FK_PRODUCT_REFERENCE_PRODUCTM
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Product') and o.name = 'FK_PRODUCT_REFERENCE_PROJECTM')
alter table Product
   drop constraint FK_PRODUCT_REFERENCE_PROJECTM
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Product') and o.name = 'FK_PRODUCT_REFERENCE_PRODUCTT')
alter table Product
   drop constraint FK_PRODUCT_REFERENCE_PRODUCTT
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Product') and o.name = 'FK_PRODUCT_REFERENCE_PRODUCTC')
alter table Product
   drop constraint FK_PRODUCT_REFERENCE_PRODUCTC
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ProductMedia') and o.name = 'FK_PRODUCTM_REFERENCE_PRODUCT')
alter table ProductMedia
   drop constraint FK_PRODUCTM_REFERENCE_PRODUCT
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Project') and o.name = 'FK_PROJECT_REFERENCE_EVENTTYP')
alter table Project
   drop constraint FK_PROJECT_REFERENCE_EVENTTYP
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ProjectMedia') and o.name = 'FK_PROJECTM_REFERENCE_PROJECT')
alter table ProjectMedia
   drop constraint FK_PROJECTM_REFERENCE_PROJECT
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ProjectMedia') and o.name = 'FK_PROJECTM_REFERENCE_PRODUCT')
alter table ProjectMedia
   drop constraint FK_PROJECTM_REFERENCE_PRODUCT
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Testimonial') and o.name = 'FK_TESTIMON_REFERENCE_EVENTTYP')
alter table Testimonial
   drop constraint FK_TESTIMON_REFERENCE_EVENTTYP
go

alter table Cart
   drop constraint PK_CART
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Cart')
            and   type = 'U')
   drop table Cart
go

alter table CartProd
   drop constraint PK_CARTPROD
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CartProd')
            and   type = 'U')
   drop table CartProd
go

alter table Contact
   drop constraint PK_CONTACT
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Contact')
            and   type = 'U')
   drop table Contact
go

alter table EventType
   drop constraint PK_EVENTTYPE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('EventType')
            and   type = 'U')
   drop table EventType
go

alter table Product
   drop constraint PK_PRODUCT
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Product')
            and   type = 'U')
   drop table Product
go

alter table ProductCategory
   drop constraint PK_PRODUCTCATEGORY
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ProductCategory')
            and   type = 'U')
   drop table ProductCategory
go

alter table ProductMedia
   drop constraint PK_PRODUCTMEDIA
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ProductMedia')
            and   type = 'U')
   drop table ProductMedia
go

alter table ProductType
   drop constraint PK_PRODUCTTYPE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ProductType')
            and   type = 'U')
   drop table ProductType
go

alter table Project
   drop constraint PK_PROJECT
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Project')
            and   type = 'U')
   drop table Project
go

alter table ProjectMedia
   drop constraint PK_PROJECTMEDIA
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ProjectMedia')
            and   type = 'U')
   drop table ProjectMedia
go

alter table Testimonial
   drop constraint PK_TESTIMONIAL
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Testimonial')
            and   type = 'U')
   drop table Testimonial
go

/*==============================================================*/
/* Table: Cart                                                  */
/*==============================================================*/
create table Cart (
   CartID               integer              identity,
   Price                decimal(10,2)        null,
   Location             nvarchar(255)        null,
   PlannedTime          datetime             null,
   IsActivate           tinyint              null,
   CreateOn             datetime             null,
   UpdateOn             datetime             null,
   ContactId            integer              null
)
go

alter table Cart
   add constraint PK_CART primary key (CartID)
go

/*==============================================================*/
/* Table: CartProd                                              */
/*==============================================================*/
create table CartProd (
   ID                   integer              identity,
   CartId               integer              null,
   ProdId               integer              null,
   Price                decimal(10,2)        null,
   Title                nvarchar(255)        null,
   SubTitle             nvarchar(255)        null,
   Quantity             integer              null
)
go

alter table CartProd
   add constraint PK_CARTPROD primary key (ID)
go

/*==============================================================*/
/* Table: Contact                                               */
/*==============================================================*/
create table Contact (
   ContactId            integer              identity,
   FirstName            nvarchar(60)         null,
   LastName             nvarchar(60)         null,
   PhoneNum             nvarchar(12)         null,
   Email                nvarchar(60)         null,
   Message              nvarchar(255)        null
)
go

alter table Contact
   add constraint PK_CONTACT primary key (ContactId)
go

/*==============================================================*/
/* Table: EventType                                             */
/*==============================================================*/
create table EventType (
   TypeId               integer              identity,
   EventName            nvarchar(255)        null
)
go

alter table EventType
   add constraint PK_EVENTTYPE primary key (TypeId)
go

/*==============================================================*/
/* Table: Product                                               */
/*==============================================================*/
create table Product (
   ProdId               integer              identity,
   ProdTypeId           integer              null,
   CategroyId           integer              null,
   Title                nvarchar(255)        null,
   SubTitle             nvarchar(255)        null,
   Description          nvarchar(2048)       null,
   TotalStock           int                  null,
   AvailableStock       int                  null,
   Price                decimal(10,2)        null,
   SpcOrDisct           smallint             null,
   Discount             decimal(10,2)        null,
   SpecialPrice         decimal(10,2)        null,
   IsActivate           tinyint              null,
   CreateOn             datetime             null
)
go

alter table Product
   add constraint PK_PRODUCT primary key (ProdId)
go

/*==============================================================*/
/* Table: ProductCategory                                       */
/*==============================================================*/
create table ProductCategory (
   CategroyId           integer              identity,
   CategroyName         nvarchar(100)        null
)
go

alter table ProductCategory
   add constraint PK_PRODUCTCATEGORY primary key (CategroyId)
go

/*==============================================================*/
/* Table: ProductMedia                                          */
/*==============================================================*/
create table ProductMedia (
   Id                   integer              identity,
   ProdId               integer              null,
   url                  nvarchar(255)        null
)
go

alter table ProductMedia
   add constraint PK_PRODUCTMEDIA primary key (Id)
go

/*==============================================================*/
/* Table: ProductType                                           */
/*==============================================================*/
create table ProductType (
   ProdTypeId           integer              identity,
   TypeName             nvarchar(100)        null
)
go

alter table ProductType
   add constraint PK_PRODUCTTYPE primary key (ProdTypeId)
go

/*==============================================================*/
/* Table: Project                                               */
/*==============================================================*/
create table Project (
   ProdjectId           integer              identity,
   Description          nvarchar(2048)       null,
   EventtypeId          integer              null,
   CustomerName         nvarchar(255)        null
)
go

alter table Project
   add constraint PK_PROJECT primary key (ProdjectId)
go

/*==============================================================*/
/* Table: ProjectMedia                                          */
/*==============================================================*/
create table ProjectMedia (
   Id                   integer              identity,
   ProjectId            integer              null,
   url                  nvarchar(255)        null
)
go

alter table ProjectMedia
   add constraint PK_PROJECTMEDIA primary key (Id)
go

/*==============================================================*/
/* Table: Testimonial                                           */
/*==============================================================*/
create table Testimonial (
   TestimonialId        integer              identity,
   CustomerName         nvarchar(255)        null,
   EventtypeId          integer              null,
   Message              nvarchar(255)        null
)
go

alter table Testimonial
   add constraint PK_TESTIMONIAL primary key (TestimonialId)
go

alter table Cart
   add constraint FK_CART_REFERENCE_CONTACT foreign key (ContactId)
      references Contact (ContactId)
go

alter table CartProd
   add constraint FK_CARTPROD_REFERENCE_CART foreign key (CartId)
      references Cart (CartID)
go

alter table CartProd
   add constraint FK_CARTPROD_REFERENCE_PRODUCT foreign key (ProdId)
      references Product (ProdId)
go

alter table Product
   add constraint FK_PRODUCT_REFERENCE_PRODUCTM foreign key ()
      references ProductMedia (Id)
go

alter table Product
   add constraint FK_PRODUCT_REFERENCE_PROJECTM foreign key ()
      references ProjectMedia (Id)
go

alter table Product
   add constraint FK_PRODUCT_REFERENCE_PRODUCTT foreign key (ProdTypeId)
      references ProductType (ProdTypeId)
go

alter table Product
   add constraint FK_PRODUCT_REFERENCE_PRODUCTC foreign key (CategroyId)
      references ProductCategory (CategroyId)
go

alter table ProductMedia
   add constraint FK_PRODUCTM_REFERENCE_PRODUCT foreign key (ProdId)
      references Product (ProdId)
go

alter table Project
   add constraint FK_PROJECT_REFERENCE_EVENTTYP foreign key (EventtypeId)
      references EventType (TypeId)
go

alter table ProjectMedia
   add constraint FK_PROJECTM_REFERENCE_PROJECT foreign key (ProjectId)
      references Project (ProdjectId)
go

alter table ProjectMedia
   add constraint FK_PROJECTM_REFERENCE_PRODUCT foreign key ()
      references Product (ProdId)
go

alter table Testimonial
   add constraint FK_TESTIMON_REFERENCE_EVENTTYP foreign key (EventtypeId)
      references EventType (TypeId)
go
