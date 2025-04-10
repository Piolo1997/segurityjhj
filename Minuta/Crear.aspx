<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Crear.aspx.cs" Inherits="SeguritycJHJ.Minuta.Crear" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="main" runat="server">
    <h2>
        <asp:Label ID="lblAccion" runat="server" Text="" /></h2>
    <table>
        <tr>
            <td>
                <asp:Label ID="lblNumMinuta" runat="server" Text="Número de minuta" AssociatedControlID="txtNumMinuta"></asp:Label></td>
            <td>
                <asp:TextBox ID="txtNumMinuta" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblIdUsuario" runat="server" Text="Usuario (ID Usuario)" AssociatedControlID="txtIdUsuario"></asp:Label></td>
            <td>
                <asp:TextBox ID="txtIdUsuario" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator
                    ID="rfvIdUsuario"
                    runat="server"
                    ErrorMessage="EL ID de usuario es obligatorio"
                    ForeColor="Red"
                    Display="Dynamic"
                    ControlToValidate="txtIdUsuario" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblFechaRegistro" runat="server" Text="Fecha de registro" AssociatedControlID="txtFechaRegistro"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtFechaRegistro" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator
                    ID="rfvFechaRegistro"
                    runat="server"
                    ErrorMessage="La fecha de registro es obligatoria"
                    ForeColor="Red"
                    Display="Dynamic"
                    ControlToValidate="txtFechaRegistro" />
                <ajaxToolkit:CalendarExtender
                    ID="calFechaRegistro"
                    runat="server"
                    TargetControlID="txtFechaRegistro"
                    Format="yyyy-MM-dd" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblHoraRegistro" runat="server" Text="Hora de registro" AssociatedControlID="txtHoraRegistro"></asp:Label>
            </td>
            <td>
                <asp:TextBox
                    ID="txtHoraRegistro"
                    runat="server"
                    TextMode="Time"
                    CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator
                    ID="rfvHoraRegistro"
                    runat="server"
                    ErrorMessage="La hora de registro es obligatoria"
                    ForeColor="Red"
                    Display="Dynamic"
                    ControlToValidate="txtHoraRegistro" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblTipoMinuta" runat="server" Text="Tipo de Minuta:" AssociatedControlID="ddlTipoMinuta"></asp:Label></td>
            <td>
                <asp:DropDownList ID="ddlTipoMinuta" runat="server"></asp:DropDownList>
                <asp:RequiredFieldValidator
                    ID="rfvTipoMinuta"
                    runat="server"
                    ErrorMessage="Por favor, seleccione un tipo de minuta"
                    ForeColor="Red"
                    Display="Dynamic"
                    ControlToValidate="ddlTipoMinuta"
                    InitialValue="0" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblObservaciones" runat="server" Text="Observaciones" AssociatedControlID="txtObservaciones"></asp:Label>
            </td>
            <td>
                <asp:TextBox
                    ID="txtObservaciones"
                    runat="server"
                    TextMode="MultiLine"
                    Rows="5"
                    Columns="40"
                    MaxLength="500">
                </asp:TextBox>
                <asp:RequiredFieldValidator
                    ID="rfvObservaciones"
                    runat="server"
                    ErrorMessage="Las observaciones son obligatorias"
                    ForeColor="Red"
                    Display="Dynamic"
                    ControlToValidate="txtObservaciones">
                </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblArchivosSoporte" runat="server" Text="Archivos de Soporte" AssociatedControlID="fuArchivosSoporte"></asp:Label>
            </td>
            <td>
                <asp:FileUpload
                    ID="fuArchivosSoporte"
                    runat="server"  
                    AllowMultiple="true"></asp:FileUpload>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: left;">
                <asp:Button ID="btnCreate" runat="server" Text="Regirtrar minuta" OnClick="btnCreate_Click" UseSubmitBehavior="true" Visible="false" />
                <asp:Button ID="btnActualizar" runat="server" Text="Actualizar minuta" OnClick="btnActualizar_Click" UseSubmitBehavior="true" Visible="false" />
                <asp:Button ID="btnVolver" runat="server" Text="Volver" UseSubmitBehavior="true" OnClientClick="window.location.href='Minuta.aspx'; return false;" />
            </td>
        </tr>
    </table>
</asp:Content>
