CREATE TABLE Categorias (
    IdCategoria INT PRIMARY KEY IDENTITY(1,1),
    NomeCategoria NVARCHAR(100) NOT NULL,
    Destaque BIT DEFAULT 0,
    ImagemURL NVARCHAR(255)
)



CREATE TABLE Produtos (
    IdProduto INT PRIMARY KEY IDENTITY(1,1),
    Nome NVARCHAR(150) NOT NULL,
    Descricao NVARCHAR(500),
    Preco DECIMAL(10,2) NOT NULL CHECK (Preco >= 0),
    Stock INT NOT NULL CHECK (Stock >= 0),
    Marca NVARCHAR(100) NOT NULL CHECK (Marca not like ''),
    CategoriaId INT NOT NULL,
    DataCriacao DATETIME DEFAULT GETDATE(),
    Destaque BIT DEFAULT 0,
    ImagemURL NVARCHAR(255),

    FOREIGN KEY (CategoriaId) REFERENCES Categorias(IdCategoria)
);



CREATE TABLE Utilizadores (
    IdUtilizador INT PRIMARY KEY IDENTITY(1,1),
    Nome NVARCHAR(100) NOT NULL,
    Email NVARCHAR(150) UNIQUE NOT NULL,
    Password NVARCHAR(255) NOT NULL,
    sal int,
	token varchar(100),
    Admin bit NOT NULL default 0, -- "Admin" ou "Cliente"
    DataRegisto DATETIME DEFAULT GETDATE(),
    Ativo bit DEFAULT 1
);


CREATE TABLE Encomendas (
    IdEncomenda INT PRIMARY KEY IDENTITY(1,1),
    IdUtilizador INT NOT NULL,
    DataEncomenda DATETIME DEFAULT GETDATE(),
    Total DECIMAL(12,2),
    Estado NVARCHAR(50) CHECK (Estado in ('Pendente','A Caminho','Entregue')) DEFAULT 'Pendente',

    FOREIGN KEY (IdUtilizador) REFERENCES Utilizadores(IdUtilizador)
);

CREATE TABLE EncomendaDetalhe (
    IdDetalhe INT PRIMARY KEY IDENTITY(1,1),
    IdEncomenda INT NOT NULL,
    IdProduto INT NOT NULL,
    Quantidade INT NOT NULL CHECK (Quantidade>0),
    PrecoUnitario DECIMAL(10,2) NOT NULL,

    FOREIGN KEY (IdEncomenda) REFERENCES Encomendas(IdEncomenda),
    FOREIGN KEY (IdProduto) REFERENCES Produtos(IdProduto)
);



CREATE TABLE Carrinho (
    IdCarrinho INT PRIMARY KEY IDENTITY(1,1),
    IdUtilizador INT NOT NULL,
    IdProduto INT NOT NULL,
    Quantidade INT NOT NULL CHECK (Quantidade > 0),
    DataAdicionado DATETIME DEFAULT GETDATE(),

    FOREIGN KEY (IdUtilizador) REFERENCES Utilizadores(IdUtilizador),
    FOREIGN KEY (IdProduto) REFERENCES Produtos(IdProduto),

    CONSTRAINT UQ_Carrinho UNIQUE (IdUtilizador, IdProduto)
);


CREATE TABLE Favoritos (
    IdFavorito INT PRIMARY KEY IDENTITY(1,1),
    IdUtilizador INT NOT NULL,
    IdProduto INT NOT NULL,

    FOREIGN KEY (IdUtilizador) REFERENCES Utilizadores(IdUtilizador),
    FOREIGN KEY (IdProduto) REFERENCES Produtos(IdProduto),

    CONSTRAINT UQ_Favorito UNIQUE (IdUtilizador, IdProduto)
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
    FOREIGN KEY (IdUtilizador) REFERENCES Utilizadores(IdUtilizador),

    --Para evitar reviewbombing
    CONSTRAINT UQ_Avaliacao_User_Produto UNIQUE (IdUtilizador, IdProduto)
);