<%@ Page Title="" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="PerfilAgregar.aspx.cs" Inherits="Front.PerfilesABM.PerfilesAgregar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-8 col-lg-6">
                <div class="card shadow-sm">
                    <div class="card-body">
                        <h1 class="text-center mb-4">Agregar Nuevo Perfil</h1>

                        <div class="form-group">
                            <asp:Label ID="lblNombre" runat="server" AssociatedControlID="TxtNombre" CssClass="form-label fw-bold">Nombre del Perfil</asp:Label>
                            <asp:TextBox ID="TxtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                            
                           
                            <asp:Label ID="LabelError" runat="server" CssClass="text-danger mt-1 d-block" Visible="false"></asp:Label>
                            <asp:Label ID="LabelErrorPerfilExistente" runat="server" CssClass="text-danger mt-1 d-block" Visible="false"></asp:Label>
                        </div>

                        <div class="form-group form-check">
                            <asp:CheckBox ID="CheckBoxEstado" runat="server" CssClass="form-check-input" />
                            <asp:Label ID="LabelEstado" runat="server" AssociatedControlID="CheckBoxEstado" CssClass="form-check-label">Activo</asp:Label>
                        </div>

                        <div class="row mt-4">
                            <div class="col-md-12 text-center">
                                <asp:Button ID="ButtonGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="ButtonGuardar_Click" />
                                <asp:Button ID="ButtonCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="ButtonCancelar_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
   

</asp:Content>
