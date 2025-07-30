<%@ Page Title="" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="ContraseñaRecupero.aspx.cs" Inherits="Front.ContraseñaRecupero" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container d-flex justify-content-center align-items-center vh-100">
        <div class="card p-4 shadow-sm" style="max-width: 400px; width: 100%;">
            <h3 class="text-center mb-4">Recuperar Contraseña</h3>
            <div class="mb-3">
                <label for="email" class="form-label">Correo Electrónico</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtEmail" Type="Text" />
            </div>

            <asp:Button Text="Recuperar Contraseña" CssClass="btn btn-primary w-100 mb-2" ID="btnRecuperar" runat="server" OnClick="btnRecuperar_Click" />
            <asp:Button Text="Volver" CssClass="btn btn-secondary w-100" ID="btnVolver" runat="server" OnClick="btnVolver_Click" />


            <div class="row mt-3 justify-content-center">
                <div class="col-16">
                    <asp:Label ID="lblMensaje" runat="server" Text="" Visible="false" CssClass="alert text-center"></asp:Label>
                </div>
            </div>

        </div>
    </div>

</asp:Content>
