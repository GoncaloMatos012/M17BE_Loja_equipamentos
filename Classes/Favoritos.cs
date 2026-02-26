using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace M17BE_Loja_equipamentos.Classes
{
    public class Favoritos
    {
        BaseDados bd;

        public Favoritos()
        {
            bd = new BaseDados();
        }


        // 1. CONSULTA: Verifica se a relação existe
        public bool ExisteNoFavorito(int idUtilizador, int idProduto)
        {
            string sql = "SELECT 1 FROM Favoritos WHERE IdUtilizador = @u AND IdProduto = @p";
            List<SqlParameter> p = new List<SqlParameter> {
                new SqlParameter("@u", idUtilizador),
                new SqlParameter("@p", idProduto)
            };
            DataTable dt = bd.devolveSQL(sql, p);
            return (dt != null && dt.Rows.Count > 0);
        }

        // 2. INSERÇÃO: Adiciona sem perguntas
        public void Adicionar(int idUtilizador, int idProduto)
        {
            string sql = "INSERT INTO Favoritos (IdUtilizador, IdProduto) VALUES (@u, @p)";
            List<SqlParameter> p = new List<SqlParameter> {
                new SqlParameter("@u", idUtilizador),
                new SqlParameter("@p", idProduto)
            };
            bd.executaSQL(sql, p);
        }

        // 3. ELIMINAÇÃO: Remove sem perguntas
        public void Remover(int idUtilizador, int idProduto)
        {
            string sql = "DELETE FROM Favoritos WHERE IdUtilizador = @u AND IdProduto = @p";
            List<SqlParameter> p = new List<SqlParameter> {
                new SqlParameter("@u", idUtilizador),
                new SqlParameter("@p", idProduto)
            };
            bd.executaSQL(sql, p);
        }

        // 4. LISTAGEM: Devolve os produtos favoritos
        public DataTable ListarPorUtilizador(int idUtilizador)
        {
            string sql = @"SELECT P.* FROM Favoritos F 
                           INNER JOIN Produtos P ON F.IdProduto = P.IdProduto 
                           WHERE F.IdUtilizador = @u";
            List<SqlParameter> p = new List<SqlParameter> {
                new SqlParameter("@u", idUtilizador)
            };
            return bd.devolveSQL(sql, p);
        }
    }
}