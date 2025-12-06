using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tarea_4._3_Transacción_desde_un_lenguaje_de_programación.Modelos;

namespace Tarea_4._3_Transacción_desde_un_lenguaje_de_programación.DAO
{
    internal class ProductoDAO
    {
        private readonly Conexion conexion;

        public ProductoDAO()
        {
            conexion = new Conexion();
        }

        /// <summary>
        /// Busca un producto por su código de barras
        /// </summary>
        public Producto? BuscarPorCodigoBarras(string codigoBarras)
        {
            Producto? producto = null;

            try
            {
                var conn = conexion.Abrir();
                using var cmd = new MySqlCommand("spBuscarProductoPorCodigo", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("pCodigoBarras", codigoBarras);

                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    producto = new Producto
                    {
                        IdProducto = reader.GetInt32("idProducto"),
                        CodigoBarras = reader.GetString("codigoBarras"),
                        Nombre = reader.GetString("nombre"),
                        Descripcion = reader.IsDBNull(reader.GetOrdinal("descripcion"))
                            ? "" : reader.GetString("descripcion"),
                        Categoria = reader.IsDBNull(reader.GetOrdinal("categoria"))
                            ? "" : reader.GetString("categoria"),
                        Precio = reader.GetDecimal("precio"),
                        Stock = reader.GetInt32("stock"),
                        FechaRegistro = reader.GetDateTime("fechaRegistro"),
                        Descontinuado = reader.GetBoolean("descontinuado")
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al buscar producto: {ex.Message}");
            }
            finally
            {
                conexion.Cerrar();
            }

            return producto;
        }

        /// <summary>
        /// Lista todos los productos
        /// </summary>
        public List<Producto> ListarTodos()
        {
            var productos = new List<Producto>();

            try
            {
                var conn = conexion.Abrir();
                using var cmd = new MySqlCommand("spListarProductos", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    productos.Add(new Producto
                    {
                        IdProducto = reader.GetInt32("idProducto"),
                        CodigoBarras = reader.GetString("codigoBarras"),
                        Nombre = reader.GetString("nombre"),
                        Descripcion = reader.IsDBNull(reader.GetOrdinal("descripcion"))
                            ? "" : reader.GetString("descripcion"),
                        Categoria = reader.IsDBNull(reader.GetOrdinal("categoria"))
                            ? "" : reader.GetString("categoria"),
                        Precio = reader.GetDecimal("precio"),
                        Stock = reader.GetInt32("stock"),
                        FechaRegistro = reader.GetDateTime("fechaRegistro"),
                        Descontinuado = reader.GetBoolean("descontinuado")
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al listar productos: {ex.Message}");
            }
            finally
            {
                conexion.Cerrar();
            }

            return productos;
        }

        /// <summary>
        /// Descontinúa un producto usando transacción
        /// ⭐ ESTA ES LA OPERACIÓN CON TRANSACCIÓN REQUERIDA
        /// </summary>
        public bool DescontinuarProducto(int idProducto)
        {
            try
            {
                var conn = conexion.Abrir();
                using var cmd = new MySqlCommand("spDescontinuarProducto", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("pIdProducto", idProducto);

                // El stored procedure maneja la transacción internamente
                using var reader = cmd.ExecuteReader();

                // Si llega aquí sin excepción, fue exitoso
                return true;
            }
            catch (MySqlException ex)
            {
                throw new Exception($"Error en la transacción: {ex.Message}");
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        /// <summary>
        /// Inserta un nuevo producto
        /// </summary>
        public int InsertarProducto(Producto producto)
        {
            int nuevoId = 0;

            try
            {
                var conn = conexion.Abrir();
                using var cmd = new MySqlCommand("spInsertarProducto", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("pCodigoBarras", producto.CodigoBarras);
                cmd.Parameters.AddWithValue("pNombre", producto.Nombre);
                cmd.Parameters.AddWithValue("pDescripcion", producto.Descripcion);
                cmd.Parameters.AddWithValue("pCategoria", producto.Categoria);
                cmd.Parameters.AddWithValue("pPrecio", producto.Precio);
                cmd.Parameters.AddWithValue("pStock", producto.Stock);

                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    nuevoId = reader.GetInt32("idProducto");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al insertar producto: {ex.Message}");
            }
            finally
            {
                conexion.Cerrar();
            }

            return nuevoId;
        }
    }
}
