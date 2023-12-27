use CrudWithADONet
-----Read All Employees----
Alter proc [DBO].[usp_Get_Employee]
CREATE PROC [DBO].[usp_Get_Employee]
As 
Begin
	Select Id, FirstName, LastName, DateOfBirth, Email, Salary From DBO.Employee with (NOLOCK)
End
-----GetById----
Create proc [DBO].[usp_Get_EmployeeById]
(
	@Id int
)
As 
Begin
	Select Id, FirstName, LastName, DateOfBirth, Email, Salary From Dbo.Employee with (NOLOCK)
	where Id = @Id
End

------ Insert
Alter proc [DBO].[usp_Insert_Employee]
(
	@FirstName Varchar(50)
	,@LastName Varchar(50)
	,@DateOfBirth Date
	,@Email Varchar(50)
	,@Salary Float 
)
AS 
Begin
	Begin Try
		Begin Tran
			Insert Into DBO.Employee(FirstName, LastName,DateOfBirth, Email, Salary)
			values
				(
					@FirstName
					,@LastName
					,@DateOfBirth
					,@Email
					,@Salary
				)
		Commit Tran
	End Try
	Begin Catch
		RollBack Tran
	End Catch
End

-----Update---
Create proc [DBO].[usp_update_Employee]
(
	@Id Int,
	@FirstName Varchar(50),
	@LastName Varchar(50),
	@DateOfBirth Date,
	@Email Varchar(50),
	@Salary Float
)
AS
Begin
Declare @RowCount Int = 0
	Begin Try
		Set @RowCount = (Select Count(1) From DBO.Employee with (NoLock) where Id = @Id)

		IF(@RowCount > 0)
			Begin
				Begin Tran 
					Update DBO.Employee
						Set
							FirstName = @FirstName,
							LastName = @LastName,
							DateOfBirth = @DateOfBirth,
							Email = @Email,
							Salary = @Salary
							Where Id = @Id
				Commit Tran
			End
	End Try
	Begin Catch
		RollBack Tran
	End Catch

End

-----Delete---
Create proc [DBO].[usp_Delete_Employee]
(
	@Id Int
)
AS
Begin
Declare @RowCount Int = 0
	Begin Try
		Set @RowCount = (Select Count(1) From DBO.Employee with (NoLock) where Id = @Id)

		IF(@RowCount > 0)
			Begin
				Begin Tran 
					Delete From DBO.Employee
						Where Id = @Id
				Commit Tran
			End
	End Try
	Begin Catch
		RollBack Tran
	End Catch

End