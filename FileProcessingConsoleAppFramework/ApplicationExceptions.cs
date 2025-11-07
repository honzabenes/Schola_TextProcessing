namespace FileProcessingConsoleAppFramework
{
    public class InvalidArgumentApplicationException : ApplicationException
    {
        public InvalidArgumentApplicationException(string message) : base(message) { }
    }

        
    public class FileAccesErrorApplicationException : ApplicationException
    {
        public FileAccesErrorApplicationException(string message) : base(message) { }
    }
}
