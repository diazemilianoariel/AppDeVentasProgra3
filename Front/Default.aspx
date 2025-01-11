<%@ Page Title="" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Front.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Catálogo de Productos</h1>

      <!-- Filtros de búsqueda -->
  <div class="filters">
      <div class="row">
          <div class="col-md-8">
              <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" Placeholder="Buscar productos..."></asp:TextBox>
          </div>
          <div class="col-md-4">
              <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="btnBuscar_Click" />
          </div>
      </div>
  </div>

    <!-- Listado de productos -->
    <div class="row product-list">
        <asp:Repeater ID="rptProductos" runat="server">
            <ItemTemplate>
                <div class="col-md-4 mb-4">
                    <div class="card h-100">
                        <img src='<%# Eval("Imagen") %>' alt='<%# Eval("Nombre") %>' class="card-img-top product-image" style="height: 200px; object-fit: cover;" />
                        <div class="card-body">
                            <h5 class="card-title"><%# Eval("Nombre") %></h5>
                            <p class="card-text">Precio: $<%# Eval("Precio") %></p>
                        </div>
                        <div class="card-footer text-center">
                            <asp:Button ID="btnAgregarCarrito" runat="server" Text="Agregar al Carrito" CommandArgument='<%# Eval("Id") %>' OnClick="btnAgregarCarrito_Click" CssClass="btn btn-success" />
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>

    <!-- Carrito de compras -->
    <div class="shopping-cart">
        <h2>Carrito de Compras</h2>
        <asp:GridView ID="gvCarrito" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="Nombre" HeaderText="Producto" />
                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                <asp:BoundField DataField="Precio" HeaderText="Precio" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CommandArgument='<%# Eval("Id") %>' OnClick="btnEliminar_Click" CssClass="btn btn-danger" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:Button ID="btnConfirmarCompra" runat="server" Text="Confirmar Compra" CssClass="btn btn-primary" OnClick="btnConfirmarCompra_Click" />
    </div>
</asp:Content>
