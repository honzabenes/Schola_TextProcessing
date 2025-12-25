using ExcelFramework;
using TokenProcessingFramework;

namespace Excel_UnitTests
{
    public class ExcelTableParser_UnitTests
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


        private void ProcessTokensUntilEndOfInput(ITokenReader reader, ITokenProcessor processor)
        {
            List<Token> outputTokens = new List<Token>();

            Token token;

            while ((token = reader.ReadToken()) is not { Type: TokenType.EoI })
            {
                processor.ProcessToken(token);
            }
        }


        [Fact]
        public void EmptyCell()
        {
            // Arrange
            Token[] tokensToRead =
            {
                new Token("[]"),
                new Token(TokenType.EoI),
            };

            ITokenReader reader = new FakeTokenReader(tokensToRead);
            ITokenProcessor excelTableParser = new ExcelTableParser();

            // Act
            ProcessTokensUntilEndOfInput(reader, excelTableParser);

            // Assert
            List<Token> tokensExpected =
            [
                new Token(TokenType.EoI),
            ];

            Assert.Equal(tokensExpected, );
        }
    }
}
