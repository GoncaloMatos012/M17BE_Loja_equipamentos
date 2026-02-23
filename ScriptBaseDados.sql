CREATE TABLE Categorias (
    IdCategoria INT PRIMARY KEY IDENTITY(1,1),
    NomeCategoria NVARCHAR(100) NOT NULL
)



CREATE TABLE Produtos (
    IdProduto INT PRIMARY KEY IDENTITY(1,1),
    Nome NVARCHAR(150) NOT NULL,
    Descricao NVARCHAR(MAX),
    Preco DECIMAL(10,2) NOT NULL,
    Stock INT NOT NULL,
    Marca NVARCHAR(100),
    CategoriaId INT NOT NULL,
    DataCriacao DATETIME DEFAULT GETDATE(),
    Destaque BIT DEFAULT 0,
    MaisVendido BIT DEFAULT 0,
    ImagemURL NVARCHAR(255),

    FOREIGN KEY (CategoriaId) REFERENCES Categorias(IdCategoria)
);



CREATE TABLE Utilizadores (
    IdUtilizador INT PRIMARY KEY IDENTITY(1,1),
    Nome NVARCHAR(100) NOT NULL,
    Email NVARCHAR(150) UNIQUE NOT NULL,
    Password NVARCHAR(255) NOT NULL,
    Admin bit NOT NULL, -- "Admin" ou "Cliente"
    DataRegisto DATETIME DEFAULT GETDATE()
);

CREATE TABLE Encomendas (
    IdEncomenda INT PRIMARY KEY IDENTITY(1,1),
    IdUtilizador INT NOT NULL,
    DataEncomenda DATETIME DEFAULT GETDATE(),
    Total DECIMAL(10,2),
    Estado NVARCHAR(50) CHECK  Estado in ('Pendente','A Caminho','Entregue')DEFAULT 'Pendente',

    FOREIGN KEY (IdUtilizador) REFERENCES Utilizadores(IdUtilizador)
);


CREATE TABLE EncomendaDetalhe (
    IdDetalhe INT PRIMARY KEY IDENTITY(1,1),
    IdEncomenda INT NOT NULL,
    IdProduto INT NOT NULL,
    Quantidade INT NOT NULL,
    PrecoUnitario DECIMAL(10,2) NOT NULL,

    FOREIGN KEY (IdEncomenda) REFERENCES Encomendas(IdEncomenda),
    FOREIGN KEY (IdProduto) REFERENCES Produtos(IdProduto)
);




--Opcional: Tabela para avaliações de produtos
CREATE TABLE Avaliacoes (
    IdAvaliacao INT PRIMARY KEY IDENTITY(1,1),
    IdProduto INT NOT NULL,
    IdUtilizador INT NOT NULL,
    Pontuacao INT CHECK (Pontuacao BETWEEN 1 AND 5),
    Comentario NVARCHAR(500),
    DataAvaliacao DATETIME DEFAULT GETDATE(),

    FOREIGN KEY (IdProduto) REFERENCES Produtos(IdProduto),
    FOREIGN KEY (IdUtilizador) REFERENCES Utilizadores(IdUtilizador)
);
