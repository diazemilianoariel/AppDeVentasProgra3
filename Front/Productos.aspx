<%@ Page Title="Gestión de Productos" Language="C#" MasterPageFile="~/MASTER.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="Front.Productos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
           
           
            background-size:cover; background-repeat: no-repeat; background-attachment: fixed;
        }
        .pagination-ys { padding-left: 0; margin: 20px 0; border-radius: 4px; }
        .pagination-ys table > tbody > tr > td { display: inline; padding: 8px 12px; text-decoration: none; color: #007bff; background-color: #fff; border: 1px solid #ddd; }
        .pagination-ys table > tbody > tr > td > span { z-index: 3; color: #fff; background-color: #007bff; border-color: #007bff; }
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div class="container-fluid mt-5">

    
       <div class="text-center bg-white p-4 rounded mb-4 shadow-sm">
           <h1 class="mb-3">Gestión de Productos</h1>
           <p class="text-muted">Administre y organice la información de sus Productos de manera sencilla y rápida.</p>
       </div>

    
       <div class="row mt-4 justify-content-center">
           <% if (IsAdmin) { %>
               <asp:HyperLink NavigateUrl="~/ProductosABM/ProductoAgregar.aspx" runat="server" CssClass="btn btn-primary shadow-sm">Nuevo Producto</asp:HyperLink>
           <% } %>
       </div>

     
       <div class="row mt-4 justify-content-center">
           <div class="col-md-8 shadow-sm rounded p-2">
               <div class="input-group">
                   <asp:TextBox ID="txtFiltro" runat="server" CssClass="form-control" placeholder="Buscar por nombre o descripción..." AutoPostBack="true" OnTextChanged="txtFiltro_TextChanged"></asp:TextBox>
               </div>
           </div>
       </div>

       <asp:Label ID="lblError" runat="server" CssClass="text-danger text-center d-block mt-3" Visible="false"></asp:Label>

      
       <div class="card shadow-sm mt-4">
           <div class="card-body p-0">
               <div class="table-responsive">
                   <asp:GridView ID="GridViewProductos" runat="server"
                       CssClass="table table-hover align-middle mb-0"
                       AutoGenerateColumns="False"
                       OnRowCommand="GridViewProductos_RowCommand"
                       AllowPaging="True" PageSize="10" OnPageIndexChanging="GridViewProductos_PageIndexChanging"
                       GridLines="None">

                       <Columns>
                           <asp:TemplateField HeaderText="Imagen">
                               <ItemTemplate>
                                   <asp:Image runat="server" ImageUrl='<%# Eval("Imagen") %>' Width="60px" Height="60px" CssClass="rounded" Style="object-fit: cover;" />
                               </ItemTemplate>
                               <ItemStyle Width="80px" HorizontalAlign="Center" />
                           </asp:TemplateField>
                           <asp:BoundField DataField="id" HeaderText="ID" />
                           <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                           <asp:TemplateField HeaderText="Descripción">
                               <ItemTemplate>
                                   <%# AcortarTexto(Eval("descripcion").ToString(), 40) %>
                               </ItemTemplate>
                           </asp:TemplateField>
                           <asp:BoundField DataField="precioVenta" HeaderText="Precio" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" />
                           <asp:TemplateField HeaderText="Stock" ItemStyle-HorizontalAlign="Center">
                               <ItemTemplate>
                                   <span class='badge p-2 <%# (int)Eval("stock") <= 10 ? "badge-danger" : "badge-secondary" %>' style="font-size: 0.9rem;">
                                       <%# Eval("stock") %>
                                   </span>
                               </ItemTemplate>
                           </asp:TemplateField>
                           <asp:BoundField DataField="Marca.nombre" HeaderText="Marca" />
                           <asp:BoundField DataField="Categoria.nombre" HeaderText="Categoría" />
                           <asp:TemplateField HeaderText="Acciones" ItemStyle-CssClass="text-right">
                               <ItemTemplate>
                                   <div class="btn-group" role="group">
                                       <asp:Button ID="btnModificar" runat="server" Text="Modificar" CommandName="Modificar" CommandArgument='<%# Eval("id") %>' CssClass="btn btn-outline-success btn-sm shadow-sm" />
                                       <asp:Button ID="btnDetalle" runat="server" Text="Detalle" CommandName="Detalle" CommandArgument='<%# Eval("id") %>' CssClass="btn btn-outline-info btn-sm shadow-sm" />
                                       <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CommandName="Eliminar" CommandArgument='<%# Eval("id") %>' CssClass="btn btn-outline-danger btn-sm shadow-sm" Visible="<%# IsAdmin %>" />
                                   </div>
                               </ItemTemplate>
                           </asp:TemplateField>
                       </Columns>
                        <PagerStyle CssClass="pagination-ys" />
                   </asp:GridView>
               </div>
           </div>
       </div>

   </div>
</asp:Content>