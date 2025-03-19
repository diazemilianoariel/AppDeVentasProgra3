<%@ Page Title="" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="Front.producto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"></script>
    <link href="estilos.css" rel="stylesheet" />
    <style>
        body {
            background-color: #f8f9fa;
        }

        .navbar {
            background-color: #007bff;
            padding: 1rem;
        }

            .navbar a {
                color: white;
                margin-right: 1rem;
            }

        .container {
            margin-top: 2rem;
        }

        .table thead th {
            background-color: #e9ecef;
        }

        .table tbody tr:nth-child(odd) {
            background-color: #f8f9fa;
        }
    </style>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="navbar">
        <a href="#">Usuarios</a>
        <a href="#">Paises</a>
        <a href="#">Categorias</a>
        <a href="#">Ciudades</a>
        <a href="#">Marcas</a>
        <a href="#">Productos</a>
        <span class="float-right">Hola Administrador | <a href="#" style="color: white;">Salir</a></span>
    </div>

    <div class="container">
        <h1 class="text-center">Administrador de productos</h1>

        <button type="button" class="btn btn-primary mb-3" data-toggle="modal" data-target="#nuevoProductoModal">
            Nuevo
        </button>

        <div class="row mb-3">
            <div class="col-md-6">
                <label for="recordsPerPage">Mostrar</label>
                <select id="recordsPerPage" class="form-control d-inline-block w-auto">
                    <option>10</option>
                    <option>25</option>
                    <option>50</option>
                </select>
                registros
          
            </div>
            <div class="col-md-6 text-right">
                <label for="search">Buscar:</label>
                <input type="text" id="search" class="form-control d-inline-block w-auto" />
            </div>
        </div>

        <div class="table-responsive">
            <asp:GridView ID="GridViewProductos" runat="server" CssClass="table table-striped table-bordered w-100" AutoGenerateColumns="False" OnRowCommand="GridViewProductos_RowCommand" OnSelectedIndexChanged="GridViewProductos_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                    <asp:BoundField DataField="precio" HeaderText="Precio de Compra"  DataFormatString="{0:N2}" HtmlEncode="false"/>
                    <asp:BoundField DataField="precioVenta" HeaderText="Precio de Venta" DataFormatString="{0:N2}" HtmlEncode="false" />
                    <asp:BoundField DataField="margenGanancia" HeaderText="Margen de Ganancia" DataFormatString="{0:0.00}%" HtmlEncode="false"  />
                    <asp:BoundField DataField="Stock" HeaderText="Stock" />
                    <asp:BoundField DataField="Categoria.nombre" HeaderText="Categoría" />
                    <asp:BoundField DataField="Marca.nombre" HeaderText="Marca" />
                    <asp:BoundField DataField="Tipo.nombre" HeaderText="Tipo" />
                    <asp:BoundField DataField="Proveedor.nombre" HeaderText="Proveedor" />

                    <asp:TemplateField HeaderText="Activo">
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBoxActivo" runat="server" Checked='<%# Eval("Estado") %>' Enabled="false" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:Button ID="btnVerDetalle" runat="server" Text="🔍" CommandName="VerDetalle" CommandArgument='<%#Container.DataItemIndex %>' CssClass="btn btn-info btn-sm" OnClick="BtnVerDetalle_Click" ToolTip="Ver Detalle" />
                            <asp:Button ID="btnEditar" runat="server" Text="✏️" CommandName="Editar" CommandArgument='<%#Container.DataItemIndex %>' CssClass="btn btn-warning btn-sm" ToolTip="Editar" />
                            <asp:Button ID="btnEliminar" runat="server" Text="🗑️" CommandName="Eliminar" CommandArgument='<%#Container.DataItemIndex %>' CssClass="btn btn-danger btn-sm" ToolTip="Eliminar" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>

        <div class="row mt-3">
            <div class="col-md-6">
                Mostrando 1 a 6 de 6 registros
          
            </div>
            <div class="col-md-6 text-right">
                <asp:Button ID="btnAnterior" runat="server" Text="Anterior" CssClass="btn btn-secondary" />
                <asp:Label ID="lblPaginaActual" runat="server" Text="1" CssClass="mx-2" />
                <asp:Button ID="btnSiguiente" runat="server" Text="Siguiente" CssClass="btn btn-secondary" />
            </div>
        </div>
    </div>

    <!-- Modal -->
    <div class="modal fade" id="nuevoProductoModal" tabindex="-1" role="dialog" aria-labelledby="nuevoProductoModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="nuevoProductoModalLabel">Agregar Nuevo Producto</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:TextBox ID="txtNombreProducto" runat="server" CssClass="form-control" placeholder="Nombre" />
                    <asp:TextBox ID="txtDescripcionProducto" runat="server" CssClass="form-control" placeholder="Descripción" />
                    <asp:TextBox ID="txtPrecioProducto" runat="server" CssClass="form-control" placeholder="Precio de Compra" />
                    <asp:TextBox ID="txtmagenGanancia" runat="server" CssClass="form-control" placeholder="Margen de Ganancia" />
                    <asp:TextBox ID="txtImagenProducto" runat="server" CssClass="form-control" placeholder="Imagen" />
                    <asp:TextBox ID="txtStockProducto" runat="server" CssClass="form-control" placeholder="Stock" />
                    <asp:DropDownList ID="ddlCategoriaProducto" runat="server" CssClass="form-control" />
                    <asp:DropDownList ID="ddlMarcaProducto" runat="server" CssClass="form-control" />
                    <asp:DropDownList ID="ddlTipoProducto" runat="server" CssClass="form-control" />
                    <asp:DropDownList ID="ddlProveedoresProducto" runat="server" CssClass="form-control" />



                    <asp:CheckBox ID="chkEstadoProducto" runat="server" Text="Activo" />

                </div>




                <div class="modal-footer">
                    <asp:Button ID="btnGuardarProducto" runat="server" CssClass="btn btn-primary" Text="Guardar" OnClick="btnGuardarProducto_Click" />

                    <asp:Button ID="btnCancelar" runat="server" CssClass="btn btn-secondary" Text="Cancelar" data-dismiss="modal" OnClick="btnCancelar_Click" />



                   
                </div>
            </div>
        </div>
    </div>


</asp:Content>
