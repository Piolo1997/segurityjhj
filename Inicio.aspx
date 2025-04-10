<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="SeguritycJHJ.Inicio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="main" runat="server">
    <h1>Página inicial</h1>

    <!-- Agregar el Label donde se mostrará el saludo -->
    <div>
        <asp:Label ID="lblSaludo" runat="server"></asp:Label>
    </div>

</asp:Content>
