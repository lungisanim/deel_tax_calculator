CREATE TABLE TaxCalculations
(
    Id INT PRIMARY KEY IDENTITY,
    PostalCode NVARCHAR(50) NOT NULL,
    AnnualIncome DECIMAL(18, 2) NOT NULL,
    CalculatedTax DECIMAL(18, 2) NOT NULL,
    CalculationDate DATETIME NOT NULL
)

CREATE TABLE PostalCodes
(
    Id INT PRIMARY KEY IDENTITY,
    Code NVARCHAR(50) NOT NULL,
    TaxCalculationType NVARCHAR(50) NOT NULL
)


INSERT INTO PostalCodes (Code, TaxCalculationType)
VALUES ('7441', 'Progressive'), ('A100', 'Flat Value'), ('7000', 'Flat rate'), ('1000', 'Progressive')


select * from PostalCodes;
select * from TaxCalculations;