using System.Text;

namespace TextProcessing
{
    public class TableSummator : ITokenProcessor
    {
        private string _sumColumnName { get; init; }
        private int? _sumColumnNumber { get; set; } = null;
        private int _currentRow { get; set; } = 0;
        private int _currentColumn { get; set; } = 0;
        private int _rowSize { get; set; } = 0;

        public long ColumnSum { get; set; } = 0;

        public const string FileIsEmptyErrorMessage = "File is empty";
        public const string RowsAreNotTheSameSizeErrorMessage = "Rows are not the same size";
        public const string EmptyLineInTableErrorMessage = "Empty line in table";

        public TableSummator(string columnName)
        {
            _sumColumnName = columnName;
        }


        public void ProcessToken(Token token)
        {
            switch (token.Type)
            {
                case TypeToken.Word:
                    ProcessWordToken(token);
                    break;

                case TypeToken.EoL:
                    ProcessEoLToken();
                    break;

                case TypeToken.EoI:
                    ProcessEoIToken();
                    break;

                default: break;
            }
        }


        private void ProcessWordToken(Token token)
        {
            _currentColumn++;
            if (_currentRow == 0)
            {
                if (_sumColumnNumber == null && _sumColumnName == token.Word)
                {
                    _sumColumnNumber = _currentColumn;
                }
                _rowSize++;
            }
            else if (_currentColumn > _rowSize)
            {
                throw new InvalidInputFormatException(RowsAreNotTheSameSizeErrorMessage);
            }
            else if (_currentColumn == _sumColumnNumber)
            {
                try
                {
                    ColumnSum += int.Parse(token.Word!);
                }
                catch (FormatException)
                {
                    throw new NotParsableByIntException();
                }
                catch (OverflowException)
                {
                    throw new NotParsableByIntException();
                }
            }
        }


        private void ProcessEoLToken()
        {
            if (_currentColumn == 0)
            {
                throw new InvalidInputFormatException(EmptyLineInTableErrorMessage);
            }
            if (_currentRow == 0 && _sumColumnNumber == null)
            {
                throw new NonExistenColumnNameInTableException();
            }
            if (_currentColumn < _rowSize)
            {
                throw new InvalidInputFormatException(RowsAreNotTheSameSizeErrorMessage);
            }
            _currentRow++;
            _currentColumn = 0;
        }


        private void ProcessEoIToken()
        {
            if (_currentRow == 0 && _currentColumn == 0)
            {
                throw new InvalidInputFormatException(FileIsEmptyErrorMessage);
            }
            if (_currentColumn > 0 && _currentColumn < _rowSize)
            {
                throw new InvalidInputFormatException(RowsAreNotTheSameSizeErrorMessage);
            }
        }


        public void WriteOut(TextWriter writer)
        {
            var sb = new StringBuilder();

            sb.AppendLine(_sumColumnName);
            foreach (char c in _sumColumnName)
            {
                sb.Append('-');
            }
            sb.AppendLine();
            sb.AppendLine(ColumnSum.ToString());

            string output = sb.ToString();

            writer.Write(output);
        }
    }
}
