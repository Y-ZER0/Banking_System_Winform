SELECT UserName , Password , Permissions
FROM UserAccounts

-- CREATE DATABASE Bank_Database;
-- USE Bank_Database;

-- CREATE TABLE Persons
-- (
--  PersonID int PRIMARY KEY,
-- 	FirstName nvarchar(50) NOT NULL,
-- 	LastName nvarchar(50) NOT NULL,
-- 	Email nvarchar(50) UNIQUE NOT NULL,
-- 	Phone nvarchar(50) NOT NULL
-- );
-- 
-- CREATE TABLE Clients
-- (
-- 	AccNumber int PRIMARY KEY,
-- 	PinCode varchar(4) NOT NULL,
-- 	Balance DECIMAL(15, 2) DEFAULT 0 NOT NULL,
-- 	PersonID int,
-- 	FOREIGN KEY (PersonID) REFERENCES Persons(PersonID) ON DELETE CASCADE
-- );
-- 
-- CREATE TABLE UserAccounts
-- (
-- 	UserID int PRIMARY KEY,
-- 	Password nvarchar(50) NOT NULL,
-- 	Permissions BINARY NOT NULL,
-- 	PersonID int,
-- 	FOREIGN KEY (PersonID) REFERENCES Persons(PersonID) ON DELETE CASCADE
-- );
-- 
-- CREATE TABLE Currency
-- (
-- 	CurrencyCode CHAR(3) PRIMARY KEY,
-- 	Country nvarchar(50) NOT NULL,
-- 	CurrencyName nvarchar(50) NOT NULL,
-- 	Rate DECIMAL(19,6) NOT NULL
-- );

-- INSERT INTO Persons (PersonID, FirstName, LastName, Email, Phone)
-- VALUES 
--     (1, 'John', 'Doe', 'john.doe@example.com', '123-456-7890'),
--     (2, 'Jane', 'Smith', 'jane.smith@example.com', '987-654-3210'),
--     (3, 'Emily', 'Brown', 'emily.brown@example.com', '456-123-7890'),
--     (4, 'Michael', 'Johnson', 'michael.johnson@example.com', '789-321-4560');
-- 
-- INSERT INTO Clients (AccNumber, PinCode, Balance, PersonID)
-- VALUES 
--     (1001, '1234', 1500.75, 1),
--     (1002, '5678', 2500.00, 2),
--     (1003, '4321', 300.50, 3),
--     (1004, '8765', 0.00, 4);
-- 
-- INSERT INTO Currency (CurrencyCode, Country, CurrencyName, Rate)
-- VALUES 
--     ('USD', 'United States', 'Dollar', 1.000000),
--     ('EUR', 'Eurozone', 'Euro', 1.100000),
--     ('JPY', 'Japan', 'Yen', 0.009100),
--     ('GBP', 'United Kingdom', 'Pound Sterling', 1.250000),
--     ('INR', 'India', 'Rupee', 0.013500);
-- 
-- 
-- INSERT INTO UserAccounts (UserID, Password, Permissions, PersonID)
-- VALUES 
--     (1, 'pass123', 0, 1),   -- Basic access
--     (2, 'secure456', 2, 2), -- Basic + write access
--     (3, 'mypassword789', 4, 3), -- Basic + delete access
--     (4, 'admin321', 128, 4),  -- Basic + write + delete access (full permissions)
--     (5, 'guest001', 64, 1);  -- No permissions
USE Bank_Database;

SELECT * FROM Persons;
SELECT * FROM Currency;
SELECT * FROM Clients;
SELECT * FROM UserAccounts;

SELECT       UserAccounts.UserName, Persons.FirstName, Persons.LastName, Persons.Email, Persons.Phone, UserAccounts.Password, UserAccounts.Permissions
FROM            UserAccounts INNER JOIN
                         Persons ON UserAccounts.PersonID = Persons.PersonID
WHERE UserName = 'john_doe';

SELECT Found = 1 FROM Clients
WHERE AccNumber = 1001;


-- UPDATE Clients
-- SET Clients.PinCode = @PinCode,
-- Clients.Balance = @Balance,
-- Persons.FirstName = @FirstName,
-- Persons.LastName = @LastName,
-- Persons.Email = @Email,
-- Persons.Phone = @Phone,
-- FROM Clients 
-- INNER JOIN Persons ON Clients.PersonID = Persons.PersonID
-- WHERE AccNumber = 1001;

SELECT        Clients.AccNumber, Clients.PinCode, Clients.Balance, Persons.FirstName, Persons.LastName, Persons.Phone, Persons.Email
FROM            Clients INNER JOIN
                         Persons ON Clients.PersonID = Persons.PersonID
WHERE AccNumber = 1001;


-- INSERT INTO Persons(FirstName,LastName,Email,Phone)
--      VALUES(@FirstName , @LastName, @Email, @Phone)
-- 
-- INSERT INTO Clients(AccNumber,PinCode,Balance)
-- VALUES(@AccNumber , @PinCode, @Balance)
-- SELECT SCOPE_IDENTITY();

DELETE UserAccounts;

USE Bank_Database;
-- using sample data from chatgpt

-- Inserting sample data into UserAccounts
INSERT INTO UserAccounts (UserName, Password, Permissions, PersonID)
VALUES 
('john_doe', 'password123', -1, 1), 
('jane_smith', 'securepass', 0, 2), 
('michael_lee', 'mypass', 4, 3),    


--UPDATE UserAccounts
--SET UserAccounts.Password = @Password,
--UserAccounts.Permissions = @Permissions,
--Persons.FirstName = @FirstName,
--Persons.LastName = @LastName,
--Persons.Email = @Email,
--Persons.Phone = @Phone,
--FROM Clients 
--INNER JOIN Persons ON Clients.PersonID = Persons.PersonID
--WHERE UserName = 'john_doe';

--  INSERT INTO UserAccounts(UserName,Password,Permissions,PersonID)
--  VALUES(@UserName,@Password,@Permissions,@PersonID)

-- UPDATE Currency
-- SET Rate = @Rate
--  WHERE CurrencyCode = @CurrencyCode;

USE Bank_Database;

Select 1 from UserAccounts
WHERE UserName = 'michael_lee' AND Password = 'mypass';

Select * from UserAccounts;
Select * from Clients;
Select * from Persons;



SELECT 1 FROM Clients
WHERE AccNumber = 1001;

SELECT        Clients.AccNumber, Clients.PinCode, Persons.FirstName, Persons.LastName, Persons.Email, Persons.Phone, Clients.Balance
FROM            Persons INNER JOIN
                         Clients ON Persons.PersonID = Clients.PersonID



-- Login User Validation
Select 1 from UserAccounts
WHERE UserName = 'michael_lee' AND Password = 'mypass';

-- Add New Client Query
INSERT INTO Persons(FirstName,LastName,Email,Phone)
VALUES('Yousef' , 'Abu-Nimreh', 'yousef.nimreh@gmail.com', 0775496698)
SELECT SCOPE_IDENTITY();
INSERT INTO Clients(AccNumber,PinCode,Balance , PersonID)
VALUES(1005 , 1234, 250000 , SCOPE_IDENTITY());

-- Ordering Clients
-- SELECT        Clients.AccNumber, Clients.PinCode, Persons.FirstName, Persons.LastName, Persons.Email, Persons.Phone, Clients.Balance
-- FROM            Persons INNER JOIN
--                          Clients ON Persons.PersonID = Clients.PersonID
-- ORDER BY AccNumber DESC;
-- 
-- SELECT        Clients.AccNumber, Clients.PinCode, Persons.FirstName, Persons.LastName, Persons.Email, Persons.Phone, Clients.Balance
-- FROM            Persons INNER JOIN
--                          Clients ON Persons.PersonID = Clients.PersonID
-- ORDER BY AccNumber; -- by default ascedingly

SELECT        Clients.AccNumber, Clients.PinCode, Persons.FirstName, Persons.LastName, Persons.Email, Persons.Phone, Clients.Balance
FROM            Persons INNER JOIN
                         Clients ON Persons.PersonID = Clients.PersonID
WHERE AccNumber = 1003

Select * from Clients;
Select * from Persons;

-- delete Client record from two tables using Acc. Number (T-SQL technique)
-- Step 1: Find the PersonID using AccNumber
DECLARE @PersonID INT;
SET @PersonID = (SELECT PersonID FROM Clients WHERE AccNumber = 1001);

-- Step 2: Delete from Clients table first (the child table)
DELETE FROM Clients WHERE AccNumber = 1001;

-- Step 3: Delete from Persons table (the parent table)
DELETE FROM Persons WHERE PersonID = @PersonID;


-- get all clients balances

SELECT * from UserAccounts;

SELECT Clients.AccNumber, Clients.PinCode, Persons.FirstName, Persons.LastName, Persons.Email, Persons.Phone, Clients.Balance
FROM Persons INNER JOIN Clients ON Persons.PersonID = Clients.PersonID;

SELECT * FROM Clients;

-------- Currency Exchange queries

-- get all Currencies
SELECT * FROM Currency

-- Get Currency using Code (Searching)
SELECT * FROM Currency
WHERE CurrencyCode = 'ZAR';

-- check if Currency exist
SELECT 1 FROM Currency
WHERE CurrencyCode = 'AED';


----------------------------------------------------------------------------------
----------------------------------------------------------------------------------
----------------------------------------------------------------------------------
----------------------------------------------------------------------------------

							-- Manage Users queries
USE Bank_Database;

SELECT * FROM UserAccounts;
SELECT * FROM Persons;

SELECT        UserAccounts.UserName, Persons.FirstName, Persons.LastName, Persons.Email, Persons.Phone, UserAccounts.Password, UserAccounts.Permissions
FROM            Persons INNER JOIN
                         UserAccounts ON Persons.PersonID = UserAccounts.PersonID

Select 1 from UserAccounts 
WHERE UserName = 'jane_smith' AND Password = 'securepass';		

Select 1 from UserAccounts 
WHERE UserName = 'jane_smith';	

SELECT        UserAccounts.UserName, Persons.FirstName, Persons.LastName, Persons.Email, Persons.Phone, UserAccounts.Password, UserAccounts.Permissions
FROM            Persons INNER JOIN
                         UserAccounts ON Persons.PersonID = UserAccounts.PersonID
Where UserAccounts.UserName = 'jane_smith';
--  Accessbility into 4 screens using a binary system Representation
   
-- 1. Binary Representation
-- Each screen or tab is assigned a binary bit:
   
-- Screen 1: 0001 (Binary) → 1 (Decimal)
-- Screen 2: 0010 (Binary) → 2 (Decimal)
-- Screen 3: 0100 (Binary) → 4 (Decimal)
-- Screen 4: 1000 (Binary) → 8 (Decimal)
-- By combining these binary values, you can represent any combination of accessible screens.
   
-- 2. Examples of Permissions
-- Using a binary system, you can manage which screens a user has access to:
   
-- Access Screen 1 Only: 0001 → 1
-- Access Screen 1 and Screen 2: 0011 → 3
-- Access All Screens: 1111 → 15
-- Access Screen 3 and Screen 4 Only: 1100 → 12

----------------------------------------------------------------------------------
----------------------------------------------------------------------------------
----------------------------------------------------------------------------------
----------------------------------------------------------------------------------

							-- LoginResgister queries

CREATE TABLE LoginRegister
(
	UserName nvarchar(50),
	FirstName nvarchar(50),
	LastName nvarchar(50),
	Email nvarchar(50),
	Phone nvarchar(50),
	Password nvarchar(50),
	LogDate DATETIME DEFAULT CURRENT_TIMESTAMP
)

-- each time a user logs in into his account >> a new record will be added to register table
SELECT *
FROM LoginRegister;

SELECT *
FROM UserAccounts;

-- add / insert query >> inserting / copying data from one table into another
INSERT INTO LoginRegister
           ( UserName, FirstName, LastName, Email, Phone, Password, LogDate )
SELECT        UserAccounts.UserName, Persons.FirstName, Persons.LastName, Persons.Email, Persons.Phone, UserAccounts.Password, CURRENT_TIMESTAMP as LogDate
FROM            Persons INNER JOIN
                         UserAccounts ON Persons.PersonID = UserAccounts.PersonID
WHERE UserAccounts.UserName = 'john_doe';


--INSERT INTO Persons(FirstName, LastName, Email, Phone)
--                VALUES('Yousef','Abu-Nimreh', 'yousef.nimreh@gmail.com', '962-775-496-698')
--                SELECT SCOPE_IDENTITY();
--                INSERT INTO Clients(AccNumber, PinCode, Balance , PersonID)
--                VALUES(1007, 1234, 100000 , SCOPE_IDENTITY());

SELECT *
FROM UserAccounts



