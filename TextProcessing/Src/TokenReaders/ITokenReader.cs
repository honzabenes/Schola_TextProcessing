namespace TextProcessing
{
    /// <summary>
    /// Defines a contract for reading and retrieving tokens from data source.
    /// </summary>
    public interface ITokenReader
    {
        Token ReadToken();
    }
}
