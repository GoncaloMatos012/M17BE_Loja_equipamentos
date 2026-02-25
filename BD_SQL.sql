-- 1. Criar a Base de Dados
CREATE DATABASE GymMarketDB;
GO

USE GymMarketDB;
GO


CREATE TABLE Utilizadores (
    IdUtilizador INT PRIMARY KEY IDENTITY(1,1),
    Nome NVARCHAR(100) NOT NULL,
    Email NVARCHAR(150) NOT NULL UNIQUE,
    Password VARBINARY(64) NOT NULL, 
    Admin BIT NOT NULL DEFAULT 0,   
    DataRegisto DATETIME DEFAULT GETDATE()
);


CREATE TABLE Categorias (
    IdCategoria INT PRIMARY KEY IDENTITY(1,1),
    Nome NVARCHAR(50) NOT NULL
);


CREATE TABLE Produtos (
    IdProduto INT PRIMARY KEY IDENTITY(1,1),
    Nome NVARCHAR(100) NOT NULL,
    Descricao NVARCHAR(MAX),
    Preco DECIMAL(10, 2) NOT NULL, -- Em Euro conforme configurado
    Stock INT DEFAULT 0,
    ImagemURL NVARCHAR(255),
    IdCategoria INT,
    FOREIGN KEY (IdCategoria) REFERENCES Categorias(IdCategoria)
);


CREATE TABLE Carrinho (
    IdCarrinho INT PRIMARY KEY IDENTITY(1,1),
    IdUtilizador INT,
    IdProduto INT,
    Quantidade INT DEFAULT 1,
    FOREIGN KEY (IdUtilizador) REFERENCES Utilizadores(IdUtilizador),
    FOREIGN KEY (IdProduto) REFERENCES Produtos(IdProduto)
);


CREATE TABLE Favoritos (
    IdFavorito INT PRIMARY KEY IDENTITY(1,1),
    IdUtilizador INT,
    IdProduto INT,
    FOREIGN KEY (IdUtilizador) REFERENCES Utilizadores(IdUtilizador),
    FOREIGN KEY (IdProduto) REFERENCES Produtos(IdProduto)
);
GO

-- 7. Criar o primeiro utilizador ADMIN (Password: admin123)
-- Nota: O HASHBYTES deve ser feito no INSERT para bater certo com o C#
INSERT INTO Utilizadores (Nome, Email, Password, Admin)
VALUES ('Administrador', 'admin@gymmarket.pt', HASHBYTES('SHA2_512', 'admin123'), 1);

-- 8. Algumas categorias de exemplo
INSERT INTO Categorias (Nome) VALUES ('Musculação'), ('Cardio'), ('Acessórios');