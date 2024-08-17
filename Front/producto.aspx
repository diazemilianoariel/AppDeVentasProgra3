<%@ Page Title="" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="producto.aspx.cs" Inherits="Front.producto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="Label1" runat="server" Text="producto"></asp:Label>
                <asp:TextBox ID="TextBox1" runat="server">ingrese producto</asp:TextBox>
                <asp:Button ID="Button1" runat="server" Text="agregar" OnClick="btnAgregar" />
            </div>
        </div>
    </div>
</asp:Content>
