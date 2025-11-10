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

                IOState.CheckArgumentsCount(3);
                IOState.OpenInputFile(0);
                IOState.OpenOutputFile(1);

                var byCharsTokenReader = new ByCharsTokenReader(IOState.Reader!);
                var baseReader = new ParagraphDetectingTokenReaderDecorator(byCharsTokenReader);

                var tableSummator = new TableSummator(args[2]);

                TokenProcessing.ProcessTokensUntilEndOfInput(baseReader, tableSummator);

                tableSummator.WriteOut(IOState.Writer!);

                IOState.Dispose();
            }
        }
    }
}
