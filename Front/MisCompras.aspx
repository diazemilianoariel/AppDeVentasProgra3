<%@ Page Title="Mis Compras" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="MisCompras.aspx.cs" Inherits="Front.MisCompras" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
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
        <div class="row">
            <div class="col-md-12">
                <h2 class="text-center mb-4">Mi Historial de Compras</h2>



                <!-- SECCIÓN DE BÚSQUEDA AÑADIDA -->
                <div class="row mb-4 justify-content-center">
                    <div class="col-md-8">
                        <div class="input-group">
                            <asp:TextBox ID="txtFiltro" runat="server" CssClass="form-control" placeholder="Buscar por N° de orden, fecha(dd/mm/aaaa) o estado..." AutoPostBack="true" OnTextChanged="txtFiltro_TextChanged"></asp:TextBox>
                        </div>
                    </div>
                </div>



                <asp:GridView ID="gvMisCompras" runat="server" AutoGenerateColumns="false" CssClass="table table-hover" GridLines="None" OnRowCommand="gvMisCompras_RowCommand" AllowPaging="True" PageSize="5" OnPageIndexChanging="gvMisCompras_PageIndexChanging">

                    <Columns>
                        <asp:BoundField DataField="IdVenta" HeaderText="N° de Orden" />
                        <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="Monto" HeaderText="Total" DataFormatString="{0:C}" />

                        <asp:TemplateField HeaderText="Estado">
                            <ItemTemplate>
                                <span class='badge <%# Eval("nombreEstadoVenta").ToString() == "Aprobado" ? "badge-success" : (Eval("nombreEstadoVenta").ToString() == "Pendiente" ? "badge-warning" : "badge-danger") %>'>
                                    <%# Eval("nombreEstadoVenta") %>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:Button ID="btnVerFactura" runat="server"
                                    Text="Ver Factura"
                                    CommandName="VerFactura"
                                    CommandArgument='<%# Eval("IdVenta") %>'
                                    CssClass="btn btn-primary btn-sm"
                                    Visible='<%# Eval("nombreEstadoVenta").ToString() == "Aprobado" %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="pagination-ys" />
                </asp:GridView>

                <%-- Panel que se muestra si no hay compras --%>
                <asp:Panel ID="pnlNoHayCompras" runat="server" Visible="false" CssClass="text-center mt-5 card p-4">
                    <p class="h5 text-muted">Aún no has realizado ninguna compra.</p>
                    <asp:HyperLink NavigateUrl="~/Default.aspx" runat="server" CssClass="btn btn-primary mt-3">Ir al Catálogo</asp:HyperLink>
                </asp:Panel>

                <%-- Label para mostrar errores --%>
                <asp:Label ID="lblError" runat="server" CssClass="text-danger text-center d-block mt-3" Visible="false"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
