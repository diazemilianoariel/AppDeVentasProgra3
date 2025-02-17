<%@ Page Title="" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="Compras.aspx.cs" Inherits="Front.Compras" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1 class="text-center">Compras a Proveedores</h1>

    <div class="container mt-5">
        <div class="row">
            <div class="col-md-6 offset-md-3">
                <div class="form-group">
                    <asp:Label ID="LabelProveedor" runat="server" AssociatedControlID="DropDownListProveedor" CssClass="form-label fw-bold">Proveedor</asp:Label>
                    <asp:DropDownList ID="DropDownListProveedor" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>

                <div class="form-group">
                    <asp:Label ID="LabelProducto" runat="server" AssociatedControlID="DropDownListProducto" CssClass="form-label fw-bold">Producto</asp:Label>
                    <asp:DropDownList ID="DropDownListProducto" runat="server" CssClass="form-control">
                     
                    </asp:DropDownList>
                </div>

                <div class="form-group">
                    <asp:Label ID="LabelCantidad" runat="server" AssociatedControlID="TextBoxCantidad" CssClass="form-label fw-bold">Cantidad</asp:Label>
                    <asp:TextBox ID="TextBoxCantidad" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                </div>

                <div class="form-group">
                    <asp:Label ID="LabelFecha" runat="server" AssociatedControlID="TextBoxFecha" CssClass="form-label fw-bold">Fecha de Compra</asp:Label>
                    <asp:TextBox ID="TextBoxFecha" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                </div>

                <div class="form-group">
                    <asp:Label ID="LabelPrecio" runat="server" AssociatedControlID="TextBoxPrecio" CssClass="form-label fw-bold">Precio Unitario</asp:Label>
                    <asp:TextBox ID="TextBoxPrecio" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                </div>

                <div class="form-group text-center mt-4">
                    <asp:Button ID="btnRealizarCompra" runat="server" CssClass="btn btn-primary" Text="Realizar Compra" OnClick="btnRealizarCompra_Click" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>
