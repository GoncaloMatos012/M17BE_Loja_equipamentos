<%@ Page Title="Login - GymMarket" Language="C#" MasterPageFile="~/Geral/ClienteMasterPage.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="M17BE_Loja_equipamentos.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://www.google.com/recaptcha/api.js" async defer></script>
    
    <style>
        /*Cartão de Login */
        .card-login { 
            background-color: #000; 
            border: 1px solid #8B0000; 
            color: white; 
            box-shadow: 0 4px 15px rgba(0,0,0,0.5);
        }

        /* Inputs */
        .form-control-custom {
            background-color: #201f1e !important;
            color: white !important;
            border: 1px solid #444 !important;
            transition: border-color 0.3s ease;
        }

        .form-control-custom:focus {
            border-color: #8B0000 !important;
            box-shadow: 0 0 5px rgba(139, 0, 0, 0.5) !important;
            outline: none;
        }

        /* Botão de Login */
        .btn-login { 
            background-color: #8B0000; 
            color: white; 
            border: none; 
            font-weight: bold;
            text-transform: uppercase;
            letter-spacing: 1px;
            transition: 0.3s ease;
        }

        .btn-login:hover { 
            background-color: #570303; 
            color: white; 
            transform: translateY(-2px);
        }

        /* Links e Labels */
        .link-highlight {
            color: #FF4D4D;
            text-decoration: none;
            transition: 0.3s;
        }

        .link-highlight:hover {
            color: #ff1a1a;
            text-decoration: underline;
        }

        /*ReCaptcha */
        .recaptcha-wrapper {
            display: flex;
            justify-content: center;
            margin-bottom: 1rem;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container d-flex justify-content-center align-items-center" style="min-height: 70vh;">
        <div class="card card-login p-4" style="width: 400px;">
            <div class="text-center mb-3">
                <img src="../Public/Img/ICON WEB.png" width="80" class="rounded-circle" />
            </div>
            
            <h2 class="text-center mb-4">Login</h2>
            
            <div class="mb-3">
                <label class="form-label">Email</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control form-control-custom"></asp:TextBox>
            </div>
            
            <div class="mb-3">
                <label class="form-label">Password</label>
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control form-control-custom" TextMode="Password"></asp:TextBox>
            </div>

            <div class="recaptcha-wrapper">
                <div class="g-recaptcha" data-sitekey='<%= System.Configuration.ConfigurationManager.AppSettings["Google.ReCaptcha.Site"] %>' data-theme="dark">></div>
            </div>

            <asp:Label ID="lblErro" runat="server" CssClass="text-danger small mb-3 d-block text-center" Visible="false"></asp:Label>
            
            <asp:Button ID="btnLogin" runat="server" Text="Entrar" CssClass="btn btn-login w-100 mb-3" OnClick="btnLogin_Click" />
            
            <div class="text-center">
                <asp:LinkButton ID="btnRecuperar" runat="server" CssClass="link-highlight small d-block mb-2" OnClick="btnRecuperar_Click">
                    Esqueci-me da palavra-passe
                </asp:LinkButton>
                
                <p class="small text-white-50">Não tem conta? 
                    <a href="Registro.aspx" class="link-highlight">Registe-se aqui</a>
                </p>
            </div>
        </div>
    </div>
</asp:Content>