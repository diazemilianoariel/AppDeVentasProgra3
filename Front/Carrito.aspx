<%@ Page Title="Carrito de Compras" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="Carrito.aspx.cs" Inherits="Front.CompraParcial" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .quantity-input {
            max-width: 70px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="container mt-5">
        <h1 class="mb-4">Carrito de Compras</h1>
        
        <asp:UpdatePanel ID="updPanelCarrito" runat="server">
            <ContentTemplate>
                <%-- Panel que se muestra cuando el carrito NO está vacío --%>
                <asp:Panel ID="pnlCarritoConItems" runat="server" Visible="false">
                    <div class="row">
                        <%-- Columna de productos --%>
                        <div class="col-lg-8">
                            <div class="card shadow-sm">
                                <div class="card-body">
                                    <asp:Repeater ID="rptCarrito" runat="server" OnItemCommand="rptCarrito_ItemCommand">
                                        <HeaderTemplate>
                                            <div class="d-none d-md-flex row font-weight-bold border-bottom pb-2 mb-3">
                                                <div class="col-md-5">Producto</div>
                                                <div class="col-md-2 text-center">Precio</div>
                                                <div class="col-md-3 text-center">Cantidad</div>
                                                <div class="col-md-2 text-right">Subtotal</div>
                                            </div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div class="row border-bottom py-3 align-items-center">
                                                <div class="col-12 col-md-5 d-flex align-items-center">
                                                    <img src='<%# Eval("Imagen") %>' alt='<%# Eval("Nombre") %>' style="width: 60px; height: 60px; object-fit: cover;" class="img-thumbnail mr-3" />
                                                    <div>
                                                        <h6 class="mb-0"><%# Eval("Nombre") %></h6>
                                                        <asp:Button ID="btnQuitar" runat="server" Text="Quitar" CommandName="Quitar" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-link btn-sm text-danger p-0" />
                                                    </div>
                                                </div>
                                                <div class="col-4 col-md-2 text-md-center mt-2 mt-md-0">
                                                    <span class="d-md-none">Precio: </span>$<%# Eval("precioVenta", "{0:F2}") %>
                                                </div>
                                                <div class="col-4 col-md-3 text-md-center mt-2 mt-md-0">
                                                    <div class="input-group justify-content-center">
                                                        <div class="input-group-prepend">
                                                            <asp:Button ID="btnDisminuir" runat="server" Text="-" CommandName="Disminuir" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-outline-secondary btn-sm" />
                                                        </div>
                                                        <asp:TextBox ID="txtCantidad" runat="server" Text='<%# Eval("Cantidad") %>' CssClass="form-control form-control-sm text-center quantity-input" ReadOnly="true" />
                                                        <div class="input-group-append">
                                                            <asp:Button ID="btnAumentar" runat="server" Text="+" CommandName="Aumentar" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-outline-secondary btn-sm" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-4 col-md-2 text-right mt-2 mt-md-0">
                                                    <span class="d-md-none">Subtotal: </span>
                                                    <strong>$<%# Eval("SubTotal", "{0:F2}") %></strong>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>

                        <%-- Columna de resumen --%>
                        <div class="col-lg-4 mt-4 mt-lg-0">
                            <div class="card shadow-sm">
                                <div class="card-body">
                                    <h4 class="card-title mb-3">Resumen de Compra</h4>
                                    <div class="d-flex justify-content-between">
                                        <span>Subtotal</span>
                                        <span>$<asp:Literal ID="litSubtotal" runat="server" Text="0.00"></asp:Literal></span>
                                    </div>
                                    <hr />
                                    <div class="d-flex justify-content-between font-weight-bold h5">
                                        <span>Total</span>
                                        <span>$<asp:Literal ID="litTotalGeneral" runat="server" Text="0.00"></asp:Literal></span>
                                    </div>
                                    <div class="d-grid gap-2 mt-4">
                                        <asp:Button ID="btnConfirmarCompra" runat="server" Text="Finalizar Compra" CssClass="btn btn-success btn-block" OnClick="btnConfirmarCompra_Click" />
                                        <asp:Button ID="btnVolverHome" runat="server" Text="Seguir Comprando" CssClass="btn btn-outline-primary btn-block" OnClick="btnVolverHome_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>

                <%-- Panel que se muestra cuando el carrito está vacío --%>
                <asp:Panel ID="pnlCarritoVacio" runat="server" Visible="false">
                    <div class="text-center card shadow-sm p-5">
                        <h3 class="text-muted">Tu carrito está vacío</h3>
                        <p>Agregá productos desde nuestro catálogo para poder verlos aquí.</p>
                        <div class="mt-3">
                            <asp:Button ID="btnIrAlCatalogo" runat="server" Text="Ir al Catálogo" CssClass="btn btn-primary" OnClick="btnVolverHome_Click" />
                        </div>
                    </div>
                </asp:Panel>

                <asp:Label ID="lblMensaje" runat="server" Text="" Visible="false" CssClass="alert alert-danger mt-3"></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
