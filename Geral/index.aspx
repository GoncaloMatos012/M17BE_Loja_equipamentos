<%@ Page Title="" Language="C#" MasterPageFile="~/Geral/ClienteMasterPage.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="M17BE_Loja_equipamentos.index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mb-5">
        <a href="Loja.aspx">
            <div class="hero-banner rounded shadow-lg" style="background: url('../Public/Img/banner-promo.jpg') no-repeat center center; background-size: cover; height: 400px; position: relative;">
                <p>Espaço para a imagem que o chatgpt n me quer fazer</p>
            </div>
        </a>
    </div>

    <div class="container mb-5">
        <h2 class="text-white mb-4 border-start border-danger ps-3" style="border-width: 5px !important;">Produtos em Destaque</h2>
        <div class="row">
            <asp:Repeater ID="rptCategoriaDestaque" runat="server" DataSourceID="">
                <ItemTemplate>
                    <div class="col-md-3 mb-4">
                        <div class="card bg-dark text-white h-100 border-secondary card-produto">
                            <img src='<%# Eval("ImagemURL") %>' class="card-img-top p-2" style="height: 180px; object-fit: contain;">
                            <div class="card-body text-center">
                                <h6 class="text-danger"><%# Eval("Nome") %></h6>
                                <p class="fw-bold"><%# String.Format("{0:C}", Eval("Preco")) %></p>
                                <a href='Produto.aspx?id=<%# Eval("IdProduto") %>' class="btn btn-sm btn-outline-danger">Ver mais</a>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>

    <div class="container mb-5">
        <h2 class="text-white mb-4 border-start border-danger ps-3" style="border-width: 5px !important;">Explorar Categorias</h2>
        <div class="row">
            <asp:Repeater ID="rptProdutoDestaque" runat="server">
                <ItemTemplate>
                    <div class="col-md-4 mb-3">
                        <a href='Loja.aspx?cat=<%# Eval("IdCategoria") %>' class="text-decoration-none">
                            <div class="category-card position-relative overflow-hidden rounded" style="height: 200px;">
                                <img src='<%# Eval("ImagemURL") %>' class="w-100 h-100" style="object-fit: cover; transition: 0.5s;">
                                <div class="position-absolute top-0 start-0 w-100 h-100 d-flex align-items-center justify-content-center" style="background: rgba(0,0,0,0.4);">
                                    <h3 class="text-white fw-bold"><%# Eval("NomeCategoria") %></h3>
                                </div>
                            </div>
                        </a>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
