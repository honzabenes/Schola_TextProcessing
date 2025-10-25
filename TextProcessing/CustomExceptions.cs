namespace TextProcessing
{
    public class InvalidInputFormatException : Exception
    {
        public InvalidInputFormatException() { }

        public InvalidInputFormatException(string message)
            : base(message) { }
    }

    public class NotParsableByIntException : Exception
    {
        public NotParsableByIntException() { }

        public NotParsableByIntException(string message)
            : base(message) { }
    }
    public class NonExistenColumnNameInTableException : Exception
    {
        public NonExistenColumnNameInTableException() { }

        public NonExistenColumnNameInTableException(string message)
            : base(message) { }
    }
}
