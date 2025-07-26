<%@ Page Title="" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Front.Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link rel="stylesheet" type="text/css" href="~/EstiloCSS/estilo.css" />

    <style>
        .product-card {
            transition: transform 0.3s ease, box-shadow 0.3s ease;
        }

            .product-card:hover {
                transform: scale(1.05); /* Aumenta ligeramente el tamaño */
                box-shadow: 0 8px 16px rgba(0, 0, 0, 0.2); /* Agrega una sombra */
                cursor: pointer; /* Cambia el cursor a una mano */
            }
    </style>

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
   
        <ContentTemplate>
            <div class="row product-list">
                <asp:Repeater ID="rptProductos" runat="server">


                    <ItemTemplate>
                        <div class="col-md-4 mb-4">
                            <asp:HyperLink NavigateUrl='<%# "~/ProductosABM/DetalleProducto.aspx?id=" + Eval("Id") %>' runat="server" CssClass="text-decoration-none">
                                <div class="card h-100 product-card">
                                    <img src='<%# Eval("Imagen") %>' alt='<%# Eval("Nombre") %>' class="card-img-top product-image" style="height: 200px; object-fit: cover;" />
                                    <div class="card-body">
                                        <h5 class="card-title"><%# Eval("Nombre") %></h5>
                                        <p class="card-text">Precio: $<%# Eval("precioVenta") %></p>
                                        <p class="card-text">Stock Disponible: <%# Eval("Stock") %></p>
                                    </div>
                                </div>
                            </asp:HyperLink>
                        </div>
                    </ItemTemplate>


                </asp:Repeater>


            </div>
        </ContentTemplate>
    


</asp:Content>
