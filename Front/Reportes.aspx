<%@ Page Title="" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="Reportes.aspx.cs" Inherits="Front.Reportes" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.11.5/css/jquery.dataTables.css">
    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/1.11.5/js/jquery.dataTables.js"></script>

    <style>
        .card-icon {
            font-size: 2rem;
        }

        .dashboard-container {
            margin: 20px;
        }
    </style>
</asp:Content>




<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="container-fluid dashboard-container">
        <div class="row">
            <!-- Tarjetas de KPIs -->
            <div class="col-md-3">
                <div class="card text-white bg-primary mb-3">
                    <div class="card-body">
                        <h5 class="card-title">Ventas Hoy</h5>
                        <p class="card-text fs-3">
                            <asp:Literal ID="litVentasHoy" runat="server" />
                        </p>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="card text-white bg-success mb-3">
                    <div class="card-body">
                        <h5 class="card-title">Usuarios registrados </h5>
                        <p class="card-text fs-3">
                            <asp:Literal ID="litUsuariosRegistrados" runat="server" />
                        </p>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="card text-white bg-warning mb-3">
                    <div class="card-body">
                        <h5 class="card-title">Pedidos Pendientes</h5>
                        <p class="card-text fs-3">
                            <asp:Literal ID="litPedidosPendientes" runat="server" />

                        </p>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="card text-white bg-danger mb-3">
                    <div class="card-body">
                        <h5 class="card-title">Cantidad Productos </h5>
                        <p class="card-text fs-3">
                            <asp:Literal ID="litCantidadProductos" runat="server" />
                        </p>
                    </div>
                </div>
            </div>
        </div>



        <!-- Sección de Gráficos -->

        <div class="row">
            <div class="col-md-6">
                <div class="card">
                    <div class="card-body">
                        <h5 class="card-title">Ventas Última Semana</h5>
                        <canvas id="ventasChart"></canvas>

                    </div>
                </div>
            </div>





            <div class="col-md-6">
                <div class="card">
                    <div class="card-body">
                        <h5 class="card-title">Productos por Categoría</h5>
                        <canvas id="ProductosChart"></canvas>
                    </div>
                </div>
            </div>
        </div>



        <!-- Tabla de Reportes -->
        <div class="row mt-4">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-body">
                        <h5 class="card-title">Historial de Ventas</h5>
                        <table class="table table-striped" id="ventasTable">
                            <thead>
                                <tr>
                                    <th>id</th>
                                    <th>Fecha</th>
                                    <th>Monto</th>
                                    <th>cliente</th>
                                    <th>Facturas</th>
                                </tr>
                            </thead>

                            <tbody>


                                <asp:Repeater ID="rptVentas" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Eval("IdVenta") %></td>
                                            <td><%# Eval("Fecha") %></td>
                                            <td><%# Eval("Monto") %></td>
                                            <td><%# Eval("Cliente.Nombre") %></td>


                                            <td>
                                                <a href="#"  class ="btn btn-primary " >Ver Factura</a>
                                             

                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>


                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Bootstrap JS -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>

    <!-- Chart.js Gráficos -->
    <script>
        // Gráfico de Ventas Última Semana



        var ctx1 = document.getElementById('ventasChart').getContext('2d');
        var ventasChart = new Chart(ctx1, {
            type: 'line',
            data: {
                labels: ['Lun', 'Mar', 'Mié', 'Jue', 'Vie', 'Sáb', 'Dom'],
                datasets: [{
                    label: 'Ventas',
                    data: [120, 150, 180, 200, 220, 250, 300],
                    borderColor: 'rgba(0, 123, 255, 1)',
                    borderWidth: 2,
                    fill: false
                }]
            }
        });

        // Gráfico de Clientes por Categoría

        var categorias = <%= CategoriasJson%>;
        var productos =  <%= ProductosJson %>;
        var ctx2 = document.getElementById('ProductosChart').getContext('2d');
        var productosChart = new Chart(ctx2, {
            type: 'doughnut',
            data: {
                labels: categorias,
                datasets: [{
                    data: productos,
                    backgroundColor: ['green', 'yellow', 'red', 'orange', 'blue', 'purple', 'cyan', 'magenta', 'lime', 'pink', 'teal', 'lavender', 'brown', 'beige', 'maroon', 'mint', 'olive', 'coral', 'navy', 'grey']
                }]
            }
        });


        // Inicializar DataTables
        $(document).ready(function () {
            $('#ventasTable').DataTable();
        });

    </script>



</asp:Content>
