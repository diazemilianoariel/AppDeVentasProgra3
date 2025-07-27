<%@ Page Title="Gestión de Perfiles" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="Perfiles.aspx.cs" Inherits="Front.Perfiles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            background-image: url('https://www.shutterstock.com/shutterstock/photos/2369360047/display_1500/stock-photo-mockup-wall-in-the-children-s-room-on-wall-cream-color-background-d-rendering-2369360047.jpg');
            background-size: cover; background-repeat: no-repeat; background-attachment: fixed;
        }
        .pagination-ys { padding-left: 0; margin: 20px 0; border-radius: 4px; }
        .pagination-ys table > tbody > tr > td { display: inline; padding: 8px 12px; text-decoration: none; color: #007bff; background-color: #fff; border: 1px solid #ddd; }
        .pagination-ys table > tbody > tr > td > span { z-index: 3; color: #fff; background-color: #007bff; border-color: #007bff; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="text-center">
            <h1 class="mb-3">Gestión de Perfiles</h1>
            <p class="text-muted">Administre y organice los roles de usuario de la aplicación.</p>
        </div>

        <div class="row mt-4 justify-content-center">
            <asp:HyperLink NavigateUrl="~/PerfilesABM/PerfilAgregar.aspx" runat="server" CssClass="btn btn-primary">Nuevo Perfil</asp:HyperLink>
        </div>

        <div class="row mt-4 justify-content-center">
            <div class="col-md-8">
                <div class="input-group">
                    <asp:TextBox ID="txtFiltro" runat="server" CssClass="form-control" placeholder="Buscar por nombre..." AutoPostBack="true" OnTextChanged="txtFiltro_TextChanged"></asp:TextBox>
                </div>
            </div>
        </div>

        <div class="row mt-4 justify-content-center">
            <div class="col-md-10">
                <div class="table-responsive">
                    <asp:GridView ID="GridViewPerfiles" runat="server" 
                        AutoGenerateColumns="False" DataKeyNames="Id" 
                        CssClass="table table-dark w-100" 
                        OnRowCommand="GridViewPerfiles_RowCommand"
                        AllowPaging="True" PageSize="5" OnPageIndexChanging="GridViewPerfiles_PageIndexChanging">
                        <Columns>
                            <asp:BoundField DataField="Id" HeaderText="ID" ReadOnly="True" />
                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                            <asp:TemplateField HeaderText="Estado">
                                <ItemTemplate>
                                    <%# Convert.ToBoolean(Eval("Estado")) ? "Activo" : "Inactivo" %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Acciones">
                                <ItemTemplate>
                                    <div class="text-right">
                                        <asp:Button ID="btnModificar" runat="server" Text="Modificar" CommandName="Modificar" CommandArgument='<%# Eval("id") %>' CssClass="btn btn-success btn-sm" />
                                        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CommandName="Eliminar" CommandArgument='<%# Eval("id") %>' CssClass="btn btn-danger btn-sm" />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                         <PagerStyle CssClass="pagination-ys" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>