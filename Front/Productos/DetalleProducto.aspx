<%@ Page Title="Detalle de Producto" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="DetalleProducto.aspx.cs" Inherits="Front.DetalleProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
        <link rel="stylesheet" type="text/css" href="~/EstiloCSS/estilo.css" />

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">


        <div class="card mb-4 border-0 shadow-lg">
            <div class="row g-0">
                <div class="col-lg-5 d-flex align-items-center">
                    <asp:Image ID="ImageProducto" runat="server" CssClass="img-fluid rounded" alt="Imagen del Producto" Style="object-fit: cover; height: 100%;" />
                </div>
                <div class="col-lg-7">
                    <div class="card-body p-4">
                        <h2 class="card-title mb-3 text-dark">
                            <asp:Label ID="LabelNombreProducto" runat="server" CssClass="fw-bold"></asp:Label>
                        </h2>
                        <p class="card-text text-muted mb-4">
                            <asp:Label ID="LabelDescripcionProducto" runat="server"></asp:Label>
                        </p>
                        <div class="mb-2">
                            <strong>Precio: </strong>
                            <asp:Label ID="LabelPrecioProducto" runat="server" CssClass="text-success fs-4"></asp:Label>
                        </div>
                        <div class="mb-2">
                            <strong>Stock: </strong>
                            <asp:Label ID="LabelStockProducto" runat="server" CssClass="text-secondary"></asp:Label>
                        </div>
                        <div class="mb-2">
                            <strong>Marca: </strong>
                            <asp:Label ID="LabelMarcaProducto" runat="server" CssClass="text-secondary"></asp:Label>
                        </div>
                        <div class="mb-2">
                            <strong>Tipo: </strong>
                            <asp:Label ID="LabelTipoProducto" runat="server" CssClass="text-secondary"></asp:Label>
                        </div>
                        <div class="mb-2">
                            <strong>Categoría: </strong>
                            <asp:Label ID="LabelCategoriaProducto" runat="server" CssClass="text-secondary"></asp:Label>
                        </div>
                        <div class="mb-2">
                            <strong>Proveedor: </strong>
                            <asp:Label ID="LabelProveedorProducto" runat="server" CssClass="text-secondary"></asp:Label>
                        </div>
                        <div class="mb-2">
                            <strong>Estado: </strong>
                            <asp:Label ID="LabelEstadoProducto" runat="server" CssClass="text-secondary"></asp:Label>
                        </div>
                        <p class="card-text mt-3">
                            <small class="text-muted">Última actualización hace 3 minutos</small>
                        </p>
                        <asp:Label ID="LabelError" runat="server" CssClass="text-danger" Visible="false"></asp:Label>

                        <asp:Button ID="btnVolver" runat="server" Text="Volver" CssClass="btn btn-primary mt-3" OnClick="btnVolver_Click" />


                       <%-- <asp:Button ID="btnAgregarCarrito" runat="server" Text="Agregar al Carrito" CommandArgument='<%# Eval("Id") %>' OnClick="btnAgregarCarrito_Click" CssClass="btn btn-success" />--%>

                        <div class="input-group mb-3">
                            <div class="input-group-prepend">
                                <asp:Button ID="btnDisminuir" runat="server" Text="-" OnClick="btnDisminuir_Click" CssClass="btn btn-outline-secondary" />
                            </div>

                            <asp:TextBox ID="txtCantidad" runat="server" Text="1" CssClass="form-control text-center" ReadOnly="true" />


                            <div class="input-group-append">
                                <asp:Button ID="btnAumentar" runat="server" Text="+" OnClick="btnAumentar_Click" CssClass="btn btn-outline-secondary" />
                            </div>
                               <asp:Button ID="btnAgregarCarrito" runat="server" Text="Agregar al Carrito" OnClick="btnAgregarCarrito_Click" CssClass="btn btn-success" />


                        </div>

                        <%--  <asp:Button ID="btnQuitarCarrito" runat="server" Text="Quitar del Carrito" CommandArgument='<%# Eval("Id") %>' OnClick="btnQuitarDelCarrito_Click" CssClass="btn btn-danger" />--%>
                    </div>
                </div>
            </div>
        </div>



    </div>
</asp:Content>
