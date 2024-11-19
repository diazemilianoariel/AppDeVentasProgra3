<%@ Page Title="" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="Marcas.aspx.cs" Inherits="Front.Marcas"  ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <h2 class="text-center">Gestión de Marcas</h2>
        <div class="row">
            <div class="col-md-4">
                <div class="card">
                    <div class="card-header bg-primary text-white">
                        <h4>Agregar Nueva Marca</h4>
                    </div>
                    <div class="card-body">
                        <div class="form-group">
                            <label for="TextBoxNombreMarca">Nombre de la Marca</label>
                            <asp:TextBox ID="TextBoxMarca" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group text-right">
                            <asp:Button ID="btnAgregarMarca" runat="server" Text="Agregar Marca" CssClass="btn btn-success" OnClick="btnAgregarMarca_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-8">
                <div class="card">
                    <div class="card-header bg-secondary text-white">
                        <h4>Lista de Marcas</h4>
                    </div>
                    <div class="card-body">
                        <asp:GridView ID="GridViewMarcas" runat="server" CssClass="table table-hover table-bordered" AutoGenerateColumns="False" OnRowCommand="GridViewMarcas_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="id" HeaderText="ID" HeaderStyle-CssClass="bg-secondary text-white" ItemStyle-CssClass="text-center" />
                                <asp:BoundField DataField="nombre" HeaderText="Nombre" HeaderStyle-CssClass="bg-secondary text-white" ItemStyle-CssClass="text-center" />
                                <asp:TemplateField HeaderText="Acciones" HeaderStyle-CssClass="bg-secondary text-white" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <div class="btn-group" role="group">
                                            <asp:Button ID="btnEditar" runat="server" Text="Editar" CommandName="Editar" CommandArgument='<%# Container.DataItemIndex %>' CssClass="btn btn-warning btn-sm" />
                                            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CommandName="Eliminar" CommandArgument='<%# Container.DataItemIndex %>' CssClass="btn btn-danger btn-sm" />
                                        </div>
                                        <asp:Panel ID="editSection" runat="server" CssClass="mt-2" Style="display: none;">
                                            <div class="form-group">
                                                <label for="TextBoxNuevoNombreMarca">Nuevo Nombre</label>
                                                <asp:TextBox ID="TextBoxNuevaMarca" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group text-right">
                                                <asp:Button ID="btnActualizarMarca" runat="server" Text="Actualizar" CssClass="btn btn-warning btn-sm" OnClick="btnActualizarMarca_Click" />
                                            </div>
                                        </asp:Panel>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
