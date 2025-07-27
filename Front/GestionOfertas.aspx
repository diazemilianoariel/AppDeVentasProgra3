<%@ Page Title="Gestión de Ofertas" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="GestionOfertas.aspx.cs" Inherits="Front.GestionOfertas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="text-center bg-white p-4 rounded shadow-sm mb-4">
            <h1 class="mb-3">Gestión de Ofertas</h1>
            <p class="text-muted">Seleccione los productos en oferta y defina un precio y fechas (opcional).</p>
        </div>

        <asp:Label ID="lblMensaje" runat="server" Visible="false" CssClass="alert alert-success"></asp:Label>

        <div class="card shadow-sm">
            <div class="card-body">
                <div class="table-responsive">
                    <asp:GridView ID="gvProductosOfertas" runat="server"
                        AutoGenerateColumns="False"
                        CssClass="table table-hover align-middle"
                        DataKeyNames="id">
                        <Columns>
                            <asp:BoundField DataField="id" HeaderText="ID" />
                            <asp:BoundField DataField="nombre" HeaderText="Producto" />
                            <asp:TemplateField HeaderText="En Oferta">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkEnOferta" runat="server" Checked='<%# Eval("EnOferta") %>' /></ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Precio Oferta">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtPrecioOferta" runat="server" Text='<%# Eval("PrecioOferta", "{0:F2}") %>' CssClass="form-control" Width="120px"></asp:TextBox></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fecha Inicio">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFechaInicio" runat="server" Text='<%# Eval("FechaInicioOferta", "{0:yyyy-MM-dd}") %>' CssClass="form-control" TextMode="Date"></asp:TextBox></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fecha Fin">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFechaFin" runat="server" Text='<%# Eval("FechaFinOferta", "{0:yyyy-MM-dd}") %>' CssClass="form-control" TextMode="Date"></asp:TextBox></ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="card-footer text-right">
                <asp:Button ID="btnGuardarOfertas" runat="server" Text="Guardar Cambios" CssClass="btn btn-primary" OnClick="btnGuardarOfertas_Click" />
            </div>
        </div>
    </div>
</asp:Content>
