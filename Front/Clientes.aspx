<%@ Page Title="" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="Front.Clientes" ContentType="text/html; charset=utf-8" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <!-- Título y descripción -->
        <div class="text-center">
            <h1 class="mb-3">Gestión de Usuarios</h1>
            <p class="text-muted">Administra y organiza la información de tus Usuarios de manera sencilla y rápida.</p>
        </div>
    </div>


    <!-- Formulario de Clientes -->

    <div class="card p-4 shadow-sm">
        <h4 class="text-center mb-4">Datos del Uruarios</h4>

        <div class="row">
            <div class="col col-6">

                <div class="mb-3">
                    <asp:Label ID="LabelIdCliente" runat="server" AssociatedControlID="TextBoxIdCliente" CssClass="form-label">ID Cliente</asp:Label>
                    <asp:TextBox ID="TextBoxIdCliente" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <asp:Label ID="LabelNombreCliente" runat="server" AssociatedControlID="TextBoxNombreCliente" CssClass="form-label">Nombre</asp:Label>
                    <asp:TextBox ID="TextBoxNombreCliente" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <asp:Label ID="LabelApellidoCliente" runat="server" AssociatedControlID="TextBoxApellidoCliente" CssClass="form-label">Apellido</asp:Label>
                    <asp:TextBox ID="TextBoxApellidoCliente" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <asp:Label ID="LabelDniCliente" runat="server" AssociatedControlID="TextBoxDniCliente" CssClass="form-label">Dni</asp:Label>
                    <asp:TextBox ID="TextBoxDniCliente" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>



            <div class="col col-6">
                <div class="mb-3">
                    <asp:Label ID="LabelDireccionCliente" runat="server" AssociatedControlID="TextBoxDireccionCliente" CssClass="form-label">Direccion</asp:Label>
                    <asp:TextBox ID="TextBoxDireccionCliente" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <asp:Label ID="LabelTelefonoCliente" runat="server" AssociatedControlID="TextBoxTelefonoCliente" CssClass="form-label">Telefono</asp:Label>
                    <asp:TextBox ID="TextBoxTelefonoCliente" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <asp:Label ID="LabelEmailCliente" runat="server" AssociatedControlID="TextBoxEmailCliente" CssClass="form-label">Email</asp:Label>
                    <asp:TextBox ID="TextBoxEmailCliente" runat="server" CssClass="form-control"></asp:TextBox>
                </div>


                <asp:Label ID="lblConfirmacion" runat="server" CssClass="alert alert-warning mt-3" Visible="false"></asp:Label>
                <asp:Button ID="btnConfirmarReactivacion" runat="server" Text="Confirmar Reactivación" OnClick="btnConfirmarReactivacion_Click" CssClass="btn btn-primary mt-2" Visible="false" />


                <div class="mb-3">
                    <asp:Label ID="LabelPerfilCliente" runat="server" AssociatedControlID="ddlPerfilCliente" CssClass="form-label">Perfil</asp:Label>
                    <asp:DropDownList ID="ddlPerfilCliente" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Cliente" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Administrador" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Vendedor" Value="3"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
    </div>





        <!-- Botones -->

        <div class="d-flex justify-content-between mt-4 col-6 mb-4">
            <asp:Button ID="btnActivarCliente" runat="server" OnClick="btnActivarCliente_Click" Style="display: none;" />
            <asp:Button ID="btnAgregarCliente" runat="server" CssClass="btn btn-success" Text="Agregar" OnClick="btnAgregarCliente_Click" />
            <asp:Button ID="btnModificarCliente" runat="server" CssClass="btn btn-warning" Text="Modificar" OnClick="btnModificarCliente_Click" />
            <asp:Button ID="btnEliminarCliente" runat="server" CssClass="btn btn-danger" Text="Eliminar" OnClick="btnEliminarCliente_Click" />
            <asp:Button ID="btnCancelarCliente" runat="server" CssClass="btn btn-secondary" Text="Cancelar" OnClick="btnCancelarCliente_Click" />
        </div>

    <!-- Tabla de Clientes -->
    <div>
        <div class="card p-4 shadow-sm">
            <h4 class="text-center mb-4">Lista de Usuarios</h4>
            <div class="table-responsive">
                <asp:GridView ID="GridViewClientes" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="False" OnRowCommand="GridViewClientes_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="ID" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="apellido" HeaderText="Apellido" />
                        <asp:BoundField DataField="dni" HeaderText="DNI" />
                        <asp:BoundField DataField="direccion" HeaderText="Dirección" />
                        <asp:BoundField DataField="telefono" HeaderText="Telefono" />
                        <asp:BoundField DataField="email" HeaderText="Email" />
                        <asp:BoundField DataField="Perfil" HeaderText="Perfil" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnSeleccionarCliente" runat="server" Text="Seleccionar" CommandName="Seleccionar" CommandArgument='<%# Container.DataItemIndex %>' CssClass="btn btn-primary btn-sm" />


                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>


    <!-- Bootstrap JS (Opcional, para interactividad como tooltips) -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
</asp:Content>


