namespace ExcelFramework
{
    public abstract class Cell
    {
        public int? Value { get; protected set; }
        public CellState State { get; protected set; }

        public Cell(int? value, CellState state)
        {
            Value = value;
            State = state;
        }


        public abstract string GetOutputString();

        public abstract int GetValue(Sheet sheet);
    }
}
