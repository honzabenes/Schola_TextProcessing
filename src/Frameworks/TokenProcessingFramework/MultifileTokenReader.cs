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
            Token? tokenToReturn = null;

            while (tokenToReturn is null)
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

                Token tempToken = _currentReader!.ReadToken();

                if (tempToken.Type == TokenType.EoI)
                {
                    Dispose();
                }
                else
                {
                    tokenToReturn = tempToken;
                }
            }

            return (Token)tokenToReturn;
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
