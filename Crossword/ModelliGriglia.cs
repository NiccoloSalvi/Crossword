using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using System.IO;
using System;

namespace Crossword
{
    public partial class ModelliGriglia : Form
    {
        private string path;
        private int dim;
        private Info i;
        
        public ModelliGriglia(Info inf)
        {
            InitializeComponent();

            i = inf;
            dim = inf.GetDim();
            path = "pic\\" + dim.ToString() + "x" + dim.ToString() + "\\"; // pathname della cartella dove sono presenti le immagini delle grigle
        }

        private void ModelliGriglia_Load(object sender, EventArgs e)
        {
            string[] array1 = Directory.GetFiles(path);
            List<string> filesList = array1.ToList(); // leggere quanti files vi sono nella cartella

            int lastLetter = filesList[0].Split('.')[0].Count();
            foreach (string s in filesList)
                listBox1.Items.Add("Modello " + s.Split('.')[0][lastLetter - 1]); // per ogni file, aggiungerne il modello nella listbox
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            if (listBox1.SelectedItem != null) // se è stato selezionato un item valido, carico l'immagino dalla cartella del modello selezionato nella picturebox
                pictureBox1.Image = Image.FromFile("pic\\" + dim.ToString() + "x" + dim.ToString() + "\\" + listBox1.SelectedItem.ToString().Split(' ')[1] + ".png");
        }

        private void ModelliGriglia_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void UsaModello_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                Hide();
                i.SetFileName(listBox1.SelectedItem.ToString().Split(' ')[1][0]);
                Crossword frm = new Crossword(i);
                frm.ShowDialog();
            }
        }

        private void Back_Click(object sender, EventArgs e)
        {
            Hide();
            SceltaGriglia frm = new SceltaGriglia();
            frm.ShowDialog();
        }
    }
}
