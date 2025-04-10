<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Minuta.aspx.cs" Inherits="SeguritycJHJ.minuta.Minuta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="../resources/css/Tablas.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="main" runat="server">
    <h1>Registros de Minuta</h1>
    <h2>
        <asp:HyperLink ID="hlRegistrarMinuta" runat="server" NavigateUrl="~/Minuta/Crear.aspx">Crear Registro de Minuta</asp:HyperLink></h2>
    <asp:GridView ID="gvDatosMinuta" runat="server" AutoGenerateColumns="false" CssClass="custom-grindview" HeaderStyle-CssClass="header" RowStyle-CssClass="row" AlternatingRowStyle-CssClass="alt-row" OnRowCommand="gvDatosMinuta_RowCommand" DataKeyNames="Num_Minuta">
        <Columns>
            <asp:BoundField DataField="Num_Minuta" HeaderText="Número de Minuta" />
            <asp:BoundField DataField="Usuario_Id_Usuario" HeaderText="ID Usuario" />
            <asp:BoundField DataField="Primer_Apellido" HeaderText="Primer Apellido" />
            <asp:BoundField DataField="Primer_Nombre" HeaderText="Primer Nombre" />
            <asp:BoundField DataField="Fecha_Registro" HeaderText="Fecha de Registro" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="false" />
            <asp:BoundField DataField="Hora" HeaderText="Hora" />
            <asp:BoundField DataField="Tipo_Minuta" HeaderText="Tipo de Minuta" />
            <asp:BoundField DataField="Observaciones" HeaderText="Observaciones" />
            <asp:TemplateField HeaderText="Soporte">
                <ItemTemplate>
                    <!-- Imagen para vista previa -->
                    <a href='<%# Eval("Num_Minuta", "~/Minuta/SoporteHandler.ashx?Num_Minuta={0}") %>' target="_blank">
                        <asp:Image
                            ID="imgSoporte"
                            runat="server"
                            ImageUrl='<%# Eval("Num_Minuta", "~/Minuta/SoporteHandler.ashx?Num_Minuta={0}") %>'
                            AlternateText="Sin imagen"
                            Visible='<%# Eval("Soporte") != DBNull.Value && Eval("Soporte") != null %>'
                            Width="100px" Height="100px" />
                    </a>

                    <!-- Enlace para descargar el archivo -->
                    <br />
                    <asp:HyperLink
                        ID="hlDescargar"
                        runat="server"
                        NavigateUrl='<%# Eval("Num_Minuta", "~/Minuta/SoporteHandler.ashx?Num_Minuta={0}&descargar=true") %>'
                        Text="Descargar Soporte"
                        Visible='<%# Eval("Soporte") != DBNull.Value && Eval("Soporte") != null %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Acciones">
                <ItemTemplate>
                    <asp:LinkButton ID="lbActualizar" runat="server" Text="Actualizar" CommandName="Actualizar" CommandArgument='<%# Eval("Num_Minuta") %>' />
                    &nbsp;&nbsp;
                    <asp:LinkButton ID="lbEliminar" runat="server" Text="Eliminar" CommandName="Eliminar" CommandArgument='<%# Eval("Num_Minuta") %>' OnClientClick="return confirm('¿Esta seguro de eliminar el registro?');" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
