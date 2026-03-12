using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using M17BE_Loja_equipamentos.Classes;

namespace M17BE_Loja_equipamentos
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarDestaques();
            }
        }

        private void CarregarDestaques()
        {
            //Carregar categorias destaques
            Categoria c = new Categoria();
            rptCategoriaDestaque.DataSource = c.ListaCategoriasDestaque();
            rptCategoriaDestaque.DataBind();


            //Carregar produtos destaques
            Classes.Produto p=new Classes.Produto();//Tá a dar problemas pq vai buscar á pagina aspx ns pq
            rptProdutoDestaque.DataSource = p.ListaDestaques();
            rptProdutoDestaque.DataBind();
        }

        

    }
}