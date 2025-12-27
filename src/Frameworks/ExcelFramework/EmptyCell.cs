namespace ExcelFramework
{
    public record EmptyCell : Cell
    {
        public EmptyCell() : base(0, CellState.Calculated) { }


        public override string GetOutputString()
        {
            return "[]";
        }


        public override EvaluationResult GetValue(Sheet sheet)
        {
            return new EvaluationResult(0);
        }
    }
}
