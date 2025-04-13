<%@ Page Title="" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="CategoriaEliminar.aspx.cs" Inherits="Front.CategoriasABM.CategoriaEliminar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container mt-5">
        <div class="card mb-4 border-0 shadow-lg">
            <div class="card-body p-4">
                <h2 class="card-title mb-3 text-dark">Eliminar Categoria</h2>
                <p class="card-text text-muted mb-4">¿Está seguro que desea eliminar la siguiente categoria?</p>
                <div class="mb-2">
                    <strong>Nombre: </strong>
                    <asp:Label ID="LabelNombreCategoria" runat="server" CssClass="text-secondary"></asp:Label>
                </div>

                <div class="mb-2">
                    <strong>Estado: </strong>
                    <asp:Label ID="LabelEstadoCategoria" runat="server" CssClass="text-secondary"
                        Text='<%# Convert.ToBoolean(Eval("estado")) ? "Activo" : "Inactivo" %>'></asp:Label>
                </div>
                <asp:Label ID="LabelError" runat="server" CssClass="text-danger" Visible="false"></asp:Label>


                <div class="d-flex justify-content-between mt-4">
                    <asp:Button IdD="btnConfirmar" runat="server" Text="Confirmar" CssClass="btn btn-danger" OnClick="btnConfirmar_Click" />
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="btnCancelar_Click" />
                </div>



            </div>
        </div>
    </div>




</asp:Content>
