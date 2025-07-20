<%@ Page Title="" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="RegistroUsuariosNuevos.aspx.cs" Inherits="Front.RegistroUsuariosNuevos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>




<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-8 col-lg-6">
                <div class="card p-4 shadow-sm">
                    <h3 class="text-center mb-4">Registro de Nuevo Usuario</h3>
                    <asp:Label ID="lblMensaje" runat="server" CssClass="alert" Visible="false"></asp:Label>
                    
                    <div class="row">
                        <div class="col-md-6 mb-3">
                            <asp:Label runat="server" AssociatedControlID="txtNombre" CssClass="form-label">Nombre</asp:Label>
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                            <asp:RequiredFieldValidator ErrorMessage="El nombre es obligatorio." ControlToValidate="txtNombre" runat="server" CssClass="text-danger" Display="Dynamic" />
                        </div>
                        <div class="col-md-6 mb-3">
                            <asp:Label runat="server" AssociatedControlID="txtApellido" CssClass="form-label">Apellido</asp:Label>
                            <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" />
                            <asp:RequiredFieldValidator ErrorMessage="El apellido es obligatorio." ControlToValidate="txtApellido" runat="server" CssClass="text-danger" Display="Dynamic" />
                        </div>
                    </div>

                    <div class="mb-3">
                        <asp:Label runat="server" AssociatedControlID="txtDni" CssClass="form-label">DNI</asp:Label>
                        <asp:TextBox ID="txtDni" runat="server" CssClass="form-control" />
                    </div>
                    <div class="mb-3">
                        <asp:Label runat="server" AssociatedControlID="txtDireccion" CssClass="form-label">Dirección</asp:Label>
                        <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" />
                    </div>
                    <div class="mb-3">
                        <asp:Label runat="server" AssociatedControlID="txtTelefono" CssClass="form-label">Teléfono</asp:Label>
                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" />
                    </div>
                    <div class="mb-3">
                        <asp:Label runat="server" AssociatedControlID="txtEmail" CssClass="form-label">Correo Electrónico</asp:Label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" />
                        <asp:RequiredFieldValidator ErrorMessage="El email es obligatorio." ControlToValidate="txtEmail" runat="server" CssClass="text-danger" Display="Dynamic" />
                    </div>
                    <div class="mb-3">
                        <asp:Label runat="server" AssociatedControlID="txtClave" CssClass="form-label">Contraseña</asp:Label>
                        <asp:TextBox ID="txtClave" runat="server" CssClass="form-control" TextMode="Password" />
                        <asp:RequiredFieldValidator ErrorMessage="La contraseña es obligatoria." ControlToValidate="txtClave" runat="server" CssClass="text-danger" Display="Dynamic" />
                    </div>
                    
                    <%-- MEJORA: Se añade campo para confirmar contraseña --%>
                    <div class="mb-3">
                        <asp:Label runat="server" AssociatedControlID="txtConfirmarClave" CssClass="form-label">Confirmar Contraseña</asp:Label>
                        <asp:TextBox ID="txtConfirmarClave" runat="server" CssClass="form-control" TextMode="Password" />
                        <asp:CompareValidator ErrorMessage="Las contraseñas no coinciden." ControlToValidate="txtConfirmarClave" ControlToCompare="txtClave" Operator="Equal" runat="server" CssClass="text-danger" Display="Dynamic" />
                    </div>

                    <div class="text-center d-grid gap-2">
                        <asp:Button ID="btnRegistrar" runat="server" CssClass="btn btn-primary" Text="Registrar" OnClick="btnRegistrar_Click" />
                        <asp:Button ID="btnVolverLogin" runat="server" CssClass="btn btn-secondary" Text="Ya tengo cuenta" OnClick="btnVolverLogin_Click" CausesValidation="false" />
                    </div>
                </div>
            </div>
        </div>
    </div>
   
</asp:Content>
