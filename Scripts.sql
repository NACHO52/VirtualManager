CREATE TABLE ResourceItem(
	Id INT PRIMARY KEY IDENTITY(1,1),
	[Name] VARCHAR(50) NULL,
	[Description] VARCHAR(100) NULL,
	Price DECIMAL(18,2) NULL,
	MeasureType INT NULL,
	MeasureValue DECIMAL(18,2) NULL
)

CREATE TABLE ResourceItemPriceHistory(
	Id INT PRIMARY KEY IDENTITY(1,1),
	Price DECIMAL(18,2) NULL,
	[Date] DateTime NULL,
	ResourceItemId INT FOREIGN KEY REFERENCES ResourceItem(Id)
)

CREATE TABLE Tax(
	Id INT PRIMARY KEY IDENTITY(1,1),
	[Name] VARCHAR(50) NULL,
	[Description] VARCHAR(100) NULL,
	Amount DECIMAL(18,2) NULL,
	[Type] INT NULL,
)
CREATE TABLE TaxAmountHistory(
	Id INT PRIMARY KEY IDENTITY(1,1),
	TaxId INT FOREIGN KEY REFERENCES Tax(Id),
	Amount DECIMAL(18,2) NULL,
	[Date] DateTime NULL
)

CREATE TABLE Product(
	Id INT PRIMARY KEY IDENTITY(1,1),
	[Name] VARCHAR(50) NULL,
	[Description] VARCHAR(100) NULL,
	Active BIT NULL
)

CREATE TABLE ProductResourceItem(
	Id INT PRIMARY KEY IDENTITY(1,1),
	ProductId INT FOREIGN KEY REFERENCES Product(Id),
	ResourceItemId INT NULL,
	Quantity DECIMAL(18,2)
)

CREATE TABLE ProductTax(
	Id INT PRIMARY KEY IDENTITY(1,1),
	ProductId INT FOREIGN KEY REFERENCES Product(Id),
	TaxId INT NULL,
	Quantity DECIMAL(18,2)
)