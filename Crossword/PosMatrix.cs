namespace Crossword
{
    class PosMatrix
    {
        private int column;
        private int row;

        public PosMatrix(int r, int c)
        {
            column = c;
            row = r;
        }

        public void SetColumn(int c) { column = c; }
        public void SetRow(int r) { row = r; }

        public int GetColumn() { return column; }
        public int GetRow() { return row; }
    }
}
