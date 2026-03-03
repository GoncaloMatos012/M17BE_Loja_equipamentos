using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17BE_Loja_equipamentos
{
    public partial class ClienteMasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MarcarBotaoAtivo();
                VerificarEstadoLogin();
            }
        }

        private void VerificarEstadoLogin()
        {
            // Verifica se a variável de sessão 'UserID' existe
            bool estaLogado = Session["UserID"] != null;

            // Altera a visibilidade dos itens da dp
            dp_Logado.Visible = estaLogado;
            dp_Visitante.Visible = !estaLogado; 
        }

        protected void btnSair_Click(object sender, EventArgs e)
        {
            // Limpa todos os dados da sessão 
            Session.Clear();
            Session.Abandon();

            // Redireciona para a página inicial ou login
            Response.Redirect("index.aspx");
        }

        private void MarcarBotaoAtivo()
        {
            // Obtém o nome da página atual do URL (ex: carrinho.aspx)
            string paginaAtual = System.IO.Path.GetFileName(Request.Url.AbsolutePath).ToLower();

            // Reset de classes (garante que todos voltam ao estado normal text-white)
            btnHome.CssClass = "nav-link text-white mb-1";
            btnProdutos.CssClass = "nav-link text-white mb-1";
            btnFavoritos.CssClass = "nav-link text-white mb-1";
            btnCarrinho.CssClass = "nav-link text-white mb-1";

            // Aplica a classe 'active' 
            switch (paginaAtual)
            {
                case "index.aspx":
                case "default.aspx":
                    btnHome.CssClass = "nav-link active mb-1";
                    break;
                case "Produto.aspx":
                case "Loja.aspx":
                    btnProdutos.CssClass = "nav-link active mb-1";
                    break;
                case "Carrinho.aspx":
                    btnCarrinho.CssClass = "nav-link active mb-1";
                    break;
                case "Favoritos.aspx":
                    btnFavoritos.CssClass = "nav-link active mb-1";
                    break;
            }
        }

        protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("index.aspx");
        }

        protected void btnLoja_Click(object sender, EventArgs e)
        {

            Response.Redirect("Produtos.aspx");
        }

        protected void btnFavoritos_Click(object sender, EventArgs e)
        {
            Response.Redirect("Favoritos.aspx");
        }

        protected void btnCarrinho_Click(object sender, EventArgs e)
        {
            Response.Redirect("Carrinho.aspx");
        }

        
    }
}