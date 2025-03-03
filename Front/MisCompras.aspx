<%@ Page Title="" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="MisCompras.aspx.cs" Inherits="Front.MisCompras" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="row mt-5">


        <div class="col-md-12">
            <h2 class="text-center">Mis COmpras</h2>


            <asp:GridView ID="gvMisCompras" runat="server" AutoGenerateColumns="false" CssClass="table table-striped" OnRowCommand="gvMisCompras_RowCommand">


                <%-- Id	IdVenta	TotalFactura	Fecha--%>
                <Columns>

                    

                    <asp:BoundField  DataField="IdVenta" HeaderText="IdVenta" />
                    <asp:BoundField  DataField="TotalFactura" HeaderText="TotalFactura"  />
                    <asp:BoundField  DataField="Fecha" HeaderText="Fecha" />


    



                    <asp:TemplateField HeaderText="Acciones">

                        <ItemTemplate>
                            <asp:Button ID="btnSeleccionar" runat="server" Text="ver Factura" CommandName="VerFactura" CommandArgument='<%#Eval("IdVenta") +  "|" + Eval("TotalFactura") %>' CssClass="btn btn-primary" />
                        </ItemTemplate>
                    </asp:TemplateField>



                </Columns>



            </asp:GridView>
        </div>
    </div>




</asp:Content>
