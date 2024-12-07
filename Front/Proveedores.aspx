<%@ Page Title="" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="Proveedores.aspx.cs" Inherits="Front.Proveedores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="estilos.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="row">
            <div class="col-md-12">
                <h1 class="text-center">Gestión de Proveedores</h1>
            </div>
        </div>
        <div class="container mt-5">
            <div class="row">
                <!-- Columna izquierda con los TextBoxes y Labels -->
                <div class="col-md-6">
                    <div class="form-group">
                        <asp:Label ID ="LabelID" runat="server" AssociatedControlID="TextBoxId" CssClass="form-label" >ID Proveedor</asp:Label>
                        <asp:TextBox ID="TextBoxId" runat="server" CssClass="form-control" ReadOnly="true" Visible="true"></asp:TextBox>

                        <asp:Label ID="LabelNombre" runat="server" AssociatedControlID="TextBoxNombre" CssClass="form-label fw-bold">Nombre</asp:Label>
                        <asp:TextBox ID="TextBoxNombre" runat="server" CssClass="form-control"></asp:TextBox>

                        <asp:Label ID="LabelDireccion" runat="server" AssociatedControlID="TextBoxDireccion" CssClass="form-label fw-bold">Dirección</asp:Label>
                        <asp:TextBox ID="TextBoxDireccion" runat="server" CssClass="form-control"></asp:TextBox>

                        <asp:Label ID="LabelTelefono" runat="server" AssociatedControlID="TextBoxTelefono" CssClass="form-label fw-bold">Teléfono</asp:Label>
                        <asp:TextBox ID="TextBoxTelefono" runat="server" CssClass="form-control"></asp:TextBox>

                        <asp:Label ID="LabelEmail" runat="server" AssociatedControlID="TextBoxEmail" CssClass="form-label fw-bold">Email</asp:Label>
                        <asp:TextBox ID="TextBoxEmail" runat="server" CssClass="form-control"></asp:TextBox>
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
            <div class="row mt-5">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView ID="GridViewProveedores" runat="server" CssClass="table table-striped table-bordered table-dark" AutoGenerateColumns="False" OnRowCommand="GridViewProveedores_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="id" HeaderText="ID" />
                                <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="direccion" HeaderText="Dirección" />
                                <asp:BoundField DataField="telefono" HeaderText="Teléfono" />
                                <asp:BoundField DataField="email" HeaderText="Email" />

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
    </div>
</asp:Content>
