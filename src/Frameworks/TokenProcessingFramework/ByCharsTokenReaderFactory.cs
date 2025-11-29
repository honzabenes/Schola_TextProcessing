using FileProcessingConsoleAppFramework;

namespace TokenProcessingFramework
{
    public class ByCharsTokenReaderFactory : ITokenReaderFactory
    {
        private Queue<string> _filepaths;

        public ByCharsTokenReaderFactory(List<string> filepaths)
        {
            _filepaths = new Queue<string>(filepaths);
        }


        public ITokenReader? GetNextReader()
        {
            if (_filepaths.Count > 0)
            {
                try
                {
                    var streamReader = new StreamReader(_filepaths.Dequeue());
                    var byCharsTokenReader = new ByCharsTokenReader(streamReader);

                    return byCharsTokenReader;
                }
                catch (IOException)
                {
                    throw new FileAccessErrorApplicationException();
                }
                catch (UnauthorizedAccessException)
                {
                    throw new FileAccessErrorApplicationException();
                }
                catch (ArgumentException)
                {
                    throw new InvalidArgumentApplicationException();
                }
            }

            return null;
        }
    }
}
