using FileProcessingConsoleAppFramework;
using TokenProcessingFramework;
using MultifileTextJustifierApp;

namespace MultifileTextJustification_UnitTests
{
    public class MultifileTextJustification_UnitTests
    {
        private class FakeTokenReader : ITokenReader
        {
            private Token[] _tokens;
            private int _nextToken = 0;

            public FakeTokenReader(Token[] tokens)
            {
                _tokens = tokens;
            }

            public Token ReadToken()
            {
                if (_nextToken >= _tokens.Length)
                {
                    return new Token(TokenType.EoI);
                }
                else
                {
                    return _tokens[_nextToken++];
                }
            }
        }


        private class FakeTokenReaderFactory(params ITokenReader[] readers) : ITokenReaderFactory
        {
            private Queue<ITokenReader> _readers = new Queue<ITokenReader>(readers);

            public ITokenReader? GetNextReader()
            {
                if (_readers.Count > 0)
                {
                    return _readers.Dequeue();
                }

                return null;
            }
        }


        private List<Token> ReadTokensUntilEndOfInput(ITokenReader reader)
        {
            List<Token> readTokens = new List<Token>();

            Token token;

            while ((token = reader.ReadToken()) is not { Type: TokenType.EoI })
            {
                readTokens.Add(token);
            }
            readTokens.Add(token);

            return readTokens;
        }


        [Fact]
        public void OneFileEmptyInput()
        {
            // Arrange
            var reader1 = new FakeTokenReader(
            [ 
                new Token(TokenType.EoI) 
            ]);

            var factory = new FakeTokenReaderFactory(reader1);

            ITokenReader multifileTokenReader = new MultifileTokenReader(factory);

            // Act
            List<Token> readTokens = ReadTokensUntilEndOfInput(multifileTokenReader);

            // Assert
            List<Token> expectedTokens = new List<Token> { new Token(TokenType.EoI) };

            Assert.Equal(expectedTokens, readTokens);
        }
    }
}
