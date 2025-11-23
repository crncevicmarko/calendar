create table Users(
	id int IDENTITY(1,1) PRIMARY KEY,
	firstName varchar(20),
	lastName varchar(20),
	email varchar(30),
	userName varchar(20),
	password varchar(255),
	isAdmin bit,
	isDeleted bit
)

create table Absences(
	id int IDENTITY(1,1) PRIMARY KEY,
	userId int,
	reason varchar(255),
	startOfTheEvent date,
	endOfTheEvent date,
	isApproved bit,
	isDeleted bit,
    FOREIGN KEY (userId) REFERENCES Users(id)
)

create table Appointments(
	id int IDENTITY(1,1) PRIMARY KEY,
	userId int,
	title varchar(255),
	creationDate date,
	startOfTheAppointment time,
	endOfTheAppointment time,
	isDeleted bit,
	FOREIGN KEY (userId) REFERENCES Users(id)
)

insert into Users (firstName, lastName, email, userName, password, isAdmin, isDeleted)
values ('admin','admin','admin@gmail.com','admin','admin123',1,0)

select * from Absences;

UPDATE Absences
SET reason = 'DayOff'
WHERE reason = 'SLOBODAN_DAN';

select * from Users;
Update Users set password = 'mika123' where id = 3;
select * from Appointments;
UPDATE Absences SET isDeleted = 1, isApproved = 0 WHERE id = 1002;


