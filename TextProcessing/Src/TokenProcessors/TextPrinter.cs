namespace TextProcessing
{
    /// <summary>
    /// Prints text based on tokens coming from the given input stream to the specified output.
    /// </summary>
    public class TextPrinter
    {
        private ITokenReader _reader;
        private TextWriter _writer;

        public TextPrinter(ITokenReader reader, TextWriter writer)
        {
            _reader = reader;
            _writer = writer;
        }


        public void PrintAllTokens()
        {
            Token token;

            while ((token = _reader.ReadToken()) is not { Type: TypeToken.EoI })
            {
                PrintToken(token);
            }

            PrintToken(token);
        }


        private void PrintToken(Token token)
        {
            char newLineChar = '\n';

            switch (token.Type)
            {
                case TypeToken.Word:
                    _writer.Write(token.Word);
                    break;

                case TypeToken.Space:
                    _writer.Write(" ");
                    break;

                case TypeToken.EoL:
                    _writer.Write(newLineChar);
                    break;

                case TypeToken.EoP:
                    for (int i = 0; i < 2; i++)
                    {
                        _writer.Write(newLineChar);
                    }
                    break;

                case TypeToken.EoI:
                    _writer.Write(newLineChar);
                    break;

                default:
                    break;
            }
        }
    }
}
