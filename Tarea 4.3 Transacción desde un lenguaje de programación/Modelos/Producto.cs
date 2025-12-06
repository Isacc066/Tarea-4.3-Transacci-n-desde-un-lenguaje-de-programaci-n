using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarea_4._3_Transacción_desde_un_lenguaje_de_programación.Modelos
{
    internal class Producto
    {
        public int IdProducto { get; set; }
        public string CodigoBarras { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Descontinuado { get; set; }

        // Propiedad calculada para mostrar el estado en el grid
        public string Estado => Descontinuado ? "DESCONTINUADO" : "ACTIVO";

        // Propiedad calculada para formato de precio
        public string PrecioFormateado => Precio.ToString("C2");
    }
}
