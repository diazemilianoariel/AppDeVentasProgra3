<%@ Page Title="" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="TipoModificar.aspx.cs" Inherits="Front.TiposABM.TIpoModificar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-8 col-lg-6">
                <div class="card shadow-sm">
                    <div class="card-body">
                        <h1 class="text-center mb-4">Modificar Tipo</h1>

                        <%-- MEJORA: Campo oculto para guardar el nombre original --%>
                        <asp:HiddenField ID="HiddenFieldNombreOriginal" runat="server" />
                        <asp:Label ID="LabelId" runat="server" Visible="false"></asp:Label>

                        <div class="form-group">
                            <asp:Label ID="LabelNombre" runat="server" AssociatedControlID="TextBoxNombre" CssClass="form-label fw-bold">Nombre del Tipo</asp:Label>
                            <asp:TextBox ID="TextBoxNombre" runat="server" CssClass="form-control"></asp:TextBox>

                            <%-- MEJORA: Labels para mostrar mensajes de error específicos --%>
                            <asp:Label ID="LabelError" runat="server" CssClass="text-danger mt-1 d-block" Visible="false"></asp:Label>
                            <asp:Label ID="LabelErrorTipoExistente" runat="server" CssClass="text-danger mt-1 d-block" Visible="false"></asp:Label>
                        </div>

                        <div class="form-group form-check">
                            <asp:CheckBox ID="CheckBoxEstado" runat="server" CssClass="form-check-input" />
                            <asp:Label ID="LabelEstado" runat="server" AssociatedControlID="CheckBoxEstado" CssClass="form-check-label">Activo</asp:Label>
                        </div>

                        <div class="row mt-4">
                            <div class="col-md-12 text-center">
                                <asp:Button ID="ButtonGuardar" runat="server" Text="Guardar Cambios" CssClass="btn btn-primary" OnClick="ButtonGuardar_Click" />
                                <asp:Button ID="ButtonCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="ButtonCancelar_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
