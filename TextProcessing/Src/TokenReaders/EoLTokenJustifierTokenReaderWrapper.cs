namespace TextProcessing
{
    /// <summary>
    /// Wraps an existing <see cref="ITokenReader"/> to justify End of Line tokens,
    /// so the row width is limited based on the given max line width.
    /// </summary>
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

            // Skip End of Line tokens
            while ((token = _reader.ReadToken()) is { Type: TypeToken.EoL }) { }

            if (token.Type == TypeToken.Word)
            {
                _currentLineWidth += token.Word!.Length;

                if (_currentLineWidth > _maxLineWidth)
                {
                    // If there's only one word on the line
                    if (_currentLineWidth == token.Word!.Length)
                    {
                        return token;
                    }

                    // Else we need to sent End of Line first
                    _priorityToken = token;
                    _currentLineWidth = token.Word!.Length;
                    return new Token(TypeToken.EoL);
                }

                _currentLineWidth += MIN_SPACE_WIDTH;

                return token;
            }

            if (token.Type == TypeToken.EoP)
            {
                _currentLineWidth = 0;

                return new Token(TypeToken.EoP);
            }

            return token;
        }
    }
}
