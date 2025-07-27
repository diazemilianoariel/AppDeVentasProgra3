<%@ Page Title="Gestión de Productos" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="Front.Productos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="estilos.css" rel="stylesheet" />
    <style>
        body {
            background-image: url('https://www.shutterstock.com/shutterstock/photos/2369360047/display_1500/stock-photo-mockup-wall-in-the-children-s-room-on-wall-cream-color-background-d-rendering-2369360047.jpg');
            background-size: cover;
            background-repeat: no-repeat;
            background-attachment: fixed;
        }

        .pagination-ys { padding-left: 0; margin: 20px 0; border-radius: 4px; }
        .pagination-ys table > tbody > tr > td { display: inline; padding: 8px 12px; text-decoration: none; color: #007bff; background-color: #fff; border: 1px solid #ddd; }
        .pagination-ys table > tbody > tr > td > span { z-index: 3; color: #fff; background-color: #007bff; border-color: #007bff; }

    </style>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="text-center">
            <h1 class="mb-3">Gestión de Productos</h1>
            <p class="text-muted">Administre y organice la información de sus Productos de manera sencilla y rápida.</p>
        </div>

        <div class="row mt-4 justify-content-center">
            <%-- MEJORA DE SEGURIDAD: Este botón solo es visible para los Administradores. --%>
            <% if (IsAdmin) { %>
                <%-- CORRECCIÓN: La ruta ahora apunta a la carpeta correcta "ProductosABM". --%>
                <asp:HyperLink NavigateUrl="~/ProductosABM/ProductoAgregar.aspx" runat="server" CssClass="btn btn-primary">Nuevo Producto</asp:HyperLink>
            <% } %>
        </div>

        <div class="row mt-4 justify-content-center">
            <div class="col-md-8">
                <div class="input-group">
                    <asp:TextBox ID="txtFiltro" runat="server" CssClass="form-control" placeholder="Buscar por nombre o descripción..." AutoPostBack="true" OnTextChanged="txtFiltro_TextChanged"></asp:TextBox>
                </div>
            </div>
        </div>


        <asp:Label ID="lblError" runat="server" CssClass="text-danger text-center d-block mt-3" Visible="false"></asp:Label>

        <div class="row mt-4">
            <div class="col">
                <div class="table-responsive">
                    <asp:GridView ID="GridViewProductos" runat="server" CssClass="table table-striped table-bordered table-dark w-100" AutoGenerateColumns="False" OnRowCommand="GridViewProductos_RowCommand"
                      AllowPaging="True" PageSize="5" OnPageIndexChanging="GridViewProductos_PageIndexChanging">

                        <Columns>
                            <asp:BoundField DataField="id" HeaderText="ID" />
                            <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                            <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                            <asp:BoundField DataField="precio" HeaderText="Precio" DataFormatString="{0:C}" />
                            <asp:BoundField DataField="stock" HeaderText="Stock" />
                            <asp:BoundField DataField="Marca.nombre" HeaderText="Marca" />
                            <asp:BoundField DataField="Categoria.nombre" HeaderText="Categoría" />
                            <asp:TemplateField HeaderText="Disponibilidad">
                                <ItemTemplate>
                                    <%# Convert.ToBoolean(Eval("Estado")) ? "Disponible" : "No disponible" %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Acciones">
                                <ItemTemplate>
                                    <asp:Button ID="btnModificar" runat="server" Text="Modificar" CommandName="Modificar" CommandArgument='<%# Eval("id") %>' CssClass="btn btn-success btn-sm" />
                                    <asp:Button ID="btnDetalle" runat="server" Text="Detalle" CommandName="Detalle" CommandArgument='<%# Eval("id") %>' CssClass="btn btn-info btn-sm" />
                                    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CommandName="Eliminar" CommandArgument='<%# Eval("id") %>' CssClass="btn btn-danger btn-sm" Visible="<%# IsAdmin %>" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle CssClass="pagination-ys" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
