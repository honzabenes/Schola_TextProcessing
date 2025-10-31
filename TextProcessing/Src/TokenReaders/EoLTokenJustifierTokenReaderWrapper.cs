namespace TextProcessing
{
    public class EoLTokenJustifierTokenReaderWrapper : ITokenReader
    {
        private ITokenReader _reader { get; set; }
        private int MaxLineWidth { get; init; }
        private Token? _priorityToken = null;
        private int currentLineWidth { get; set; } = 0;
        private const int MIN_SPACE_WIDTH = 1;

        public EoLTokenJustifierTokenReaderWrapper(ITokenReader reader, int maxLineWidth)
        {
            _reader = reader;
            MaxLineWidth = maxLineWidth;
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

            while ((token = _reader.ReadToken()) is { Type: TypeToken.EoL })
            {
                token = _reader.ReadToken();
            }

            if (token.Type == TypeToken.Word)
            {
                currentLineWidth += token.Word!.Length;

                if (currentLineWidth > MaxLineWidth)
                {
                    _priorityToken = token;
                    return new Token(TypeToken.EoL);
                }

                if (currentLineWidth < MaxLineWidth)
                {
                    currentLineWidth += MIN_SPACE_WIDTH;
                }

                return token;
            }

            if (token.Type == TypeToken.EoP)
            {
                currentLineWidth = 0;
                _priorityToken = token;

                return new Token(TypeToken.EoL);
            }

            return token;
        }
    }
}
