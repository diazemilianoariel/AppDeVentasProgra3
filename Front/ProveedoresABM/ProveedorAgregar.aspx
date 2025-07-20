<%@ Page Title="" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="ProveedorAgregar.aspx.cs" Inherits="Front.ProveedoresABM.ProveedorAgregar" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />

</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

   <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-8 col-lg-6">
                <div class="card shadow-sm">
                    <div class="card-body">
                        <h1 class="text-center mb-4">Agregar Nuevo Proveedor</h1>

                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="TextBoxNombre" CssClass="form-label fw-bold">Nombre</asp:Label>
                            <asp:TextBox ID="TextBoxNombre" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ErrorMessage="El nombre es obligatorio." ControlToValidate="TextBoxNombre" runat="server" CssClass="text-danger" Display="Dynamic" />
                        </div>

                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="TextBoxDireccion" CssClass="form-label fw-bold">Dirección</asp:Label>
                            <asp:TextBox ID="TextBoxDireccion" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                        </div>

                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="TextBoxTelefono" CssClass="form-label fw-bold">Teléfono</asp:Label>
                            <asp:TextBox ID="TextBoxTelefono" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="TextBoxEmail" CssClass="form-label fw-bold">Email</asp:Label>
                            <asp:TextBox ID="TextBoxEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                            <asp:RequiredFieldValidator ErrorMessage="El email es obligatorio." ControlToValidate="TextBoxEmail" runat="server" CssClass="text-danger" Display="Dynamic" />
                            <asp:RegularExpressionValidator ErrorMessage="El formato del email no es válido." ControlToValidate="TextBoxEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" runat="server" CssClass="text-danger" Display="Dynamic" />
                        </div>

                        <div class="form-group form-check">
                            <asp:CheckBox ID="CheckBoxEstado" runat="server" CssClass="form-check-input" Checked="true" />
                            <asp:Label runat="server" AssociatedControlID="CheckBoxEstado" CssClass="form-check-label">Activo</asp:Label>
                        </div>

                        <div class="row mt-4">
                            <div class="col-md-12 text-center">
                                <asp:Button ID="ButtonGuardar" runat="server" Text="Guardar Proveedor" CssClass="btn btn-primary" OnClick="ButtonGuardar_Click" />
                                <asp:Button ID="ButtonCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="ButtonCancelar_Click" CausesValidation="false" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
