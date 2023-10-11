create table DisciplineDB (
	Id uniqueidentifier primary key,
	CreatedAt DateTimeOffset null,
	CreatedBy nvarchar(100) null,
	UpdatedAt DateTimeOffset null,
	UpdatedBy nvarchar(100) null,
	DeletedAt DateTimeOffset null,
	Name nvarchar(200) null,
	Description nvarchar(MAX) null
);



create table DocumentDB (
	Id uniqueidentifier primary key,
	CreatedAt DateTimeOffset null,
	CreatedBy nvarchar(100) null,
	UpdatedAt DateTimeOffset null,
	UpdatedBy nvarchar(100) null,
	DeletedAt DateTimeOffset null,
	Number nvarchar(40) null,
	Series nvarchar(40) null,
	IssuedAt DateTime null,
	IssuedBy nvarchar(100) null,
	DocumentType nvarchar(50) not null,
	Person_id uniqueidentifier not null
);

create table DocumentTypes (
	Id nvarchar(50) primary key
);
create table EmployeeDB (
	Id uniqueidentifier primary key,
	CreatedAt DateTimeOffset null,
	CreatedBy nvarchar(100) null,
	UpdatedAt DateTimeOffset null,
	UpdatedBy nvarchar(100) null,
	DeletedAt DateTimeOffset null,
	EmployeeType nvarchar(50) not null,
	Person_id uniqueidentifier not null
);

create table EmployeeTypes (
	Id nvarchar(50) primary key
);


create table PersonDB (
	Id uniqueidentifier primary key,
	CreatedAt DateTimeOffset null,
	CreatedBy nvarchar(100) null,
	UpdatedAt DateTimeOffset null,
	UpdatedBy nvarchar(100) null,
	DeletedAt DateTimeOffset null,
	LastName nvarchar(50) not null,
	FirstName nvarchar(50) not null,
	Patronymic nvarchar(50) null,
	Email nvarchar(MAX) not null,
	Phone nvarchar(20) not null,
	Group_id uniqueidentifier null
);

create table GroupDB (
	Id uniqueidentifier primary key,
	CreatedAt DateTimeOffset null,
	CreatedBy nvarchar(100) null,
	UpdatedAt DateTimeOffset null,
	UpdatedBy nvarchar(100) null,
	DeletedAt DateTimeOffset null,
	Name nvarchar(50) not null,
	Description nvarchar(MAX) not null,
	Employee_id uniqueidentifier null
);
create table TimeTableItemDB (
	Id uniqueidentifier primary key,
	CreatedAt DateTimeOffset null,
	CreatedBy nvarchar(100) null,
	UpdatedAt DateTimeOffset null,
	UpdatedBy nvarchar(100) null,
	DeletedAt DateTimeOffset null,
	StartDate DateTimeOffset null,
	EndDate DateTimeOffset null,
	Discipline_id uniqueidentifier not null,
	Group_id uniqueidentifier not null,
	RoomNumber smallint not null,
	Teacher_id uniqueidentifier null
);

alter table DocumentDB
add constraint fk_DocumentDB_DocumentTypes
foreign key (DocumentType)
references DocumentTypes (Id);

alter table DocumentDB
add constraint fk_DocumentDB_PersonDB
foreign key (Person_id)
references PersonDB (Id);

alter table EmployeeDB
add constraint fk_EmployeeDB_EmployeeTypes
foreign key (EmployeeType)
references EmployeeTypes (Id);

alter table GroupDB
add constraint fk_GroupDB_EmployeeDB
foreign key (Employee_id)
references EmployeeDB (Id);

alter table PersonDB
add constraint fk_PersonDB_GroupDB
foreign key (Group_id)
references GroupDB (Id);

alter table TimeTableItemDB
add constraint fk_TimeTableItemDB_DisciplineDB
foreign key (Discipline_id)
references DisciplineDB (Id);

alter table TimeTableItemDB
add constraint fk_TimeTableItemDB_GroupDB
foreign key (Group_id)
references GroupDB (Id);

alter table TimeTableItemDB
add constraint fk_TimeTableItemDB_PersonDB
foreign key (Teacher_id)
references PersonDB (Id);

insert into EmployeeTypes values
	('Student'),
	('Teacher')


	'Teacher',

insert into DocumentTypes (Id) values
	('None'),
	('Passport'),
	('BirthCertificate')




insert into PersonDB (Id, LastName, FirstName, Email, Phone) values(
	'1CC8C3FC-DAF3-4EEF-B145-BFBC51348A65', 'Анатолий', 'Коноплев', 'ahatorn@mail.ru', '82342342123'
);
insert into PersonDB (Id, LastName, FirstName,Patronymic, Email, Phone) values(
	'1CC8C3FC-DAF3-4EEF-B145-BFBC51348A66', 'Николаев', 'Вячеслав','Алексеевич', 'nik@gmail.com', '89642313243'
);


insert into EmployeeDB (Id, EmployeeType, Person_id) values(
	'1CC8C3FC-DAF3-4EEF-B145-BFBC00000000', 'Student', '1CC8C3FC-DAF3-4EEF-B145-BFBC51348A66'
);
insert into EmployeeDB (Id, EmployeeType, Person_id) values(
	'1CC8C3FC-DAF3-4EEF-B145-BFBC00000001', 'Teacher', '1CC8C3FC-DAF3-4EEF-B145-BFBC51348A65'
);

insert into DisciplineDB (Id, Name, Description) values(
	'1CC8C3FC-DAF3-4EEF-B145-BFBC51348A11', 'Русский язык', 'Предмет обязательный'
);

insert into DisciplineDB (Id, Name, Description) values(
	'1CC8C3FC-DAF3-4EEF-B145-BFBC51348A12', 'Математика', 'Предмет обязательный'
);

insert into DisciplineDB (Id, DeletedAt, Name, Description) values(
	'1CC8C3FC-DAF3-4EEF-B145-BFBC51348A13','2023/09/23', 'Философия', 'Предмет не обязательный'
);

insert into DocumentDB(Id, Number, Series, DocumentType, Person_id) values(
	'755D28A2-5383-455B-A221-F0C53B3CBD86', '32131', '2311', 'Passport', '1CC8C3FC-DAF3-4EEF-B145-BFBC51348A66'
);
insert into DocumentDB(Id, Number, Series, DocumentType, Person_id) values(
	'755D28A2-5383-455B-A221-F0C53B3CBD87', '23121', '23123', 'BirthCertificate', '1CC8C3FC-DAF3-4EEF-B145-BFBC51348A65'
);

insert into GroupDB(Id, Name, Description, Employee_id) values(
	'1cc8c3fc-daf3-4eef-b145-bfbc51348a00', 'ИП-20-3', 'Информационные системы и программирования','1cc8c3fc-daf3-4eef-b145-bfbc00000000'
);

update PersonDB set Group_id = '1cc8c3fc-daf3-4eef-b145-bfbc51348a00' where LastName = 'Николаев' and FirstName = 'Вячеслав';

insert into TimeTableItemDB(Id, Discipline_id, Group_id, RoomNumber, Teacher_id) values(
	'007b34c2-1c6c-4b79-bac3-d11a95b9a736', '1CC8C3FC-DAF3-4EEF-B145-BFBC51348A12', '1cc8c3fc-daf3-4eef-b145-bfbc51348a00',123,'1CC8C3FC-DAF3-4EEF-B145-BFBC51348A65'
);

