<%@ Page Title="" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="ProductoModificar.aspx.cs" Inherits="Front.ProductosABM.ProductoModificar" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />


    <script type="text/javascript">
        function updateImagePreview() {
            var url = document.getElementById('<%= TextBoxImagen.ClientID %>').value;
            var img = document.getElementById('<%= ImagenProducto.ClientID %>');
            if (url) {
                img.src = url;
                img.style.display = 'block';
            } else {
                img.style.display = 'none';
            }
        }
    </script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-10 col-lg-8">
                <div class="card shadow-sm">
                    <div class="card-body">
                        <h1 class="text-center mb-4">Modificar Producto</h1>
                        <asp:Label ID="LabelId" runat="server" Visible="false"></asp:Label>

                        <div class="row">
                            <%-- Columna Izquierda --%>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label runat="server" AssociatedControlID="TextBoxNombre" CssClass="form-label fw-bold">Nombre</asp:Label>
                                    <asp:TextBox ID="TextBoxNombre" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ErrorMessage="El nombre es obligatorio." ControlToValidate="TextBoxNombre" runat="server" CssClass="text-danger" Display="Dynamic" />
                                </div>
                                <div class="form-group">
                                    <asp:Label runat="server" AssociatedControlID="TextBoxDescripcion" CssClass="form-label fw-bold">Descripción</asp:Label>
                                    <asp:TextBox ID="TextBoxDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:Label runat="server" AssociatedControlID="TextBoxPrecio" CssClass="form-label fw-bold">Precio</asp:Label>
                                    <asp:TextBox ID="TextBoxPrecio" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:CompareValidator ErrorMessage="Debe ser un número válido." ControlToValidate="TextBoxPrecio" Operator="DataTypeCheck" Type="Currency" runat="server" CssClass="text-danger" Display="Dynamic" />
                                </div>
                                <div class="form-group">
                                    <asp:Label runat="server" AssociatedControlID="TextBoxGanancia" CssClass="form-label fw-bold">Margen de Ganancia (%)</asp:Label>
                                    <asp:TextBox ID="TextBoxGanancia" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:CompareValidator ErrorMessage="Debe ser un número válido." ControlToValidate="TextBoxGanancia" Operator="DataTypeCheck" Type="Currency" runat="server" CssClass="text-danger" Display="Dynamic" />
                                </div>
                                <div class="form-group">
                                    <asp:Label runat="server" AssociatedControlID="TextBoxStock" CssClass="form-label fw-bold">Stock</asp:Label>
                                    <asp:TextBox ID="TextBoxStock" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:CompareValidator ErrorMessage="Debe ser un número entero." ControlToValidate="TextBoxStock" Operator="DataTypeCheck" Type="Integer" runat="server" CssClass="text-danger" Display="Dynamic" />
                                </div>
                            </div>

                            <%-- Columna Derecha --%>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label runat="server" AssociatedControlID="DropDownListMarca" CssClass="form-label fw-bold">Marca</asp:Label>
                                    <asp:DropDownList ID="DropDownListMarca" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <asp:Label runat="server" AssociatedControlID="DropDownListTipo" CssClass="form-label fw-bold">Tipo</asp:Label>
                                    <asp:DropDownList ID="DropDownListTipo" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <asp:Label runat="server" AssociatedControlID="DropDownListCategoria" CssClass="form-label fw-bold">Categoría</asp:Label>
                                    <asp:DropDownList ID="DropDownListCategoria" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <asp:Label runat="server" AssociatedControlID="cblProveedores" CssClass="form-label fw-bold">Proveedores</asp:Label>
                                    <asp:CheckBoxList ID="cblProveedores" runat="server" CssClass="checkbox-list-class"></asp:CheckBoxList>
                                </div>
                                <div class="form-group">
                                    <asp:Label runat="server" AssociatedControlID="TextBoxImagen" CssClass="form-label fw-bold">URL de la Imagen</asp:Label>
                                    <asp:TextBox ID="TextBoxImagen" runat="server" CssClass="form-control" onkeyup="updateImagePreview();"></asp:TextBox>
                                    <img id="ImagenProducto" runat="server" class="img-fluid mt-2 rounded" style="max-height: 150px;" />
                                </div>
                                <div class="form-group form-check mt-4">
                                    <asp:CheckBox ID="CheckBoxEstado" runat="server" CssClass="form-check-input" />
                                    <asp:Label runat="server" AssociatedControlID="CheckBoxEstado" CssClass="form-check-label">Producto Activo</asp:Label>
                                </div>
                            </div>
                        </div>

                        <div class="row mt-3 justify-content-center">
                            <div class="col-md-10 col-lg-8">
                                <asp:Label ID="LabelError" runat="server" Visible="false" Width="100%"></asp:Label>
                            </div>
                        </div>

                        <div class="row mt-4">
                            <div class="col-md-12 text-center">
                                <asp:Button ID="ButtonGuardar" runat="server" Text="Guardar Cambios" CssClass="btn btn-primary" OnClick="ButtonGuardar_Click" />
                                <asp:Button ID="ButtonCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="ButtonCancelar_Click" CausesValidation="false" />
                            </div>
                        </div>



                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
