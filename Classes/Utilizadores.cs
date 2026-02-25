using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace M17BE_Loja_equipamentos.Classes
{
    /*
    public class Utilizadores
    {
        public int IdUtilizador { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Admin { get; set; } 
        public DateTime DataRegisto { get; set; }

        BaseDados bd;

        public Utilizadores()
        {
            bd = new BaseDados();
        }

        public bool VerificaLogin()
        {
            // Nota: Removi o 'concat' com Sal pois não está no teu CREATE TABLE. 
            // Se adicionares o Sal, volta a colocar o concat.
            string sql = "SELECT * FROM Utilizadores WHERE Email=@Email AND Password=HASHBYTES('SHA2_512', @Password)";

            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter("@Email", SqlDbType.NVarChar) { Value = this.Email },
                new SqlParameter("@Password", SqlDbType.VarChar) { Value = this.Password }
            };

            DataTable dados = bd.devolveSQL(sql, parametros);

            if (dados == null || dados.Rows.Count == 0)
                return false;

            this.IdUtilizador = int.Parse(dados.Rows[0]["IdUtilizador"].ToString());
            this.Nome = dados.Rows[0]["Nome"].ToString();
            this.Email = dados.Rows[0]["Email"].ToString();
            this.IsAdmin = bool.Parse(dados.Rows[0]["Admin"].ToString());

            return true;
        }

        public void Adicionar()
        {
            string sql = @"INSERT INTO Utilizadores (Nome, Email, Password, Admin)
                           VALUES (@Nome, @Email, HASHBYTES('SHA2_512', @Password), @Admin)";

            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter("@Nome", SqlDbType.NVarChar) { Value = this.Nome },
                new SqlParameter("@Email", SqlDbType.NVarChar) { Value = this.Email },
                new SqlParameter("@Password", SqlDbType.VarChar) { Value = this.Password },
                new SqlParameter("@Admin", SqlDbType.Bit) { Value = this.IsAdmin }
            };

            bd.executaSQL(sql, parametros);
        }

        public DataTable ListaTodosUtilizadores()
        {
            return bd.devolveSQL("SELECT IdUtilizador, Nome, Email, Admin, DataRegisto FROM Utilizadores");
        }

        public void AtualizarUtilizador()
        {
            string sql = "UPDATE Utilizadores SET Nome=@Nome, Email=@Email WHERE IdUtilizador=@Id";

            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter("@Nome", SqlDbType.NVarChar) { Value = this.Nome },
                new SqlParameter("@Email", SqlDbType.NVarChar) { Value = this.Email },
                new SqlParameter("@Id", SqlDbType.Int) { Value = this.IdUtilizador }
            };
            bd.executaSQL(sql, parametros);
        }

        public DataTable DevolveDadosUtilizador(int id)
        {
            string sql = "SELECT * FROM Utilizadores WHERE IdUtilizador=@Id";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter("@Id", SqlDbType.Int) { Value = id }
            };
            return bd.devolveSQL(sql, parametros);
        }

        public void RemoverUtilizador(int id)
        {
            string sql = "DELETE FROM Utilizadores WHERE IdUtilizador=@Id";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter("@Id", SqlDbType.Int) { Value = id }
            };
            bd.executaSQL(sql, parametros);
        }

        // Método para atualizar password (útil para recuperação)
        public void AtualizarPassword(int id, string novaPassword)
        {
            string sql = "UPDATE Utilizadores SET Password=HASHBYTES('SHA2_512', @Password) WHERE IdUtilizador=@Id";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter("@Password", SqlDbType.VarChar) { Value = novaPassword },
                new SqlParameter("@Id", SqlDbType.Int) { Value = id }
            };
            bd.executaSQL(sql, parametros);
        }
    }*/
}