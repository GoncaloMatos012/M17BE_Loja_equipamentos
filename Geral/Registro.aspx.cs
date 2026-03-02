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
            Utilizadores novoUser = new Utilizadores();
            novoUser.Nome = txtNome.Text;
            novoUser.Email = txtEmail.Text;
            novoUser.Password = txtPassword.Text;
            novoUser.Admin = false; // Por defeito, regista como cliente
            novoUser.Adicionar();
            Response.Redirect("Login.aspx");
        }
    }
}