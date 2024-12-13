<%@ Page Title="" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Front.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row" id="productCards">
            <!-- Aquí se agregarán las tarjetas de productos dinámicamente -->

            <div class="col-md-4">
                <div class="card mb-4 shadow-sm">
                    <img src="ruta_de_la_imagen_del_producto" class="card-img-top" alt="nombre_del_producto">
                    <div class="card-body">
                        <h5 class="card-title">nombre_del_producto</h5>
                        <p class="card-text">descripcion_del_producto</p>
                        <div class="d-flex justify-content-between align-items-center">
                            <div class="btn-group">
                                <a href="ComprarProducto.aspx?id=id_del_producto" class="btn btn-sm btn-outline-primary">Comprar</a>
                            </div>
                            <small class="text-muted">$precio_del_producto</small>
                        </div>
                    </div>
                </div>
            </div>


        </div>
    </div>
</asp:Content>
