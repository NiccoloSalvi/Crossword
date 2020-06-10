using System.Collections.Generic;

namespace Crossword
{
    class HorizontalWord
    {
        private List<string> wordList;
        private int numBackTracking;
        private int lunParola;
        private PosMatrix pos;

        public HorizontalWord(int l, PosMatrix p)
        {
            wordList = new List<string>();
            numBackTracking = 0;
            lunParola = l;
            pos = p;
        }

        public void SetWordsTried(List<string> wt) { wordList = wt; }
        public void IncrementNumBackTracking() { numBackTracking++; }
        public void ResetNumBackTracking() { numBackTracking = 0; }

        public int GetNumBackTracking() { return numBackTracking; }
        public List<string> GetWordsTried() { return wordList; }
        public PosMatrix GetPosMatrix() { return pos; }
        public int GetLunParola() { return lunParola; }
    }
}
