using System.Text;

namespace TokenProcessingFramework
{
    /// <summary>
    /// Reads characters from a <see cref="TextReader"/> and converts them into tokens.
    /// </summary>
    public class ByCharsTokenReader : ITokenReader
    {
        private TextReader _reader;

        public ByCharsTokenReader(TextReader reader)
        {
            _reader = reader;
        }


        public Token ReadToken()
        {
            int peekChar;

            while ((peekChar = _reader.Peek()) != -1)
            {
                char ch = (char)peekChar;

                // New line or non white char found, need to be tokenized
                if (ch == '\n' || !char.IsWhiteSpace(ch))
                {
                    break;
                }

                // White character found, need to be skipped
                _reader.Read();
            }

            // Tokenize if we ended at the end of input
            if (peekChar == -1)
            {
                return new Token(TypeToken.EoI);
            }

            char currentChar = (char)peekChar;

            // Move the cursor and tokenize if we ended at new line
            if (currentChar == '\n')
            {
                _reader.Read();
                return new Token(TypeToken.EoL);
            }

            // Read and tokenize word if we ended at non-white character
            var wordBuilder = new StringBuilder();

            while ((peekChar = _reader.Peek()) != -1)
            {
                char ch = (char)peekChar;

                if (char.IsWhiteSpace(ch))
                {
                    break;
                }

                wordBuilder.Append(ch);

                _reader.Read();
            }

            string word = wordBuilder.ToString();

            return new Token(word);
        }
    }
}
