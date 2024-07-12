DROP Procedure Ins_EmployeeSelect
GO
CREATE PROCEDURE Ins_EmployeeSelect
AS BEGIN
  SELECT EmployeeID,Name,Salary,Experience,Gender,DesiredLocation FROM Employee


  
END;DROP Procedure Ins_EmployeeUpdate
GO 
CREATE PROCEDURE Ins_EmployeeUpdate
    @EmployeeID int,
@Name varchar(50),
@Salary int,
@Experience int,
@Gender nvarchar(50),
@DesiredLocation nvarchar(50)
AS
BEGIN
    UPDATE Employee
    SET
    Name=@Name, Salary=@Salary, Experience=@Experience, Gender=@Gender,
DesiredLocation=@DesiredLocation
WHERE EmployeeID=@EmployeeID
END;


DROP Procedure Ins_EmployeeInsert
GO
CREATE PROCEDURE Ins_EmployeeInsert
    @Name VARCHAR(50),
@Salary int,
@Experience int,
@Gender nvarchar(50),
@DesiredLocation nvarchar(50)
AS
BEGIN
   INSERT into Employee (Name, Salary, Experience, Gender, DesiredLocation)
   Values (@Name, @Salary, @Experience, @Gender, @DesiredLocation)
END;


Drop Procedure Ins_EmployeeDelete
GO
Create Procedure Ins_EmployeeDelete
@EmployeeID int
AS Begin
DELETE From Employee WHERE EmployeeID=@EmployeeID
End
