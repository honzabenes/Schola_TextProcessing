namespace TokenProcessingFramework
{
    /// <summary>
    /// Wraps an existing <see cref="ITokenReader"/> and detects paragraph based on EoL tokens.
    /// </summary>
    public class ParagraphDetectingTokenReaderDecorator : ITokenReader
    {
        private ITokenReader _reader { get; set; }
        private Token? _priorityToken = null;
        private bool _wordFound { get; set; } = false;

        public ParagraphDetectingTokenReaderDecorator(ITokenReader reader)
        {
            _reader = reader;
        }


        public Token ReadToken()
        {
            if (_priorityToken is not null)
            {
                var token = _priorityToken.Value;
                _priorityToken = null;

                return token;
            }
            else
            {
                int newLinesFound = 0;

                Token token;
                while ((token = _reader.ReadToken()).Type == TokenType.EoL)
                {
                    if (_wordFound)
                    {
                        newLinesFound++;
                    }
                }

                if (token.Type == TokenType.Word && !_wordFound)
                {
                    _wordFound = true;
                }

                if (_wordFound)
                {
                    if (token.Type == TokenType.EoI)
                    {
                        _priorityToken = token;
                        return new Token(TokenType.EoP);
                    }

                    if (newLinesFound > 1)
                    {
                        _priorityToken = token;
                        return new Token(TokenType.EoP);
                    }
                }

                return token;
            }
        }
    }
}
