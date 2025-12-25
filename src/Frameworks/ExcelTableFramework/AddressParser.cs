namespace ExcelFramework
{
    public static class AddressParser
    {
        public static (int col, int row) Parse(string addressLabel)
        {
            string colPart = "";
            string rowPart = "";

            foreach (char c in addressLabel)
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
            int rowIdx = int.Parse(rowPart);

            return (colIdx, rowIdx);
        }


        private static int ParseColumn(string columnLabel)
        {
            int result = 0;

            foreach (char c in columnLabel)
            {
                result *= 26;
                result += c - 'A' + 1;
            }

            return result;
        }
    }
}
