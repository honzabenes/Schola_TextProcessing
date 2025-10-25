namespace TextProcessing
{
    public class InvalidTableFormatException : Exception
    {
        public InvalidTableFormatException() { }

        public InvalidTableFormatException(string message)
        : base(message) { }
    }
}
