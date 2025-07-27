<%@ Page Title="Gestión de Ventas" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="Ventas.aspx.cs" Inherits="Front.Ventas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .kpi-card { border-left: 5px solid #007bff; padding: 1rem; background-color: #f8f9fa; border-radius: .25rem; margin-bottom: 1rem; }
        .kpi-card h5 { font-size: 1rem; color: #6c757d; text-transform: uppercase; }
        .kpi-card .kpi-value { font-size: 2rem; font-weight: 700; color: #343a40; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="mb-4">Panel de Gestión de Ventas</h1>

    <div class="row">
        <div class="col-md-3">
            <div class="kpi-card shadow-sm"><%-- Ventas Pendientes --%>
                <h5>Ventas Pendientes</h5>
                <div class="kpi-value"><asp:Literal ID="litVentasPendientes" runat="server" Text="0"></asp:Literal></div>
            </div>
        </div>
        <div class="col-md-3">
            <div class="kpi-card shadow-sm" style="border-left-color: #ffc107;"><%-- Monto Pendiente --%>
                <h5>Monto Pendiente</h5>
                <div class="kpi-value"><asp:Literal ID="litMontoPendiente" runat="server" Text="$0.00"></asp:Literal></div>
            </div>
        </div>
        <div class="col-md-3">
            <div class="kpi-card shadow-sm" style="border-left-color: #28a745;"><%-- Ventas Aprobadas --%>
                <h5>Ventas Aprobadas</h5>
                <div class="kpi-value"><asp:Literal ID="litVentasAprobadas" runat="server" Text="0"></asp:Literal></div>
            </div>
        </div>
        <div class="col-md-3">
            <div class="kpi-card shadow-sm" style="border-left-color: #17a2b8;"><%-- Ingresos Totales --%>
                <h5>Ingresos Totales</h5>
                <div class="kpi-value"><asp:Literal ID="litIngresosTotales" runat="server" Text="$0.00"></asp:Literal></div>
            </div>
        </div>
    </div>
    
    <asp:Panel ID="pnlMensaje" runat="server" Visible="false" CssClass="alert mt-3"><asp:Label ID="lblMensaje" runat="server"></asp:Label></asp:Panel>

    <div class="card shadow-sm mt-4">
        <div class="card-header bg-warning text-dark"><h4 class="mb-0">Ventas Pendientes de Aprobación</h4></div>
        <div class="card-body">
            <asp:GridView ID="gvVentasPendientes" runat="server" AutoGenerateColumns="False" CssClass="table table-hover" OnRowCommand="gvVentas_RowCommand" GridLines="None">
                <Columns>
                    <asp:BoundField DataField="IdVenta" HeaderText="ID" />
                    <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:TemplateField HeaderText="Cliente"><ItemTemplate><%# Eval("Cliente.Nombre") %> <%# Eval("Cliente.Apellido") %></ItemTemplate></asp:TemplateField>
                    <asp:BoundField DataField="Monto" HeaderText="Monto" DataFormatString="{0:C}" />
                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:Button ID="btnAprobar" runat="server" Text="Aprobar" CommandName="Aprobar" CommandArgument='<%# Eval("IdVenta") %>' CssClass="btn btn-success btn-sm" />
                            <asp:Button ID="btnRechazar" runat="server" Text="Rechazar" CommandName="Rechazar" CommandArgument='<%# Eval("IdVenta") %>' CssClass="btn btn-danger btn-sm" />

                            <!-- aca  debe existir un boton para ver resumen de la compra a aprobar -->
                            <asp:Button ID="btnVerResumen" runat="server" Text="Ver Resumen" CommandName="VerResumen" CommandArgument='<%# Eval("IdVenta") %>' CssClass="btn btn-info btn-sm" />


                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <div class="card shadow-sm mt-5">
        <div class="card-header bg-light"><h4 class="mb-0">Historial de Ventas</h4></div>
        <div class="card-body">
            <div class="row mb-4 align-items-end p-3 bg-light border rounded">
                <div class="col-md-4"><label for="<%=txtFechaDesde.ClientID%>"><b>Desde:</b></label><asp:TextBox ID="txtFechaDesde" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox></div>
                <div class="col-md-4"><label for="<%=txtFechaHasta.ClientID%>"><b>Hasta:</b></label><asp:TextBox ID="txtFechaHasta" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox></div>
                <div class="col-md-2"><asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" CssClass="btn btn-primary w-100" OnClick="btnFiltrar_Click" /></div>
                <div class="col-md-2"><asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-secondary w-100" OnClick="btnLimpiar_Click" CausesValidation="false" /></div>
            </div>
            <asp:GridView ID="gvVentasRealizadas" runat="server" AutoGenerateColumns="False" CssClass="table table-hover" GridLines="None" AllowPaging="True" PageSize="10" OnPageIndexChanging="gvVentasRealizadas_PageIndexChanging">
                <Columns>
                    <asp:BoundField DataField="IdVenta" HeaderText="ID" />
                    <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:TemplateField HeaderText="Cliente"><ItemTemplate><%# Eval("Cliente.Nombre") %> <%# Eval("Cliente.Apellido") %></ItemTemplate></asp:TemplateField>
                    <asp:BoundField DataField="Monto" HeaderText="Monto" DataFormatString="{0:C}" />
                    <asp:TemplateField HeaderText="Estado"><ItemTemplate><span class='badge <%# (Eval("nombreEstadoVenta").ToString() == "Aprobado") ? "badge-success" : "badge-danger" %>'><%# Eval("nombreEstadoVenta") %></span></ItemTemplate></asp:TemplateField>
                    <asp:TemplateField HeaderText="Acciones"><ItemTemplate><asp:HyperLink runat="server" Text="Ver Factura" NavigateUrl='<%# "~/Factura.aspx?id=" + Eval("IdVenta") %>' CssClass="btn btn-info btn-sm" /></ItemTemplate></asp:TemplateField>
                </Columns>
                 <PagerStyle CssClass="pagination-ys" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>