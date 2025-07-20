<%@ Page Title="" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="TipoAgregar.aspx.cs" Inherits="Front.TiposABM.TipoAgregar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-8 col-lg-6">
                <div class="card shadow-sm">
                    <div class="card-body">
                        <h1 class="text-center mb-4">Agregar Nuevo Tipo</h1>

                        <div class="form-group">
                            <asp:Label ID="LabelNombre" runat="server" AssociatedControlID="TextBoxNombre" CssClass="form-label fw-bold">Nombre del Tipo</asp:Label>
                            <asp:TextBox ID="TextBoxNombre" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ErrorMessage="El nombre es obligatorio." ControlToValidate="TextBoxNombre" runat="server" CssClass="text-danger" Display="Dynamic" />
                            <asp:Label ID="LabelErrorTipoExistente" runat="server" CssClass="text-danger mt-1 d-block" Visible="false"></asp:Label>
                        </div>

                        <div class="form-group form-check">
                            <asp:CheckBox ID="CheckBoxEstado" runat="server" CssClass="form-check-input" Checked="true" />
                            <asp:Label ID="LabelEstado" runat="server" AssociatedControlID="CheckBoxEstado" CssClass="form-check-label">Activo</asp:Label>
                        </div>
                        
                        <asp:Label ID="LabelError" runat="server" CssClass="text-danger text-center d-block mt-3" Visible="false"></asp:Label>

                        <div class="row mt-4">
                            <div class="col-md-12 text-center">
                                <asp:Button ID="ButtonGuardar" runat="server" Text="Guardar Tipo" CssClass="btn btn-primary" OnClick="ButtonGuardar_Click" />
                                <asp:Button ID="ButtonCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="ButtonCancelar_Click" CausesValidation="false" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
