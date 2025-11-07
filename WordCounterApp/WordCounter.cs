using TokenProcessingFramework;

namespace WordCounterApp
{
    /// <summary>
    /// Processes tokens to count the total number of words encountered.
    /// </summary>
    public class WordCounter : ITokenProcessor
    {
        private int _wordlCount = 0;

        public void ProcessToken(Token token)
        {
            if (token.Type == TypeToken.Word)
            {
                _wordlCount++;
            }
        }


        public void WriteOut(TextWriter writer)
        {
            writer.WriteLine(_wordlCount);
        }
    }
}
