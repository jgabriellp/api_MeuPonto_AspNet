CREATE TABLE Company (
    Id BIGINT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(255) NOT NULL,
    Cnpj NVARCHAR(18) UNIQUE NOT NULL, 
    Email NVARCHAR(255)
    -- PlanoId (removido)
    -- ... outras colunas
);