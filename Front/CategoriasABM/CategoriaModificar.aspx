<%@ Page Title="" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="CategoriaModificar.aspx.cs" Inherits="Front.CategoriasABM.CategoriaModificar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>




<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="container mt-5">
        <div class="row">
            <div class="col-md-12">
                <h1 class="text-center">Modificar Categoria</h1>
            </div>
        </div>

        <div class="row mt-4">
            <div class="col-md-6">
                <div class="form-group">
                    <asp:Label ID="LabelId" runat="server" CssClass="form-label fw-bold" Visible="false"></asp:Label>

                    <asp:Label ID="LabelNombre" runat="server" CssClass="form-label fw-bold">Nombre:</asp:Label>
                    <asp:TextBox ID="TextBoxNombre" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:Label ID="LabelError" runat="server" CssClass="text-danger"></asp:Label>
                    <asp:Label ID="LabelErrorCategoriaExistente" runat="server" CssClass="text-danger"></asp:Label>
                    <asp:HiddenField ID="HiddenFieldNombreOriginal" runat="server" />

                </div>




                <div class="form-group">
                    <div class="form-check">
                        <asp:CheckBox ID="CheckBoxEstado" runat="server"
                            Text="Categoria Disponible"
                            CssClass="form-check-input" />
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
