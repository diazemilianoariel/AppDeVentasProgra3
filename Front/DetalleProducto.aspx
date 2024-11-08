<%@ Page Title="" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="DetalleProducto.aspx.cs" Inherits="Front.DetalleProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="card mb-3" style="max-width: 540px;">
        <div class="row g-0">
            <div class="col-md-4">
                <asp:Image ID="ImageProducto" runat="server" CssClass="img-fluid rounded-start" alt="Imagen del Producto" />
            </div>
            <div class="col-md-8">
                <div class="card-body">
                    <asp:Label ID="LabelNombreProducto" runat="server" CssClass="card-title"></asp:Label>
                    <asp:Label ID="LabelDescripcionProducto" runat="server" CssClass="card-text"></asp:Label>
                    <asp:Label ID="LabelPrecioProducto" runat="server" CssClass="card-text"></asp:Label>
                    <p class="card-text"><small class="text-muted">Última actualización hace 3 minutos</small></p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
