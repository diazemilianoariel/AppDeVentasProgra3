<%@ Page Title="" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="Factura.aspx.cs" Inherits="Front.Factura" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">




    <div class="invoice-container  border border-dark m-3   ">
        <!-- Encabezado -->
        <div class="row">
            <div class="col-md-3">
                <div class="logo-box">
                    <p>Tienda elicomer</p>
                    <img src="https://png.pngtree.com/png-clipart/20190611/original/pngtree-wolf-logo-png-image_2306634.jpg" alt="Logo" class="img-fluid" style="max-width: 200px;" />
                </div>
            </div>
            <div class="col-md-9 text-center">
                <h2>Tienda </h2>
                <p>Los Producto mas sustentables</p>
                <div class="contact-info">
                    <p>📍 Dirección: Calle Cualquiera 123, Ciudad</p>
                    <p>📧 contacto@sitio.com | ☎ (55) 1234-5678</p>

                    
                </div>
            </div>
        </div>

        <hr />

        <!-- Datos del Cliente -->
        <div class="row ">
            <div class="col-md-6 ">
                <p><strong>SEÑORES:</strong>
                    <asp:Literal ID="litCliente" runat="server"></asp:Literal></p>
                <p><strong>DIRECCIÓN:</strong>
                    <asp:Literal ID="litDireccion" runat="server"></asp:Literal></p>
            </div>
            <div class="col-md-6 ">
                <p><strong>TELÉFONO:</strong>
                    <asp:Literal ID="litTelefono" runat="server"></asp:Literal></p>
                <p><strong>Email:</strong>
                    <asp:Literal ID="litEmail" runat="server"></asp:Literal></p>
            </div>
        </div>

        <!-- Tabla de productos -->
        <table class="table table-bordered mt-3 ">
            <thead>
                <tr>
                    <th>Codigo</th>
                    <th>Nombre</th>
                    <th>Cantidad</th>
                    <th>Precio Unitario</th>
                    <th>SubTotal</th>



                    
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rptProductos" runat="server">
                    <ItemTemplate>
                        <tr>
                        
                            <td><%# Eval("Id") %></td>
                            <td><%# Eval("Nombre") %></td>
                            <td><%# Eval("cantidad") %></td>
                            <td><%# Eval("Precio", "{0:C}") %></td>
                            <td><%# Eval("SubTotalEnFactura", "{0:C}") %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>

        <!-- Observaciones -->
      

        <!-- Total y Firma -->
        <div class="row mt-3">
            <div class="col-md-6">
                    <p><strong>TOTAL:</strong>
                    <asp:Literal ID="litTotalFactura" runat="server"></asp:Literal></p>
            </div>
           
        </div>

        <!-- Pie de página -->
        <div class="footer-text">
            SERVICIO A DOMICILIO : no Disponible
        </div>
    </div>

 
    <asp:Button ID="btnVolver" runat="server" Text="Volver a mis compras" CssClass="btn btn-primary" OnClick="btnVolver_Click" />

    <asp:Button ID="btnImprimir" runat="server" Text="Imprimir Factura" CssClass="btn btn-success" OnClick="btnImprimir_Click" />


</asp:Content>
