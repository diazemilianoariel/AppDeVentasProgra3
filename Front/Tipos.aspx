<%@ Page Title="" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="Tipos.aspx.cs" Inherits="Front.Tipos" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" rel="stylesheet" />
    <link href="estilos.css" rel="stylesheet" />
    <style>
        body {
            background-image: url('https://www.shutterstock.com/shutterstock/photos/2369360047/display_1500/stock-photo-mockup-wall-in-the-children-s-room-on-wall-cream-color-background-d-rendering-2369360047.jpg');
            background-size: cover;
            background-repeat: no-repeat;
            background-attachment: fixed;
        }

        .pagination-ys {
            padding-left: 0;
            margin: 20px 0;
            border-radius: 4px;
        }

            .pagination-ys table > tbody > tr > td {
                display: inline;
                padding: 8px 12px;
                text-decoration: none;
                color: #007bff;
                background-color: #fff;
                border: 1px solid #ddd;
            }

                .pagination-ys table > tbody > tr > td > span {
                    z-index: 3;
                    color: #fff;
                    background-color: #007bff;
                    border-color: #007bff;
                }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="text-center">
            <h1 class="mb-3">Gestión de Tipos</h1>
            <p class="text-muted">Administre y organice los Tipos de productos de la aplicación.</p>
        </div>

        <div class="row mt-4 justify-content-center">
            <asp:HyperLink NavigateUrl="~/TiposABM/TipoAgregar.aspx" runat="server" CssClass="btn btn-primary">Nuevo Tipo</asp:HyperLink>
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
                    <asp:GridView ID="GridViewTipos" runat="server"
                        AutoGenerateColumns="False" DataKeyNames="Id"
                        CssClass="table table-dark w-100"
                        OnRowCommand="GridViewTipos_RowCommand"
                        AllowPaging="True" PageSize="5" OnPageIndexChanging="GridViewTipos_PageIndexChanging">
                        <Columns>
                            <asp:BoundField DataField="Id" HeaderText="ID" ReadOnly="True" />
                            <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                            <asp:TemplateField HeaderText="Estado">
                                <ItemTemplate>
                                    <%# Convert.ToBoolean(Eval("estado")) ? "Activo" : "Inactivo" %>
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
