namespace TextProcessing
{
    /// <summary>
    /// Processes tokens to count the total number of words encountered.
    /// </summary>
    public class WordCounter : ITokenProcessor
    {
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


        public void ProcessToken(Token token)
        {
            if (token.Type == TypeToken.Word)
            {
                OverallCount++;
            }
        }


        public void WriteOut(TextWriter writer)
        {
            writer.WriteLine(OverallCount);
        }
    }
}
