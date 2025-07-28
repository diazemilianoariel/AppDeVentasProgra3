<%@ Page Title="Mi Perfil" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="MiPerfil.aspx.cs" Inherits="Front.MiPerfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-8">
                <div class="card shadow-sm">
                    <div class="card-body p-4">
                        <h2 class="card-title text-center mb-4">Mi Perfil</h2>
                        
                        <asp:Label ID="lblMensaje" runat="server" Visible="false" CssClass="alert" />

                        <div class="row">
                            <div class="col-md-6 mb-3">
                                <label for="<%=txtNombre.ClientID%>" class="form-label fw-bold">Nombre</label>
                                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-6 mb-3">
                                <label for="<%=txtApellido.ClientID%>" class="form-label fw-bold">Apellido</label>
                                <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6 mb-3">
                                <label for="<%=txtDni.ClientID%>" class="form-label fw-bold">DNI</label>
                                <asp:TextBox ID="txtDni" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-6 mb-3">
                                <label for="<%=txtTelefono.ClientID%>" class="form-label fw-bold">Teléfono</label>
                                <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="mb-3">
                            <label for="<%=txtDireccion.ClientID%>" class="form-label fw-bold">Dirección</label>
                            <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <label for="<%=txtEmail.ClientID%>" class="form-label fw-bold">Email</label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" ReadOnly="true" ToolTip="El email no se puede modificar."></asp:TextBox>
                        </div>

                        <hr />
                        <h4 class="mt-4">Cambiar Contraseña</h4>
                        <p class="text-muted small">Dejar en blanco para no modificar la contraseña actual.</p>
                        <div class="row">
                             <div class="col-md-6 mb-3">
                                <label for="<%=txtClaveNueva.ClientID%>" class="form-label fw-bold">Nueva Contraseña</label>
                                <asp:TextBox ID="txtClaveNueva" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                            </div>
                             <div class="col-md-6 mb-3">
                                <label for="<%=txtConfirmarClave.ClientID%>" class="form-label fw-bold">Confirmar Contraseña</label>
                                <asp:TextBox ID="txtConfirmarClave" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                <asp:CompareValidator ErrorMessage="Las contraseñas no coinciden." ControlToValidate="txtConfirmarClave" ControlToCompare="txtClaveNueva" Operator="Equal" runat="server" CssClass="text-danger" Display="Dynamic" />
                            </div>
                        </div>
                        
                        <div class="text-center mt-4">
                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar Cambios" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>