namespace TextProcessing
{
    public class Program
    {
        static void Main(string[] args)
        {
            var IOState = new InputOutputState();

            //IOState for Word Counter, Word Frequency Counter, Paragraph Word Counter
            //if (!IOState.InitializeReaderFromCLIArguments(args))
            //{
            //    return;
            //}

            //IOState for Table Summator
            if (!IOState.InitializeReaderWriterColumnNameFromCLIArguments(args))
            {
                return;
            }

            var tokenReader = new TokenReaderByChars(IOState.Reader!);

            //ITokenProcessor wordCounter = new WordCounter();
            //ITokenProcessor wordFreqCounter = new WordFrequencyCounter();
            //ITokenProcessor paragWordCounter = new ParagraphWordCounter();
            ITokenProcessor tableSummator = new TableSummator(IOState.ColumnName!);

            //Executor.ProcessAllWords(tokenReader, wordCounter, IOState.Writer!, Console.Out);
            //Executor.ProcessAllWords(tokenReader, paragWordFreqCounter, IOState.Writer!, Console.Out);
            //Executor.ProcessAllWords(tokenReader, paragWordCounter, IOState.Writer!, Console.Out);
            Executor.ProcessAllWords(tokenReader, tableSummator, IOState.Writer!, Console.Out);


            IOState.Dispose();
        }
    }
}
