namespace TextProcessing
{
    public abstract class TokenReader
    {
        private TextReader _reader;

        public TokenReader(TextReader reader)
        {
            _reader = reader;
        }

        public abstract Token ReadToken();
    }
}
