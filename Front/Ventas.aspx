<%@ Page Title="" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="Ventas.aspx.cs" Inherits="Front.Ventas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-6">
                <h1 class="text-center">Registrar Venta</h1>
                <asp:Panel ID="pnlFormularioVenta" runat="server" CssClass="form-group">
                    <div class="form-group">
                        <label for="txtIdVenta">ID Venta</label>
                        <asp:TextBox ID="txtIdVenta" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="txtFecha">Fecha</label>
                        <asp:TextBox ID="txtFecha" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="txtMonto">Monto</label>
                        <asp:TextBox ID="txtMonto" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="txtIdUsuario">ID Usuario</label>
                        <asp:TextBox ID="txtIdUsuario" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="chkEnLocal">En Local</label>
                        <asp:CheckBox ID="chkEnLocal" runat="server" CssClass="form-control"></asp:CheckBox>
                    </div>
                    <div class="form-group">
                        <label for="ddlEstadoVenta">Estado de Venta</label>
                        <asp:DropDownList ID="ddlEstadoVenta" runat="server" CssClass="form-control">
                            <asp:ListItem Value="1">Pendiente</asp:ListItem>
                            <asp:ListItem Value="2">Completada</asp:ListItem>
                            <asp:ListItem Value="3">Cancelada</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <asp:Button ID="btnRegistrarVenta" runat="server" Text="Registrar Venta" CssClass="btn btn-primary btn-block" OnClick="btnRegistrarVenta_Click" />
                </asp:Panel>
            </div>
        </div>

        <div class="row mt-5">
            <div class="col-md-12">
                <h2 class="text-center">Ventas Realizadas</h2>
                <asp:GridView ID="gvVentas" runat="server" AutoGenerateColumns="False" CssClass="table table-striped">
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="ID Venta" />
                        <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
                        <asp:BoundField DataField="Monto" HeaderText="Monto" />
                        <asp:BoundField DataField="IdUsuario" HeaderText="ID Usuario" />
                        <asp:BoundField DataField="EnLocal" HeaderText="En Local" />
                        <asp:BoundField DataField="EstadoVenta" HeaderText="Estado" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnAprobar" runat="server" Text="Aprobar" CommandArgument='<%# Eval("Id") %>' OnClick="btnAprobar_Click" CssClass="btn btn-success btn-sm" />
                                <asp:Button ID="btnRechazar" runat="server" Text="Rechazar" CommandArgument='<%# Eval("Id") %>' OnClick="btnRechazar_Click" CssClass="btn btn-danger btn-sm" />
                                <asp:Button ID="btnNotificar" runat="server" Text="Notificar" CommandArgument='<%# Eval("Id") %>' OnClick="btnNotificar_Click" CssClass="btn btn-info btn-sm" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>


    </div>



</asp:Content>
