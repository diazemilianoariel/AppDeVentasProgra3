<%@ Page Title="" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="Carrito.aspx.cs" Inherits="Front.CompraParcial" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    <h1>Carrito de Compras</h1>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="updPanelCarrito" runat="server">
        <ContentTemplate>
            <asp:Repeater ID="rptCarrito" runat="server">
                <ItemTemplate>
                    <div class="row mb-3">
                        <div class="col-md-2">
                            <img src='<%# Eval("Imagen") %>' alt='<%# Eval("Nombre") %>' class="img-thumbnail" />
                        </div>
                        <div class="col-md-3">
                            <h5><%# Eval("Nombre") %></h5>
                        </div>
                        <div class="col-md-2">
                            <asp:Label Text="Cantidad" runat="server" />
                            <asp:TextBox ID="txtCantidad" runat="server" Text='<%# Eval("Cantidad") %>' CssClass="form-control text-center" AutoPostBack="true" OnTextChanged="txtCantidad_TextChanged" ReadOnly="true" />
                        </div>
                        <div class="col-md-2">
                            <p>Precio: $<%# Eval("precioVenta") %></p>
                        </div>
                        <div class="col-md-2">
                            <p>Subtotal: $<%# Eval("Subtotal") %></p>
                        </div>
                        <div class="col-md-1">
                            <asp:Button ID="btnQuitar" runat="server" Text="🗑️" CommandArgument='<%# Eval("Id") %>' OnClick="btnQuitar_Click" CssClass="btn btn-danger" />
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <div class="row">
                <div class="col-md-12 text-right">
                    <h4>Total General: $<asp:Label ID="lblTotalGeneral" runat="server" Text="0"></asp:Label></h4>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-right">
                    <asp:Button ID="btnConfirmarCompra" runat="server" Text="Confirmar Compra" CssClass="btn btn-success" OnClick="btnConfirmarCompra_Click" />
                </div>
            </div>


            <div class="row">
                <div class="col-md-12 text-right">
                    <asp:Button ID="btnVolverHome" runat="server" Text="Volver al Home" CssClass="btn btn-secondary" OnClick="btnVolverHome_Click" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblMensaje" runat="server" Text="" Visible="false" CssClass="alert alert-danger"></asp:Label>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>




</asp:Content>
