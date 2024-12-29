<%@ Page Title="Cargar Compras" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="Vendedor.aspx.cs" Inherits="Front.Vendedor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Cargar Compras del Cliente</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Cargar Compras del Cliente</h1>
    <asp:Label ID="LabelMensaje" runat="server" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="LabelCliente" runat="server" Text="Cliente:"></asp:Label>
    <asp:TextBox ID="TextBoxCliente" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="LabelProducto" runat="server" Text="Producto:"></asp:Label>
    <asp:TextBox ID="TextBoxProducto" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="LabelCantidad" runat="server" Text="Cantidad:"></asp:Label>
    <asp:TextBox ID="TextBoxCantidad" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="LabelFecha" runat="server" Text="Fecha:"></asp:Label>
    <asp:TextBox ID="TextBoxFecha" runat="server"></asp:TextBox>
    <br />
    <asp:Button ID="ButtonCargarCompra" runat="server" Text="Cargar Compra" OnClick="ButtonCargarCompra_Click" />
</asp:Content>
