using System.Text;

namespace TextProcessing
{
    public class TokenReaderByChars : TokenReader
    {
        private int _newLineStreak { get; set; } = 0;
        private bool _wordFound { get; set; } = false;

        public TokenReaderByChars(TextReader reader, params char[] whiteSpaces)
            : base(reader, whiteSpaces) { }


        public override Token ReadToken()
        {
            int peekChar;

            while ((peekChar = _reader.Peek()) != -1)
            {
                char ch = (char)peekChar;

                // New line or non white char found, need to be tokenized
                if (ch == '\n' || !_whiteSpaces.Contains(ch))
                {
                    break;
                }

                // White character found, need to be skipped
                _reader.Read();
            }

            // Tokenize if we ended at the end of input
            if (peekChar == -1)
            {
                // First tokenize end of paragraph if found
                if (_newLineStreak >= 2)
                {
                    _newLineStreak = 0;
                    return new Token(TypeToken.EoP);
                }

                return new Token(TypeToken.EoI);
            }

            char currentChar = (char)peekChar;

            // Move the cursor and tokenize if we ended at new line
            if (currentChar == '\n')
            {
                // If we have already found some paragraph
                if (_wordFound)
                {
                    _newLineStreak++;
                }

                _reader.Read();
                return new Token(TypeToken.EoL);
            }

            // Tokenize end of paragraph, if we found a new one
            if (_newLineStreak >= 2)
            {
                _newLineStreak = 0;
                return new Token(TypeToken.EoP);
            }

            // Read and tokenize word if we ended at non-white character
            var wordBuilder = new StringBuilder();

            while ((peekChar = _reader.Peek()) != -1)
            {
                char ch = (char)peekChar;

                if (_whiteSpaces.Contains(ch))
                {
                    break;
                }

                wordBuilder.Append(ch);

                _reader.Read();
            }

            string word = wordBuilder.ToString();

            _newLineStreak = 0;
            _wordFound = true;

            return new Token(word);
        }
    }
}
