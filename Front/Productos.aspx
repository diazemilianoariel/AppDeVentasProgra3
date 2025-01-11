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
        <div class="container mt-5">
            <div class="row">
                <!-- Columna izquierda con los TextBoxes y Labels -->
                <div class="col-md-6">
                    <div class="form-group">
                        <asp:TextBox ID="TextBoxId" runat="server" CssClass="form-control" ReadOnly="true" Visible="false"></asp:TextBox>

                        <asp:Label ID="LabelNombre" runat="server" AssociatedControlID="TextBoxNombre" CssClass="form-label fw-bold">Nombre</asp:Label>
                        <asp:TextBox ID="TextBoxNombre" runat="server" CssClass="form-control"></asp:TextBox>

                        <asp:Label ID="LabelDescripcion" runat="server" AssociatedControlID="TextBoxDescripcion" CssClass="form-label fw-bold">Descripción</asp:Label>
                        <asp:TextBox ID="TextBoxDescripcion" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control"></asp:TextBox>

                        <asp:Label ID="LabelPrecio" runat="server" AssociatedControlID="TextBoxPrecio" CssClass="form-label fw-bold">Precio</asp:Label>
                        <asp:TextBox ID="TextBoxPrecio" runat="server" CssClass="form-control"></asp:TextBox>


                        <asp:Label ID="LabelGanancia" runat="server" AssociatedControlID="TextBoxGanancia" CssClass="form-label fw-bold">Margen De Ganancia</asp:Label>

                        <asp:TextBox ID="TextBoxGanancia" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="TextBoxGanancia_TextChanged"></asp:TextBox>





                        <asp:Label ID="LabelImagen" runat="server" AssociatedControlID="TextBoxImagen" CssClass="form-label fw-bold">Imagen URL</asp:Label>
                        <asp:TextBox ID="TextBoxImagen" runat="server" CssClass="form-control"></asp:TextBox>

                        <asp:Label ID="LabelStock" runat="server" AssociatedControlID="TextBoxStock" CssClass="form-label fw-bold">Stock</asp:Label>
                        <asp:TextBox ID="TextBoxStock" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <!-- Columna derecha con los DropDownList y Labels -->
                <div class="col-md-6">
                    <div class="form-group">
                        <asp:Label ID="LabelMarca" runat="server" AssociatedControlID="DropDownListMarca" CssClass="form-label fw-bold">Marca</asp:Label>
                        <asp:DropDownList ID="DropDownListMarca" runat="server" CssClass="form-control"></asp:DropDownList>

                        <asp:Label ID="LabelTipo" runat="server" AssociatedControlID="DropDownListTipo" CssClass="form-label fw-bold">Tipo</asp:Label>
                        <asp:DropDownList ID="DropDownListTipo" runat="server" CssClass="form-control"></asp:DropDownList>

                        <asp:Label ID="LabelCategoria" runat="server" AssociatedControlID="DropDownListCategoria" CssClass="form-label fw-bold">Categoría</asp:Label>
                        <asp:DropDownList ID="DropDownListCategoria" runat="server" CssClass="form-control"></asp:DropDownList>

                        <asp:Label ID="LabelProveedor" runat="server" AssociatedControlID="DropDownListProveedor" CssClass="form-label fw-bold">Proveedor</asp:Label>
                        <asp:DropDownList ID="DropDownListProveedor" runat="server" CssClass="form-control"></asp:DropDownList>

                        <asp:Label ID="LabelEstado" runat="server" AssociatedControlID="CheckBoxEstado" CssClass="form-label fw-bold">Estado</asp:Label>
                        <asp:CheckBox ID="CheckBoxEstado" runat="server" CssClass="form-control" Checked="true"></asp:CheckBox>
                    </div>
                </div>
            </div>
        </div>



        <!-- Botones del ABM -->
        <div class="row mt-3">
            <div class="col-md-12">
                <div class="d-flex justify-content-between">
                    <asp:Button ID="btnAgregar" runat="server" CssClass="btn btn-success" Text="Agregar" OnClick="btnAgregar_Click" />
                    <asp:Button ID="btnModificar" runat="server" CssClass="btn btn-warning" Text="Modificar" OnClick="btnModificar_Click" />
                    <asp:Button ID="btnEliminar" runat="server" CssClass="btn btn-danger" Text="Eliminar" OnClick="btnEliminar_Click" />
                    <asp:Button ID="btnCancelar" runat="server" CssClass="btn btn-secondary" Text="Cancelar" OnClick="btnCancelar_Click" />


                </div>
            </div>
        </div>





        <!-- Grilla donde van los datos seleccionados -->
        <div class="row mt-40">
            <div class="col-md-15">
                <div class="table-responsive">
                    <asp:GridView ID="GridViewProductos" runat="server" CssClass="table table-striped table-bordered table-dark w-100" AutoGenerateColumns="False" OnRowCommand="GridViewProductos_RowCommand" OnSelectedIndexChanged="GridViewProductos_SelectedIndexChanged">
                        <Columns>
                            <asp:BoundField DataField="id" HeaderText="ID" />
                            <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                            <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                            <asp:BoundField DataField="precio" HeaderText="Precio" />
                            <asp:BoundField DataField="margenGanancia" HeaderText="margenGanancia" />
                            <asp:BoundField DataField="Stock" HeaderText="Stock" />
                            <asp:BoundField DataField="Marca" HeaderText="Marca" />
                            <asp:BoundField DataField="Tipo" HeaderText="Tipo" />
                            <asp:BoundField DataField="Categoria" HeaderText="Categoría" />
                            <asp:BoundField DataField="Proveedor" HeaderText="Proveedor" />
                            <asp:BoundField DataField="Estado" HeaderText="Estado" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnVerDetalle" runat="server" Text="Ver Detalle" CommandName="VerDetalle" CommandArgument='<%#Container.DataItemIndex %>' CssClass="btn btn-info" OnClick="BtnVerDetalle_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnSeleccionar" runat="server" Text="Seleccionar" CommandName="Seleccionar" CommandArgument='<%# Container.DataItemIndex %>' CssClass="btn btn-primary" />
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>


                </div>
            </div>
        </div>


    </div>
</asp:Content>
