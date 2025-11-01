namespace TextProcessing
{
    public class Program
    {
        static void Main(string[] args)
        {
            RunTextJustifier(args);
        }


        private static void RunWordCounter(string[] args)
        {
            var IOState = new InputOutputState();

            if (!IOState.InitializeReaderFromCLIArguments(args))
            {
                return;
            }

            ITokenReader tokenReader = new TokenReaderByChars(IOState.Reader!);

            ITokenProcessor wordCounter = new WordCounter();

            Executor.ProcessAllWords(tokenReader, wordCounter, IOState.Writer!, Console.Out);

            IOState.Dispose();
        }


        private static void RunWordFrequencyCounter(string[] args)
        {
            var IOState = new InputOutputState();

            if (!IOState.InitializeReaderFromCLIArguments(args))
            {
                return;
            }

            ITokenReader tokenReader = new TokenReaderByChars(IOState.Reader!);

            ITokenProcessor wordFrequencyCounter = new WordFrequencyCounter();

            Executor.ProcessAllWords(tokenReader, wordFrequencyCounter, IOState.Writer!, Console.Out);

            IOState.Dispose();
        }


        private static void RunParagraphWordCounter(string[] args)
        {
            var IOState = new InputOutputState();

            if (!IOState.InitializeReaderWriterAndMaxTextWidthFromCLIArguments(args))
            {
                return;
            }

            ITokenReader tokenReader = new TokenReaderByChars(IOState.Reader!);
            ITokenReader tokenParagraphReader = new ParagraphDetectingTokenReaderWrapper(tokenReader);

            ITokenProcessor paragraphWordCounter = new ParagraphWordCounter();

            Executor.ProcessAllWords(tokenParagraphReader, paragraphWordCounter, IOState.Writer!, Console.Out);

            IOState.Dispose();
        }


        private static void RunTableSummator(string[] args)
        {
            var IOState = new InputOutputState();

            if (!IOState.InitializeReaderWriterAndColumnNameFromCLIArguments(args))
            {
                return;
            }

            ITokenReader tokenReader = new TokenReaderByChars(IOState.Reader!);
            ITokenReader tokenParagraphReader = new ParagraphDetectingTokenReaderWrapper(tokenReader);

            ITokenProcessor tableSummator = new TableSummator(IOState.ColumnName!);

            Executor.ProcessAllWords(tokenParagraphReader, tableSummator, IOState.Writer!, Console.Out);

            IOState.Dispose();
        }

        
        private static void RunTextJustifier(string[] args)
        {
            var IOState = new InputOutputState();

            if (!IOState.InitializeReaderWriterAndMaxTextWidthFromCLIArguments(args))
            {
                return;
            }

            ITokenReader tokenReader = new TokenReaderByChars(IOState.Reader!);
            ITokenReader tokenParagraphReader = new ParagraphDetectingTokenReaderWrapper(tokenReader);
            ITokenReader tokenEoLJustifier = new EoLTokenJustifierTokenReaderWrapper(tokenParagraphReader, IOState.MaxTextWidth);
            ITokenReader tokenDebugPrintingReader = new DebugPrintingTokenReaderWrapper(tokenEoLJustifier);

            ITokenProcessor textPrinter = new TextPrinter(Console.Out);

            Executor.ProcessAllWords(tokenEoLJustifier, textPrinter, Console.Out, Console.Out);

            IOState.Dispose();
        }
    }
}
