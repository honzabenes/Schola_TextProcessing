using TokenProcessingFramework;

namespace ByteProcessingFramework
{
    public class ByteReaderToTokenReaderAdapter : ITokenReader
    {
        private IByteReader _byteReader;

        public ByteReaderToTokenReaderAdapter(IByteReader byteReader)
        {
            _byteReader = byteReader;
        }

        public Token ReadToken()
        {
            byte? readByte;

            readByte = _byteReader.ReadByte();

            if (readByte == null)
            {
                return new Token(TokenType.EoI);
            }

            return new Token(readByte.ToString()!);
        }
    }
}
