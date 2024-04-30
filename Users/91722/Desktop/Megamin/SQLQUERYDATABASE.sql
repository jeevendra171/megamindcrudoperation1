create TABLE tbl_UserInfo (
    UserId INT PRIMARY KEY IDENTITY(1,1),
   [Name] VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    PhoneNo VARCHAR(20) NOT NULL,
    [Address] VARCHAR(200),
    [State] VARCHAR(50) NOT NULL,
    City VARCHAR(50) NOT NULL,
    Agree BIT NOT NULL
   
); 

CREATE PROCEDURE sp_InsertUsers
(
    @Name VARCHAR(100),
    @Email VARCHAR(100),
    @PhoneNo VARCHAR(20),
    @Address VARCHAR(200),
    @State VARCHAR(50),
    @City VARCHAR(50),
    @Agree BIT = 0
)
AS
BEGIN
    INSERT INTO tbl_UserInfo (Name, Email, PhoneNo, Address, State, City, Agree)
        VALUES (@Name, @Email, @PhoneNo, @Address, @State, @City, @Agree);
END


Create PROCEDURE sp_SelectUserInfos
AS
BEGIN
    SELECT * FROM tbl_UserInfo;
END

Create PROCEDURE sp_UpdateUserInfos
(
    @UserId INT,
    @Name VARCHAR(100),
    @Email VARCHAR(100),
    @PhoneNo VARCHAR(20),
    @Address VARCHAR(200),
    @State VARCHAR(50),
    @City VARCHAR(50),
    @Agree BIT
)
AS
BEGIN
    UPDATE tbl_UserInfo
    SET Name = @Name,
        Email = @Email,
        PhoneNo = @PhoneNo,
        Address = @Address,
        State = @State,
        City = @City,
        Agree = @Agree
    WHERE UserId = @UserId;
END

Create PROCEDURE sp_DeleteUserInfos
(
    @UserId INT
)
AS
BEGIN
    DELETE FROM tbl_UserInfo
    WHERE UserId = @UserId;
END