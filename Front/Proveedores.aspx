<%@ Page Title="" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="Proveedores.aspx.cs" Inherits="Front.Proveedores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                <h1 class="text-center">Gestión de Proveedores</h1>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <h7 class="text-center">Gestione Sus Proveedores de Manera Ágil y sencilla, Agregue, Modifique o Elimine los Proveedores que Desee</h7>
            </div>
        </div>

        <!-- Botón para agregar nuevo proveedor -->
        <div class="row mt-5 justify-content-center">
            <a href="ProveedoresABM/ProveedorAgregar.aspx" class="btn btn-primary">Nuevo</a>
        </div>

        <!-- Tabla de proveedores -->
        <div class="row mt-5">
            <div class="col-md-12">
                <div class="table-responsive">
                    <asp:GridView ID="GridViewProveedores" runat="server" CssClass="table table-striped table-bordered table-dark w-100" AutoGenerateColumns="False" OnRowCommand="GridViewProveedores_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="id" HeaderText="ID" />
                            <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                            <asp:BoundField DataField="direccion" HeaderText="Dirección" />
                            <asp:BoundField DataField="telefono" HeaderText="Teléfono" />
                            <asp:BoundField DataField="email" HeaderText="Email" />
                            <asp:BoundField DataField="estado" HeaderText="Estado" />


                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnModificar" runat="server" Text="Modificar" CommandName="Modificar" CommandArgument='<%# Eval("id") %>' CssClass="btn btn-success btn-sm" />
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
