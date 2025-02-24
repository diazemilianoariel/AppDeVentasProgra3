using System;
using dominio;


namespace negocio
{
    public class CompraNegocio
    {



        public void InsertarCompra(Compra compra)
        {



            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("insert into Compras (idProveedor, fecha, Total) values (@IdProveedor, @Fecha, @Total)");
                datos.SetearParametro("@IdProveedor", compra.IdProveedor);
                datos.SetearParametro("@Fecha", compra.Fecha.ToString("yyyy-MM-dd"));
                datos.SetearParametro("@Total", compra.Total);
                datos.EjecutarLectura();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al realizar la compra: " + ex.Message);
            }
            finally
            {

                datos.CerrarConexion();
            }
            // que se haga otro try catch


            AccesoDatos datos1 = new AccesoDatos();


            // obtener el ultimo id de compra
            int UltimoIdCompra = 0;


            try
            {
                // obtener el ultimo Id de compra de la tabla compra
                datos1.SetearConsulta("select top 1 id from Compras order by id desc");
                datos1.EjecutarLectura();


                if (datos1.Lector.Read())
                {
                    UltimoIdCompra = (int)datos1.Lector["id"];
                }

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos1.CerrarConexion();
            }





            AccesoDatos datos3 = new AccesoDatos();
            try
            {
                /// insertar en la tabla detalleCompra
                datos3.SetearConsulta("insert into DetalleCompras (idCompra, idProducto, cantidad, precioCompra) values (@IdCompra, @IdProducto, @Cantidad, @PrecioCompra)");
                datos3.SetearParametro("@IdCompra", UltimoIdCompra);
                datos3.SetearParametro("@IdProducto", compra.IdProducto);
                datos3.SetearParametro("@Cantidad", compra.Cantidad);
                datos3.SetearParametro("@PrecioCompra", compra.PrecioCompra);
                datos3.EjecutarLectura();


            }
            catch (Exception ex)
            {

                throw new Exception("Error al insertar el DetalleCompra: " + ex.Message);
            }
            finally
            {
                datos3.CerrarConexion();
            }


            //actualizar el Stock el al tabla stock
            AccesoDatos datos4 = new AccesoDatos();
            try
            {
                datos4.SetearConsulta("update Stock set cantidad = cantidad + @Cantidad where idProducto = @IdProducto");
                datos4.SetearParametro("@Cantidad", compra.Cantidad);
                datos4.SetearParametro("@IdProducto", compra.IdProducto);
                datos4.EjecutarLectura();




            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos4.CerrarConexion();
            }




        }








    }
}