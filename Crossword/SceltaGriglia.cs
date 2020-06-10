using System.Windows.Forms;
using System;

namespace Crossword
{
    public partial class SceltaGriglia : Form
    {
        public SceltaGriglia()
        {
            InitializeComponent();
        }

        private void _5_Click(object sender, EventArgs e)
        {
            Hide();
            ModelliGriglia frm = new ModelliGriglia(new Info(5, (int)Larg.Value, (int)Alt.Value));
            frm.ShowDialog();
        }

        private void _6_Click(object sender, EventArgs e)
        {
            Hide();
            ModelliGriglia frm = new ModelliGriglia(new Info(6, (int)Larg.Value, (int)Alt.Value));
            frm.ShowDialog();
        }

        private void _7_Click(object sender, EventArgs e)
        {
            Hide();
            ModelliGriglia frm = new ModelliGriglia(new Info(7, (int)Larg.Value, (int)Alt.Value));
            frm.ShowDialog();
        }

        private void _8_Click(object sender, EventArgs e)
        {
            Hide();
            ModelliGriglia frm = new ModelliGriglia(new Info(8, (int)Larg.Value, (int)Alt.Value));
            frm.ShowDialog();
        }

        private void _9_Click(object sender, EventArgs e)
        {
            Hide();
            ModelliGriglia frm = new ModelliGriglia(new Info(9, (int)Larg.Value, (int)Alt.Value));
            frm.ShowDialog();
        }

        private void _10_Click(object sender, EventArgs e)
        {
            Hide();
            ModelliGriglia frm = new ModelliGriglia(new Info(10, (int)Larg.Value, (int)Alt.Value));
            frm.ShowDialog();
        }

        private void _11_Click(object sender, EventArgs e) 
        {
            Hide();
            ModelliGriglia frm = new ModelliGriglia(new Info(11, (int)Larg.Value, (int)Alt.Value));
            frm.ShowDialog();
        }

        private void _21_Click(object sender, EventArgs e)
        {
            Hide();
            ModelliGriglia frm = new ModelliGriglia(new Info(21, (int)Larg.Value, (int)Alt.Value));
            frm.ShowDialog();
        }        
    }
}
