using System.Text;

namespace TextProcessing
{
    public class TokenReaderByChars : TokenReader
    {
        // Sometimes I need to process more than one Token at the same time, that's the reason of this q.
        private Queue<Token> TokenQueue = new();
        private char[] Separators { get; init; }
        private int NewLineStreak { get; set; } = 2;

        public TokenReaderByChars(TextReader reader, params char[] separators) 
            : base(reader) 
        { 
            Separators = separators;
        }


        private char? ReadChar()
        {
            int chValue = _reader.Read();

            if (chValue == -1)
            {
                return null;
            }

            char ch = (char)chValue;

            return ch;
        }


        private void EnqueueToken()
        {
            string word = "";
            char? ch;

            while ((ch = ReadChar()) is not null)
            {
                if (!Separators.Contains((char)ch))
                {
                    word += ch;

                    if (NewLineStreak >= 2)
                    {
                        TokenQueue.Enqueue(new Token(TypeToken.EoP));
                    }
                    NewLineStreak = 0;
                }
                else
                {
                    if (word.Length > 0)
                    {
                        TokenQueue.Enqueue(new Token(word));
                        word = "";
                    }
                    if (ch == '\n')
                    {
                        NewLineStreak++;

                        TokenQueue.Enqueue(new Token(TypeToken.EoL));
                    }
                }
            }

            if (word.Length > 0)
            {
                TokenQueue.Enqueue(new Token(word));
            }

            TokenQueue.Enqueue(new Token(TypeToken.EoF));
        }


        public override Token ReadToken()
        {
            if (TokenQueue.Count > 0)
            {
                return TokenQueue.Dequeue();
            }
            else EnqueueToken();

            return TokenQueue.Dequeue();
        }
    }
}
