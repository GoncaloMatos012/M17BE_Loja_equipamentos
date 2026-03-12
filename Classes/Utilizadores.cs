using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace M17BE_Loja_equipamentos.Classes
{
    public class Utilizadores
    {
        public int IdUtilizador { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Sal { get; set; }
        public string Token { get; set; }
        public bool Admin { get; set; }
        public DateTime DataRegisto { get; set; }

        BaseDados bd;

        public Utilizadores()
        {
            bd = new BaseDados();
        }

        #region Autenticação e Segurança

        public bool VerificaLogin()
        {
            string sqlSal = "SELECT Sal FROM Utilizadores WHERE Email = @Email";
            List<SqlParameter> pSal = new List<SqlParameter> {
                new SqlParameter("@Email", SqlDbType.NVarChar) { Value = this.Email }
            };
            DataTable dtSal = bd.devolveSQL(sqlSal, pSal);

            if (dtSal == null || dtSal.Rows.Count == 0) return false;

            int salRecuperado = int.Parse(dtSal.Rows[0]["Sal"].ToString());

            string sql = "SELECT * FROM Utilizadores WHERE Email=@Email AND Password = HASHBYTES('SHA2_512', CONCAT(@Password, @Sal))";

            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter("@Email", SqlDbType.NVarChar) { Value = this.Email },
                new SqlParameter("@Password", SqlDbType.VarChar) { Value = this.Password },
                new SqlParameter("@Sal", SqlDbType.Int) { Value = salRecuperado }
            };

            DataTable dados = bd.devolveSQL(sql, parametros);

            if (dados == null || dados.Rows.Count == 0)
                return false;

            this.IdUtilizador = int.Parse(dados.Rows[0]["IdUtilizador"].ToString());
            this.Nome = dados.Rows[0]["Nome"].ToString();
            this.Email = dados.Rows[0]["Email"].ToString();
            this.Admin = bool.Parse(dados.Rows[0]["Admin"].ToString());

            return true;
        }

        public void recuperarPassword(string email, string guid)
        {
            string sql = "UPDATE utilizadores set token=@lnk WHERE email=@email";

            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@email",SqlDbType=SqlDbType.VarChar,Value=email },
                new SqlParameter() {ParameterName="@lnk",SqlDbType=SqlDbType.VarChar,Value=guid },
            };
            bd.executaSQL(sql, parametros);
        }

        public void atualizarPassword(string guid, string password)
        {
            string sql = "UPDATE utilizadores set palavra_passe=HASHBYTES('SHA2_512',concat(@password,sal)),token=null WHERE token=@lnk";

            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@password",SqlDbType=SqlDbType.VarChar,Value=password},
                new SqlParameter() {ParameterName="@lnk",SqlDbType=SqlDbType.VarChar,Value=guid },
            };
            bd.executaSQL(sql, parametros);
        }

        #endregion

        #region CRUD (Gestão de Utilizadores)

        public void Adicionar()
        {
            if (!EmailValido(this.Email))
            {
                throw new Exception("O formato do email não é válido.");
            }

            if (!EmailDisponivel(this.Email))
            {
                throw new Exception("O email indicado já se encontra registado no sistema.");
            }

            if (!PasswordForte(this.Password))
            {
                throw new Exception("A password deve ter pelo menos 8 caracteres e conter pelo menos um número.");
            }

            Random r = new Random();
            int novoSal = r.Next(1403, 9999);

            string sql = @"INSERT INTO Utilizadores (Nome, Email, Password, Sal, Admin)
                           VALUES (@Nome, @Email, HASHBYTES('SHA2_512', CONCAT(@Password, @Sal)), @Sal, @Admin)";

            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter("@Nome", SqlDbType.NVarChar) { Value = this.Nome },
                new SqlParameter("@Email", SqlDbType.NVarChar) { Value = this.Email },
                new SqlParameter("@Password", SqlDbType.VarChar) { Value = this.Password },
                new SqlParameter("@Sal", SqlDbType.Int) { Value = novoSal },
                new SqlParameter("@Admin", SqlDbType.Bit) { Value = this.Admin }
            };

            bd.executaSQL(sql, parametros);
        }

        public DataTable ListaTodosUtilizadores()
        {
            return bd.devolveSQL("SELECT IdUtilizador, Nome, Email, Admin, DataRegisto FROM Utilizadores");
        }

        public void RemoverUtilizador(int id)
        {
            string sql = "DELETE FROM Utilizadores WHERE IdUtilizador=@Id";
            List<SqlParameter> p = new List<SqlParameter> { new SqlParameter("@Id", id) };
            bd.executaSQL(sql, p);
        }
        #endregion

        #region Verificações e Consultas

        public DataTable devolveDadosUtilizador(string email)
        {
            string sql = "SELECT * FROM utilizadores WHERE email=@email";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@email",SqlDbType=SqlDbType.VarChar,Value=email }
            };
            DataTable dados = bd.devolveSQL(sql, parametros);
            return dados;
        }

        public bool EmailDisponivel(string email)
        {
            string sql = "SELECT COUNT(*) FROM Utilizadores WHERE Email = @Email";
            List<SqlParameter> p = new List<SqlParameter> { new SqlParameter("@Email", email) };
            DataTable dt = bd.devolveSQL(sql, p);
            return int.Parse(dt.Rows[0][0].ToString()) == 0;
        }

        public bool EmailValido(string email)
        {
            if (string.IsNullOrEmpty(email)) return false;
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        public bool PasswordForte(string password)
        {
            if (string.IsNullOrEmpty(password) || password.Length < 8)
                return false;

            return Regex.IsMatch(password, @"[0-9]");
        }
        #endregion
    }
}