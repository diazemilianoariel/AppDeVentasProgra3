<%@ Page Title="" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="RegistroUsuariosNuevos.aspx.cs" Inherits="Front.RegistroUsuariosNuevos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>




<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container mt-5">
        <div class="row">
            <div class="col-md-6 offset-md-3">
                <div class="card p-4 shadow-sm">
                    <h3 class="text-center mb-4">Registro Usuario Nuevo</h3>
                    <asp:Label ID="lblMensaje" runat="server" CssClass="alert" Visible="false"></asp:Label>
                    <div class="mb-3">
                        <label for="txtNombre" class="form-label">Nombre</label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                    </div>
                    <div class="mb-3">
                        <label for="txtApellido" class="form-label">Apellido</label>
                        <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" />
                    </div>
                    <div class="mb-3">
                        <label for="txtDni" class="form-label">DNI</label>
                        <asp:TextBox ID="txtDni" runat="server" CssClass="form-control" />
                    </div>
                    <div class="mb-3">
                        <label for="txtDireccion" class="form-label">Dirección</label>
                        <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" />
                    </div>
                    <div class="mb-3">
                        <label for="txtTelefono" class="form-label">Teléfono</label>
                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" />
                    </div>
                    <div class="mb-3">
                        <label for="txtEmail" class="form-label">Correo Electrónico</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
                    </div>
                    <div class="mb-3">
                        <label for="txtClave" class="form-label">Contraseña</label>
                        <asp:TextBox ID="txtClave" runat="server" CssClass="form-control" TextMode="Password" />
                    </div>
                    <div class="text-center">
                        <asp:Button ID="btnRegistrar" runat="server" CssClass="btn btn-primary" Text="Registrar" OnClick="btnRegistrar_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
