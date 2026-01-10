-- Users/Login Table
CREATE TABLE Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    Username VARCHAR(50) UNIQUE NOT NULL,
    Email VARCHAR(100) UNIQUE NOT NULL,
    PasswordHash VARCHAR(255) NOT NULL,
    CreatedAt DATETIME2 DEFAULT GETDATE(),
    IsActive BIT DEFAULT 1
);

-- Type Master (for Income/Expense categories)
CREATE TABLE TypeMaster (
    TypeId INT IDENTITY(1,1) PRIMARY KEY,
    TypeName VARCHAR(50) NOT NULL,  -- 'Salary', 'Groceries', etc.
    Description VARCHAR(255),
    IsActive BIT DEFAULT 1
);



CREATE TABLE Income (
    IncomeId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    TypeId INT NOT NULL,
    Amount DECIMAL(15,2) NOT NULL,
    Description VARCHAR(255),
    IncomeDate DATE NOT NULL,
    CreatedAt DATETIME2 DEFAULT GETDATE(),
    FOREIGN KEY (UserId) REFERENCES Users(UserId),
    FOREIGN KEY (TypeId) REFERENCES TypeMaster(TypeId)
); 


CREATE TABLE Expenses (
    ExpenseId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    TypeId INT NOT NULL,
    Amount DECIMAL(15,2) NOT NULL,
    Description VARCHAR(255),
    ExpenseDate DATE NOT NULL,
    CreatedAt DATETIME2 DEFAULT GETDATE(),
    FOREIGN KEY (UserId) REFERENCES Users(UserId),
    FOREIGN KEY (TypeId) REFERENCES TypeMaster(TypeId)
);


