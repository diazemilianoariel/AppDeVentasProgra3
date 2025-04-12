<%@ Page Title="" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="Perfiles.aspx.cs" Inherits="Front.Perfiles" %>

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
    </style>
</asp:Content>




<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container mt-5">
        <div class="row">
            <div>
                <h1 class="text-center">Gestion de Perfiles</h1>
                <p class="text-muted">Administre y organice la información de sus Perfiles de manera sencilla y rápida.</p>
            </div>
        </div>
    </div>


    <div class="row mt-6 justify-content-center">
        <a href="PerfilesABM/PerfilAgregar.aspx" class="btn btn-primary">Nuevo</a>
    </div>


        <div class="row mt-12">
            <div class="table-responsive">
                <asp:GridView ID="GridViewPerfiles" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-dark w-100"  OnRowCommand="GridViewPerfiles_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="ID" ReadOnly="True" SortExpression="Id" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                        <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <div class="text-right">
                                    <asp:Button ID="btnModificar" runat="server" Text="Modificar" CommandName="Modificar" CommandArgument='<%# Eval("id") %>' CssClass="btn btn-success btn-sm" />

                                    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CommandName="Eliminar" CommandArgument='<%# Eval("id") %>' CssClass="btn btn-danger btn-sm" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

            </div>
        </div>
 





</asp:Content>
