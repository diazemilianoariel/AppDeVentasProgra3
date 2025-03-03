<%@ Page Title="" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="Ventas.aspx.cs" Inherits="Front.Ventas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row mt-3">
        <div class="col-md-12 text-center">
            <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control d-inline-block w-50" placeholder="Buscar por Id..."></asp:TextBox>
            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="btnBuscar_Click" />
        </div>
    </div>


    <div class="row mt-3">
        <div class="col-md-12 text-center">
            <asp:Label ID="lblMensaje" runat="server" CssClass="alert" Visible="false"></asp:Label>
        </div>
    </div>


    <div class="row mt-5">
        <div class="col-md-12">
            <h2 class="text-center">Ventas Pendientes</h2>
            <asp:GridView ID="gvVentasPendientes" runat="server" AutoGenerateColumns="False" CssClass="table table-striped">
                <Columns>
                    <asp:BoundField DataField="idVenta" HeaderText="ID Venta" />
                    <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
                    <asp:BoundField DataField="Monto" HeaderText="Monto" />
                    <asp:BoundField DataField="Cliente.id" HeaderText="ID Usuario" />
                    <asp:BoundField DataField="EnLocal" HeaderText="En Local" />
                    <asp:BoundField DataField="idEstadoVenta" HeaderText="Id Estado" />
                    <asp:BoundField DataField="nombreEstadoVenta" HeaderText="Estado" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnAprobar" runat="server" Text="Aprobar" CommandArgument='<%# Eval("IdVenta") %>' OnClick="btnAprobar_Click" CssClass="btn btn-success btn-sm" />
                            <asp:Button ID="btnRechazar" runat="server" Text="Rechazar" CommandArgument='<%# Eval("IdVenta") %>' OnClick="btnRechazar_Click" CssClass="btn btn-danger btn-sm" />

                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>

    </div>





    <div class="row mt-5">
        <div class="col-md-12">
            <h2 class="text-center">Ventas Realizadas</h2>
            <asp:GridView ID="gvVentas" runat="server" AutoGenerateColumns="False" CssClass="table table-striped">
                <Columns>
                    <asp:BoundField DataField="idVenta" HeaderText="ID Venta" />
                    <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
                    <asp:BoundField DataField="Monto" HeaderText="Monto" />
                    <asp:BoundField DataField="Cliente.id" HeaderText="ID Usuario" />
                    <asp:BoundField DataField="EnLocal" HeaderText="En Local" />
                    <asp:BoundField DataField="idEstadoVenta" HeaderText="Id Estado" />
                    <asp:BoundField DataField="nombreEstadoVenta" HeaderText="Estado" />
                </Columns>
            </asp:GridView>
        </div>
    </div>




</asp:Content>
