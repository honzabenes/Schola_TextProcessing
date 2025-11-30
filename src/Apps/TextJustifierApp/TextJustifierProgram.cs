using FileProcessingConsoleAppFramework;
using TokenProcessingFramework;

namespace TextJustifierApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var app = new AppErrorHandler(Console.Out);

            app.Execute(new TextJustifierProgramCore(), args);
        }


        public class TextJustifierProgramCore : IProgramCore
        {
            public void Run(string[] args)
            {
                var IOState = new InputOutputState(args);

                try
                {
                    IOState.CheckArgumentsCount(3);
                    IOState.OpenInputFile(0);
                    IOState.OpenOutputFile(1);
                    int maxLineWidth = IOState.ParsePositiveIntFromArgument(2);

                    var byCharsTokenReader = new ByCharsTokenReader(IOState.Reader!);
                    var paragraphDecorator = new ParagraphDetectingTokenReaderDecorator(byCharsTokenReader);
                    var EoLJustifierDecorator = new EoLTokenJustifierTokenReaderDecorator(paragraphDecorator, maxLineWidth);
                    var baseReader = new SpaceAddingTokenReaderDecorator(EoLJustifierDecorator, maxLineWidth);

                    var tokenPrinter = new TokenPrinter(baseReader, IOState.Writer!);

                    tokenPrinter.PrintAllTokens();

                }
                finally
                {
                    IOState.Dispose();
                }
            }
        }
    }
}
