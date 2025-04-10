<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="SeguritycJHJ.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div><h1>El usuario <%=Session["usuario"] %> ingresó</h1>
            <asp:Button ID="btnLogout" runat="server" Text="Salir" OnClick="btnLogout_Click" />
        </div>
    </form>
</body>
</html>
