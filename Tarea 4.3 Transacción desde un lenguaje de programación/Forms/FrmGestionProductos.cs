using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Tarea_4._3_Transacción_desde_un_lenguaje_de_programación.DAO;
using Tarea_4._3_Transacción_desde_un_lenguaje_de_programación.Modelos;

namespace Tarea_4._3_Transacción_desde_un_lenguaje_de_programación
{
    public partial class FrmGestionProductos : Form
    {

        private readonly ProductoDAO productoDAO;
        private readonly BindingList<Producto> listaProductos;
        private int indiceSeleccionado = -1;
        public FrmGestionProductos()
        {
            InitializeComponent();

            productoDAO = new ProductoDAO();
            listaProductos = new BindingList<Producto>();

            // Eventos
            txtCodigoBarras.KeyDown += txtCodigoBarras_KeyDown;
            dgvProductos.CellClick += dgvProductos_CellClick;
            btnBuscar.Click += btnBuscar_Click;
            btnDescontinuar.Click += btnDescontinuar_Click;
            btnCargarTodos.Click += btnCargarTodos_Click;
            btnLimpiar.Click += btnLimpiar_Click;

            // Configuración inicial
            ConfigurarGrid();
            txtCodigoBarras.Focus();
        }

        // ========================================
        // CONFIGURACIÓN DEL DATAGRIDVIEW
        // ========================================
        private void ConfigurarGrid()
        {
            dgvProductos.ReadOnly = true;
            dgvProductos.AllowUserToAddRows = false;
            dgvProductos.AllowUserToDeleteRows = false;
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.MultiSelect = false;
            dgvProductos.AutoGenerateColumns = false;
            dgvProductos.Columns.Clear();

            // Columna: ID Producto
            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdProducto",
                DataPropertyName = "IdProducto",
                HeaderText = "ID",
                Width = 50
            });

            // Columna: Código de Barras
            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CodigoBarras",
                DataPropertyName = "CodigoBarras",
                HeaderText = "Código de Barras",
                Width = 120
            });

            // Columna: Nombre
            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nombre",
                DataPropertyName = "Nombre",
                HeaderText = "Nombre",
                Width = 200
            });

            // Columna: Categoría
            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Categoria",
                DataPropertyName = "Categoria",
                HeaderText = "Categoría",
                Width = 100
            });

            // Columna: Precio
            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Precio",
                DataPropertyName = "Precio",
                HeaderText = "Precio",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
            });

            // Columna: Stock
            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Stock",
                DataPropertyName = "Stock",
                HeaderText = "Stock",
                Width = 70
            });

            // Columna: Fecha Registro
            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaRegistro",
                DataPropertyName = "FechaRegistro",
                HeaderText = "Fecha Registro",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });

            // Columna: Estado (Activo/Descontinuado)
            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Estado",
                DataPropertyName = "Estado",
                HeaderText = "Estado",
                Width = 120
            });

            // Asignar DataSource
            dgvProductos.DataSource = listaProductos;

            // Color para productos descontinuados
            dgvProductos.CellFormatting += DgvProductos_CellFormatting;
        }

        // ========================================
        // FORMATO DE CELDAS (color rojo si está descontinuado)
        // ========================================
        private void DgvProductos_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= listaProductos.Count)
                return;

            var producto = listaProductos[e.RowIndex];
            if (producto.Descontinuado)
            {
                e.CellStyle.BackColor = System.Drawing.Color.LightCoral;
                e.CellStyle.ForeColor = System.Drawing.Color.DarkRed;
            }
        }

        // ========================================
        // BUSCAR PRODUCTO POR CÓDIGO DE BARRAS
        // ========================================
        private void btnBuscar_Click(object? sender, EventArgs e)
        {
            BuscarProducto();
        }

        private void BuscarProducto()
        {
            string codigo = txtCodigoBarras.Text.Trim();

            if (string.IsNullOrEmpty(codigo))
            {
                MessageBox.Show("Ingrese un código de barras", "Campo vacío",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCodigoBarras.Focus();
                return;
            }

            try
            {
                var producto = productoDAO.BuscarPorCodigoBarras(codigo);

                if (producto == null)
                {
                    MessageBox.Show($"No se encontró ningún producto con el código:\n{codigo}",
                        "Producto no encontrado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                // Verificar si ya está en la lista
                var existe = listaProductos.FirstOrDefault(p => p.IdProducto == producto.IdProducto);
                if (existe == null)
                {
                    listaProductos.Add(producto);
                }
                else
                {
                    MessageBox.Show("Este producto ya está en la lista",
                        "Producto duplicado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }

                // Limpiar y enfocar
                txtCodigoBarras.Clear();
                txtCodigoBarras.Focus();

                // Seleccionar el producto agregado
                if (dgvProductos.Rows.Count > 0)
                {
                    dgvProductos.ClearSelection();
                    int ultimaFila = dgvProductos.Rows.Count - 1;
                    dgvProductos.Rows[ultimaFila].Selected = true;
                    indiceSeleccionado = ultimaFila;
                }

                ActualizarEstadisticas();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar producto:\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ========================================
        // ENTER EN TEXTBOX = BUSCAR
        // ========================================
        private void txtCodigoBarras_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                BuscarProducto();
            }
        }

        // ========================================
        // CARGAR TODOS LOS PRODUCTOS
        // ========================================
        private void btnCargarTodos_Click(object? sender, EventArgs e)
        {
            try
            {
                var productos = productoDAO.ListarTodos();

                listaProductos.Clear();
                foreach (var p in productos)
                {
                    listaProductos.Add(p);
                }

                MessageBox.Show($"Se cargaron {productos.Count} productos",
                    "Carga exitosa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                ActualizarEstadisticas();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar productos:\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ========================================
        // DESCONTINUAR PRODUCTO (CON TRANSACCIÓN)
        // ========================================
        private void btnDescontinuar_Click(object? sender, EventArgs e)
        {
            if (listaProductos.Count == 0)
            {
                MessageBox.Show("No hay productos en la lista",
                    "Lista vacía",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (indiceSeleccionado < 0 || indiceSeleccionado >= listaProductos.Count)
            {
                MessageBox.Show("Seleccione un producto de la lista",
                    "Sin selección",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var productoSeleccionado = listaProductos[indiceSeleccionado];

            if (productoSeleccionado.Descontinuado)
            {
                MessageBox.Show("Este producto ya está descontinuado",
                    "Producto descontinuado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            var confirmacion = MessageBox.Show(
                $"¿Está seguro de descontinuar el producto?\n\n" +
                $"Código: {productoSeleccionado.CodigoBarras}\n" +
                $"Nombre: {productoSeleccionado.Nombre}\n\n" +
                $"Esta acción marcará el producto como DESCONTINUADO.",
                "Confirmar descontinuación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacion != DialogResult.Yes)
                return;

            try
            {
                // ⭐ EJECUTAR TRANSACCIÓN
                bool exito = productoDAO.DescontinuarProducto(productoSeleccionado.IdProducto);

                if (exito)
                {
                    // Actualizar el objeto en la lista
                    productoSeleccionado.Descontinuado = true;

                    // Forzar actualización del grid
                    dgvProductos.Refresh();

                    MessageBox.Show(
                        $"✓ Producto descontinuado exitosamente\n\n" +
                        $"El producto '{productoSeleccionado.Nombre}' ha sido marcado como DESCONTINUADO.",
                        "Operación exitosa",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    ActualizarEstadisticas();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al descontinuar producto:\n{ex.Message}",
                    "Error en la transacción",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ========================================
        // LIMPIAR LISTA
        // ========================================
        private void btnLimpiar_Click(object? sender, EventArgs e)
        {
            if (listaProductos.Count == 0)
                return;

            var confirmacion = MessageBox.Show(
                "¿Limpiar toda la lista de productos?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                listaProductos.Clear();
                indiceSeleccionado = -1;
                ActualizarEstadisticas();
            }
        }

        // ========================================
        // SELECCIÓN DE FILA EN EL GRID
        // ========================================
        private void dgvProductos_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= listaProductos.Count)
            {
                indiceSeleccionado = -1;
                return;
            }

            indiceSeleccionado = e.RowIndex;
            dgvProductos.Rows[e.RowIndex].Selected = true;
        }

        // ========================================
        // ACTUALIZAR ESTADÍSTICAS (OPCIONAL)
        // ========================================
        private void ActualizarEstadisticas()
        {
            int total = listaProductos.Count;
            int activos = listaProductos.Count(p => !p.Descontinuado);
            int descontinuados = listaProductos.Count(p => p.Descontinuado);

            lblEstadisticas.Text = $"Total: {total} | Activos: {activos} | Descontinuados: {descontinuados}";
        }
    }
}
