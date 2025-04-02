<%@ Page Title="" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="Front.producto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--     <link href="ColumnaLateral.css" rel="stylesheet" />--%>
    <link href="estilos.css" rel="stylesheet" />
    <style>
        body {
            background-image: url('https://www.shutterstock.com/shutterstock/photos/2369360047/display_1500/stock-photo-mockup-wall-in-the-children-s-room-on-wall-cream-color-background-d-rendering-2369360047.jpg');
            background-size: cover;
            background-repeat: no-repeat;
            background-attachment: fixed;
        }
    </style>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="container mt-5">
        <div class="row">
            <div class="col-md-12">
                <h1 class="text-center">Gestion de Productos</h1>
            </div>
        </div>


        <div class="row ">
            <div class="col-md-12 ">
                <h7 class="text-center">Gestione Sus Productos de Manera Ágil y sencilla, Agregue, Modifique o Elimine los Productos que Desee</h7>
            </div>
        </div>






        <!-- Grilla donde van los datos seleccionados -->
        <div class="row mt-5 justify-content-center">
            <a href="Productos/ProductoAgregar.aspx" class="btn btn-primary">Nuevo</a>
            <div class="">
                <div class="table-responsive">
                    <asp:GridView ID="GridViewProductos" runat="server" CssClass="table table-striped table-bordered table-dark w-100" AutoGenerateColumns="False" OnRowCommand="GridViewProductos_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="id" HeaderText="ID" />
                            <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                            <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                            <asp:BoundField DataField="precio" HeaderText="Precio" />
                            <asp:BoundField DataField="margenGanancia" HeaderText="margenGanancia" />
                            <asp:BoundField DataField="Stock" HeaderText="Stock" />
                            <asp:BoundField DataField="Marca.nombre" HeaderText="Marca" />
                            <asp:BoundField DataField="Tipo.nombre" HeaderText="Tipo" />
                            <asp:BoundField DataField="Categoria.nombre" HeaderText="Categoría" />
                            <asp:BoundField DataField="Proveedor.nombre" HeaderText="Proveedor" />
                            <asp:BoundField DataField="Estado" HeaderText="Estado" />


                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnModificar" runat="server" Text="Modificar" CommandName="Modificar" CommandArgument='<%# Eval("id") %>' CssClass="btn btn-success btn-sm" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnDetalle" runat="server" Text="Detalle" CommandName="Detalle" CommandArgument='<%# Eval("id") %>' CssClass="btn btn-info btn-sm" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CommandName="Eliminar" CommandArgument='<%# Eval("id") %>' CssClass="btn btn-danger btn-sm" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>


                </div>
            </div>
        </div>

    </div>
</asp:Content>
