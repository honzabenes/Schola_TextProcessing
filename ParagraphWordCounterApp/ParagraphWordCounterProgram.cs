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

            IOState.CheckArgumentsCount(1);
            IOState.OpenInputFile(0);

            var byCharsTokenReader = new ByCharsTokenReader(IOState.Reader!);
            var baseReader = new ParagraphDetectingTokenReaderDecorator(byCharsTokenReader);

            var paragraphWordCounter = new ParagraphWordCounter();

            TokenProcessing.ProcessTokensUntilEndOfInput(baseReader, paragraphWordCounter);

            paragraphWordCounter.WriteOut(Console.Out);

            IOState.Dispose();
        }
    }
}
