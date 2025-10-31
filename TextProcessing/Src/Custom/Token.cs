namespace TextProcessing
{
    public enum TypeToken
    { 
        Word,
        EoI,
        EoL,
        EoP
    }

    /// <summary>
    /// Represents a lexical token.
    /// </summary>
    public readonly struct Token
    {
        public TypeToken Type { get; init; }
        public string? Word { get; init; }

        public Token(TypeToken type)
        {
            if (type == TypeToken.Word)
            {
                throw new InvalidOperationException("Use Token(string word) constructor instead.");
            }

            Type = type;
            Word = null;
        }

        public Token(string word)
        {
            Type = TypeToken.Word;
            Word = word;
        }

        public override string ToString()
        {
            return $"Type: {Type}, Word: {Word}";
        }
    }
}
