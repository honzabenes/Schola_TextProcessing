using FileProcessingConsoleAppFramework;

namespace TokenProcessingFramework
{
    public class MultifileTokenReader : ITokenReader, IDisposable
    {
        private ITokenReader? _currentReader;
        private ITokenReaderFactory _tokenReaderFactory;

        public MultifileTokenReader(ITokenReaderFactory tokenReaderFactory)
        {
            _tokenReaderFactory = tokenReaderFactory;
        }


        public Token ReadToken()
        {
            while (true)
            {
                if (_currentReader is null)
                {
                    try
                    {
                        _currentReader = _tokenReaderFactory.GetNextReader();
                    }
                    catch (FileAccessErrorApplicationException)
                    {
                        continue;
                    }
                    catch (InvalidArgumentApplicationException)
                    {
                        continue;
                    }

                    if (_currentReader is null)
                    {
                        return new Token(TokenType.EoI);
                    }
                }

                Token token = _currentReader.ReadToken();

                if (token.Type != TokenType.EoI)
                {
                    return token;
                }

                Dispose();
            }
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
