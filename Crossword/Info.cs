namespace Crossword
{
    public class Info
    {
        private int dim;
        private int heightTextbox;
        private int widthTextbox;
        private char fileName;

        public Info(int d, int h, int w)
        {
            dim = d;
            heightTextbox = h;
            widthTextbox = w;
            fileName = '\0';
        }

        public void SetFileName(char f) { fileName = f; }
        public int GetDim() { return dim; }
        public int GetHeight() { return heightTextbox; }
        public int GetWidth() { return widthTextbox; }
        public char GetFileName() { return fileName; }
    }
}
