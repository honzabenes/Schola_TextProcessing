namespace ExcelFramework
{
    public struct CellAddress
    {
        public int ColIdx { get; private set; }
        public int RowIdx { get; private set; }

        public CellAddress(string addressLabel)
        {
            (ColIdx, RowIdx) = Parse(addressLabel);
        }

        public CellAddress(int colIdx, int rowIdx)
        {
            ColIdx = colIdx;
            RowIdx = rowIdx;
        }


        private (int colIdx, int rowIdx) Parse(string addressLabel)
        {
            string colPart = "";
            string rowPart = "";

            foreach(char c in addressLabel)
            {
                if (char.IsLetter(c))
                {
                    colPart += c;
                }
                else if (char.IsDigit(c))
                {
                    rowPart += c;
                }
                else
                {
                    throw new FormatException("Invalid address label format.");
                }
            }

            if (colPart.Length == 0 || rowPart.Length == 0)
            {
                throw new FormatException("Invalid address label format.");
            }

            int colIdx = ParseColumn(colPart);
            int rowIdx = int.Parse(rowPart) - 1;

            return (colIdx, rowIdx);
        }


        private int ParseColumn(string columnLabel)
        {
            int result = 0;

            foreach(char c in columnLabel)
            {
                result *= 26;
                result += c - 'A' + 1;
            }

            result -= 1;

            return result;
        }


        public override string ToString()
        {
            return $"({ColIdx}, {RowIdx})";
        }
    }
}
