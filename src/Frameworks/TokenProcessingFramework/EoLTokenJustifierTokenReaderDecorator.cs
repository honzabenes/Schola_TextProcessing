namespace TokenProcessingFramework
{
    /// <summary>
    /// Wraps an existing <see cref="ITokenReader"/> to justify End of Line tokens,
    /// so the row width is limited based on the given max line width.
    /// </summary>
    public class EoLTokenJustifierTokenReaderDecorator : ITokenReader
    {
        private ITokenReader _reader { get; set; }
        private Token? _priorityToken = null;
        private int _maxLineWidth { get; init; }
        private int _currentLineWidth { get; set; } = 0;
        private const int MinSpaceWidth = 1;

        public EoLTokenJustifierTokenReaderDecorator(ITokenReader reader, int maxLineWidth)
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

                if (token.Type == TokenType.Word)
                {
                    _currentLineWidth = token.Word!.Length + MinSpaceWidth;
                }

                return token;
            }

            // Skip EoL tokens
            while ((token = _reader.ReadToken()) is { Type: TokenType.EoL }) 
            {
                continue;
            }

            if (token.Type == TokenType.Word)
            {
                _currentLineWidth += token.Word!.Length;

                if (_currentLineWidth > _maxLineWidth)
                {
                    // If there's only one word on the line
                    if (_currentLineWidth == token.Word!.Length)
                    {
                        _currentLineWidth = 0;

                        _priorityToken = new Token(TokenType.EoL);

                        return token;
                    }

                    // Else we need to sent EoL first
                    _priorityToken = token;
                    _currentLineWidth = 0;

                    return new Token(TokenType.EoL);
                }

                _currentLineWidth += MinSpaceWidth;

                return token;
            }

            if (token.Type == TokenType.EoP)
            {
                _currentLineWidth = 0;
            }

            return token;
        }
    }
}
