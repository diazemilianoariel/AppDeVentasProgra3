<%@ Page Title="Factura" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="Factura.aspx.cs" Inherits="Front.Factura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .invoice-container {
            background-color: #fff;
            border: 1px solid #dee2e6;
            padding: 30px;
        }
        @media print {
            body * {
                visibility: hidden;
            }
            .invoice-container, .invoice-container * {
                visibility: visible;
            }
            .invoice-container {
                position: absolute;
                left: 0;
                top: 0;
                width: 100%;
            }
            .no-print {
                display: none;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="invoice-container border border-dark m-3">
        <!-- Encabezado -->
        <div class="row align-items-center">
            <div class="col-md-6">
                <img src="https://png.pngtree.com/png-clipart/20190611/original/pngtree-wolf-logo-png-image_2306634.jpg" alt="Logo" class="img-fluid" style="max-width: 150px;" />
                <h4 class="mt-2">La Tienda Online</h4>
                <p class="text-muted mb-0">Calle Falsa 123, Ciudad</p>
                <p class="text-muted mb-0">contacto@tienda.com</p>
            </div>
            <div class="col-md-6 text-md-right">
                <h2>FACTURA</h2>
                <p class="mb-1"><strong>N° Factura:</strong> <asp:Literal ID="litNumeroFactura" runat="server"></asp:Literal></p>
                <p class="mb-0"><strong>Fecha:</strong> <asp:Literal ID="litFecha" runat="server"></asp:Literal></p>
            </div>
        </div>
        <hr />

        <!-- Datos del Cliente -->
        <div class="row">
            <div class="col-md-12">
                <h5>Facturar a:</h5>
                <p class="mb-0"><strong>Señor/a:</strong> <asp:Literal ID="litCliente" runat="server"></asp:Literal></p>
                <p class="mb-0"><strong>Dirección:</strong> <asp:Literal ID="litDireccion" runat="server"></asp:Literal></p>
                <p class="mb-0"><strong>Teléfono:</strong> <asp:Literal ID="litTelefono" runat="server"></asp:Literal></p>
                <p class="mb-0"><strong>Email:</strong> <asp:Literal ID="litEmail" runat="server"></asp:Literal></p>
            </div>
        </div>

        <!-- Tabla de productos -->
        <table class="table table-bordered mt-4">
            <thead class="thead-light">
                <tr>
                    <th>Código</th>
                    <th>Nombre</th>
                    <th class="text-center">Cantidad</th>
                    <th class="text-right">Precio Unitario</th>
                    <th class="text-right">SubTotal</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rptProductos" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("id") %></td>
                            <td><%# Eval("nombre") %></td>
                            <td class="text-center"><%# Eval("Cantidad") %></td>
                            <td class="text-right"><%# Eval("precioVenta", "{0:C}") %></td>
                            <td class="text-right"><%# Eval("SubTotalEnFactura", "{0:C}") %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>

        <!-- Total -->
        <div class="row justify-content-end mt-3">
            <div class="col-md-4">
                <div class="text-right">
                    <h4 class="font-weight-bold">TOTAL: <asp:Literal ID="litTotalFactura" runat="server"></asp:Literal></h4>
                </div>
            </div>
        </div>
        <hr />
        <div class="footer-text text-center text-muted">
            <p>¡Gracias por su compra!</p>
        </div>
    </div>

    <asp:Label ID="lblError" runat="server" CssClass="text-danger text-center d-block mt-3" Visible="false"></asp:Label>

    <div class="text-center my-4 no-print">
        <asp:Button ID="btnVolver" runat="server" Text="Volver a Mis Compras" CssClass="btn btn-secondary" OnClick="btnVolver_Click" />
        <asp:Button ID="btnImprimir" runat="server" Text="Imprimir Factura" CssClass="btn btn-success" OnClick="btnImprimir_Click" />
    </div>
</asp:Content>
