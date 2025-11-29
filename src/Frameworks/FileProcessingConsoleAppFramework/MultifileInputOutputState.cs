namespace FileProcessingConsoleAppFramework
{
    public class MultifileInputOutputState(string[] _args)
    {
        public List<string> InputFilepaths = new List<string>();


        public void CheckArgumentsCount(int expectedCount)
        {
            if (_args.Length < expectedCount)
            {
                throw new InvalidArgumentApplicationException();
            }
        }
    }
}
