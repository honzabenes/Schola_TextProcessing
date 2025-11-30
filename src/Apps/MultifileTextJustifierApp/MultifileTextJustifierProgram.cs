using FileProcessingConsoleAppFramework;
using TokenProcessingFramework;

namespace MultifileTextJustifierApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var app = new AppErrorHandler(Console.Out);

            app.Execute(new MultifileTextJustifierProgramCore(), args);
        }


        public class MultifileTextJustifierProgramCore : IProgramCore
        {
            public void Run(string[] args)
            {
                var IOState = new InputOutputState(args);

                try
                {
                    bool isHighlightOptionOn = IOState.IsHighlightOptionOn();

                    int minimumArgsCount = isHighlightOptionOn ? 4 : 3;
                    IOState.EnsureMinimumArgumentCount(minimumArgsCount);

                    int inputFilepathsStartIndex = isHighlightOptionOn ? 1 : 0;
                    int inputFilepathsEndIndex = args.Length - 2; // End index is not included in the range
                    List<string> inputFilepaths = IOState.GetFilepathsFromArguments(inputFilepathsStartIndex, inputFilepathsEndIndex);

                    IOState.OpenOutputFile(args.Length - 2);
                    int maxLineWidth = IOState.ParsePositiveIntFromArgument(args.Length - 1);

                    var byCharsTokenReaderFactory = new ByCharsTokenReaderFactory(inputFilepaths);

                    var multifileTokenReader = new MultifileTokenReader(byCharsTokenReaderFactory);
                    var paragraphDecorator = new ParagraphDetectingTokenReaderDecorator(multifileTokenReader);
                    var EoLJustifierDecorator = new EoLTokenJustifierTokenReaderDecorator(paragraphDecorator, maxLineWidth);
                    var baseReader = new SpaceAddingTokenReaderDecorator(EoLJustifierDecorator, maxLineWidth);

                    var tokenPrinter = new TokenPrinter(baseReader, IOState.Writer!);

                    tokenPrinter.PrintAllTokens(isHighlightOptionOn);
                }
                finally 
                {
                    IOState.Dispose();
                }
            }
        }
    }
}
