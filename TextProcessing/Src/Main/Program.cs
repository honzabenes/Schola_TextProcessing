namespace TextProcessing
{
    public class Program
    {
        static void Main(string[] args)
        {
            RunTextJustifier(args);
        }


        // Text Processors

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
            ITokenReader paragraphDetectingTokenReaderWrapper = new ParagraphDetectingTokenReaderWrapper(tokenReader);
            ITokenReader tokenEoLJustifier = new EoLTokenJustifierTokenReaderWrapper(paragraphDetectingTokenReaderWrapper, IOState.MaxTextWidth);
            ITokenReader spaceAddingTokenReaderWrapper = new SpaceAddingTokenReaderWrapper(tokenEoLJustifier, IOState.MaxTextWidth);
            //ITokenReader debugPrintingTokenReaderWrapper = new DebugPrintingTokenReaderWrapper(tokenEoLJustifier);
            ITokenReader debugPrintingTokenReaderWrapper = new DebugPrintingTokenReaderWrapper(spaceAddingTokenReaderWrapper);

            ITokenProcessor textPrinter = new TextPrinter(IOState.Writer!);

            Executor.ProcessAllWords(debugPrintingTokenReaderWrapper, textPrinter, Console.Out, Console.Out);

            IOState.Dispose();
        }
    }
}
