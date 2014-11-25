namespace Corecalc
{
    public class ImmutableCell
    {
        private readonly int _row;

        public int Row
        {
            get { return _row; }
        }

        private readonly bool _rowAbsolute;

        public bool RowAbsolute
        {
            get { return _rowAbsolute; }
        }

        private readonly int _column;

        public int Column
        {
            get { return _column; }
        }
        
        private readonly bool _columnAbsolute;

        public bool ColumnAbsolute
        {
            get { return _columnAbsolute; }
        }

        public Cell(int row, bool rowAbsolute, int column, bool columnAbsolute)
        {
            _row = row;
            _rowAbsolute = rowAbsolute;
            _column = column;
            _columnAbsolute = columnAbsolute;
        }
    }
}
