namespace TextProcessing
{
    public abstract class TokenReader
    {
        protected TextReader _reader;

        public TokenReader(TextReader reader)
        {
            _reader = reader;
        }

        public abstract Token ReadToken();
    }
}
