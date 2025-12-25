using ExcelFramework;
using FileProcessingConsoleAppFramework;
using TokenProcessingFramework;

namespace ExcelApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var app = new AppErrorHandler(Console.Out);

            app.Execute(new Excel0ProgramCore(), args);
        }
    }


    public class Excel0ProgramCore : IProgramCore
    {
        public void Run(string[] args)
        {
            var IOState = new InputOutputState(args);

            try
            {
                IOState.CheckArgumentsCount(2);
                IOState.OpenInputFile(0);
                IOState.OpenOutputFile(1);

                var tokenReader = new ByCharsTokenReader(IOState.Reader!);

                var sheet = new Sheet();
                var sheetParser = new SheetParser(sheet);
                var sheetRenderer = new SheetRenderer(IOState.Writer!);

                TokenProcessing.ProcessTokensUntilEndOfInput(tokenReader, sheetParser);

                sheet.CalculateAll();

                sheetRenderer.Render(sheet);
            }
            finally
            {
                IOState.Dispose();
            }
        }
    }
}
