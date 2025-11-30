using FileProcessingConsoleAppFramework;
using TokenProcessingFramework;

namespace ParagraphWordCounterApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var app = new AppErrorHandler(Console.Out);

            app.Execute(new ParagraphWordCounterProgramCore(), args);
        }
    }


    public class ParagraphWordCounterProgramCore : IProgramCore
    {
        public void Run(string[] args)
        {
            var IOState = new InputOutputState(args);

            try
            {
                IOState.CheckArgumentsCount(1);
                IOState.OpenInputFile(0);

                var byCharsTokenReader = new ByCharsTokenReader(IOState.Reader!);
                var baseReader = new ParagraphDetectingTokenReaderDecorator(byCharsTokenReader);
                var debugReader = new DebugPrintingTokenReaderWrapper(baseReader);

                var paragraphWordCounter = new ParagraphWordCounter();

                TokenProcessing.ProcessTokensUntilEndOfInput(debugReader, paragraphWordCounter);

                paragraphWordCounter.WriteOut(Console.Out);
            }
            finally
            {
                IOState.Dispose();
            }
        }
    }
}
