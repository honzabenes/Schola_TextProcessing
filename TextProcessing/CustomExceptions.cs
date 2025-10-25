namespace TextProcessing
{
    public class InvalidInputFormatException : Exception
    {
        public InvalidInputFormatException() { }

        public InvalidInputFormatException(string message)
        : base(message) { }
    }
}
