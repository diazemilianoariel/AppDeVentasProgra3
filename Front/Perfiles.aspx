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



                        <asp:Label ID="LabelNombre" runat="server" AssociatedControlID="TextBoxNombre" CssClass="form-label fw-bold">Nombre</asp:Label>
                        <asp:TextBox ID="TextBoxNombre" runat="server" CssClass="form-control"></asp:TextBox>



                        <asp:Label ID="LabelEstado" runat="server" AssociatedControlID="CheckBoxEstado" CssClass="form-label fw-bold">Activo?</asp:Label>
                        <asp:CheckBox ID="CheckBoxEstado" runat="server" CssClass="form-control"></asp:CheckBox>


                    </div>
                </div>
            </div>
        </div>



    </div>





    <div class="container mt-5">
        <div class="row">
            <div class="col-md-12">
                <asp:Button ID="BtnAgregar" runat="server" Text="Agregar" CssClass="btn btn-primary" OnClick="BtnAgregar_Click" />
                <asp:Button ID="BtnModificar" runat="server" Text="Modificar" CssClass="btn btn-warning" OnClick="BtnModificar_Click" />
                <asp:Button ID="BtnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" OnClick="BtnEliminar_Click" />
                <asp:Button ID="BtnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="BtnCancelar_Click" />
            </div>
        </div>
    </div>



    <div class="row mt-5">
        
        <div class=" row mt-12">
            <div class="table-responsive">

                <asp:GridView runat="server">

                </asp:GridView>
             


            </div>

        </div>


    </div>

</asp:Content>
