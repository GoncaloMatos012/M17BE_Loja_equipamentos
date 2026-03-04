using M17BE_Loja_equipamentos.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17BE_Loja_equipamentos
{
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


        }

        protected void btnRegistar_Click(object sender, EventArgs e)
        {

            try
            {
                Utilizadores novoUser = new Utilizadores();
                novoUser.Nome = txtNome.Text;
                novoUser.Email = txtEmail.Text;
                novoUser.Password = txtPassword.Text;
                novoUser.Adicionar();
                Response.Redirect("Login.aspx");
            }
            catch (Exception ex)
            {
                lblErro.Text = ex.Message;
                lblErro.Visible = true;
                lblErro.CssClass = "text-danger small mb-2 d-block"; // Garante que fica vermelho
            }

        }
    }
}