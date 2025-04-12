<%@ Page Title="" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="UsuarioAgregar.aspx.cs" Inherits="Front.UsuariosABM.UsuarioAgregar" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Formulario de Clientes -->

    <div class="card p-4 shadow-sm">
        <h4 class="text-center mb-4">Datos del Uruarios</h4>

        <div class="row">
            <div class="col col-6">

                <div class="mb-3">
                    <asp:Label ID="LabelIdCliente" runat="server" AssociatedControlID="TextBoxIdCliente" CssClass="form-label">ID</asp:Label>
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


                <div class="mb-3">
                    <asp:Label ID="LabelClaveCliente" runat="server" AssociatedControlID="TextBoxClaveCliente" CssClass="form-label">Clave</asp:Label>
                    <asp:TextBox ID="TextBoxClaveCliente" runat="server" CssClass="form-control"></asp:TextBox>
                </div>


                <%--   <asp:Label ID="lblConfirmacion" runat="server" CssClass="alert alert-warning mt-3" Visible="false"></asp:Label>
                <asp:Button ID="btnConfirmarReactivacion" runat="server" Text="Confirmar Reactivación" OnClick="btnConfirmarReactivacion_Click" CssClass="btn btn-primary mt-2" Visible="false" />--%>


                <div class="mb-3">
                    <asp:Label ID="LabelPerfilCliente" runat="server" AssociatedControlID="ddlPerfilCliente" CssClass="form-label">Perfiles</asp:Label>
                    <asp:DropDownList ID="ddlPerfilCliente" runat="server" CssClass="form-control"></asp:DropDownList>
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
