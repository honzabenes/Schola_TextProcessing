namespace TokenProcessingFramework
{
    public class MultifileTokenReader : ITokenReader, IDisposable
    {
        private ITokenReader? _currentReader;
        private ITokenReaderFactory _factory;

        public MultifileTokenReader(ITokenReaderFactory factory)
        {
            _factory = factory;
        }


        public Token ReadToken()
        {
            if (_currentReader is null)
            {
                _currentReader = _factory.GetNextReader();
                if ( _currentReader is null )
                {
                    return new Token(TokenType.EoI);
                }
            }

            Token token = _currentReader.ReadToken();

            if (token.Type == TokenType.EoI)
            {
                Dispose();
                ReadToken();
            }

            return token;
        }


        public void Dispose()
        {
            if ( _currentReader is IDisposable disposable )
            {
                disposable.Dispose();
            }
            _currentReader = null;
        }
    }
}
