namespace TextProcessing
{
    public class EoLTokenJustifierTokenReaderWrapper : ITokenReader
    {
        private ITokenReader _reader { get; set; }
        private Token? _priorityToken = null;
        private int _maxLineWidth { get; init; }
        private int _currentLineWidth { get; set; } = 0;
        private const int MIN_SPACE_WIDTH = 1;

        public EoLTokenJustifierTokenReaderWrapper(ITokenReader reader, int maxLineWidth)
        {
            _reader = reader;
            _maxLineWidth = maxLineWidth;
        }


        public Token ReadToken()
        {
            Token token;

            if (_priorityToken is not null)
            {
                token = (Token)_priorityToken;
                _priorityToken = null;
                _currentLineWidth += 1; // Add space
                return token;
            }

            while ((token = _reader.ReadToken()) is { Type: TypeToken.EoL }) { }

            if (token.Type == TypeToken.Word)
            {
                _currentLineWidth += token.Word!.Length;

                if (_currentLineWidth > _maxLineWidth)
                {
                    // If the word isn't the only one on the line
                    if (_currentLineWidth != token.Word!.Length)
                    {
                        _priorityToken = token;
                        _currentLineWidth = token.Word!.Length;
                        return new Token(TypeToken.EoL);
                    }
                    
                    return token;
                }

                _currentLineWidth += MIN_SPACE_WIDTH;

                return token;
            }

            if (token.Type == TypeToken.EoP)
            {
                _currentLineWidth = 0;
                _priorityToken = token;

                return new Token(TypeToken.EoL);
            }

            return token;
        }
    }
}
