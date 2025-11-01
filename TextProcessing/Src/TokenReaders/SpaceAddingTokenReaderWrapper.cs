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
        private int _currentSpaceWidth { get; set; } = 0;
        private bool _remainderSent { get; set; } = false;
        private bool _isOneWordLine { get; set; } = false;

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

            if (_currentSpaceWidth < _baseSpaceWidth)
            {
                _currentSpaceWidth++;
                return new Token(TypeToken.Space);
            }

            if (_spacesRemainder > 0 && !_remainderSent)
            {
                _spacesRemainder--;
                _remainderSent = true;

                return new Token(TypeToken.Space);
            }

            if (_isOneWordLine)
            {
                _isOneWordLine = false;
                _totalLengthOfWordsInLine = 0;
                _baseSpaceWidth = 0;
                token = (Token)_lineEndingToken!;
                _lineEndingToken = null;

                return token;
            }

            _currentSpaceWidth = 0;
            _remainderSent = false;

            // Try to fill the word buffer
            if (_wordBuffer.Count == 0)
            {
                while ((token = _reader.ReadToken()) is { Type: TypeToken.Word })
                {
                    _wordBuffer.Enqueue(token);
                    _totalLengthOfWordsInLine += token.Word!.Length;
                }

                // Buffer wasn't filled, there was different token than word
                if (_wordBuffer.Count == 0)
                {
                    return token;
                }

                // Buffer was filled, save the token, which closed the line
                _lineEndingToken = token;

                if (_wordBuffer.Count == 1)
                {
                    // There is only one word in the line and it's shorter than max line width
                    if (_totalLengthOfWordsInLine < _maxLineWidth)
                    {
                        _isOneWordLine = true;
                        totalSpacesWidth = _maxLineWidth - _totalLengthOfWordsInLine;
                        _baseSpaceWidth = totalSpacesWidth;
                        _spacesRemainder = 0;
                    }
                    // Longer than max line width
                    else
                    {
                        _isOneWordLine = true;
                        _baseSpaceWidth = 0;
                        _spacesRemainder = 0;
                    }
                }

                if (_wordBuffer.Count > 1)
                {
                    numberOfSpaces = _wordBuffer.Count - 1;
                    totalSpacesWidth = _maxLineWidth - _totalLengthOfWordsInLine;
                    _baseSpaceWidth = totalSpacesWidth / numberOfSpaces;
                    _spacesRemainder = totalSpacesWidth % numberOfSpaces;
                }

                return _wordBuffer.Dequeue();
            }

            // There's a last word token in the buffer, in the next call return the token, which closed this line
            if (_wordBuffer.Count == 1)
            {
                _priorityToken = _lineEndingToken;
                _lineEndingToken = null;
                _totalLengthOfWordsInLine = 0;
                _baseSpaceWidth = 0;

                return _wordBuffer.Dequeue();
            }

            return _wordBuffer.Dequeue();
        }
    }
}
