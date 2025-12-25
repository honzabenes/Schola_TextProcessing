namespace ExcelFramework
{
    public class FormulaCell : Cell
    {
        char Operator;
        CellAddress FirstOperand;
        CellAddress SecondOperand;

        public FormulaCell(char @operator, CellAddress firstOperand, CellAddress secondOperand)
            : base(null, CellState.Uncalculated)
        {
            Operator = @operator;
            FirstOperand = firstOperand;
            SecondOperand = secondOperand;
        }


        public override string GetOutputString()
        {
            return $"{Value}";
        }


        public override int GetValue(Sheet sheet)
        {
            if (State == CellState.Calculated)
            {
                return (int)Value;
            }

            if (State == CellState.Calculating)
            {
                throw new InvalidOperationException("Cycle detected.");
            }

            State = CellState.Calculating;

            int val1 = sheet.GetCellValue(FirstOperand);
            int val2 = sheet.GetCellValue(SecondOperand);

            int result = 0;

            switch (Operator)
            {
                case '+': result = val1 + val2; break;
                case '-': result = val1 - val2; break;
                case '*': result = val1 * val2; break;
                case '/':
                    if (val2 == 0) throw new DivideByZeroException("Dividing by 0.");
                    else result = val1 / val2;
                    break;
            }

            Value = result;
            State = CellState.Calculated;

            return result;
        }
    }
}
