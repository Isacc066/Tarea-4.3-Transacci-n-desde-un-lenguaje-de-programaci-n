namespace Tarea_4._3_Transacción_desde_un_lenguaje_de_programación
{
    partial class FrmGestionProductos
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dgvProductos = new DataGridView();
            btnBuscar = new Button();
            txtCodigoBarras = new TextBox();
            label1 = new Label();
            btnCargarTodos = new Button();
            btnDescontinuar = new Button();
            btnLimpiar = new Button();
            lblEstadisticas = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).BeginInit();
            SuspendLayout();
            // 
            // dgvProductos
            // 
            dgvProductos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProductos.Location = new Point(331, 90);
            dgvProductos.Name = "dgvProductos";
            dgvProductos.RowHeadersWidth = 51;
            dgvProductos.Size = new Size(907, 400);
            dgvProductos.TabIndex = 0;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(116, 158);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(111, 29);
            btnBuscar.TabIndex = 1;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // txtCodigoBarras
            // 
            txtCodigoBarras.Location = new Point(116, 67);
            txtCodigoBarras.Name = "txtCodigoBarras";
            txtCodigoBarras.Size = new Size(125, 27);
            txtCodigoBarras.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.Info;
            label1.Location = new Point(116, 21);
            label1.Name = "label1";
            label1.Size = new Size(124, 20);
            label1.TabIndex = 3;
            label1.Text = "Código de Barras";
            // 
            // btnCargarTodos
            // 
            btnCargarTodos.Location = new Point(116, 204);
            btnCargarTodos.Name = "btnCargarTodos";
            btnCargarTodos.Size = new Size(111, 29);
            btnCargarTodos.TabIndex = 4;
            btnCargarTodos.Text = "Cargar Todos";
            btnCargarTodos.UseVisualStyleBackColor = true;
            btnCargarTodos.Click += btnCargarTodos_Click;
            // 
            // btnDescontinuar
            // 
            btnDescontinuar.Location = new Point(116, 250);
            btnDescontinuar.Name = "btnDescontinuar";
            btnDescontinuar.Size = new Size(111, 29);
            btnDescontinuar.TabIndex = 5;
            btnDescontinuar.Text = "Descontinuar";
            btnDescontinuar.UseVisualStyleBackColor = true;
            btnDescontinuar.Click += btnDescontinuar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(116, 306);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(111, 29);
            btnLimpiar.TabIndex = 6;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // lblEstadisticas
            // 
            lblEstadisticas.AutoSize = true;
            lblEstadisticas.BackColor = SystemColors.Info;
            lblEstadisticas.Location = new Point(724, 21);
            lblEstadisticas.Name = "lblEstadisticas";
            lblEstadisticas.Size = new Size(85, 20);
            lblEstadisticas.TabIndex = 7;
            lblEstadisticas.Text = "Estadísticas";
            // 
            // FrmGestionProductos
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientInactiveCaption;
            ClientSize = new Size(1345, 584);
            Controls.Add(lblEstadisticas);
            Controls.Add(btnLimpiar);
            Controls.Add(btnDescontinuar);
            Controls.Add(btnCargarTodos);
            Controls.Add(label1);
            Controls.Add(txtCodigoBarras);
            Controls.Add(btnBuscar);
            Controls.Add(dgvProductos);
            Name = "FrmGestionProductos";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Tabla de Productos";
            ((System.ComponentModel.ISupportInitialize)dgvProductos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvProductos;
        private Button btnBuscar;
        private TextBox txtCodigoBarras;
        private Label label1;
        private Button btnCargarTodos;
        private Button btnDescontinuar;
        private Button btnLimpiar;
        private Label lblEstadisticas;
    }
}
