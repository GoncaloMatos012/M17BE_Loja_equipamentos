using M17BE_Loja_equipamentos.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17BE_Loja_equipamentos
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarDestauqes();
            }
        }

        private void CarregarDestauqes()
        {
            //Carregar categorias destaques
            Categoria c = new Categoria();
            rptCategoriaDestaque.DataSource = c.ListaCategoriasDestaque();
            rptCategorias.DataBind();


            //Carregar produtos destaques
            Produto p = new Produto();
            rptProdutoDestaque.DataSource = p.ListaDestaques();
            rptDestaques.DataBind();
        }

        

    }
}