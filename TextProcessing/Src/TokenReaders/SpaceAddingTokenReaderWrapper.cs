namespace TextProcessing
{
    public class SpaceAddingTokenReaderWrapper : ITokenReader
    {
        private ITokenReader _reader { get; set; }
        private Queue<Token> _wordBuffer { get; set; } = new Queue<Token>();
        private Token? _priorityToken { get; set; } = null;
        private Token? _lineEndingToken { get; set; } = null;
        private int _maxLineWidth { get; init; }
        private int _totalLengthOfWordsInLine { get; set; } = 0;
        private int _baseSpaceWidth { get; set; } = 0;
        private int _spacesRemainder { get; set; } = 0;
        private int _numberOfSpacesSent { get; set; } = 0;
        private bool _remainderSent { get; set; } = false;

        public SpaceAddingTokenReaderWrapper(ITokenReader reader, int maxLineWidth)
        {
            _reader = reader;
            _maxLineWidth = maxLineWidth;
        }


        public Token ReadToken()
        {
            int numberOfSpaces;
            int totalSpacesWidth;

            Token token;

            if (_priorityToken is not null)
            {
                token = (Token)_priorityToken;
                _priorityToken = null;

                return token;
            }

            // Try to fill the word buffer
            if (_wordBuffer.Count == 0)
            {
                while ((token = _reader.ReadToken()) is { Type: TypeToken.Word })
                {
                    _wordBuffer.Enqueue(token);
                    _totalLengthOfWordsInLine += token.Word!.Length;
                }

                // Buffer wasn't filled
                if (_wordBuffer.Count == 0)
                {
                    _numberOfSpacesSent = 0;
                    _remainderSent = false;
                    _totalLengthOfWordsInLine = 0;
                    return token;
                }

                // Buffer was filled, save the token, which closed the line
                _lineEndingToken = token;

                if (_wordBuffer.Count == 1)
                {
                    if (_totalLengthOfWordsInLine < _maxLineWidth)
                    {
                        numberOfSpaces = 1;
                        totalSpacesWidth = _maxLineWidth - _totalLengthOfWordsInLine;
                        _baseSpaceWidth = totalSpacesWidth;
                        _spacesRemainder = 0;
                    }

                    _lineEndingToken = token;
                }

                if (_wordBuffer.Count > 1)
                {
                    numberOfSpaces = _wordBuffer.Count - 1;
                    totalSpacesWidth = _maxLineWidth - _totalLengthOfWordsInLine;
                    _baseSpaceWidth = totalSpacesWidth / numberOfSpaces;
                    _spacesRemainder = totalSpacesWidth % numberOfSpaces;

                    return _wordBuffer.Dequeue();
                }

            }

            // There are 2 or more word tokens in the word buffer
            if (_numberOfSpacesSent < _baseSpaceWidth)
            {
                _numberOfSpacesSent++;
                return new Token(TypeToken.Space);
            }

            if (_spacesRemainder > 0 && !_remainderSent)
            {
                _spacesRemainder--;
                _remainderSent = true;

                return new Token(TypeToken.Space);
            }

            _numberOfSpacesSent = 0;
            _remainderSent = false;

            // There's a last word token in the buffer, in the next step return the token, which closed this line
            if (_wordBuffer.Count == 1)
            {
                _priorityToken = _lineEndingToken;
                _lineEndingToken = null;
                _totalLengthOfWordsInLine = 0;
                return _wordBuffer.Dequeue();
            }

            return _wordBuffer.Dequeue();
        }
    }
}
