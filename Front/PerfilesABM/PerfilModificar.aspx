<%@ Page Title="" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="PerfilModificar.aspx.cs" Inherits="Front.PerfilesABM.PerfilesModificar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>




<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row">
        <div class="col-md-12">
            <h1 class="text-center">Modificar Perfil</h1>
        </div>
    </div>

    <div class="row mt-4">
        <div class="col-md-6">
            <div class="form-group">
                <asp:Label ID="LabelId" runat="server" CssClass="form-label fw-bold" Visible="false"></asp:Label>

                <asp:Label ID="LabelNombre" runat="server" CssClass="form-label fw-bold">Nombre:</asp:Label>
                <asp:TextBox ID="TextBoxNombre" runat="server" CssClass="form-control"></asp:TextBox>

                <asp:Label ID="LabelEstado" runat="server" CssClass="form-label fw-bold">Perfil Disponible:</asp:Label>
                <asp:CheckBox ID="CheckBoxEstado" runat="server" CssClass="form-control" />
            </div>
        </div>
    </div>


    <div class="row mt-4">
        <div class="col-md-12 text-center">
            <asp:Button ID="ButtonGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="ButtonGuardar_Click" />
            <asp:Button ID="ButtonCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="ButtonCancelar_Click" />
        </div>
    </div>



</asp:Content>
