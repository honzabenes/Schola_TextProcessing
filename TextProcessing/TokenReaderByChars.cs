using System.Text;

namespace TextProcessing
{
    public class TokenReaderByChars : TokenReader
    {
        private int NewLineStreak { get; set; } = 2;

        public TokenReaderByChars(TextReader reader, params char[] separators)
            : base(reader, separators) { }


        public override Token ReadToken()
        {
            int peekChar;

            while ((peekChar = _reader.Peek()) != -1)
            {
                char ch = (char)peekChar;

                // New line found, need to be tokenized
                if (ch == '\n')
                {
                    break;
                }

                // White character found, need to be skipped
                if (_separators.Contains(ch))
                {
                    _reader.Read();
                }

                // Non white character found, means beginning of the new word, need to be tokenized
                else
                {
                    break;
                }
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
                NewLineStreak++;
                _reader.Read();
                return new Token(TypeToken.EoL);
            }

            // Tokenize end of paragraph, if we found a new one
            if (NewLineStreak >= 2)
            {
                NewLineStreak = 0;
                return new Token(TypeToken.EoP);
            }

            // Read and tokenize word if we ended at non-white character
            var wordBuilder = new StringBuilder();

            while ((peekChar = _reader.Peek()) != -1)
            {
                char ch = (char)peekChar;

                if (_separators.Contains(ch))
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
