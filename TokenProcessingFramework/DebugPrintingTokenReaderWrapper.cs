namespace TokenProcessingFramework
{
    /// <summary>
    /// Wraps an existing <see cref="ITokenReader"/> and prints the tokens to the standard output.
    /// </summary>
    public class DebugPrintingTokenReaderWrapper : ITokenReader
    {
        private ITokenReader _reader;

        public DebugPrintingTokenReaderWrapper(ITokenReader reader)
        {
            _reader = reader;
        }

        
        public Token ReadToken()
        {
            Token token = _reader.ReadToken();

            Console.WriteLine(token);

            return token;
        }
    }
}
