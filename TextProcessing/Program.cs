namespace TextProcessing
{
    public class Program
    {
        static void Main(string[] args)
        {
            // IOState for Word Counter, Word Frequency Counter, Paragraph Word Counter
            var IOState = new InputOutputState();
            if (!IOState.InitializeReaderFromCLIArguments(args))
            {
                return;
            }

            // IOState for Table Summator
            //if (!IOState.InitializeReaderWriterColumnNameFromCLIArguments(args))
            //{
            //    return;
            //}

            var tokenReader = new TokenReaderByChars(IOState.Reader!, IOState.WhiteSpaces);

            //ITokenProcessor wordCounter = new WordCounter(IOState.Writer!);
            //ITokenProcessor wordFreqCounter = new WordFrequencyCounter(IOState.Writer!);
            //ITokenProcessor paragWordCounter = new ParagraphWordCounter(IOState.Writer!);
            ITokenProcessor tableSummator = new TableSummator(IOState.Writer!, IOState.ColumnName!);

            Executor.ProcessAllWords(tokenReader, tableSummator, Console.Out);


            IOState.Dispose();
        }
    }
}
