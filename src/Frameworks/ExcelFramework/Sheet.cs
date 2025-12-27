namespace ExcelFramework
{
    public class Sheet
    {
        public Dictionary<CellAddress, Cell> Cells { get; private set; }

        public Sheet()
        {
            Cells = new Dictionary<CellAddress, Cell>();
        }

        public Sheet(Dictionary<CellAddress, Cell> cells)
        {
            Cells = cells;
        }


        public void AddCell(CellAddress address, string content)
        {
            if (content == "[]")
            {
                Cells[address] = new EmptyCell();
                return;
            }

            if (!content.StartsWith('='))
            {
                if (int.TryParse(content, out int number))
                {
                    Cells[address] = new NumberCell(number);
                    return;
                }
                else
                {
                    Cells[address] = new ErrorCell(ErrorMessages.Invval);
                    return;
                }
            }

            if (content.StartsWith('='))
            {
                try
                {
                    AddFromulaCell(address, content);
                }
                catch (InvalidCellAddressLabelApplicationException)
                {
                    Cells[address] = new ErrorCell(ErrorMessages.Formula);
                    return;
                }
            }
        }


        private void AddFromulaCell(CellAddress address, string content)
        {
            string formula = content.Substring(1);

            char[] operators = { '+', '-', '*', '/' };
            char @operator = ' ';

            foreach (char c in formula)
            {
                if (operators.Contains(c))
                {
                    @operator = c;
                }
            }

            string[] operands = formula.Split(operators);

            if (@operator == ' ')
            {
                Cells[address] = new ErrorCell(ErrorMessages.MissOp);
                return;
            }

            if (operands.Length != 2)
            {
                Cells[address] = new ErrorCell(ErrorMessages.Formula);
                return;
            }

            string leftPart = operands[0];
            string rightPart = operands[1];

            var op1 = new CellAddress(leftPart);
            var op2 = new CellAddress(rightPart);

            Cells[address] = new FormulaCell(@operator, op1, op2);
        }


        public Cell? GetCell(CellAddress address)
        {
            if (Cells.TryGetValue(address, out Cell cell))
            {
                return cell;
            }

            return null;
        }


        public EvaluationResult GetCellValue(CellAddress address)
        {
            if (Cells.TryGetValue(address, out Cell cell))
            {
                return cell.GetValue(this);
            }

            return new EvaluationResult(0);
        }


        public void CalculateAll()
        {
            foreach (Cell cell in Cells.Values)
            {
                cell.GetValue(this);
            }
        }
    }
}
