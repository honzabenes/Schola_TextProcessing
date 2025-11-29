namespace TokenProcessingFramework
{
    public interface ITokenReaderFactory
    {
        ITokenReader? GetNextReader();
    }
}
