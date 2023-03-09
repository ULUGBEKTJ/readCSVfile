create database DataBase_1
use DataBase_1

SELECT * FROM [Employees]

create table [Employees]
(
	[Payroll_Number] NVARCHAR(50) not null,
	[Forenames]		 NVARCHAR(50) not null,
	[Surname]		 NVARCHAR(50) not null,
	[Date_of_Birth]  DATETIME     not null,
	[Telephone]		 INT          not null,
	[Mobile]		 INT		  not null,
	[Address]		 NVARCHAR(50) not null,
	[Address_2]		 NVARCHAR(50) not null,
	[Postcode]		 NVARCHAR(50) not null,
	[EMail_Home]		 NVARCHAR(50) not null,
	[Start_Date]	 DATETIME     not null, 
)

INSERT INTO [Employees] VALUES( '12','12','12','12/12/2000',11,11,'11','11','11','11','12/12/2000' )