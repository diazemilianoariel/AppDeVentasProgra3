<%@ Page Title="" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="Tipos.aspx.cs" Inherits="Front.Tipo" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <h2 class="text-center">Gestión de Tipos</h2>
        <div class="row">
            <div class="col-md-4">
                <div class="card">
                    <div class="card-header bg-primary text-white">
                        <h4>Agregar Nuevo Tipo</h4>
                    </div>
                    <div class="card-body">
                        <div class="form-group">
                            <label for="TextBoxNombreTipo">Nombre del Tipo</label>
                            <asp:TextBox ID="TextBoxTipo" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group text-right">
                            <asp:Button ID="btnAgregarTipo" runat="server" Text="Agregar Tipo" CssClass="btn btn-success" OnClick="btnAgregarTipo_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-8">
                <div class="card">
                    <div class="card-header bg-secondary text-white">
                        <h4>Lista de Tipos</h4>
                    </div>
                    <div class="card-body">
                        <asp:GridView ID="GridViewTipos" runat="server" CssClass="table table-hover table-bordered" AutoGenerateColumns="False" OnRowCommand="GridViewTipos_RowCommand">
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
                                                <label for="TextBoxNuevoNombreTipo">Nuevo Nombre</label>
                                                <asp:TextBox ID="TextBoxNuevoTipo" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group text-right">
                                                <asp:Button ID="btnActualizarTipo" runat="server" Text="Actualizar" CssClass="btn btn-warning btn-sm" OnClick="btnActualizarTipo_Click" />
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
