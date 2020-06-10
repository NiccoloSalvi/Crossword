namespace Crossword
{
    class Word
    {
        private int index;
        private bool isUsed;

        public Word(int i)
        {
            index = i;
            isUsed = false;
        }

        public void SetIsUsed(bool u)
        {
            isUsed = u;
        }
    }
}
