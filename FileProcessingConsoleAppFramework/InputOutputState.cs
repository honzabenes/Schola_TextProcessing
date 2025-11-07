namespace FileProcessingConsoleAppFramework
{
    /// <summary>
    /// Manages the initialization and cleanup of input and output streams based on command line arguments.
    /// </summary>
    public class InputOutputState(string[] _args) : IDisposable
    {
        public TextReader? Reader {  get; set; }
        public TextWriter? Writer { get; set; }

        public const string FileErrorMessage = "File Error";
        public const string ArgumentErrorMessage = "Argument Error";


        public void CheckArgumentsCount(int expectedCount)
        {
            if (_args.Length != expectedCount)
            {
                throw new InvalidArgumentApplicationException(ArgumentErrorMessage);
            }
        }


        public void OpenInputFile(string filepath)
        {
            try
            {
                Reader = new StreamReader(filepath);
            }
            catch (IOException)
            {
                throw new FileAccesErrorApplicationException(FileErrorMessage);
            }
            catch (UnauthorizedAccessException)
            {
                throw new FileAccesErrorApplicationException(FileErrorMessage);
            }
            catch (ArgumentException)
            {
                throw new InvalidArgumentApplicationException(ArgumentErrorMessage);
            }
        }


        public void OpenOutputFile(string filepath)
        {
            try
            {
                Reader = new StreamReader(filepath);
            }
            catch (IOException)
            {
                throw new FileAccesErrorApplicationException(FileErrorMessage);
            }
            catch (UnauthorizedAccessException)
            {
                throw new FileAccesErrorApplicationException(FileErrorMessage);
            }
            catch (ArgumentException)
            {
                throw new InvalidArgumentApplicationException(ArgumentErrorMessage);
            }
        }


        public void Dispose()
        {
            Reader?.Dispose();
            Writer?.Dispose();
        }
    }
}
