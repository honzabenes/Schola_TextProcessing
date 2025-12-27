namespace ExcelFramework
{
    public record NumberCell : Cell
    {
        public NumberCell(int value) : base(value, CellState.Calculated) { }


        public override string GetOutputString()
        {
            return $"{Value}";
        }


        public override EvaluationResult GetValue(Sheet sheet)
        {
            return new EvaluationResult((int)Value!);
        }
    }
}
