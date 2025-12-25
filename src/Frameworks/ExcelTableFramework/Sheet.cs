namespace ExcelFramework
{
    public class Sheet
    {
        private Dictionary<CellAddress, Cell> _cells = new Dictionary<CellAddress, Cell>();


        public List<CellAddress> GetAdresses()
        {
            return _cells.Keys.ToList();
        }

        public void AddCell(CellAddress address, string content)
        {
            if (content == "[]")
            {
                _cells[address] = new EmtpyCell();
                return;
            }

            if (!content.StartsWith('='))
            {
                if (int.TryParse(content, out int number))
                {
                    _cells[address] = new NumberCell(number);
                    return;
                }
                else
                {
                    throw new FormatException("Invalid content");
                }
            }

            if (content.StartsWith('='))
            {
                string formula = content.Substring(1);

                char[] operators = { '+', '-', '*', '/' };
                char @operator = ' ';
                int operatorsCount = 0;

                foreach (char c in formula)
                {
                    if (operators.Contains(c))
                    {
                        operatorsCount++;
                        @operator = c;
                    }
                }

                string[] operands = formula.Split(operators);

                if (@operator == ' ')
                {
                    throw new FormatException("Operator not found");
                }

                if (operands.Length != 2)
                {
                    throw new FormatException("Invalid formula format.");
                }

                string leftPart = operands[0];
                string rightPart = operands[1];

                var op1 = new CellAddress(leftPart);
                var op2 = new CellAddress(rightPart);

                _cells[address] = new FormulaCell(@operator, op1, op2);
            }
        }


        public Cell? GetCell(CellAddress address)
        {
            if (_cells.TryGetValue(address, out Cell cell))
            {
                return cell;
            }

            return null;
        }


        public int GetCellValue(CellAddress address)
        {
            if (_cells.TryGetValue(address, out Cell cell))
            {
                return cell.GetValue(this);
            }

            return 0;
        }


        public void CalculateAll()
        {
            foreach (Cell cell in _cells.Values)
            {
                cell.GetValue(this);
            }
        }
    }
}
