using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace M17BE_Loja_equipamentos.Classes
{
    public class Categoria
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public bool Destaque { get; set; }
        public string ImagemURL { get; set; }
        BaseDados bd;

        public Categoria()
        {
            bd = new BaseDados();
        }

        public DataTable ListaCategoriasDestaque()
        {
            string sql = "SELECT IdCategoria, NomeCategoria, ImagemURL FROM Categorias WHERE Destaque = 1";
            return bd.devolveSQL(sql);
        }

        
        public DataTable ListarCategorias()
        {
            string sql = "SELECT * FROM Categorias ORDER BY NomeCategoria ASC";
            return bd.devolveSQL(sql);
        }
    }
}