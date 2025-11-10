namespace FileProcessingConsoleAppFramework
{
    /// <summary>
    /// Manages the initialization and cleanup of input and output streams based on command line arguments.
    /// </summary>
    public class InputOutputState(string[] _args) : IDisposable
    {
        public TextReader? Reader {  get; set; }
        public TextWriter? Writer { get; set; }


        public void CheckArgumentsCount(int expectedCount)
        {
            if (_args.Length != expectedCount)
            {
                throw new InvalidArgumentApplicationException();
            }
        }


        public void OpenInputFile(int argument)
        {
            try
            {
                Reader = new StreamReader(_args[argument]);
            }
            catch (IOException)
            {
                throw new FileAccesErrorApplicationException();
            }
            catch (UnauthorizedAccessException)
            {
                throw new FileAccesErrorApplicationException();
            }
            catch (ArgumentException)
            {
                throw new InvalidArgumentApplicationException();
            }
        }


        public void OpenOutputFile(int argument)
        {
            try
            {
                Writer = new StreamWriter(_args[argument]);
            }
            catch (IOException)
            {
                throw new FileAccesErrorApplicationException();
            }
            catch (UnauthorizedAccessException)
            {
                throw new FileAccesErrorApplicationException();
            }
            catch (ArgumentException)
            {
                throw new InvalidArgumentApplicationException();
            }
        }


        public void Dispose()
        {
            Reader?.Dispose();
            Writer?.Dispose();
        }
    }
}
