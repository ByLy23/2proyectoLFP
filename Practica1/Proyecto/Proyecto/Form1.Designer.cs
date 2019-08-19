namespace Proyecto
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.lienzo = new System.Windows.Forms.RichTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.wdjfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarComoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.analizarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.acercaDeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cajitaDibujos = new System.Windows.Forms.PictureBox();
            this.descripcionxd = new System.Windows.Forms.Label();
            this.arbolito = new System.Windows.Forms.TreeView();
            this.timepo = new System.Windows.Forms.DateTimePicker();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cajitaDibujos)).BeginInit();
            this.SuspendLayout();
            // 
            // lienzo
            // 
            this.lienzo.AcceptsTab = true;
            this.lienzo.Location = new System.Drawing.Point(12, 27);
            this.lienzo.Name = "lienzo";
            this.lienzo.Size = new System.Drawing.Size(372, 411);
            this.lienzo.TabIndex = 4;
            this.lienzo.Text = "";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wdjfoToolStripMenuItem,
            this.guardarToolStripMenuItem,
            this.guardarComoToolStripMenuItem,
            this.analizarToolStripMenuItem,
            this.acercaDeToolStripMenuItem,
            this.salirToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1128, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // wdjfoToolStripMenuItem
            // 
            this.wdjfoToolStripMenuItem.Name = "wdjfoToolStripMenuItem";
            this.wdjfoToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.wdjfoToolStripMenuItem.Text = "Abrir";
            this.wdjfoToolStripMenuItem.Click += new System.EventHandler(this.WdjfoToolStripMenuItem_Click);
            // 
            // guardarToolStripMenuItem
            // 
            this.guardarToolStripMenuItem.Name = "guardarToolStripMenuItem";
            this.guardarToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.guardarToolStripMenuItem.Text = "Guardar";
            this.guardarToolStripMenuItem.Click += new System.EventHandler(this.GuardarToolStripMenuItem_Click);
            // 
            // guardarComoToolStripMenuItem
            // 
            this.guardarComoToolStripMenuItem.Name = "guardarComoToolStripMenuItem";
            this.guardarComoToolStripMenuItem.Size = new System.Drawing.Size(147, 20);
            this.guardarComoToolStripMenuItem.Text = "Abrir Manual de Usuario";
            this.guardarComoToolStripMenuItem.Click += new System.EventHandler(this.GuardarComoToolStripMenuItem_Click);
            // 
            // analizarToolStripMenuItem
            // 
            this.analizarToolStripMenuItem.Name = "analizarToolStripMenuItem";
            this.analizarToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.analizarToolStripMenuItem.Text = "Analizar";
            this.analizarToolStripMenuItem.Click += new System.EventHandler(this.AnalizarToolStripMenuItem_Click);
            // 
            // acercaDeToolStripMenuItem
            // 
            this.acercaDeToolStripMenuItem.Name = "acercaDeToolStripMenuItem";
            this.acercaDeToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.acercaDeToolStripMenuItem.Text = "Acerca de";
            this.acercaDeToolStripMenuItem.Click += new System.EventHandler(this.AcercaDeToolStripMenuItem_Click);
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.salirToolStripMenuItem.Text = "Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.SalirToolStripMenuItem_Click);
            // 
            // cajitaDibujos
            // 
            this.cajitaDibujos.Location = new System.Drawing.Point(801, 198);
            this.cajitaDibujos.Name = "cajitaDibujos";
            this.cajitaDibujos.Size = new System.Drawing.Size(315, 240);
            this.cajitaDibujos.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.cajitaDibujos.TabIndex = 1;
            this.cajitaDibujos.TabStop = false;
            // 
            // descripcionxd
            // 
            this.descripcionxd.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.descripcionxd.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.descripcionxd.Location = new System.Drawing.Point(801, 27);
            this.descripcionxd.Name = "descripcionxd";
            this.descripcionxd.Size = new System.Drawing.Size(315, 158);
            this.descripcionxd.TabIndex = 0;
            // 
            // arbolito
            // 
            this.arbolito.Location = new System.Drawing.Point(390, 53);
            this.arbolito.Name = "arbolito";
            this.arbolito.Size = new System.Drawing.Size(405, 385);
            this.arbolito.TabIndex = 7;
            this.arbolito.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView1_AfterSelect);
            this.arbolito.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Arbolito_MouseDoubleClick);
            // 
            // timepo
            // 
            this.timepo.CustomFormat = "dd/mm/yyyy";
            this.timepo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.timepo.Location = new System.Drawing.Point(513, 27);
            this.timepo.Name = "timepo";
            this.timepo.Size = new System.Drawing.Size(155, 20);
            this.timepo.TabIndex = 6;
            this.timepo.ValueChanged += new System.EventHandler(this.Timepo_ValueChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1128, 450);
            this.Controls.Add(this.timepo);
            this.Controls.Add(this.cajitaDibujos);
            this.Controls.Add(this.arbolito);
            this.Controls.Add(this.descripcionxd);
            this.Controls.Add(this.lienzo);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cajitaDibujos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox lienzo;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem wdjfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guardarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guardarComoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem analizarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem acercaDeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.TreeView arbolito;
        private System.Windows.Forms.DateTimePicker timepo;
        private System.Windows.Forms.PictureBox cajitaDibujos;
        private System.Windows.Forms.Label descripcionxd;
    }
}

