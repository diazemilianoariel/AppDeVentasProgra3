<%@ Page Title="" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="Buscar.aspx.cs" Inherits="Front.Buscar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Resultados de Búsqueda</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="row">
            <div class="col-md-12">
                <h1 class="text-center">Resultados de Búsqueda</h1>
            </div>
        </div>

        <!-- Agregar TextBox para la búsqueda -->
        <div class="row mt-3">
            <div class="col-md-12">
                <asp:TextBox ID="TextBoxBuscar" runat="server" CssClass="form-control" placeholder="Escribe tu búsqueda aquí"></asp:TextBox>
                <asp:Button ID="ButtonBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="ButtonBuscar_Click" />
                <asp:Button ID="ButtonLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-secondary" OnClick="ButtonLimpiar_Click" />
            </div>
        </div>

        <!-- Formulario ABM -->
        <div class="container mt-5">
            <div class="row">
                <div class="col-md-6">
                    <div class="form-group">
                        <asp:Label ID="LabelNombre" runat="server" AssociatedControlID="TextBoxNombre" CssClass="form-label fw-bold">Nombre</asp:Label>
                        <asp:TextBox ID="TextBoxNombre" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:Label ID="LabelDescripcion" runat="server" AssociatedControlID="TextBoxDescripcion" CssClass="form-label fw-bold">Descripción</asp:Label>
                        <asp:TextBox ID="TextBoxDescripcion" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control"></asp:TextBox>
                        <asp:Label ID="LabelPrecio" runat="server" AssociatedControlID="TextBoxPrecio" CssClass="form-label fw-bold">Precio</asp:Label>
                        <asp:TextBox ID="TextBoxPrecio" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:Label ID="LabelImagen" runat="server" AssociatedControlID="TextBoxImagen" CssClass="form-label fw-bold">Imagen URL</asp:Label>
                        <asp:TextBox ID="TextBoxImagen" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:Label ID="LabelStock" runat="server" AssociatedControlID="TextBoxStock" CssClass="form-label fw-bold">Stock</asp:Label>
                        <asp:TextBox ID="TextBoxStock" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="col-md-6">
                    <div class="form-group">
                        <asp:Label ID="LabelMarca" runat="server" AssociatedControlID="TextBoxMarca" CssClass="form-label fw-bold">Marca</asp:Label>
                        <asp:TextBox ID="TextBoxMarca" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:Label ID="LabelTipo" runat="server" AssociatedControlID="TextBoxTipo" CssClass="form-label fw-bold">Tipo</asp:Label>
                        <asp:TextBox ID="TextBoxTipo" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:Label ID="LabelCategoria" runat="server" AssociatedControlID="TextBoxCategoria" CssClass="form-label fw-bold">Categoría</asp:Label>
                        <asp:TextBox ID="TextBoxCategoria" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:Label ID="LabelProveedor" runat="server" AssociatedControlID="TextBoxProveedor" CssClass="form-label fw-bold">Proveedor</asp:Label>
                        <asp:TextBox ID="TextBoxProveedor" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:Label ID="LabelEstado" runat="server" AssociatedControlID="TextBoxEstado" CssClass="form-label fw-bold">Estado</asp:Label>
                        <asp:TextBox ID="TextBoxEstado" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
