using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace M17BE_Loja_equipamentos.Classes
{
    public class Produto
    {
        public int IdProduto { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public int Stock { get; set; }
        public string Marca { get; set; }
        public int CategoriaId { get; set; }
        public bool Destaque { get; set; }
        public bool MaisVendido { get; set; }
        public string ImagemURL { get; set; }

        BaseDados bd;

        public Produto()
        {
            bd = new BaseDados();
        }

        // Listar todos os produtos (Geral)
        public DataTable ListaTodosProdutos()
        {
            return bd.devolveSQL("SELECT P.*, C.NomeCategoria FROM Produtos P INNER JOIN Categorias C ON P.CategoriaId = C.IdCategoria");
        }

        // Listar produtos disponíveis para a Loja (com Stock)
        public DataTable ListaProdutosDisponiveis()
        {
            return bd.devolveSQL("SELECT * FROM Produtos WHERE Stock > 0 ORDER BY DataCriacao DESC");
        }

        public DataTable ListaDestaques()
        {
            return bd.devolveSQL("SELECT * FROM Produtos WHERE Destaque = 1 AND Stock > 0");
        }

        // Filtrar por Categoria
        public DataTable ListaPorCategoria(int idCategoria)
        {
            string sql = "SELECT * FROM Produtos WHERE CategoriaId = @catId AND Stock > 0";
            List<SqlParameter> p = new List<SqlParameter>
            {
                new SqlParameter("@catId", SqlDbType.Int) { Value = idCategoria }
            };
            return bd.devolveSQL(sql, p);
        }

        // Devolver detalhes de um único produto
        public DataTable DevolveDadosProduto(int id)
        {
            string sql = "SELECT P.*, C.NomeCategoria FROM Produtos P INNER JOIN Categorias C ON P.CategoriaId = C.IdCategoria WHERE P.IdProduto = @id";
            List<SqlParameter> p = new List<SqlParameter>
            {
                new SqlParameter("@id", SqlDbType.Int) { Value = id }
            };
            return bd.devolveSQL(sql, p);
        }

        // Adicionar Produto (Para o painel Admin)
        public void Adicionar()
        {
            string sql = @"INSERT INTO Produtos (Nome, Descricao, Preco, Stock, Marca, CategoriaID, Destaque, MaisVendido, ImagemURL)
                           VALUES (@nome, @descricao, @preco, @stock, @marca, @categoriaID, @destaques, @maisVendido, @img)";

            List<SqlParameter> p = new List<SqlParameter>
            {
                new SqlParameter("@nome", Nome),
                new SqlParameter("@descricao", Descricao),
                new SqlParameter("@preco", Preco),
                new SqlParameter("@stock", Stock),
                new SqlParameter("@marca", Marca),
                new SqlParameter("@categoriaID", CategoriaId),
                new SqlParameter("@estaques", Destaque),
                new SqlParameter("@maisVendido", MaisVendido),
                new SqlParameter("@img", ImagemURL)
            };
            bd.executaSQL(sql, p);
        }
    }
}