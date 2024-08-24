<%@ Page Title="" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="Front.producto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
       
            <div class="row">
                <div class="col-md-6">
                    <div class="form-group">
                        <asp:Label ID="LabelNombre" runat="server" Text="Nombre" CssClass="control-label"></asp:Label>
                        <asp:TextBox ID="TextBoxNombre" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <asp:Label ID="LabelDescripcion" runat="server" Text="Descripción" CssClass="control-label"></asp:Label>
                        <asp:TextBox ID="TextBoxDescripcion" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <asp:Label ID="LabelPrecio" runat="server" Text="Precio" CssClass="control-label"></asp:Label>
                        <asp:TextBox ID="TextBoxPrecio" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <asp:Label ID="LabelImagen" runat="server" Text="Imagen URL" CssClass="control-label"></asp:Label>
                        <asp:TextBox ID="TextBoxImagen" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <asp:Label ID="LabelStock" runat="server" Text="Stock" CssClass="control-label"></asp:Label>
                        <asp:TextBox ID="TextBoxStock" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <asp:Label ID="LabelMarca" runat="server" Text="Marca" CssClass="control-label"></asp:Label>
                        <asp:TextBox ID="TextBoxMarca" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <asp:Label ID="LabelTipo" runat="server" Text="Tipo" CssClass="control-label"></asp:Label>
                        <asp:TextBox ID="TextBoxTipo" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <asp:Label ID="LabelCategoria" runat="server" Text="Categoría" CssClass="control-label"></asp:Label>
                        <asp:TextBox ID="TextBoxCategoria" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <asp:Label ID="LabelProveedor" runat="server" Text="Proveedor" CssClass="control-label"></asp:Label>
                        <asp:TextBox ID="TextBoxProveedor" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <asp:Label ID="LabelEstado" runat="server" Text="Estado" CssClass="control-label"></asp:Label>
                        <asp:TextBox ID="TextBoxEstado" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <asp:Button ID="ButtonAgregar" runat="server" Text="Agregar Producto" OnClick="btnAgregar_Click" CssClass="btn btn-primary" />
                    </div>
                </div>
            </div>
        
    </div>
</asp:Content>
