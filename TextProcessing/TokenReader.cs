namespace TextProcessing
{
    public abstract class TokenReader
    {
        protected TextReader _reader;
        protected char[] _separators { get; init; }

        public TokenReader(TextReader reader, params char[] separators)
        {
            _reader = reader;

            if (separators.Length == 0)
            {
                throw new ArgumentException("At least one separator must be provided.");
            }

            _separators = separators;
        }

        public abstract Token ReadToken();
    }
}
