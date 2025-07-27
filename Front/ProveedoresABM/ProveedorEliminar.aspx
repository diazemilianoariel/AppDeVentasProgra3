<%@ Page Title="" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="ProveedorEliminar.aspx.cs" Inherits="Front.ProveedoresABM.ProveedorEliminar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container mt-5">
        <div class="card mb-4 border-0 shadow-lg">
            <div class="card-body p-4">
                <h2 class="card-title mb-3 text-dark">Eliminar Proveedor</h2>
                <p class="card-text text-muted mb-4">¿Está seguro que desea eliminar el siguiente proveedor?</p>
                <div class="mb-2">
                    <strong>Nombre: </strong>
                    <asp:Label ID="LabelNombreProveedor" runat="server" CssClass="text-secondary"></asp:Label>
                </div>
                <div class="mb-2">
                    <strong>Dirección: </strong>
                    <asp:Label ID="LabelDireccionProveedor" runat="server" CssClass="text-secondary"></asp:Label>
                </div>
                <div class="mb-2">
                    <strong>Teléfono: </strong>
                    <asp:Label ID="LabelTelefonoProveedor" runat="server" CssClass="text-secondary"></asp:Label>
                </div>
                <div class="mb-2">
                    <%--El texto ahora es "Email" en lugar de "Stock" --%>
                    <strong>Email: </strong>
                    <asp:Label ID="LabelEmailProveedor" runat="server" CssClass="text-secondary"></asp:Label>
                </div>
                <div class="mb-2">
                    <strong>Estado: </strong>
                    <asp:Label ID="LabelEstadoProveedor" runat="server" CssClass="text-secondary"></asp:Label>
                </div>

                <asp:Label ID="LabelError" runat="server" CssClass="text-danger" Visible="false"></asp:Label>

                <div class="d-flex justify-content-end mt-4">
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary mr-2" onClick="btnCancelar_Click"/>
                    <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar Eliminación" CssClass="btn btn-danger" OnClick="btnConfirmar_Click" />
                </div>
            </div>
        </div>
</asp:Content>
