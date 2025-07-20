<%@ Page Title="" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="UsuarioEliminar.aspx.cs" Inherits="Front.UsuariosABM.UsuarioEliminar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-8 col-lg-6">
                <div class="card mb-4 border-0 shadow-lg">
                    <div class="card-body p-4">
                        <h2 class="card-title mb-3 text-dark">Eliminar Usuario</h2>
                        <p class="card-text text-muted mb-4">¿Está seguro que desea dar de baja al siguiente usuario?</p>

                        <div class="mb-2">
                            <strong>Nombre: </strong>
                            <asp:Label ID="LabelNombreUsuario" runat="server" CssClass="text-secondary fw-bold"></asp:Label>
                        </div>
                        <div class="mb-2">
                            <strong>Email: </strong>
                            <asp:Label ID="LabelEmailUsuario" runat="server" CssClass="text-secondary"></asp:Label>
                        </div>
                        <div class="mb-2">
                            <strong>Perfil: </strong>
                            <asp:Label ID="LabelPerfilUsuario" runat="server" CssClass="text-secondary"></asp:Label>
                        </div>
                        <div class="mb-2">
                            <strong>Estado Actual: </strong>
                            <asp:Label ID="LabelEstadoUsuario" runat="server" CssClass="text-secondary"></asp:Label>
                        </div>

                        <asp:Label ID="LabelError" runat="server" CssClass="text-danger d-block text-center mt-3" Visible="false"></asp:Label>

                        <div class="d-flex justify-content-end mt-4">
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary mr-2" OnClick="btnCancelar_Click" />
                            <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar Baja" CssClass="btn btn-danger" OnClick="btnConfirmar_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
