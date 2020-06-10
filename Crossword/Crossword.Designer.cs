namespace Crossword
{
    partial class Crossword
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Crossword));
            this.Visualisation = new System.Windows.Forms.Timer(this.components);
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.Esc = new System.Windows.Forms.PictureBox();
            this.Nuovo = new System.Windows.Forms.PictureBox();
            this.Annulla = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Esc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Nuovo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Annulla)).BeginInit();
            this.SuspendLayout();
            // 
            // Visualisation
            // 
            this.Visualisation.Enabled = true;
            this.Visualisation.Interval = 50;
            this.Visualisation.Tick += new System.EventHandler(this.Visualisation_Tick);
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 22;
            this.listBox1.Location = new System.Drawing.Point(12, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(153, 378);
            this.listBox1.TabIndex = 7;
            // 
            // Esc
            // 
            this.Esc.Image = ((System.Drawing.Image)(resources.GetObject("Esc.Image")));
            this.Esc.Location = new System.Drawing.Point(185, 102);
            this.Esc.Name = "Esc";
            this.Esc.Size = new System.Drawing.Size(39, 40);
            this.Esc.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Esc.TabIndex = 8;
            this.Esc.TabStop = false;
            this.Esc.Click += new System.EventHandler(this.Esc_Click);
            // 
            // Nuovo
            // 
            this.Nuovo.Image = ((System.Drawing.Image)(resources.GetObject("Nuovo.Image")));
            this.Nuovo.Location = new System.Drawing.Point(185, 12);
            this.Nuovo.Name = "Nuovo";
            this.Nuovo.Size = new System.Drawing.Size(39, 40);
            this.Nuovo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Nuovo.TabIndex = 9;
            this.Nuovo.TabStop = false;
            this.Nuovo.Click += new System.EventHandler(this.Nuovo_Click);
            // 
            // Annulla
            // 
            this.Annulla.Image = ((System.Drawing.Image)(resources.GetObject("Annulla.Image")));
            this.Annulla.Location = new System.Drawing.Point(185, 56);
            this.Annulla.Name = "Annulla";
            this.Annulla.Size = new System.Drawing.Size(39, 40);
            this.Annulla.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Annulla.TabIndex = 10;
            this.Annulla.TabStop = false;
            this.Annulla.Click += new System.EventHandler(this.Annulla_Click);
            // 
            // Crossword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(262, 410);
            this.Controls.Add(this.Annulla);
            this.Controls.Add(this.Nuovo);
            this.Controls.Add(this.Esc);
            this.Controls.Add(this.listBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Crossword";
            this.Text = "Cruciverba";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Crossword_FormClosed);
            this.Load += new System.EventHandler(this.Crossword_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Esc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Nuovo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Annulla)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer Visualisation;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.PictureBox Esc;
        private System.Windows.Forms.PictureBox Nuovo;
        private System.Windows.Forms.PictureBox Annulla;
    }
}

