namespace TokenProcessingFramework
{
    /// <summary>
    /// Defines a contract for processing tokens and reporting the results.
    /// </summary>
    public interface ITokenProcessor
    {
        void ProcessToken(Token token);

        void WriteOut(TextWriter writer);
    }
}
