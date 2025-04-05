<%@ Page Title="" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="ProductoAgregar.aspx.cs" Inherits="Front.Productos.ProductoAgregar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="row">
            <div class="col-md-12">
                <h1 class="text-center">Agregar Producto</h1>
            </div>
        </div>

        <div class="row mt-4">
            <div class="col-md-6">
                <div class="form-group">
                    <asp:Label ID="LabelNombre" runat="server" CssClass="form-label fw-bold">Nombre:</asp:Label>
                    <asp:TextBox ID="TextBoxNombre" runat="server" CssClass="form-control"></asp:TextBox>

                    <asp:Label ID="LabelDescripcion" runat="server" CssClass="form-label fw-bold">Descripción:</asp:Label>
                    <asp:TextBox ID="TextBoxDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4"></asp:TextBox>

                    <asp:Label ID="LabelPrecio" runat="server" CssClass="form-label fw-bold">Precio:</asp:Label>
                    <asp:TextBox ID="TextBoxPrecio" runat="server" CssClass="form-control"></asp:TextBox>

                    <asp:Label ID="LabelGanancia" runat="server" CssClass="form-label fw-bold">Margen De Ganancia:</asp:Label>
                    <asp:TextBox ID="TextBoxGanancia" runat="server" CssClass="form-control"></asp:TextBox>

                    <asp:Label ID="LabelStock" runat="server" CssClass="form-label fw-bold">Stock:</asp:Label>
                    <asp:TextBox ID="TextBoxStock" runat="server" CssClass="form-control"></asp:TextBox>

                    <asp:Label ID="LabelMarca" runat="server" CssClass="form-label fw-bold">Marca:</asp:Label>
                    <asp:DropDownList ID="DropDownListMarca" runat="server" CssClass="form-control"></asp:DropDownList>

                    <asp:Label ID="LabelTipo" runat="server" CssClass="form-label fw-bold">Tipo:</asp:Label>
                    <asp:DropDownList ID="DropDownListTipo" runat="server" CssClass="form-control"></asp:DropDownList>

                    <asp:Label ID="LabelCategoria" runat="server" CssClass="form-label fw-bold">Categoría:</asp:Label>
                    <asp:DropDownList ID="DropDownListCategoria" runat="server" CssClass="form-control"></asp:DropDownList>

                    <asp:Label ID="LabelProveedor" runat="server" CssClass="form-label fw-bold">Proveedor:</asp:Label>
                    <asp:DropDownList ID="DropDownListProveedor" runat="server" CssClass="form-control"></asp:DropDownList>

                    <asp:Label ID="LabelEstado" runat="server" CssClass="form-label fw-bold">Producto Disponible:</asp:Label>
                    <asp:CheckBox ID="CheckBoxEstado" runat="server" CssClass="form-control" />
                </div>
            </div>

            <div class="col-md-6">
                <div class="form-group">
                    <asp:Label ID="LabelImagen" runat="server" CssClass="form-label fw-bold">Imagen URL:</asp:Label>
                    <asp:TextBox ID="TextBoxImagen" runat="server" CssClass="form-control"></asp:TextBox>
                    <img id="ImagenProducto" runat="server" class="img-fluid mt-3" alt="Imagen del Producto" style="max-width: 100%; height: auto;" />
                </div>
            </div>
        </div>

        <div class="row mt-4">
            <div class="col-md-12 text-center">
                <asp:Button ID="ButtonGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="ButtonGuardar_Click" />
                <asp:Button ID="ButtonCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="ButtonCancelar_Click" />
            </div>
        </div>
    </div>



</asp:Content>
