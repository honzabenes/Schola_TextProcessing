namespace TextProcessing
{
    // Tato třída je inspirována kódem Mgr. Pavla Ježka, Ph.D.
    public class InputOutputState : IDisposable
    {
        public TextReader? Reader {  get; set; }
        public TextWriter? Writer { get; set; }
        public String? ColumnName { get; private set; }


        public const string FileErrorMessage = "File Error";
        public const string ArgumentErrorMessage = "Argument Error";


        public bool InitializeReaderFromCLIArguments(string[] args)
        {
            const int ARGS_COUNT = 1;

            // Check arguments
            if (args.Length != ARGS_COUNT)
            {
                Console.WriteLine(ArgumentErrorMessage);
                return false;
            }

            // Initialize Reader
            string inputFilePath = args[0];
            try
            {
                Reader = new StreamReader(inputFilePath);
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

            Writer = Console.Out;

            return true;
        }


        public bool InitializeReaderWriterColumnNameFromCLIArguments(string[] args)
        {
            const int ARGS_COUNT = 3;

            // Check arguments
            if (args.Length != ARGS_COUNT)
            {
                Console.WriteLine(ArgumentErrorMessage);
                return false;
            }

            // Initialize Reader
            string inputFilePath = args[0];
            try
            {
                Reader = new StreamReader(inputFilePath);
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


            // Initialize Writer
            string outputFilePath = args[1];
            try
            {
                Writer = new StreamWriter(outputFilePath);
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

            //Writer = Console.Out;


            // initialize ColumnName
            ColumnName = args[2];

            return true;
        }


        public void Dispose()
        {
            Reader?.Dispose();
            Writer?.Dispose();
        }
    }
}
