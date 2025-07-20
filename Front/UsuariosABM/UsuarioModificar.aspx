<%@ Page Title="" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="UsuarioModificar.aspx.cs" Inherits="Front.UsuariosABM.UsuarioModificar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-10 col-lg-8">
                <div class="card shadow-sm">
                    <div class="card-body">
                        <h1 class="text-center mb-4">Modificar Usuario</h1>
                        <asp:Label ID="LabelId" runat="server" Visible="false"></asp:Label>

                        <div class="row">
                            <%-- Columna Izquierda --%>
                            <div class="col-md-6">
                                <div class="mb-3">
                                    <asp:Label runat="server" AssociatedControlID="TextBoxNombre" CssClass="form-label fw-bold">Nombre</asp:Label>
                                    <asp:TextBox ID="TextBoxNombre" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ErrorMessage="El nombre es obligatorio." ControlToValidate="TextBoxNombre" runat="server" CssClass="text-danger" Display="Dynamic" />
                                </div>
                                <div class="mb-3">
                                    <asp:Label runat="server" AssociatedControlID="TextBoxApellido" CssClass="form-label fw-bold">Apellido</asp:Label>
                                    <asp:TextBox ID="TextBoxApellido" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ErrorMessage="El apellido es obligatorio." ControlToValidate="TextBoxApellido" runat="server" CssClass="text-danger" Display="Dynamic" />
                                </div>
                                <div class="mb-3">
                                    <asp:Label runat="server" AssociatedControlID="TextBoxDni" CssClass="form-label fw-bold">DNI</asp:Label>
                                    <asp:TextBox ID="TextBoxDni" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="mb-3">
                                    <asp:Label runat="server" AssociatedControlID="TextBoxDireccion" CssClass="form-label fw-bold">Dirección</asp:Label>
                                    <asp:TextBox ID="TextBoxDireccion" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <%-- Columna Derecha --%>
                            <div class="col-md-6">
                                <div class="mb-3">
                                    <asp:Label runat="server" AssociatedControlID="TextBoxTelefono" CssClass="form-label fw-bold">Teléfono</asp:Label>
                                    <asp:TextBox ID="TextBoxTelefono" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="mb-3">
                                    <asp:Label runat="server" AssociatedControlID="TextBoxEmail" CssClass="form-label fw-bold">Email</asp:Label>
                                    <asp:TextBox ID="TextBoxEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                                    <asp:RequiredFieldValidator ErrorMessage="El email es obligatorio." ControlToValidate="TextBoxEmail" runat="server" CssClass="text-danger" Display="Dynamic" />
                                </div>
                                <div class="mb-3">
                                    <asp:Label runat="server" AssociatedControlID="TextBoxClave" CssClass="form-label fw-bold">Nueva Clave (dejar en blanco para no cambiar)</asp:Label>
                                    <asp:TextBox ID="TextBoxClave" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                </div>
                                <div class="mb-3">
                                    <asp:Label runat="server" AssociatedControlID="ddlPerfil" CssClass="form-label fw-bold">Perfil</asp:Label>
                                    <asp:DropDownList ID="ddlPerfil" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="form-group form-check">
                                    <asp:CheckBox ID="CheckBoxEstado" runat="server" CssClass="form-check-input" />
                                    <asp:Label runat="server" AssociatedControlID="CheckBoxEstado" CssClass="form-check-label">Activo</asp:Label>
                                </div>
                            </div>
                        </div>

                        <div class="row mt-4">
                            <div class="col-md-12 text-center">
                                <asp:Button ID="ButtonGuardar" runat="server" Text="Guardar Cambios" CssClass="btn btn-primary" OnClick="ButtonGuardar_Click" />
                                <asp:Button ID="ButtonCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="ButtonCancelar_Click" CausesValidation="false" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
