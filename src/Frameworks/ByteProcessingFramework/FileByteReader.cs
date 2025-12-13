namespace ByteProcessingFramework
{
    public class FileByteReader : IByteReader, IDisposable
    {
        private FileStream _fileStream;

        public FileByteReader(FileStream fileStream)
        {
            _fileStream = fileStream;
        }


        public byte? ReadByte()
        {
            int byteToReturn = _fileStream.ReadByte();

            if (byteToReturn == -1)
            {
                return null;
            }

            return (byte)byteToReturn;
        }


        public void Dispose()
        {
            _fileStream.Dispose();
        }
    }
}
