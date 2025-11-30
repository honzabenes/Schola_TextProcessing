using FileProcessingConsoleAppFramework;
using TokenProcessingFramework;

namespace WordCounterApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var app = new AppErrorHandler(Console.Out);

            app.Execute(new WordCounterProgramCore(), args);
        }
    }


    public class WordCounterProgramCore : IProgramCore
    {
        public void Run(string[] args)
        {
            var IOState = new InputOutputState(args);

            try
            {
                IOState.CheckArgumentsCount(1);
                IOState.OpenInputFile(0);

                var tokenReader = new ByCharsTokenReader(IOState.Reader!);

                var wordCounter = new WordCounter();

                TokenProcessing.ProcessTokensUntilEndOfInput(tokenReader, wordCounter);

                wordCounter.WriteOut(Console.Out);
            }
            finally
            {
                IOState.Dispose();
            }
        }
    }
}
