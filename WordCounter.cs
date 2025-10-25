namespace TextProcessing
{
    public class WordCounter : ITokenProcessor
    {
        private TextWriter _writer;

        private int _overallCount = 0;
        public int OverallCount
        {
            get { return _overallCount; }
            private set
            {
                if (value >= 0)
                {
                    _overallCount = value;
                }
                else
                {
                    Console.WriteLine("Word count cannot be negative, setting to 0...");
                    _overallCount = 0;
                }
            }
        }

        public WordCounter(TextWriter writer)
        {
            _writer = writer;
        }


        public void ProcessToken(Token token)
        {
            if (token.Type == TypeToken.Word)
            {
                OverallCount++;
            }
        }


        public void WriteOut()
        {
            _writer.WriteLine(OverallCount);
        }
    }
}
