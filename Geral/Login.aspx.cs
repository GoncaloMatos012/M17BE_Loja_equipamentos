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
                string email = txtEmail.Text.Trim();
                if (string.IsNullOrEmpty(email))
                    throw new Exception("Indique o seu email no campo de login.");

                Utilizadores utilizador = new Utilizadores();
                
                string token = Guid.NewGuid().ToString();
                utilizador.GuardarToken(email, token);

                // Configuração da mensagem 
                string link = "https://" + Request.Url.Authority + "/Geral/RecuperarPassword.aspx?token=" + Server.UrlEncode(token);
                string mensagem = $"Clique no link para recuperar a sua password: <a href='{link}'>Recuperar agora</a>";

                
                lblErro.Text = "Se o email existir, receberá instruções de recuperação.";
                lblErro.CssClass = "text-success small mb-2 d-block"; // Muda para verde
                lblErro.Visible = true;
            }
            catch (Exception ex)
            {
                lblErro.Text = "Erro: " + ex.Message;
                lblErro.Visible = true;
            }
        }
    }
}