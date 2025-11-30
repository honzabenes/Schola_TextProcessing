namespace TokenProcessingFramework
{
    /// <summary>
    /// Defines a contract for a factory that produces token readers sequentially.
    /// </summary>
    public interface ITokenReaderFactory
    {
        ITokenReader? GetNextReader();
    }
}
