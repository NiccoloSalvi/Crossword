namespace Crossword
{
    class Letter
    {
        private PosMatrix pos;
        private int lunParola;
        private bool isBlack;
        private char letter;

        public Letter()
        {
            lunParola = -1;
            letter = '\0';
            pos = null;
        }

        public Letter(PosMatrix p, int l)
        {
            lunParola = l;
            pos = p;
        }

        public void SetLunParola(int l) { lunParola = l; }
        public void SetPosMatrix(PosMatrix p) { pos = p; }
        public void SetLetter(char l) { letter = l; }
        public void SetBlack() { isBlack = true; }
        public void Azzera() { letter = '\0'; }

        public int GetLunParola() { return lunParola; }
        public PosMatrix GetPosMatrix() { return pos; }
        public char GetLetter() { return letter; }
        public bool GetIsBlack() { return isBlack; }
    }
}
