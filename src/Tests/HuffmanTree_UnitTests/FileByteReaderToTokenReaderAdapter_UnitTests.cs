using ByteProcessingFramework;
using TokenProcessingFramework;

namespace HuffmanTree_UnitTests
{
    public class FileByteReaderToTokenReaderAdapter_UnitTests
    {
        private class FakeByteReader : IByteReader
        {
            private byte[] _data;
            private int _position;

            public FakeByteReader(int dataSize)
            {
                _data = new byte[dataSize];
                
                var random = new Random();
                random.NextBytes(_data);

                _position = 0;
            }

            public FakeByteReader(byte[] data)
            {
                _data = data;
                _position = 0;
            }


            public byte? ReadByte()
            {
                if (_position >= _data.Length)
                {
                    return null;
                }

                return _data[_position++];
            }
        }

        public List<Token> ReadAllTokens(ITokenReader reader)
        {
            var readTokens = new List<Token>();

            Token token;
            while ((token = reader.ReadToken()) is not { Type: TokenType.EoI })
            {
                readTokens.Add(token);
            }
            readTokens.Add(token);

            return readTokens;
        }


        [Fact]
        public void EmptyInput()
        {
            // Arrange
            IByteReader fakeByteReader = new FakeByteReader(0);
            var adapter = new ByteReaderToTokenReaderAdapter(fakeByteReader);

            // Act
            var readTokens = ReadAllTokens(adapter);


            // Assert
            List<Token> expectedTokens = new List<Token>{
                new Token(TokenType.EoI)
            };

            Assert.Equal(expectedTokens, readTokens);
        }


        [Fact]
        public void OneByte()
        {
            // Arrange
            IByteReader fakeByteReader = new FakeByteReader([101]);
            var adapter = new ByteReaderToTokenReaderAdapter(fakeByteReader);

            // Act
            var readTokens = ReadAllTokens(adapter);
            
            // Assert
            List<Token> expectedTokens = new List<Token>{ 
                new Token("101"), 
                new Token(TokenType.EoI) 
            };

            Assert.Equal(expectedTokens, readTokens);
        }


        [Fact]
        public void MultipleBytes()
        {
            // Arrange
            IByteReader fakeByteReader = new FakeByteReader([101, 26, 51]);
            var adapter = new ByteReaderToTokenReaderAdapter(fakeByteReader);

            // Act
            var readTokens = ReadAllTokens(adapter);

            // Assert
            List<Token> expectedTokens = new List<Token>{
                new Token("101"),
                new Token("26"),
                new Token("51"),
                new Token(TokenType.EoI)
            };

            Assert.Equal(expectedTokens, readTokens);
        }
    }
}
