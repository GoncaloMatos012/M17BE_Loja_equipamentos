<%@ Page Title="Registo - GymMarket" Language="C#" MasterPageFile="~/Geral/ClienteMasterPage.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="M17BE_Loja_equipamentos.Registro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .card-register { background-color: #000; border: 1px solid #8B0000; color: white; }
        .btn-register { background-color: #8B0000; color: white; border: none; }
        .btn-register:hover { background-color: #570303; color: white; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container d-flex justify-content-center align-items-center" style="min-height: 70vh;">
        <div class="card card-register p-4" style="width: 400px;">
            <h2 class="text-center mb-4">Criar Conta</h2>
            <div class="mb-3">
                <label class="form-label">Nome Completo</label>
                <asp:TextBox ID="txtNome" runat="server" CssClass="form-control bg-dark text-white border-secondary"></asp:TextBox>
            </div>
            <div class="mb-3">
                <label class="form-label">Email</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control bg-dark text-white border-secondary" TextMode="Email"></asp:TextBox>
            </div>
            <div class="mb-3">
                <label class="form-label">Password</label>
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control bg-dark text-white border-secondary" TextMode="Password"></asp:TextBox>
            </div>
            <asp:Label ID="lblErro" runat="server" CssClass="text-danger small mb-3 d-block text-center" Visible="false"></asp:Label>
            <asp:Button ID="btnRegistar" runat="server" Text="Registar" CssClass="btn btn-register w-100 mb-3" OnClick="btnRegistar_Click" />
            <p class="text-center small">Já tem conta? <a href="Login.aspx" style="color: #FF4D4D;">Faça Login</a></p>
        </div>
    </div>
</asp:Content>