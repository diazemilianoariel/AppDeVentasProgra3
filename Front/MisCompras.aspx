<%@ Page Title="" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="MisCompras.aspx.cs" Inherits="Front.MisCompras" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1>aca se va a ver las compras  con facturas ,que hizo el cliente</h1>

    <div class="row mt-5">
        <div class="col-md-12">
            <h2 class="text-center"> Mis COmpras</h2>
            <asp:GridView ID="gvMisCompras" runat="server" AutoGenerateColumns="True" CssClass="table table-striped">
               
            </asp:GridView>
        </div>
    </div>




</asp:Content>
