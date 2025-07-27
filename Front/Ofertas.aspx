<%@ Page Title="Ofertas" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="Ofertas.aspx.cs" Inherits="Front.Ofertas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- Reutilizamos el estilo del catálogo --%>
    <style>
        .product-card:hover {
            transform: scale(1.05);
            box-shadow: 0 8px 16px rgba(0,0,0,.2);
            cursor: pointer;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <div class="text-center bg-white p-4 rounded shadow-sm mb-4">
            <h1 class="mb-3">Ofertas Especiales</h1>
            <p class="text-muted">¡Aprovechá nuestros productos seleccionados a un precio increíble!</p>
        </div>

        <div class="row mt-4">
            <asp:Repeater ID="rptOfertas" runat="server">
                <ItemTemplate>
                    <div class="col-lg-3 col-md-4 col-sm-6 mb-4">
                        <asp:HyperLink NavigateUrl='<%# "~/ProductosABM/DetalleProducto.aspx?id=" + Eval("id") %>' runat="server" CssClass="text-decoration-none text-dark">
                            <div class="card h-100 product-card">
                                <img src='<%# Eval("Imagen") %>' alt='<%# Eval("nombre") %>' class="card-img-top" style="height: 250px; object-fit: cover;" />
                                <div class="card-body">
                                    <h5 class="card-title"><%# Eval("nombre") %></h5>
                                    <p class="card-text font-weight-bold">Precio: <%# ((decimal)Eval("precioVenta")).ToString("C") %></p>
                                    <p class="card-text">Stock Disponible: <%# Eval("stock") %></p>
                                </div>
                            </div>
                        </asp:HyperLink>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <asp:Panel ID="pnlNoHayOfertas" runat="server" Visible="false" CssClass="text-center w-100">
                <div class="alert alert-info">
                    <h4>No hay ofertas disponibles en este momento.</h4>
                    <p>¡Vuelve a visitarnos pronto!</p>
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
