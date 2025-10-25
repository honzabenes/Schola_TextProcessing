namespace TextProcessing
{
    public class Program
    {
        static void Main(string[] args)
        {
            var IOState = new InputOutputState();
            if (!IOState.InitializeFromCLIArguments(args))
            {
                return;
            }


            var tokenReader = new TokenReaderByChars(IOState.Reader!, IOState.Separators);

            ITokenProcessor paragraphWordCounter = new WordFrequencyCounter(IOState.Writer!);

            Executor.ProcessAllWords(tokenReader, paragraphWordCounter);


            IOState.Dispose();
        }
    }
}
