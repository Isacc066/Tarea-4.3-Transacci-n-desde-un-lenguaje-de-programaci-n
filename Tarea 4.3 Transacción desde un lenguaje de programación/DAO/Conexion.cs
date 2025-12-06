using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Tarea_4._3_Transacción_desde_un_lenguaje_de_programación.DAO
{
    internal class Conexion
    {
        /// <summary>
        /// Aqui se hace la conexión con la base de datos
        /// </summary>
        private readonly string cadena = "Server=localhost;Database=VENTAS;Uid=root;Pwd=Tacodeguayaba16;";

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(cadena);
        }

        private MySqlConnection conn;

        public Conexion()
        {
            conn = new MySqlConnection("Server=localhost;Database=GestionProductos;Uid=root;Pwd=Tacodeguayaba16;");
        }

        public MySqlConnection Abrir()
        {
            if (conn.State == System.Data.ConnectionState.Closed)
                conn.Open();
            return conn;
        }

        public void Cerrar()
        {
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();
        }
    }
}
