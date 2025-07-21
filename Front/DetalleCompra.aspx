<%@ Page Title="" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="DetalleCompra.aspx.cs" Inherits="Front.DetalleCompra" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">





</asp:Content>





<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="row mt-5 justify-content-center">
        <div class="col-md-8">
            <div class="card">
                <div class="card-header">
                    <h4>Detalles de la Compra</h4>
                </div>
                <div class="card-body">
                    
                    <%-- ENCABEZADO CON INFO GENERAL --%>
                    <div class="row mb-4">
                        <div class="col-md-6">
                            <strong>Nro. de Compra:</strong>
                            <asp:Literal ID="litIdCompra" runat="server"></asp:Literal>
                        </div>
                        <div class="col-md-6 text-md-right">
                            <strong>Fecha:</strong>
                            <asp:Literal ID="litFechaCompra" runat="server"></asp:Literal>
                        </div>
                    </div>

                    <%-- TABLA DE PRODUCTOS --%>
                    <div class="table-responsive">
                        <asp:GridView ID="gvDetalles" runat="server"
                            CssClass="table"
                            AutoGenerateColumns="false"
                            EmptyDataText="No se encontraron detalles para esta compra.">
                            <Columns>
                                <asp:BoundField DataField="nombre" HeaderText="Producto" />
                                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" />
                                <asp:BoundField DataField="precio" HeaderText="Precio Unitario" DataFormatString="{0:c}" ItemStyle-CssClass="text-right" HeaderStyle-CssClass="text-right" />
                            </Columns>
                        </asp:GridView>
                    </div>

                    <hr />

                    <%-- RESUMEN FINAL --%>
                    <div class="row justify-content-end">
                        <div class="col-md-4 text-right">
                            <h3>
                                <small>Total:</small>
                                <asp:Literal ID="litTotalCompra" runat="server"></asp:Literal>
                            </h3>
                        </div>
                    </div>
                </div>
                <div class="card-footer text-center">
                    <a href="MisCompras.aspx" class="btn btn-secondary">Volver a Mis Compras</a>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
