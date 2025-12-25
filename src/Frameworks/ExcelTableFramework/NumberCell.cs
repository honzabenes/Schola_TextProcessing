namespace ExcelFramework
{
    public class NumberCell : Cell
    {
        public NumberCell(int value) : base(value, CellState.Calculated) { }


        public override string GetOutputString()
        {
            return $"{Value}";
        }


        public override int GetValue(Sheet sheet)
        {
            return (int)Value!;
        }
    }
}
