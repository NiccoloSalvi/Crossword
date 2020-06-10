using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using System.Timers;
using System;

namespace Crossword
{
    public partial class Crossword : Form
    {
        private List<HorizontalWord> HorizWords;
        private System.Timers.Timer aTimer;
        private List<string> wordsUsed;
        private List<string> wordsTried;
        private List<string> VerticalWords;
        private TextBox[,] t;
        private int num;
        private Matrix m;

        private readonly int widthTextbox;
        private readonly int heightTextbox;
        private bool backTracking;
        private int lunBefore;
        private int lunAfter;
        private int i;
        
        private Thread thr;

        public Crossword(Info inf)
        {
            InitializeComponent();
            num = inf.GetDim();

            // dimensioni delle textbox in base ai valori selezionati nella form "SceltaGriglia"
            widthTextbox = inf.GetWidth();
            heightTextbox = inf.GetHeight();
            
            HorizWords = new List<HorizontalWord>(); // parole orizzontali
            wordsUsed = new List<string>(); // parole utilizzate
            wordsTried = new List<string>(); // parole testate e non valide
            VerticalWords = new List<string>(); // parole verticali
            t = new TextBox[num, num]; // matrice dinamica di textbox
            m = new Matrix(num, inf.GetFileName());
            
            // timer che non necessita di un thread per essere eseguito
            aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 2000;
            aTimer.Enabled = true;

            // dimensione della form in base al numero di textbox
            Size = new System.Drawing.Size(widthTextbox + num * widthTextbox + 5 * num + 320, (heightTextbox + heightTextbox / 5) * num + 10 + 50);

            // thread che si occuperà di riempire la matrice
            thr = new Thread(CreateCrossword);
        }

        private void Crossword_Load(object sender, EventArgs e)
        {
            AddTextboxes(num, num); // aggiunte tutte le textbox alla form e visualizzate
            VisualizeTextboxes();

            int pos = widthTextbox + num * widthTextbox + 5 * num;
            // posizione dinamica in base alla posizione dell'ultima textbox in colonna
            Nuovo.Location = new System.Drawing.Point(pos, 10);
            Annulla.Location = new System.Drawing.Point(pos, 65);
            Esc.Location = new System.Drawing.Point(pos, 120);
            // posizione e dimensione dinimica della listbox 
            listBox1.Size = new System.Drawing.Size(240, (heightTextbox + heightTextbox / 5) * num);
            listBox1.Location = new System.Drawing.Point(pos + 50, 10);

            // attivazione thread
            thr = new Thread(CreateCrossword);
            thr.Start();
            Thread.Sleep(100);
        }

        private void AddTextboxes(int nRows, int nCols)
        {
            for (int r = 0; r < nRows; r++)
            {
                for (int c = 0; c < nCols; c++)
                {
                    t[r, c] = SingleTextbox(r, c);
                    Controls.Add(t[r, c]);
                }
            }
        }

        private TextBox SingleTextbox(int r, int c)
        {
            return new TextBox
            {
                Size = new System.Drawing.Size(widthTextbox, heightTextbox),
                Location = new System.Drawing.Point(widthTextbox + c * widthTextbox + 5 * c, (heightTextbox + heightTextbox / 5) * r + 10),
                Font = new System.Drawing.Font("Arial", 28 * heightTextbox / 50),
                TextAlign = HorizontalAlignment.Center
            };
        }

        private void Create_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            aTimer.Enabled = true;
            thr = new Thread(CreateCrossword);
            thr.Start();
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            // m.StampaMatrice(); // stampa della matrice ogni X secondi in Console
        }

        private void VisualizeTextboxes() // visualizzazione delle textbox
        {
            for (int r = 0; r < num; r++)
                for (int c = 0; c < num; c++)
                {
                    if (m.GetMatrix()[r, c].GetIsBlack())
                    {
                        t[r, c].BackColor = System.Drawing.Color.Black;
                        t[r, c].Enabled = false;
                    }
                    else
                        t[r, c].Text = m.GetMatrix()[r, c].GetLetter().ToString().ToUpper();
                }
        }

        private void CreateCrossword()
        {
            // azzeramento variabili
            backTracking = false;
            i = 0;
            lunBefore = 0;
            lunAfter = 0;
            
            // azzeramento liste e matrici
            m.AzzeraMatrice();
            HorizWords.Clear();
            wordsUsed.Clear();
            VerticalWords.Clear();

            // aggiunte tutte le informazioni riguardo parole orizzontali e verticali
            m.SetHorizontalWords(HorizWords);
            m.SetVericalWords();

            while (i < HorizWords.Count) // si scorrono tutte le parole orizzontali
            {
                do
                {
                    wordsTried = HorizWords[i].GetWordsTried(); // caricate le parole testate
                    lunBefore = HorizWords[i].GetLunParola(); // caricata la lunghezza della parola 

                    backTracking = m.LoadWord(HorizWords[i].GetPosMatrix(), lunBefore, lunAfter, wordsUsed, wordsTried);
                    lunAfter = lunBefore;
                    if (backTracking)
                    {
                        m.RemoveWord(HorizWords[i].GetPosMatrix(), HorizWords[i].GetLunParola()); // rimozione della parola dalla matrice
                        break;
                    }
                } while (!m.CheckVerticalMatches(HorizWords[i].GetPosMatrix(), HorizWords[i].GetLunParola(), wordsUsed, wordsTried));

                if (backTracking)
                {
                    HorizWords[i].IncrementNumBackTracking(); // incremento il numero che indica quante volte ho fatto un salto all'indietro
                    HorizWords[i].GetWordsTried().Clear(); // resetto le parole testate

                    i--; // mi muovo alla parola orizzontale precedente
                    HorizWords[i].GetWordsTried().Add(wordsUsed[wordsUsed.Count - 1]); // aggiungo alle parole orizzontali provate l'ultima parola usata per quella parola orizzontale
                    wordsUsed.RemoveAt(wordsUsed.Count - 1); // rimossa l'ultima delle parole utilizzate

                    if (HorizWords[i + 1].GetNumBackTracking() > 2) // ogni volta che faccio più di 2 salti all'indietro
                    {
                        HorizWords[i].GetWordsTried().Clear(); // reset delle parole provate della penultima posizione
                        HorizWords[i].ResetNumBackTracking(); // resert del numero che indica il numero di salti all'indietro
                        HorizWords[i + 1].ResetNumBackTracking(); // reset delle parole provate dell'ultima posizione

                        i--; // un ulteriore salto all'indietro
                        HorizWords[i].GetWordsTried().Add(wordsUsed[wordsUsed.Count - 1]); // aggiunta nelle parole provate dell'ultima parola orizzontale a cui sono saltato all'indietro
                        wordsUsed.RemoveAt(wordsUsed.Count - 1); // rimozione dalle parole usate
                    }
                }
                else
                    i++; // incremento dell'indice
                wordsTried.Clear();
            }
            VerticalWords = m.FindVerticalWords(); // trovate tutte le parole verticali
            this.Invoke((MethodInvoker)(() => listBox1.Items.Add("PAROLE UTILIZZATE:"))); // scritto nella listbox tramite il thread
            VisualizeWordsUsed(); // visualizzazione delle parole utilizzate nella listbox
            thr.Interrupt(); // interruzione del thread
            aTimer.Enabled = false;
        }

        private void VisualizeWordsUsed() // aggiunta delle parole usate nella listbox tramite thread
        {
            foreach (string s in wordsUsed)
                if (s.Length > 1)
                    this.Invoke((MethodInvoker)(() => listBox1.Items.Add(s.ToUpper())));
            foreach (string s in VerticalWords)
                this.Invoke((MethodInvoker)(() => listBox1.Items.Add(s.ToUpper())));
        }

        private void Visualisation_Tick(object sender, EventArgs e)
        {
            VisualizeTextboxes();
        }

        private void Crossword_FormClosed(object sender, FormClosedEventArgs e)
        {
            thr.Abort();
            Application.Exit();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            thr.Abort();
            Application.Exit();
        }

        private void Nuovo_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            aTimer.Enabled = true;
            thr = new Thread(CreateCrossword);
            thr.Start();
        }

        private void Esc_Click(object sender, EventArgs e)
        {
            thr.Abort();
            Application.Exit();
        }

        private void Annulla_Click(object sender, EventArgs e)
        {
            thr.Abort();
            m.AzzeraMatrice();
            VisualizeTextboxes();
        }
    }
}