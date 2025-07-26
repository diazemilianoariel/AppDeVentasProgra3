<%@ Page Title="" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="ProductoEliminar.aspx.cs" Inherits="Front.ProductosABM.ProductoEliminar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="card mb-4 border-0 shadow-lg">
            <div class="card-body p-4">
                <h2 class="card-title mb-3 text-dark">Eliminar Producto</h2>
                <p class="card-text text-muted mb-4">¿Está seguro que desea eliminar el siguiente producto?</p>

                <div class="row">
                    <%-- Columna para la Imagen --%>
                    <div class="col-md-4 d-flex align-items-center justify-content-center">
                        <asp:Image ID="ImageProducto" runat="server" CssClass="img-fluid rounded shadow-sm" alt="Imagen del Producto" Style="max-height: 250px;" />
                    </div>

                    <%-- Columna para los Detalles --%>
                    <div class="col-md-8">
                        <div class="mb-2">
                            <strong>Nombre: </strong>
                            <asp:Label ID="LabelNombreProducto" runat="server" CssClass="text-secondary"></asp:Label>
                        </div>
                        <div class="mb-2">
                            <strong>Descripción: </strong>
                            <asp:Label ID="LabelDescripcionProducto" runat="server" CssClass="text-secondary"></asp:Label>
                        </div>
                        <div class="mb-2">
                            <strong>Precio: </strong>
                            <asp:Label ID="LabelPrecioProducto" runat="server" CssClass="text-success fs-5"></asp:Label>
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
                    </div>
                </div>

                <asp:Label ID="LabelError" runat="server" CssClass="text-danger d-block text-center mt-3" Visible="false"></asp:Label>

                <div class="d-flex justify-content-end mt-4">
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary mr-2" OnClick="btnCancelar_Click" />
                    <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar Eliminación" CssClass="btn btn-danger" OnClick="btnConfirmar_Click" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>
