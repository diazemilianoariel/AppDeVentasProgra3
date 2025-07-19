<%@ Page Title="" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="Front.Error" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>




<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container text-center mt-5">
        <div class="row justify-content-center">
            <div class="col-md-8">
                <div class="card shadow-sm">
                    <div class="card-header bg-danger text-white">
                        <h2>Oops... Algo no salió como esperábamos.</h2>
                    </div>
                    <div class="card-body">
                        <p class="card-text">Hemos encontrado un error inesperado. Nuestro equipo técnico ha sido notificado.</p>
                        <p>Por favor, intenta volver a la página de inicio.</p>
                        
                        <%-- Mensaje de error técnico SOLO visible para administradores --%>
                        <asp:Panel ID="pnlErrorAdmin" runat="server" Visible="false" CssClass="alert alert-warning text-left mt-4">
                            <strong>Detalle técnico (Solo para Admins):</strong>
                            <asp:Literal ID="litErrorDetalle" runat="server"></asp:Literal>
                        </asp:Panel>
                        
                        <asp:HyperLink NavigateUrl="~/Default.aspx" runat="server" CssClass="btn btn-primary mt-3">Volver al Inicio</asp:HyperLink>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
