<%@ Page Title="" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="UsuarioModificar.aspx.cs" Inherits="Front.UsuariosABM.UsuarioModificar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="row">
            <div class="col-md-12">
                <h1 class="text-center">Modificar Usuario</h1>
            </div>
        </div>

        <div class="row mt-4">
            <div class="col-md-6">
                <div class="form-group">

                    <asp:Label ID="LabelId" runat="server" CssClass="form-label fw-bold" Visible="false"></asp:Label>

                    <asp:Label ID="LabelNombre" runat="server" CssClass="form-label fw-bold">Nombre:</asp:Label>
                    <asp:TextBox ID="TextBoxNombre" runat="server" CssClass="form-control"></asp:TextBox>

                    <div class="form-group">
                        <asp:Label ID="LabelApellido" runat="server" CssClass="form-label fw-bold">Apellido:</asp:Label>
                        <asp:TextBox ID="TextBoxApellido" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="LabelDni" runat="server" CssClass="form-label fw-bold">Dni:</asp:Label>
                        <asp:TextBox ID="TextBoxDni" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="LabelDireccion" runat="server" CssClass="form-label fw-bold">Direccion:</asp:Label>
                        <asp:TextBox ID="TextBoxDireccion" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <asp:Label ID="LabelTelefono" runat="server" CssClass="form-label fw-bold">Telefono:</asp:Label>
                        <asp:TextBox ID="TextBoxTelefono" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <asp:Label ID="LabelEmail" runat="server" CssClass="form-label fw-bold">Email:</asp:Label>
                        <asp:TextBox ID="TextBoxEmail" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <asp:Label ID="LabelClave" runat="server" CssClass="form-label fw-bold">Clave:</asp:Label>
                        <asp:TextBox ID="TextBoxClave" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="mb-3">
                        <asp:Label ID="LabelPerfilCliente" runat="server" AssociatedControlID="ddlPerfilCliente" CssClass="form-label">Perfiles</asp:Label>
                        <asp:DropDownList ID="ddlPerfilCliente" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>


                    <div class="form-group">
                        <asp:Label ID="LabelEstado" runat="server" CssClass="form-label fw-bold">Estado:</asp:Label>
                        <asp:CheckBox ID="CheckBoxEstado" runat="server" CssClass="form-control" />
                    </div>


                </div>
            </div>
        </div>

        <div class="row mt-4">
            <div class="col-md-12 text-center">
                <asp:Button ID="ButtonGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="ButtonGuardar_Click" />


                <asp:Button ID="ButtonCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="ButtonCancelar_Click" />
            </div>
        </div>
    </div>

</asp:Content>
