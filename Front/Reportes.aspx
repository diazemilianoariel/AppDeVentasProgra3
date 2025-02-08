<%@ Page Title="" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="Reportes.aspx.cs" Inherits="Front.Reportes" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
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
                                                <asp:HiddenField ID="hfVentasSemana" runat="server" />

                          

                    </div>
                </div>
            </div>





            <div class="col-md-6">
                <div class="card">
                    <div class="card-body">
                        <h5 class="card-title">Clientes por Categoría</h5>
                        <canvas id="clientesChart"></canvas>
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
                        <table class="table table-striped">
                            <thead>
                                <tr>
                                    <th>#</th>
                                    <th>Cliente</th>
                                    <th>Producto</th>
                                    <th>Fecha</th>
                                    <th>Monto</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>1</td>
                                    <td>Juan Pérez</td>
                                    <td>Producto A</td>
                                    <td>2025-02-10</td>
                                    <td>$150</td>
                                </tr>
                                <tr>
                                    <td>2</td>
                                    <td>Ana Gómez</td>
                                    <td>Producto B</td>
                                    <td>2025-02-10</td>
                                    <td>$200</td>
                                </tr>
                                <tr>
                                    <td>3</td>
                                    <td>Pedro Ruiz</td>
                                    <td>Producto C</td>
                                    <td>2025-02-10</td>
                                    <td>$350</td>
                                </tr>
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
        var ctx2 = document.getElementById('clientesChart').getContext('2d');
        var clientesChart = new Chart(ctx2, {
            type: 'doughnut',
            data: {
                labels: ['Frecuentes', 'Ocasionales', 'Nuevos'],
                datasets: [{
                    data: [50, 30, 20],
                    backgroundColor: ['green', 'yellow', 'red']
                }]
            }
        });
    </script>



</asp:Content>
