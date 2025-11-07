namespace TokenProcessingFramework
{
    /// <summary>
    /// Wraps an existing <see cref="ITokenReader"/> and detects paragraph based on EoL tokens.
    /// </summary>
    public class ParagraphDetectingTokenReaderDecorator : ITokenReader
    {
        private ITokenReader _reader { get; set; }
        private Token? _priorityToken = null;
        private int _newLineStreak { get; set; } = 0;
        private bool _wordFound { get; set; } = false;

        public ParagraphDetectingTokenReaderDecorator(ITokenReader reader)
        {
            _reader = reader;
        }


        public Token ReadToken()
        {
            Token token;

            if (_priorityToken is not null)
            {
                token = (Token)_priorityToken;
                _priorityToken = null;

                return token;
            }

            token = _reader.ReadToken();


            switch (token.Type)
            {
                case TypeToken.EoL:
                    if (_wordFound)
                    {
                        _newLineStreak++;
                    }
                    break;

                case TypeToken.Word:
                    if (_newLineStreak >= 2)
                    {
                        _newLineStreak = 0;
                        _priorityToken = token;

                        return new Token(TypeToken.EoP);
                    }
                    _newLineStreak = 0;
                    _wordFound = true;
                    break;

                case TypeToken.EoI:
                    if (_wordFound)
                    {
                        _priorityToken = token;

                        return new Token(TypeToken.EoP);
                    }
                    break;

                default: 
                    break;
            }

            return token;
        }
    }
}
