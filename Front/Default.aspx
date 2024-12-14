<%@ Page Title="" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Front.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row mt-4" id="productCards">





            <asp:Repeater runat="server" ID="rptProductos">

                <ItemTemplate>
                    <div class="col-md-4">
                        <div class="card mb-4 shadow-sm">
                            <img src="<%#Eval("imagen") %>" class="card-img-top img-fluid"  style="height: 200px; object-fit: cover;">

                            <div class="card-body">
                                <h5 class="card-title"><%#Eval("nombre") %></h5>

                                <p class="card-text"><%#Eval("descripcion") %></p>

                                <div class="d-flex justify-content-between align-items-center">
                                    <div class="btn-group">


                                        <a href="CompraParcial.aspx" class="btn btn-sm btn-outline-primary">Comprar</a>
                                        


                                        <asp:LinkButton ID="btnVerDetalle" runat="server" CssClass="btn btn-retro w-100" CommandArgument='<%# Eval("id") %>' OnClick="btnVerDetalle_Click">Comprar</asp:LinkButton>



                                    </div>
                                    <small class="text-muted">$ <%#Eval("precio") %></small>
                                </div>
                            </div>
                        </div>
                    </div>


                </ItemTemplate>


            </asp:Repeater>


        </div>
    </div>
</asp:Content>
