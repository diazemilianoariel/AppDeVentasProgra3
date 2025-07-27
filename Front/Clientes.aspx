<%@ Page Title="" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="Front.Clientes" ContentType="text/html; charset=utf-8" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            background-image: url('https://www.shutterstock.com/shutterstock/photos/2369360047/display_1500/stock-photo-mockup-wall-in-the-children-s-room-on-wall-cream-color-background-d-rendering-2369360048.jpg');
            background-size: cover;
            background-repeat: no-repeat;
            background-attachment: fixed;
        }
        .pagination-ys { /* Estilo para la paginación */
            padding-left: 0; margin: 20px 0; border-radius: 4px;
        }
        .pagination-ys table > tbody > tr > td {
            display: inline; padding: 8px 12px; line-height: 1.42857143; text-decoration: none; color: #007bff; background-color: #fff; border: 1px solid #ddd;
        }
        .pagination-ys table > tbody > tr > td > span {
            z-index: 3; color: #fff; cursor: default; background-color: #007bff; border-color: #007bff;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="container mt-5">
        <div class="text-center">
            <h1 class="mb-3">Gestión de Usuarios</h1>
            <p class="text-muted">Administra y organiza la información de tus Usuarios de manera sencilla y rápida.</p>
        </div>

        <div class="row mt-4 justify-content-center">
            <asp:HyperLink NavigateUrl="~/UsuariosABM/UsuarioAgregar.aspx" runat="server" CssClass="btn btn-primary">Nuevo Usuario</asp:HyperLink>
        </div>

        <div class="row mt-4 justify-content-center">
            <div class="col-md-8">
                <div class="input-group">
                    <asp:TextBox ID="txtFiltro" runat="server" CssClass="form-control" placeholder="Buscar por nombre, apellido o email..." AutoPostBack="true" OnTextChanged="txtFiltro_TextChanged"></asp:TextBox>
                    <div class="input-group-append">
                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-secondary" OnClick="btnBuscar_Click" />
                    </div>
                </div>
            </div>
        </div>

        <div class="mt-4">
            <h4 class="text-center mb-4">Lista de Usuarios</h4>
            <asp:GridView ID="GridViewClientes" runat="server" 
                CssClass="table table-striped table-bordered table-dark w-100" 
                AutoGenerateColumns="False" 
                OnRowCommand="GridViewClientes_RowCommand"
                AllowPaging="True" PageSize="10" OnPageIndexChanging="GridViewClientes_PageIndexChanging">
                <Columns>
                    <asp:BoundField DataField="id" HeaderText="ID" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="apellido" HeaderText="Apellido" />
                    <asp:BoundField DataField="dni" HeaderText="DNI" />
                    <asp:BoundField DataField="email" HeaderText="Email" />
                    <asp:BoundField DataField="Perfil.Nombre" HeaderText="Perfil" />
                    <asp:TemplateField HeaderText="Estado">
                        <ItemTemplate>
                            <%# Convert.ToBoolean(Eval("estado")) ? "Activo" : "Inactivo" %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnModificar" runat="server" Text="Modificar" CommandName="Modificar" CommandArgument='<%# Eval("id") %>' CssClass="btn btn-success btn-sm" />
                            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CommandName="Eliminar" CommandArgument='<%# Eval("id") %>' CssClass="btn btn-danger btn-sm" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="pagination-ys" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>


