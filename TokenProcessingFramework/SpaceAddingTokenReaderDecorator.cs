namespace TextProcessing
{
    /// <summary>
    /// Wraps an existing <see cref="ITokenReader"/> for text justification implementation based
    /// on the given maxLineWidth. This class assumes justified End of Line tokens on the input.
    /// </summary>
    public class SpaceAddingTokenReaderDecorator : ITokenReader
    {
        private ITokenReader _reader { get; set; }
        private Queue<Token> _tokenBuffer { get; set; } = new Queue<Token>();
        private int _maxLineWidth { get; init; }

        public SpaceAddingTokenReaderDecorator(ITokenReader reader, int maxLineWidth)
        {
            _reader = reader;
            _maxLineWidth = maxLineWidth;
        }


        public Token ReadToken()
        {
            if (_tokenBuffer.Count == 0)
            {
                LoadNextLine();
            }

            return _tokenBuffer.Dequeue();
        }


        private void LoadNextLine()
        {
            List<Token> wordsInLine = new List<Token>();
            Token lineEndingToken;

            Token token;


            while ((token = _reader.ReadToken()) is { Type: TypeToken.Word })
            {
                wordsInLine.Add(token);
            }

            lineEndingToken = token;

            // Reader didn't load any word token
            if (wordsInLine.Count == 0)
            {
                _tokenBuffer.Enqueue(lineEndingToken);
                return;
            }

            // One word line, justify to the left
            if (wordsInLine.Count == 1)
            {
                _tokenBuffer.Enqueue(wordsInLine[0]);
                _tokenBuffer.Enqueue(lineEndingToken);
                return;
            }


            bool justified = lineEndingToken.Type == TypeToken.EoL;

            int spacesCount = wordsInLine.Count - 1;

            int totalWordsLength = 0;
            foreach (Token word in wordsInLine)
            {
                totalWordsLength += word.Word!.Length;
            }

            int totalSpacesWidth = _maxLineWidth - totalWordsLength;

            int baseSpaceWidth;
            int spacesRemainder;

            if (justified)
            {
                baseSpaceWidth = totalSpacesWidth / spacesCount;
                spacesRemainder = totalSpacesWidth % spacesCount;
            }
            else
            {
                baseSpaceWidth = 1;
                spacesRemainder = 0;
            }


            for (int i = 0; i < wordsInLine.Count; i++)
            {
                _tokenBuffer.Enqueue(wordsInLine[i]);

                // If the word isn't the last on the line, add spaces
                if (i < spacesCount)
                {
                    for (int j = 0; j < baseSpaceWidth; j++)
                    {
                        _tokenBuffer.Enqueue(new Token(TypeToken.Space));
                    }

                    if (spacesRemainder > 0)
                    {
                        _tokenBuffer.Enqueue(new Token(TypeToken.Space));
                        spacesRemainder--;
                    }
                }
            }

            _tokenBuffer.Enqueue(lineEndingToken);
        }
    }
}
