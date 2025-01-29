<%@ Page Title="" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="Perfiles.aspx.cs" Inherits="Front.Perfiles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>




<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container mt-5">


        <div class="row">
            <div>
                <h1 class="text-center">Gestion de Perfiles</h1>
                <p class="text-muted">Administre y organice la información de sus Perfiles de manera sencilla y rápida.</p>
            </div>
        </div>


        <div class="container mt-5">
            <div class="row">
                <!-- Columna izquierda con los TextBoxes y Labels -->
                <div class="col-md-6">
                    <div class="form-group col-md-12">



                        <asp:Label ID="lblNombre" runat="server" AssociatedControlID="TxtNombre" CssClass="form-label fw-bold">Nombre</asp:Label>
                        <asp:TextBox ID="TxtNombre" runat="server" CssClass="form-control"></asp:TextBox>



                        <asp:Label ID="LabelEstado" runat="server" AssociatedControlID="CheckBoxEstado" CssClass="form-label fw-bold">Activo?</asp:Label>
                        <asp:CheckBox ID="CheckBoxEstado" runat="server" CssClass="form-control"></asp:CheckBox>

                        <asp:Label ID="lblConfirmacion" runat="server" CssClass="alert alert-warning mt-3" Visible="false"></asp:Label>


                        <asp:Button ID="btnConfirmarReactivacion" runat="server" Text="ConfirmarReactivación" OnClick="btnConfirmarReactivacion_Click" CssClass="btn btn-primary mt-2" Visible="false" />





                    </div>
                </div>
            </div>
        </div>



    </div>





    <div class="container mt-5">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="LabelMensaje" runat="server" CssClass="form-label fw-bold" Visible="false"></asp:Label>

                <asp:Button ID="BtnAgregar" runat="server" Text="Agregar" CssClass="btn btn-primary" OnClick="BtnAgregar_Click" />
                <asp:Button ID="BtnModificar" runat="server" Text="Modificar" CssClass="btn btn-warning" OnClick="BtnModificar_Click" />
                <asp:Button ID="BtnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" OnClick="BtnEliminar_Click" OnClientClick="return confirm('Atención: Esta acción no se puede deshacer y el perfil se eliminará de forma permanente. ¿Desea continuar?');" />
                <asp:Button ID="BtnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="BtnCancelar_Click" />

            </div>
        </div>
    </div>



    <asp:HiddenField ID="HiddenFieldPerfilId" runat="server" />
    <div class="row mt-5">
        <div class="row mt-12">
            <div class="table-responsive">
                <asp:GridView ID="GridViewPerfiles" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-striped">
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="ID" ReadOnly="True" SortExpression="Id" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />



                        <asp:TemplateField HeaderText=" Estado">
                            <ItemTemplate>
                                <asp:Label ID="lblEstado" runat="server" Text='<%# Eval("Estado", "{0}") == "True"? "Activo" : "Inactivo"  %>'></asp:Label>
                            </ItemTemplate>
                                </asp:TemplateField> 


                    


                        <asp:TemplateField HeaderText="Seleccionar">
                            <ItemTemplate>
                                <asp:Button ID="SeleccionarPerfil" runat="server" Text="Seleccionar" CssClass="btn btn-info" CommandArgument='<%# Eval("Id") %>' OnClick="SeleccionarPerfil_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>






                    </Columns>
                </asp:GridView>

            </div>
        </div>
    </div>





</asp:Content>
