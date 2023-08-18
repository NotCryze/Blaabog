USE master;
GO
ALTER DATABASE BlaaBog SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
GO
DROP DATABASE IF EXISTS BlaaBog;
GO

CREATE DATABASE BlaaBog;
GO
USE BlaaBog;
GO


DROP TABLE IF EXISTS Classes;
CREATE TABLE Classes(
    id INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    start_date DATE NOT NULL,
    token CHAR(6) UNIQUE NOT NULL,
    deleted BIT NOT NULL DEFAULT 0
);
GO

INSERT INTO Classes(start_date, token)
VALUES (GETDATE(), '123456')


DROP TABLE IF EXISTS Students;
CREATE TABLE Students(
    id INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    name NVARCHAR(100) NOT NULL,
    image VARCHAR(41) NOT NULL DEFAULT 'default.png',
    description NVARCHAR(4000) DEFAULT NULL,
    email NVARCHAR(320) UNIQUE NOT NULL,
    speciality VARCHAR(15) DEFAULT NULL, -- IT Supporter | Programmering | Infrastruktur
    fk_class INT NOT NULL FOREIGN KEY REFERENCES Classes(id),
    end_date DATE DEFAULT NULL,
    password VARCHAR(60) NOT NULL,
    deleted BIT NOT NULL DEFAULT 0
);
GO

INSERT INTO Students(name, email, fk_class, password, end_date)
VALUES ('Student', 'student@example.com', 1, '$2a$11$TwxkzN1iqAnRMQ4IRjTbWO.DhhZPdA64EYBwa3VZOMQasmw44MdYW', GETDATE())


DROP TABLE IF EXISTS StudentsPendingChanges;
CREATE TABLE StudentsPendingChanges(
	id INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	fk_student INT NOT NULL FOREIGN KEY REFERENCES Students(id),
	name NVARCHAR(100) DEFAULT NULL,
    image VARCHAR(41) DEFAULT NULL,
    description NVARCHAR(4000) DEFAULT NULL
);
GO


DROP TABLE IF EXISTS Teachers;
CREATE TABLE Teachers(
    id INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    name NVARCHAR(100) NOT NULL,
    email NVARCHAR(320) NOT NULL,
    password VARCHAR(60) NOT NULL,
	admin BIT NOT NULL DEFAULT 0,
    deleted BIT NOT NULL DEFAULT 0
);
GO

INSERT INTO Teachers(name, email, admin, password)
VALUES ('Teacher', 'teacher@example.com', 1, '$2a$11$TwxkzN1iqAnRMQ4IRjTbWO.DhhZPdA64EYBwa3VZOMQasmw44MdYW')
GO


DROP TABLE IF EXISTS Comments;
CREATE TABLE Comments(
    id INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    fk_author INT NOT NULL FOREIGN KEY REFERENCES Students(id),
    fk_subject INT NOT NULL FOREIGN KEY REFERENCES Students(id),
	content NVARCHAR(500) NOT NULL,
    approved BIT NOT NULL DEFAULT 0,
    approved_by INT FOREIGN KEY REFERENCES Teachers(id) DEFAULT NULL,
    approved_at DATETIME DEFAULT NULL,
    created_at DATETIME NOT NULL DEFAULT GETUTCDATE(),
    deleted BIT NOT NULL DEFAULT 0
);
GO


DROP TABLE IF EXISTS Reports;
CREATE TABLE Reports(
	id INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	fk_comment INT NOT NULL FOREIGN KEY REFERENCES Comments(id),
	reason NVARCHAR(250) NOT NULL,
	created_at DATETIME NOT NULL DEFAULT GETUTCDATE(),
	deleted BIT NOT NULL DEFAULT 0
);
GO


DROP TABLE IF EXISTS TeacherTokens;
CREATE TABLE TeacherTokens(
	id INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	token CHAR(6) NOT NULL,
	fk_teacher INT FOREIGN KEY REFERENCES Teachers(id) DEFAULT NULL,
	created_at DATETIME NOT NULL DEFAULT GETUTCDATE(),
	deleted BIT NOT NULL DEFAULT 0
);

INSERT INTO TeacherTokens(token) VALUES ('123456');