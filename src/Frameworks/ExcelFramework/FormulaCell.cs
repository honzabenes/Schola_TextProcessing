namespace ExcelFramework
{
    /// <summary>
    /// Represents a cell in the sheet that contains a valid formula.
    /// </summary>
    public record FormulaCell : Cell
    {
        private string ErrorMessage = "";
        private char Operator;
        private CellAddress FirstOperandAddress;
        private CellAddress SecondOperandAddress;

        public FormulaCell(char @operator, CellAddress firstOperand, CellAddress secondOperand)
            : base(null, CellState.Uncalculated)
        {
            Operator = @operator;
            FirstOperandAddress = firstOperand;
            SecondOperandAddress = secondOperand;
        }


        public override string GetOutputString()
        {
            if (State == CellState.Error)
            {
                return ErrorMessage;
            }

            return $"{Value}";
        }


        public override EvaluationResult GetValue(Sheet sheet)
        {
            if (State == CellState.Calculated)
            {
                return new EvaluationResult((int)Value!);
            }

            if (State == CellState.Error)
            {
                return new EvaluationResult(ErrorMessage);
            }

            if (State == CellState.Calculating)
            {
                return new EvaluationResult(ErrorMessages.Cycle, this);
            }

            State = CellState.Calculating;

            EvaluationResult val1 = sheet.GetCellValue(FirstOperandAddress);
            EvaluationResult val2 = sheet.GetCellValue(SecondOperandAddress);

            if (!val1.IsSucces || !val2.IsSucces)
            {
                return HandleError(val1.IsSucces ? val2 : val1);
            }

            if (Operator == '/' && val2.Value == 0)
            {
                SetErrorState(ErrorMessages.DivZero);
                return new EvaluationResult(ErrorMessages.DivZero);
            }

            Value = CalculateResult(val1.Value, val2.Value);
            State = CellState.Calculated;

            return new EvaluationResult((int)Value);
        }

        
        /// <summary>
        /// Calculates the mathematical formula of two valid integer values.
        /// </summary>
        private int CalculateResult(int val1, int val2)
        {
            switch (Operator)
            {
                case '+': return val1 + val2;
                case '-': return val1 - val2;
                case '*': return val1 * val2;
                case '/': return val1 / val2;
                default: return 0;
            }
        }


        /// <summary>
        /// Handles all errors that may occur during formula evaluation.
        /// </summary>
        private EvaluationResult HandleError(EvaluationResult error)
        {
            if (error.ErrorMessage == ErrorMessages.Cycle && error.CycleInitiatior is not null)
            {
                if (ReferenceEquals(this, error.CycleInitiatior))
                {
                    SetErrorState(ErrorMessages.Cycle);
                    return new EvaluationResult(ErrorMessages.Cycle);
                }
                else
                {
                    SetErrorState(ErrorMessages.Cycle);
                    return error;
                }
            }

            SetErrorState(ErrorMessages.Error);
            return new EvaluationResult(ErrorMessages.Error);
        }


        private void SetErrorState(string message)
        {
            ErrorMessage = message;
            State = CellState.Error;
        }
    }
}
