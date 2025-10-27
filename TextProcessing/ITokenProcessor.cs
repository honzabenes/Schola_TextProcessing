namespace TextProcessing
{
    public interface ITokenProcessor
    {
        void ProcessToken(Token token);

        void WriteOut(TextWriter writer);
    }
}
