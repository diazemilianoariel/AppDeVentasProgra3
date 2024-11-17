<%@ Page Title="" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="Categoria.aspx.cs" Inherits="Front.Categoria" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <h2>Gestión de Categorías</h2>
        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <label for="TextBoxNombreCategoria">Nombre de la Categoría</label>
                    <asp:TextBox ID="TextBoxNombreCategoria" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Button ID="btnAgregarCategoria" runat="server" Text="Agregar Categoría" CssClass="btn btn-primary" OnClick="btnAgregarCategoria_Click" />
                </div>
            </div>
        </div>
        <div class="row mt-4">
            <div class="col-md-12">
                <asp:GridView ID="GridViewCategorias" runat="server" CssClass="table table-striped" AutoGenerateColumns="False" OnRowCommand="GridViewCategorias_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="ID" />
                        <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnEditar" runat="server" Text="Editar" CommandName="Editar" CommandArgument='<%# Eval("id") %>' CssClass="btn btn-warning btn-sm" />
                                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CommandName="Eliminar" CommandArgument='<%# Eval("id") %>' CssClass="btn btn-danger btn-sm" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
