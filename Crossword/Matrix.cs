using System.Collections.Generic;
using System.IO;
using System;

namespace Crossword
{
    class Matrix
    {
        private readonly string lunDict;
        private readonly int num;
        private Letter[,] matrix;
        private WordsList w;
        private List<string> VerticalWords;

        public Matrix(int n, char nameFile)
        {
            lunDict = "long"; // si seleziona il dizionario
            num = n;

            w = new WordsList(num);
            w.Load(lunDict + "Wordlist.txt");

            matrix = new Letter[num, num];
            for (int r = 0; r < num; r++)
                for (int c = 0; c < num; c++)
                    matrix[r, c] = new Letter();

            LoadGrid("grids\\" + num.ToString() + "x" + num.ToString() + "\\" + nameFile + ".txt"); // si carica la griglia
            VerticalWords = new List<string>();
        }

        public Letter[,] GetMatrix() { return matrix; }

        public void SetHorizontalWords(List<HorizontalWord> horizWords) // si aggiungono tutte le parole orizzontali
        {
            int lParola, last;

            for (int r = 0; r < num; r++)
            {
                last = 0;
                for (int c = 0; c < num; c++)
                {
                    if (GetMatrix()[r, c].GetIsBlack())
                    {
                        lParola = c - last;
                        if (lParola > 0)
                            horizWords.Add(new HorizontalWord(lParola, new PosMatrix(r, last)));
                        last = c + 1;
                    }
                }
                if (!GetMatrix()[r, num - 1].GetIsBlack())
                {
                    lParola = num - last;
                    horizWords.Add(new HorizontalWord(lParola, new PosMatrix(r, last)));
                }
            }
        }

        public void SetVericalWords() // si indica, per ogni cella della matrice, dove inizia la parola verticale che incrocia
        {
            int rTemp, len;
            for (int r = 0; r < num; r++)
            {
                for (int c = 0; c < num; c++)
                {
                    if (!matrix[r, c].GetIsBlack())
                    {
                        rTemp = r;
                        len = 0;
                        while (rTemp >= 0 && !matrix[rTemp, c].GetIsBlack())
                        {
                            rTemp--;
                            len++;
                        }
                        matrix[r, c].SetPosMatrix(new PosMatrix(rTemp + 1, c));
                        rTemp = r + 1;
                        while (rTemp < num && !matrix[rTemp, c].GetIsBlack())
                        {
                            rTemp++;
                            len++;
                        }
                        if (len == 1)
                            matrix[r, c].SetPosMatrix(null);
                        else
                            matrix[r, c].SetLunParola(len);
                    }
                }
            }
        }

        public void LoadGrid(string fileName) // caricata la griglia
        {
            int r = 0, c;
            foreach (var l in File.ReadAllLines(fileName))
            {
                c = 0;
                foreach (char s in l)
                {
                    if (s == 'o') // se nel file viene letto il carattere 'o', la matrice viene inizializzata
                        matrix[r, c].Azzera();
                    else
                        matrix[r, c].SetBlack(); 
                    c++;
                }
                r++;
            }
        }

        public void AzzeraMatrice() // reset dell'intera matrice, tranne per le celle nere
        {
            for (int r = 0; r < num; r++)
                for (int c = 0; c < num; c++)
                    if (!matrix[r, c].GetIsBlack())
                        matrix[r, c].Azzera();
        }

        public void StampaMatrice() // stampa della matrice nella Console
        {
            for (int r = 0; r < num; r++)
            {
                for (int c = 0; c < num; c++)
                {
                    if (matrix[r, c].GetIsBlack())
                        Console.Write('*' + "  ");
                    else
                        Console.Write(matrix[r, c].GetLetter() + "  ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void RemoveWord(PosMatrix p, int lun) // rimozione della parola dalla matrice
        {
            for (int c = p.GetColumn(); c < p.GetColumn() + lun; c++)
                matrix[p.GetRow(), c].Azzera();
        }

        public bool LoadWord(PosMatrix p, int lunParolaBef, int lunParoleAft, List<string> wordsUsed, List<string> wordsTried)
        {
            string newWord = w.GetWord(lunParolaBef, lunParoleAft, wordsTried, wordsUsed); // nuova parola da inserire
            int startCol = p.GetColumn();

            if (newWord != "") // se la parola non è nulla
            {
                wordsUsed.Add(newWord); // si aggiunge la parola a quelle usate
                foreach (char c in newWord) // si aggiunge la parola alla matrice
                {
                    matrix[p.GetRow(), startCol].SetLetter(c);
                    startCol++;
                }
                // StampaMatrice();
                return false; // backtracking risulta false
            }
            return true;
        }

        public List<string> FindVerticalWords() // si trovano tutte le parole verticali
        {
            VerticalWords.Clear();
            string word;

            for (int c = 0; c < num; c++)
            {
                word = "";
                for (int r = 0; r < num; r++)
                {
                    if (matrix[r, c].GetIsBlack())
                    {
                        if (word.Length > 1)
                            VerticalWords.Add(word); // si aggiunge la parola alla lista
                        word = "";
                    }
                    else
                        word += matrix[r, c].GetLetter();
                }
                if (word.Length > 1)
                    VerticalWords.Add(word);
            }
            return VerticalWords;
        }

        public bool CheckVerticalMatches(PosMatrix p, int lun, List<string> wordsUsed, List<string> wordsTried) // si controlla che da una parola si generino delle combinazioni accettabili
        {
            string startCaracters = "";
            int row, r;

            for (int c = p.GetColumn(); c < p.GetColumn() + lun; c++) // per ogni colonna, calcolare i caratteri iniziali
            {
                r = p.GetRow();
                if (matrix[r, c].GetLunParola() > 1)
                {
                    startCaracters = "";
                    row = matrix[r, c].GetPosMatrix().GetRow();
                    while (r >= 0 && r >= row)
                    {
                        startCaracters += matrix[r, c].GetLetter().ToString();
                        r--;
                    }

                    startCaracters = Reverse(startCaracters); // inverso della stringa dei caratteri iniziali

                    if (!w.ExistWord(matrix[row, c].GetLunParola(), startCaracters)) // se non esiste una parola che inizia con determinati caratteri speciali
                    {
                        wordsTried.Add(wordsUsed[wordsUsed.Count - 1]); // parola aggiunta a quelle provate
                        wordsUsed.RemoveAt(wordsUsed.Count - 1); // parola rimossa da quelle usate
                        return false;
                    }
                }
            }
            return true;
        }

        private string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
