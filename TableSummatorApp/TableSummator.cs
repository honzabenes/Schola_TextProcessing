using System.Text;
using TokenProcessingFramework;

namespace TableSummatorApp
{
    /// <summary>
    /// Processes tokens for summing values from a given table column.
    /// </summary>
    public class TableSummator : ITokenProcessor
    {
        private string _targetColumnName { get; init; }
        private int? _targetColumnIndex { get; set; } = null;
        private int _currentRow { get; set; } = 0;
        private int _currentColumn { get; set; } = 0;
        private int _rowSize { get; set; } = 0;
        private long _columnSum { get; set; } = 0;

        public TableSummator(string columnName)
        {
            _targetColumnName = columnName;
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
                if (_targetColumnIndex == null && _targetColumnName == token.Word)
                {
                    _targetColumnIndex = _currentColumn;
                }
                _rowSize++;
            }
            else if (_currentColumn > _rowSize)
            {
                throw new InvalidTableFormatException();
            }
            else if (_currentColumn == _targetColumnIndex)
            {
                try
                {
                    _columnSum += int.Parse(token.Word!);
                }
                catch (FormatException)
                {
                    throw new InvalidParseException();
                }
                catch (OverflowException)
                {
                    throw new InvalidParseException();
                }
            }
        }


        private void ProcessEoLToken()
        {
            if (_currentColumn == 0)
            {
                throw new InvalidTableFormatException();
            }
            if (_currentRow == 0 && _targetColumnIndex == null)
            {
                throw new NonExistenColumnNameException();
            }
            if (_currentColumn < _rowSize)
            {
                throw new InvalidTableFormatException();
            }
            _currentRow++;
            _currentColumn = 0;
        }


        private void ProcessEoIToken()
        {
            if (_currentRow == 0 && _currentColumn == 0)
            {
                throw new InvalidTableFormatException();
            }
            if (_currentColumn > 0 && _currentColumn < _rowSize)
            {
                throw new InvalidTableFormatException();
            }
        }


        public void WriteOut(TextWriter writer)
        {
            var sb = new StringBuilder();

            sb.AppendLine(_targetColumnName);
            foreach (char c in _targetColumnName)
            {
                sb.Append('-');
            }
            sb.AppendLine();
            sb.AppendLine(_columnSum.ToString());

            string output = sb.ToString();

            writer.Write(output);
        }
    }
}
