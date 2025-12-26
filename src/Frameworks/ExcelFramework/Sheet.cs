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

                Cells[address] = new FormulaCell(@operator, op1, op2);
            }
        }


        public Cell? GetCell(CellAddress address)
        {
            if (Cells.TryGetValue(address, out Cell cell))
            {
                return cell;
            }

            return null;
        }


        public int GetCellValue(CellAddress address)
        {
            if (Cells.TryGetValue(address, out Cell cell))
            {
                return cell.GetValue(this);
            }

            return 0;
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
