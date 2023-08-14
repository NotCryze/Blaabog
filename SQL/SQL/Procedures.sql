use BlaaBog;
GO


/*
	Classes
*/

-- Create Class
CREATE OR ALTER PROCEDURE spCreateClass
	@start_date DATE,
	@token CHAR(6)
AS
BEGIN
	INSERT INTO Classes(start_date, token)
	VALUES (@start_date, @token)
END
GO


-- Read Class
CREATE OR ALTER PROCEDURE spGetClass
	@id INT
AS
BEGIN
	SELECT *
	FROM Classes
	WHERE
		id = @id
		AND deleted = 0
END
GO

CREATE OR ALTER PROCEDURE spGetClasses
AS
BEGIN
	SELECT *
	FROM Classes
	WHERE deleted = 0
END
GO

CREATE OR ALTER PROCEDURE spGetClassByToken
	@token CHAR(6)
AS
BEGIN
	SELECT *
	FROM Classes
	WHERE
		token = @token
		AND deleted = 0
END
GO


-- Update Class
CREATE OR ALTER PROCEDURE spUpdateClass
	@id INT,
	@start_date DATE = NULL,
	@token CHAR(5) = NULL
AS
BEGIN
	UPDATE Classes
	SET
		start_date = ISNULL(@start_date, start_date),
		token = ISNULL(@token, token)
	WHERE
		id = @id
		AND deleted = 0
END
GO


-- Delete Class
CREATE OR ALTER PROCEDURE spDeleteClass
	@id INT
AS
BEGIN
	UPDATE Classes
	SET deleted = 1
	WHERE
		id = @id
		AND deleted = 0
END
GO


/*
	Students
*/

-- Create Student
CREATE OR ALTER PROCEDURE spCreateStudent
	@name NVARCHAR(100),
	@email NVARCHAR(320),
	@password VARCHAR(60),
	@class INT
AS
BEGIN
	INSERT INTO Students(name, email, password, fk_class)
	VALUES (@name, @email, @password, @class)
END
GO

-- Read Student
CREATE OR ALTER PROCEDURE spGetStudent
	@id INT
AS
BEGIN
	SELECT *
	FROM Students
	WHERE
		id = @id
		AND deleted = 0
END
GO

CREATE OR ALTER PROCEDURE spGetStudents
AS
BEGIN
	SELECT *
	FROM Students
	WHERE deleted = 0
END
GO

CREATE OR ALTER PROCEDURE spGetStudentsByName
	@name NVARCHAR(100)
AS
BEGIN
	SELECT *
	FROM Students
	WHERE
		name LIKE '%' + @name + '%'
		AND deleted = 0
END
GO

CREATE OR ALTER PROCEDURE spGetStudentByEmail
	@email NVARCHAR(320)
AS
BEGIN
	SELECT *
	FROM Students
	WHERE
		email = @email
		AND deleted = 0
END
GO

CREATE OR ALTER PROCEDURE spGetStudentsBySpeciality
	@speciality VARCHAR(15)
AS
BEGIN
	SELECT *
	FROM Students
	WHERE
		speciality = @speciality
		AND deleted = 0
END
GO

CREATE OR ALTER PROCEDURE spGetStudentsByClass
	@id INT
AS
BEGIN
	SELECT *
	FROM Students
	WHERE
		fk_class = @id
		AND deleted = 0
END
GO


-- Update Student
CREATE OR ALTER PROCEDURE spUpdateStudent
	@id INT,
	@name NVARCHAR(100) = NULL,
	@image VARCHAR(41) = NULL,
	@description NVARCHAR(4000) = NULL,
	@email NVARCHAR(320) = NULL,
	@speciality VARCHAR(15) = NULL,
	@fk_class INT = NULL,
	@end_date DATE = NULL,
	@password VARCHAR(60) = NULL
AS
BEGIN
	UPDATE Students
	SET
		name = ISNULL(@name, name),
		image = ISNULL(@image, image),
		description = ISNULL(@description, description),
		email = ISNULL(@email, email),
		speciality = ISNULL(@speciality, speciality),
		fk_class = ISNULL(@fk_class, fk_class),
		end_date = ISNULL(@end_date, end_date),
		password = ISNULL(@password, password)
	WHERE
		id = @id
		AND deleted = 0
END
GO

-- Delete Student
CREATE OR ALTER PROCEDURE spDeleteStudent
	@id INT
AS
BEGIN
	UPDATE Students
	SET deleted = 1
	WHERE
		id = @id
		AND deleted = 0
END
GO


/*
	Student Pending Changes
*/

-- Create Change
CREATE OR ALTER PROCEDURE spCreatePendingChange
	@fk_student INT,
	@name NVARCHAR(100) = NULL,
	@image VARCHAR(41) = NULL,
	@description NVARCHAR(4000) = NULL
AS
BEGIN
	INSERT INTO StudentsPendingChanges(fk_student, name, image, description)
	VALUES (@fk_student, @name, @image, @description)
END
GO

-- Read Change
CREATE OR ALTER PROCEDURE spGetPendingChange
	@id INT
AS
BEGIN
	SELECT *
	FROM StudentsPendingChanges
	WHERE
		id = @id
END
GO

CREATE OR ALTER PROCEDURE spGetPendingChanges
AS
BEGIN
	SELECT *
	FROM StudentsPendingChanges
END
GO

---- Update Change
--CREATE OR ALTER PROCEDURE spUpdatePendingChange
--	@id INT
--AS
--BEGIN
--	RETURN NULL
--END
--GO

-- Delete Change
CREATE OR ALTER PROCEDURE spDeletePendingChange
	@id INT
AS
BEGIN
	DELETE FROM StudentsPendingChanges
	WHERE id = @id
END
GO



/*
	Teachers
*/

-- Create Teacher
CREATE OR ALTER PROCEDURE spCreateTeacher
	@name NVARCHAR(100),
	@email NVARCHAR(320),
	@password VARCHAR(60)
AS
BEGIN
	INSERT INTO Teachers(name, email, password)
	VALUES (@name, @email, @password)
END
GO

-- Read Teacher
CREATE OR ALTER PROCEDURE spGetTeacher
	@id INT
AS
BEGIN
	SELECT *
	FROM Teachers
	WHERE
		id = @id
		AND deleted = 0
END
GO

CREATE OR ALTER PROCEDURE spGetTeachers
AS
BEGIN
	SELECT *
	FROM Teachers
	WHERE deleted = 0
END
GO

CREATE OR ALTER PROCEDURE spGetTeachersByName
	@name NVARCHAR(100)
AS
BEGIN
	SELECT *
	FROM Teachers
	WHERE
		name LIKE '%' + @name + '%'
		AND deleted = 0
END
GO

CREATE OR ALTER PROCEDURE spGetTeacherByEmail
	@email NVARCHAR(320)
AS
BEGIN
	SELECT *
	FROM Teachers
	WHERE 
		email = @email
		AND deleted = 0
END
GO

-- Update Teacher
CREATE OR ALTER PROCEDURE spUpdateTeacher
	@id INT,
	@name NVARCHAR(100) = NULL,
	@email NVARCHAR(320) = NULL,
	@password VARCHAR(60) = NULL,
	@admin BIT = NULL
AS
BEGIN
	UPDATE Teachers
	SET
		name = ISNULL(@name, name),
		email = ISNULL(@email, email),
		password = ISNULL(@password, password),
		admin = ISNULL(@admin, admin)
	WHERE
		id = @id
		AND deleted = 0
END
GO

-- Delete Teacher
CREATE OR ALTER PROCEDURE spDeleteTeacher
	@id INT
AS
BEGIN
	UPDATE Teachers
	SET deleted = 1
	WHERE
		id = @id
		AND deleted = 0
END
GO


/*
	Comments
*/

-- Create Comment
CREATE OR ALTER PROCEDURE spCreateComment
	@fk_author INT,
	@fk_subject INT,
	@content NVARCHAR(512)
AS
BEGIN
	INSERT INTO Comments(fk_author, fk_subject, content)
	VALUES (@fk_author, @fk_subject, @content)
END
GO

-- Read Comment
CREATE OR ALTER PROCEDURE spGetComment
	@id INT
AS
BEGIN
	SELECT *
	FROM Comments
	WHERE
		id = @id
		AND deleted = 0
END
GO

CREATE OR ALTER PROCEDURE spGetComments
AS
BEGIN
	SELECT *
	FROM Comments
	WHERE
		approved = 1
		AND deleted = 0
END
GO

CREATE OR ALTER PROCEDURE spGetNonApprovedComments
AS
BEGIN
	SELECT *
	FROM Comments
	WHERE
		approved = 0
		AND deleted = 0
END
GO

CREATE OR ALTER PROCEDURE spGetCommentsByAuthor
	@id INT
AS
BEGIN
	SELECT *
	FROM Comments
	WHERE
		fk_author = @id
		AND approved = 1
		AND deleted = 0
END
GO

CREATE OR ALTER PROCEDURE spGetCommentsBySubject
	@id INT
AS
BEGIN
	SELECT *
	FROM Comments
	WHERE
		fk_subject = @id
		AND approved = 1
		AND deleted = 0
END
GO

-- Update Comment
CREATE OR ALTER PROCEDURE spUpdateComment
	@id INT,
	@approved BIT = NULL,
	@approved_by INT = NULL,
	@approved_at DATETIME = NULL
AS
BEGIN
	UPDATE Comments
	SET
		approved = ISNULL(@approved, approved),
		approved_by = ISNULL(@approved_by, approved_by),
		approved_at = ISNULL(@approved_at, approved_at)
	WHERE
		id = @id
		AND deleted = 0
END
GO

-- Delete Comment
CREATE OR ALTER PROCEDURE spDeleteComment
	@id INT
AS
BEGIN
	UPDATE Comments
	SET deleted = 1
	WHERE
		id = @id
		AND deleted = 0
END
GO

-- Other
CREATE OR ALTER PROCEDURE spApproveComment
	@id INT,
	@approved_by INT
AS
BEGIN
	UPDATE Comments
	SET
		approved = 1,
		approved_by = @approved_by,
		approved_at = GETUTCDATE()
	WHERE
		id = @id
		AND deleted = 0
END
GO


/*
	CommentsReported
*/

-- Create Report
CREATE OR ALTER PROCEDURE spCreateReport
	@fk_comment INT,
	@reason NVARCHAR(250)
AS
BEGIN
	INSERT INTO Reports(fk_comment, reason)
	VALUES (@fk_comment, @reason)
END
GO

-- Read Report
CREATE OR ALTER PROCEDURE spGetReport
	@id INT
AS
BEGIN
	SELECT *
	FROM Reports
	WHERE
		id = @id
		AND deleted = 0
END
GO

CREATE OR ALTER PROCEDURE spGetReports
	@id INT
AS
BEGIN
	SELECT *
	FROM Reports
	WHERE deleted = 0
END
GO

-- Update Report
--CREATE OR ALTER PROCEDURE spUpdateReport
--	@id INT
--AS
--BEGIN
--	UPDATE Reports
--	SET id = id
--	WHERE id = @id
--END
--GO

-- Delete Report
CREATE OR ALTER PROCEDURE spDeleteReport
	@id INT
AS
BEGIN
	UPDATE Reports
	SET deleted = 1
	WHERE
		id = @id
		AND deleted = 0
END
GO