namespace TextProcessing
{
    public class TableSummator : ITokenProcessor
    {
        private TextWriter _writer;

        private string _columnName { get; set; }
        private int _currentRow { get; set; } = 0;
        private int _currentColumn { get; set; } = 0;
        private bool _isNewLineLastToken { get; set; } = true;

        public const string FileIsEmptyErrorMessage = "File is empty";
        public const string RowsAreNotTheSameSizeErrorMessage = "Rows are not the same size";
        public const string EmptyLineInTableErrorMessage = "Empty line in table";

        public TableSummator(TextWriter writer, string columnName)
        {
            _writer = writer;
            _columnName = columnName;
        }


        public void ProcessToken(Token token)
        {
            // TODO: actually implement
            if (token.Type == TypeToken.EoI)
            {
                if (_currentRow == 0 && _currentColumn == 0)
                {
                    throw new InvalidInputFormatException(FileIsEmptyErrorMessage);
                }
            }

            if (token.Type == TypeToken.Word)
            {
                _currentColumn++;
            }
        }


        public void WriteOut()
        {
            // TODO: actually implement
        }
    }
}
