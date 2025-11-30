using FileProcessingConsoleAppFramework;
using TokenProcessingFramework;

namespace TableSummatorApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var app = new TableSummatorAppErrorHandler(Console.Out);

            app.Execute(new TableSummatorProgramCore(), args);
        }


        public class TableSummatorProgramCore : IProgramCore
        {
            public void Run(string[] args)
            {
                var IOState = new InputOutputState(args);

                try
                {
                    IOState.CheckArgumentsCount(3);
                    IOState.OpenInputFile(0);
                    IOState.OpenOutputFile(1);

                    var byCharsTokenReader = new ByCharsTokenReader(IOState.Reader!);

                    var tableSummator = new TableSummator(args[2]);

                    TokenProcessing.ProcessTokensUntilEndOfInput(byCharsTokenReader, tableSummator);

                    tableSummator.WriteOut(IOState.Writer!);
                }
                finally
                {
                    IOState.Dispose();
                }
            }
        }
    }
}
