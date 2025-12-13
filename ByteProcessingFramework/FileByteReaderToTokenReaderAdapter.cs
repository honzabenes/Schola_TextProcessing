using TokenProcessingFramework;

namespace ByteProcessingFramework
{
    public class FileByteReaderToTokenReaderAdapter : ITokenReader
    {
        private IByteReader _fileByteReader;

        public FileByteReaderToTokenReaderAdapter(IByteReader fileByteReader)
        {
            _fileByteReader = fileByteReader;
        }

        public Token ReadToken()
        {
            byte? readByte;

            readByte = _fileByteReader.ReadByte();

            if (readByte == null)
            {
                return new Token(TokenType.EoI);
            }

            return new Token(readByte.ToString()!);
        }
    }
}
