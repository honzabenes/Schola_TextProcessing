namespace TextProcessing
{
    /// <summary>
    /// Manages the initialization and cleanup of input and output streams based on command line arguments.
    /// </summary>
    public class InputOutputState : IDisposable
    {
        public TextReader? Reader {  get; set; }
        public TextWriter? Writer { get; set; }
        public string? ColumnName { get; private set; }
        public int MaxTextWidth { get; private set; }

        public const string FileErrorMessage = "File Error";
        public const string ArgumentErrorMessage = "Argument Error";


        public bool InitializeReaderFromCLIArguments(string[] args)
        {
            int ARGS_COUNT = 1;

            if (!CheckArgumentsCount(ARGS_COUNT, args))
            {
                return false;
            }

            string inputFilepath = args[0];

            if (!InitializeStreamReader(inputFilepath))
            {
                return false;
            }

            InitializeWriter(Console.Out);

            return true;
        }


        public bool InitializeReaderWriterAndColumnNameFromCLIArguments(string[] args)
        {
            int ARGS_COUNT = 3;

            if (!CheckArgumentsCount(ARGS_COUNT, args))
            {
                return false;
            }

            string inputFilepath = args[0];

            if (!InitializeStreamReader(inputFilepath))
            {
                return false;
            }

            string outputFilepath = args[1];

            if (!InitializeStreamWriter(outputFilepath))
            {
                return false;
            }

            ColumnName = args[2];

            return true;
        }


        public bool InitializeReaderWriterAndMaxTextWidthFromCLIArguments(string[] args)
        {
            int ARGS_COUNT = 3;

            if (!CheckArgumentsCount(ARGS_COUNT, args))
            {
                return false;
            }

            string inputFilepath = args[0];

            if (!InitializeStreamReader(inputFilepath))
            {
                return false;
            }

            string outputFilepath = args[1];

            if (!InitializeStreamWriter(outputFilepath))
            {
                return false;
            }

            try
            {
                MaxTextWidth = int.Parse(args[2]);
            }
            catch (FormatException)
            {
                return false;
            }
            catch (OverflowException)
            {
                return false;
            }

            if (MaxTextWidth <= 0)
            {
                return false;
            }

            return true;
        }


        private bool CheckArgumentsCount(int expectedCount, string[] args)
        {
            if (args.Count() != expectedCount)
            {
                Console.WriteLine(ArgumentErrorMessage);
                return false;
            }

            return true;
        }


        private void InitializeReader(TextReader reader)
        {
            Reader = reader;
        }


        private bool InitializeStreamReader(string filepath)
        {
            try
            {
                Reader = new StreamReader(filepath);
                return true;
            }
            catch (IOException)
            {
                Console.WriteLine(FileErrorMessage);
                return false;
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine(FileErrorMessage);
                return false;
            }
            catch (ArgumentException)
            {
                Console.WriteLine(ArgumentErrorMessage);
                return false;
            }
        }


        private void InitializeWriter(TextWriter writer)
        {
            Writer = writer;
        }


        private bool InitializeStreamWriter(string filepath)
        {
            try
            {
                Writer = new StreamWriter(filepath);
                return true;
            }
            catch (IOException)
            {
                Console.WriteLine(FileErrorMessage);
                return false;
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine(FileErrorMessage);
                return false;
            }
            catch (ArgumentException)
            {
                Console.WriteLine(ArgumentErrorMessage);
                return false;
            }
        }


        public void Dispose()
        {
            Reader?.Dispose();
            Writer?.Dispose();
        }
    }
}
