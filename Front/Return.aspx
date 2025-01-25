<%@ Page Title="" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="Return.aspx.cs" Inherits="Front.Return" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="row">
            <div class="col-md-12 text-center">
                <asp:Label ID="lblMensaje" runat="server" CssClass="alert alert-danger" Visible="false"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
