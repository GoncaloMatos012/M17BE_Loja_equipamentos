using M17BE_Loja_equipamentos.Classes; 
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17BE_Loja_equipamentos
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Se já estiver logado, redireciona para a home
            if (Session["UserID"] != null)
            {
                RedirecionarUtilizador((bool)Session["IsAdmin"]);
            }
            lblErro.Visible = false;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {

            lblErro.Visible = false;
            lblErro.CssClass = "text-danger small mb-3 d-block text-center"; // fica vermelho
                             
            // Validar campos vazios
            if (string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                lblErro.Text = "Por favor, preencha todos os campos.";
                lblErro.Visible = true;
                return;
            }

            
            Utilizadores user = new Utilizadores();
            user.Email = txtEmail.Text;
            user.Password = txtPassword.Text;

            var resposta = Request.Form["g-recaptcha-response"];
            var validou = ReCaptcha.Validate(resposta);

            if (validou == false) {
                lblErro.Text = "Por favor, confirme que não é um robô.";
                lblErro.CssClass = "text-danger small mb-3 d-block text-center";
                lblErro.Visible = true;
                return;
            }
            if (user.VerificaLogin()) 
            {
                //cria os dados da sessao
                Session["UserID"] = user.IdUtilizador;
                Session["Nome"] = user.Nome;
                Session["Email"] = user.Email;
                Session["IsAdmin"] = user.Admin;

                
                Session["ip"] = Request.UserHostAddress;
                Session["useragent"] = Request.UserAgent;

                
                RedirecionarUtilizador(user.Admin);
            }
            else
            {
                lblErro.Text = "Login falhou. Email ou Password incorretos.";
                lblErro.Visible = true;
            }
        }

        private void RedirecionarUtilizador(bool isAdmin)
        {
            if (isAdmin)
                Response.Redirect("../Admin/Dashboard.aspx");
            else
                Response.Redirect("index.aspx");
        }


        protected void btnRecuperar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtEmail.Text.Trim().Length == 0)
                    throw new Exception("Indique um email");
                string email = txtEmail.Text.Trim();
                //Verificar se existe o email
                Classes.Utilizadores utilizador = new Classes.Utilizadores();
                DataTable dados = utilizador.devolveDadosUtilizador(email);
                if (dados == null || dados.Rows.Count != 1)
                    throw new Exception("Caso o email indicado exista, foi enviada uma mensagem para recuperação da palavra passe.");
                //TOKEN => GUID
                Guid guid = Guid.NewGuid();
                string token=guid.ToString();
                utilizador.recuperarPassword(email, token);
                //Criar uma mensagem
                string mensagem = "Clique no link para recuperar a sua password.<br/>";
                mensagem += "<a href='https://" + Request.Url.Authority + "/recuperarpassword.aspx?";
                mensagem += "token=" + Server.UrlEncode(token) + "'>Clique aqui</a>";

                //Enviar a mensagem
                string meuemail = ConfigurationManager.AppSettings["email"].ToString();
                string palavrapasse = ConfigurationManager.AppSettings["email_password"].ToString();
                Helper.enviarMail(meuemail, palavrapasse, email,
                    "Recuperação de palavra passe", mensagem);
                lblErro.Text = @"Caso o email indicado exista, 
                  foi enviada uma mensagem para recuperação da palavra passe.";
            }
            catch (Exception ex)
            {
                
                lblErro.Text = ex.Message;
            }
        }

        protected void btnRecuperar_Click1(object sender, EventArgs e)
        {
            try
            {
                if (txtEmail.Text.Trim().Length == 0)
                    throw new Exception("Indique um email");
                string email = txtEmail.Text.Trim();
                //Verificar se existe o email
                Classes.Utilizadores utilizador = new Classes.Utilizadores();
                DataTable dados = utilizador.devolveDadosUtilizador(email);
                if (dados == null || dados.Rows.Count != 1)
                    throw new Exception("Caso o email indicado exista, foi enviada uma mensagem para recuperação da palavra passe.");
                //TOKEN => GUID
                Guid guid = Guid.NewGuid();
                string token = guid.ToString();
                utilizador.recuperarPassword(email, token);
                //Criar uma mensagem
                string mensagem = "Clique no link para recuperar a sua password.<br/>";
                mensagem += "<a href='https://" + Request.Url.Authority + "/recuperarpassword.aspx?";
                mensagem += "token=" + Server.UrlEncode(token) + "'>Clique aqui</a>";

                //Enviar a mensagem
                string meuemail = ConfigurationManager.AppSettings["email"].ToString();
                string palavrapasse = ConfigurationManager.AppSettings["email_password"].ToString();
                Helper.enviarMail(meuemail, palavrapasse, email,
                    "Recuperação de palavra passe", mensagem);
                lblErro.Text = @"Caso o email indicado exista, 
                  foi enviada uma mensagem para recuperação da palavra passe.";
                lblErro.Visible = true;
            }
            catch (Exception ex)
            {
                lblErro.Visible = true;
                lblErro.Text = ex.Message;
            }

        }
    }
}