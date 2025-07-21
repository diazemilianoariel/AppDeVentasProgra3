<%@ Page Title="" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="MisCompras.aspx.cs" Inherits="Front.MisCompras" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row mt-5">
        <div class="col-md-12">
            <h2 class="text-center">Mis Compras</h2>

            <%-- Eliminamos el GridView y usamos un Repeater para los grupos de meses --%>
            <asp:Repeater ID="rptGruposCompras" runat="server">
                <ItemTemplate>

                    <%-- Título para el grupo (ej: julio 2025) --%>
                    <h4 class="mt-4 mb-3" style="text-transform: capitalize;"><%# Eval("Periodo", "{0:MMMM yyyy}") %></h4>

                    <div class="table-responsive">
                        <table class="table table-hover">
                            <thead>
                                <tr>
                                    <th>ID Venta</th>
                                    <th>Fecha</th>
                                    <th>Total</th>
                                    <th>Estado</th>
                                    <th class="text-center">Acciones</th>
                                </tr>
                            </thead>

                            <%-- Repeater anidado para mostrar las compras de ESE mes --%>
                            <%-- Nota el OnItemCommand, que se conecta con tu C# --%>
                            <asp:Repeater ID="rptCompras" runat="server" DataSource='<%# Eval("ComprasDelPeriodo") %>' OnItemCommand="rptCompras_ItemCommand">
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Eval("IdVenta") %></td>
                                        <td><%# Eval("Fecha", "{0:dd/MM/yyyy}") %></td>
                                        <td><%# Eval("TotalFactura", "{0:c}") %></td>
                                        <td>
                                            <%-- Usamos el método de ayuda para mostrar el badge de color --%>
                                            <span class='<%# GetStatusBadgeClass(Eval("Estado")) %>'>
                                                <%# Eval("Estado") %>
                                            </span>
                                        </td>
                                        <td class="text-center">
                                            <%-- Este botón ejecutará el código del servidor OnItemCommand --%>
                                            <asp:Button ID="btnVerDetalles" runat="server"
                                                Text="Ver Detalles"
                                                CssClass="btn btn-info btn-sm"
                                                CommandName="VerDetalles"
                                                CommandArgument='<%# Eval("IdVenta") %>' />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>

                </ItemTemplate>
            </asp:Repeater>

            <%-- Mensaje opcional si no hay compras --%>
            <asp:Panel ID="pnlNoHayCompras" runat="server" Visible="false" CssClass="text-center mt-5">
                <p>Aún no has realizado ninguna compra.</p>
            </asp:Panel>

            <div class="text-center mt-4">
                <asp:Button ID="btnCargarMas" runat="server" Text="Cargar más compras"
                    CssClass="btn btn-outline-primary" OnClick="btnCargarMas_Click" />
            </div>

        </div>
    </div>
</asp:Content>
