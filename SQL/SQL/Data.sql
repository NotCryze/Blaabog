use BlaaBog;
GO

DECLARE @start_date DATE = GETDATE();
DECLARE @token VARCHAR(50) = '123456';

EXEC spCreateClass @start_date = @start_date, @token = @token;