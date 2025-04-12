<%@ Page Title="" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="PerfilAgregar.aspx.cs" Inherits="Front.PerfilesABM.PerfilesAgregar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container mt-5">
        <div class="row">
            <!-- Columna izquierda con los TextBoxes y Labels -->
            <div class="col-md-6">
                <div class="form-group col-md-12">



                    <asp:Label ID="lblNombre" runat="server" AssociatedControlID="TxtNombre" CssClass="form-label fw-bold">Nombre</asp:Label>
                    <asp:TextBox ID="TxtNombre" runat="server" CssClass="form-control"></asp:TextBox>



                    <asp:Label ID="LabelEstado" runat="server" AssociatedControlID="CheckBoxEstado" CssClass="form-label fw-bold">Activo?</asp:Label>
                    <asp:CheckBox ID="CheckBoxEstado" runat="server" CssClass="form-control"></asp:CheckBox>

                    <asp:Label ID="lblConfirmacion" runat="server" CssClass="alert alert-warning mt-3" Visible="false"></asp:Label>


                    <asp:Button ID="btnConfirmarReactivacion" runat="server" Text="ConfirmarReactivación" OnClick="btnConfirmarReactivacion_Click" CssClass="btn btn-primary mt-2" Visible="false" />


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


</asp:Content>
