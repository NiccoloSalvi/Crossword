using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;

namespace Crossword
{
    class WordsList
    {
        private List<string>[] ListLetters;
        private List<string> listTemp;
        private readonly int num;
        private Random rdn;
        
        public WordsList(int n)
        {
            num = n;
            rdn = new Random();

            ListLetters = new List<string>[19]; // vettore di liste
            for (int i = 0; i < 19; i++)
                ListLetters[i] = new List<string>(); // una lista contiene tutte le parole lunghe X
            listTemp = new List<string>();
        }

        public void Load(string fileName)
        {
            foreach (var l in File.ReadAllLines(fileName))
                if (AccettableWord(l.ToString())) // si escludono tutte le parole con un accento o caratteri speciali
                    ListLetters[l.Length - 1].Add(l.ToString()); // aggiunta la parola, in base alla sua lunghezza, alla rispettiva lista
        }

        public string GetWord(int numLetters, int lunParoleAft, List<string> wordsTried, List<string> wordsUsed)
        {
            bool f = false;

            if (lunParoleAft == numLetters && numLetters != 0) // per evitare di copiare nuovamente la stessa lista
            {
                if (wordsTried.Count > 0)
                    listTemp.Remove(wordsTried[wordsTried.Count - 1]); // rimosse dalla lista l'ultima parola usata
                if (listTemp.Count == 0) // se tutte le parole sono già state utilizzate
                    return ""; // stringa di errore
                return listTemp[rdn.Next(0, listTemp.Count)]; // numero casuale tra gli elementi della lista
            }

            listTemp.Clear();
            listTemp = ListLetters[numLetters - 1].ToList();

            if (!f && wordsUsed.Count > 0)
                listTemp.Remove(wordsUsed[wordsUsed.Count - 1]); // rimossa dalla lista l'ultima parola usata
            if (wordsTried.Count > 0)
                listTemp.Remove(wordsTried[wordsTried.Count - 1]); // rimossa dalla lista l'ultima parola provata
            if (listTemp.Count == 0)
                return ""; // stringa di errore
            return listTemp[rdn.Next(0, listTemp.Count)]; // se tutte le parole sono già state utilizzate
        }

        public bool ExistWord(int numLetters, string startCaracters)
        {
            if (numLetters == 1)
                return true;

            List<string> listTemp = new List<string>();
            listTemp = ListLetters[numLetters - 1].ToList();
             
            int index = listTemp.BinarySearch(startCaracters); // ricerca dicotomica, per capire se una parola esista o meno
            if (index < 0)
            {
                int i = index * -1 - 1;
                if (i != listTemp.Count && listTemp[i].StartsWith(startCaracters))
                    return true;
                else
                    return false;
            }
            return true;
        }

        private bool AccettableWord(string word)
        {
            foreach (char c in word)
                if (c > 122)
                    return false;
            return true;
        }
    }
}
