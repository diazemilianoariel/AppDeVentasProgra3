<%@ Page Title="" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Front.Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function actualizarContadorCarrito(totalProductos) {
            document.getElementById('<%= ((Front.MASTER)Master).CartCountClientID %>').innerText = totalProductos;
        }
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <h1>Catálogo de Productos</h1>

    <!-- Filtros de búsqueda -->
    <div class="filters mb-4 ">
        <div class="row">
            <div class="col-md-8">
                <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" Placeholder="Buscar productos..." AutoPostBack="true" OnTextChanged="txtBuscar_TextChanged"></asp:TextBox>

            </div>

        </div>
    </div>

    <!-- Mensaje de confirmación -->
    <asp:UpdatePanel ID="updPanelMensaje" runat="server">
        <ContentTemplate>
            <asp:Label ID="lblMensaje" runat="server" CssClass="alert alert-success" Visible="false"></asp:Label>
        </ContentTemplate>
    </asp:UpdatePanel>

    <!-- Listado de productos -->
    <asp:UpdatePanel ID="updPanelProductos" runat="server">
        <ContentTemplate>
            <div class="row product-list">
                <asp:Repeater ID="rptProductos" runat="server">


                    <ItemTemplate>
                        <div class="col-md-4 mb-4">
                            <div class="card h-100">
                                <img src='<%# Eval("Imagen") %>' alt='<%# Eval("Nombre") %>' class="card-img-top product-image" style="height: 200px; object-fit: cover;" />
                                <div class="card-body">
                                    <h5 class="card-title"><%# Eval("Nombre") %></h5>
                                    <p class="card-text">Precio: $<%# Eval("precioVenta") %></p>


                                    <p class="card-text">Stock Disponible: <%# Eval("Stock") %></p>

                                    <div class="input-group">

                                       <%-- <div class="input-group-prepend">
                                            <asp:Button ID="btnDisminuir" runat="server" Text="-" CommandArgument='<%# Eval("Id") %>' OnClick="btnDisminuir_Click" CssClass="btn btn-outline-secondary" />
                                        </div>--%>


                                       <%-- <asp:TextBox ID="txtCantidad" runat="server" Text="1" CssClass="form-control text-center" ReadOnly="true" />--%>



                                        <%--<div class="input-group-append">
                                            <asp:Button ID="btnAumentar" runat="server" Text="+" CommandArgument='<%# Eval("Id") + "," + Eval("Stock") %>' OnClick="btnAumentar_Click" CssClass="btn btn-outline-secondary" />
                                        </div>--%>

                                    </div>
                                </div>


                                <div class="card-footer text-center">
                                    <%--<asp:Button ID="btnAgregarCarrito" runat="server" Text="Agregar al Carrito" CommandArgument='<%# Eval("Id") %>' OnClick="btnAgregarCarrito_Click" CssClass="btn btn-success" />--%>
                                    <asp:Button ID="btnVerDetalle" runat="server" Text="Ver Detalle" CommandArgument='<%# Eval("Id") %>' OnClick="btnVerDetalle_Click" CssClass="btn btn-info" />
                                   <%-- <asp:Button ID="btnQuitarCarrito" runat="server" Text="Quitar del Carrito" CommandArgument='<%# Eval("Id") %>' OnClick="btnQuitarCarrito_Click" CssClass="btn btn-danger" />--%>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>


                </asp:Repeater>


            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
