using TokenProcessingFramework;

namespace TextJustifierApp
{
    /// <summary>
    /// Prints text based on tokens coming from the given input stream to the specified output.
    /// </summary>
    public class TokenPrinter
    {
        private ITokenReader _reader;
        private TextWriter _writer;
        private bool _isNewParagraph = false;

        public TokenPrinter(ITokenReader reader, TextWriter writer)
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
                case TypeToken.Space:
                    _writer.Write(" ");
                    break;

                case TypeToken.EoL:
                    _writer.Write(newLineChar);
                    break;

                case TypeToken.EoP:
                    _isNewParagraph = true;
                    _writer.Write(newLineChar);
                    break;

                case TypeToken.Word:
                    if (_isNewParagraph)
                    {
                        _writer.Write(newLineChar);
                        _isNewParagraph = false;
                    }
                    _writer.Write(token.Word);
                    break;

                default:
                    break;
            }
        }
    }
}
