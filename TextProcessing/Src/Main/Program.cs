namespace TextProcessing
{
    public class Program
    {
        static void Main(string[] args)
        {
            var IOState = new InputOutputState();

            //IOState for Word Counter, Word Frequency Counter, Paragraph Word Counter
            if (!IOState.InitializeReaderFromCLIArguments(args))
            {
                return;
            }

            //IOState for Table Summator
            //if (!IOState.InitializeReaderWriterColumnNameFromCLIArguments(args))
            //{
            //    return;
            //}

            ITokenReader tokenReader = new TokenReaderByChars(IOState.Reader!);
            ITokenReader tokenParagraphReader = new ParagraphDetectingTokenReaderWrapper(tokenReader); 
            ITokenReader tokenDebugPrintingReader = new DebugPrintingTokenReaderWrapper(tokenParagraphReader);   

            //ITokenProcessor wordCounter = new WordCounter();
            //ITokenProcessor wordFreqCounter = new WordFrequencyCounter();
            ITokenProcessor paragWordCounter = new ParagraphWordCounter();
            //ITokenProcessor tableSummator = new TableSummator(IOState.ColumnName!);

            //Executor.ProcessAllWords(tokenReader, wordCounter, IOState.Writer!, Console.Out);
            //Executor.ProcessAllWosrds(tokenReader, wordFreqCounter, IOState.Writer!, Console.Out);
            Executor.ProcessAllWords(tokenDebugPrintingReader, paragWordCounter, IOState.Writer!, Console.Out);
            //Executor.ProcessAllWords(tokenReader, tableSummator, IOState.Writer!, Console.Out);


            IOState.Dispose();
        }
    }
}
