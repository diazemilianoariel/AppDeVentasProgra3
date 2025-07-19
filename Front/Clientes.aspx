<%@ Page Title="" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="Front.Clientes" ContentType="text/html; charset=utf-8" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="estilos.css" rel="stylesheet" />
    <style>
        body {
            background-image: url('https://www.shutterstock.com/shutterstock/photos/2369360047/display_1500/stock-photo-mockup-wall-in-the-children-s-room-on-wall-cream-color-background-d-rendering-2369360048.jpg');
            background-size: cover;
            background-repeat: no-repeat;
            background-attachment: fixed;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <!-- Título y descripción -->
        <div class="text-center">
            <h1 class="mb-3">Gestión de Usuarios</h1>
            <p class="text-muted">Administra y organiza la información de tus Usuarios de manera sencilla y rápida.</p>
        </div>
    </div>



    <div class="row mt-5 justify-content-center">
        <asp:HyperLink NavigateUrl="~/UsuariosABM/UsuarioAgregar.aspx" runat="server" CssClass="btn btn-primary">Nuevo Usuario</asp:HyperLink>
    </div>





    <!-- Tabla de Clientes -->

     <div class="mt-4">
        <h4 class="text-center mb-4">Lista de Usuarios</h4>
        <asp:GridView ID="GridViewClientes" runat="server" CssClass="table table-striped table-bordered table-dark w-100" AutoGenerateColumns="False" OnRowCommand="GridViewClientes_RowCommand">
            <Columns>
                <asp:BoundField DataField="id" HeaderText="ID" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="apellido" HeaderText="Apellido" />
                <asp:BoundField DataField="dni" HeaderText="DNI" />
                <asp:BoundField DataField="direccion" HeaderText="Dirección" />
                <asp:BoundField DataField="telefono" HeaderText="Telefono" />
                <asp:BoundField DataField="email" HeaderText="Email" />
             
                <%-- <asp:BoundField DataField="clave" HeaderText="Clave" /> --%>
                
              
                <asp:BoundField DataField="Perfil.Nombre" HeaderText="Perfil" />
                
                <asp:BoundField DataField="estado" HeaderText="Estado" />

                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnModificar" runat="server" Text="Modificar" CommandName="Modificar" CommandArgument='<%# Eval("id") %>' CssClass="btn btn-success btn-sm" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CommandName="Eliminar" CommandArgument='<%# Eval("id") %>' CssClass="btn btn-danger btn-sm" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>






    <%--    <!-- Bootstrap JS (Opcional, para interactividad como tooltips) -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>--%>
</asp:Content>


