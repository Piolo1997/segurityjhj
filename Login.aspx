<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SeguritycJHJ.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Inicio de sesión</title>
    <link rel="stylesheet" type="text/css" href="resources/css/login.css" />
</head>
<body>
    <div class="login-container">
        <form id="form1" runat="server" class="login-form">
            <div class="logo-container">
                <img src="resources/Imágenes/JHJ.png" alt="Logo" class="logo" />
            </div>

            <div class="input-group">
                <asp:Label ID="lblUsuario" runat="server" Text="Correo Electrónico"></asp:Label>
                <asp:TextBox ID="txtUsuario" runat="server" AutoCompleteType="Disabled" CssClass="input-box"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvUsuario" runat="server" ErrorMessage="El Correo electrónico es obligatorio" ControlToValidate="txtUsuario" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>

            <div class="input-group">
                <asp:Label ID="lblPassword" runat="server" Text="Contraseña"></asp:Label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="input-box"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvPasssword" runat="server" ErrorMessage="La contraseña es obligatoria" ControlToValidate="txtPassword" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>

            <div class="button-group">
                <asp:Button ID="btnIngresar" runat="server" Text="Iniciar Sesión" OnClick="btnIngresar_Click" CssClass="button" />
            </div>

            <div class="status-message">
                <asp:Label ID="lblEstado" runat="server" ForeColor="Red"></asp:Label>
            </div>
        </form>
    </div>
</body>
</html>
