<%@ Page Title="Agregar Producto" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="ProductoAgregar.aspx.cs" Inherits="Front.ProductosABM.ProductoAgregar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- Estilos para mejorar la apariencia de la lista de checkboxes --%>
    <style>
        .checkbox-list-class {
            max-height: 200px;
            overflow-y: auto;
            border: 1px solid #ced4da;
            padding: 10px;
            border-radius: .25rem;
            display: grid;
            grid-template-columns: repeat(2, 1fr);
            gap: 5px;
        }
        .checkbox-list-class label {
            margin-bottom: 0;
            margin-left: 5px;
        }
    </style>

    <%-- Script para la previsualización de la imagen --%>
    <script type="text/javascript">
        function updateImagePreview() {
            var url = document.getElementById('<%= TextBoxImagen.ClientID %>').value;
            var img = document.getElementById('<%= ImagePreview.ClientID %>');
            if (url && url.trim() !== '') {
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
                        <h1 class="text-center mb-4">Agregar Nuevo Producto</h1>

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
                                    <asp:RangeValidator ErrorMessage="El precio no puede ser negativo." ControlToValidate="TextBoxPrecio" MinimumValue="0" MaximumValue="99999999" Type="Currency" runat="server" CssClass="text-danger" Display="Dynamic" />
                                </div>
                                <div class="form-group">
                                    <asp:Label runat="server" AssociatedControlID="TextBoxGanancia" CssClass="form-label fw-bold">Margen de Ganancia (%)</asp:Label>
                                    <asp:TextBox ID="TextBoxGanancia" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:CompareValidator ErrorMessage="Debe ser un número válido." ControlToValidate="TextBoxGanancia" Operator="DataTypeCheck" Type="Currency" runat="server" CssClass="text-danger" Display="Dynamic" />
                                    <asp:RangeValidator ErrorMessage="El margen no puede ser negativo." ControlToValidate="TextBoxGanancia" MinimumValue="0" MaximumValue="10000" Type="Currency" runat="server" CssClass="text-danger" Display="Dynamic" />
                                </div>
                                <div class="form-group">
                                    <asp:Label runat="server" AssociatedControlID="TextBoxStock" CssClass="form-label fw-bold">Stock Inicial</asp:Label>
                                    <asp:TextBox ID="TextBoxStock" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:CompareValidator ErrorMessage="Debe ser un número entero." ControlToValidate="TextBoxStock" Operator="DataTypeCheck" Type="Integer" runat="server" CssClass="text-danger" Display="Dynamic" />
                                    <asp:RangeValidator ErrorMessage="El stock no puede ser negativo." ControlToValidate="TextBoxStock" MinimumValue="0" MaximumValue="999999" Type="Integer" runat="server" CssClass="text-danger" Display="Dynamic" />
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
                                    <asp:Image ID="ImagePreview" runat="server" CssClass="img-fluid mt-2 rounded" style="max-height: 150px; display: none;" />
                                </div>
                                <div class="form-group form-check mt-4">
                                    <asp:CheckBox ID="CheckBoxEstado" runat="server" CssClass="form-check-input" Checked="true" />
                                    <asp:Label runat="server" AssociatedControlID="CheckBoxEstado" CssClass="form-check-label">Producto Activo</asp:Label>
                                </div>
                            </div>
                        </div>

                        <asp:Label ID="LabelError" runat="server" CssClass="text-danger text-center d-block mt-3" Visible="false"></asp:Label>

                        <div class="row mt-4">
                            <div class="col-md-12 text-center">
                                <asp:Button ID="ButtonGuardar" runat="server" Text="Guardar Producto" CssClass="btn btn-primary" OnClick="ButtonGuardar_Click" />
                                <asp:Button ID="ButtonCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="ButtonCancelar_Click" CausesValidation="false" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>