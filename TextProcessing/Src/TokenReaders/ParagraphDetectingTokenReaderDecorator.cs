namespace TextProcessing
{
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

            if (token.Type == TypeToken.EoL)
            {
                if (_wordFound)
                {
                    _newLineStreak++;
                }

                return token;
            }
            else if (_newLineStreak >= 2)
            {
                _newLineStreak = 0;
                _priorityToken = token;

                return new Token(TypeToken.EoP);
            }
            else
            {
                _newLineStreak = 0;

                return token;
            }
        }
    }
}
