using FileProcessingConsoleAppFramework;
using TokenProcessingFramework;

namespace WordFrequencyCounterApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var app = new AppErrorHandler(Console.Out);

            app.Execute(new WordFrequencyCounterProgramCore(), args);
        }
    }

    public class WordFrequencyCounterProgramCore : IProgramCore
    {
        public void Run(string[] args)
        {
            var IOState = new InputOutputState(args);

            try
            {
                IOState.CheckArgumentsCount(1);
                IOState.OpenInputFile(0);

                var tokenReader = new ByCharsTokenReader(IOState.Reader!);

                var wordFrequencyCounter = new WordFrequencyCounter();

                TokenProcessing.ProcessTokensUntilEndOfInput(tokenReader, wordFrequencyCounter);

                wordFrequencyCounter.WriteOut(Console.Out);

            }
            finally
            {
                IOState.Dispose();
            }
        }
    }
}
