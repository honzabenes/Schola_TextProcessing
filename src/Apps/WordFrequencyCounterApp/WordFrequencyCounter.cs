using TokenProcessingFramework;

namespace WordFrequencyCounterApp
{
    /// <summary>
    /// Processes tokens to count the number of each unique word encountered.
    /// </summary>
    public class WordFrequencyCounter : ITokenProcessor
    {
        public Dictionary<string, int> Words { get; private set; } = new Dictionary<string, int>();

        public void ProcessToken(Token token)
        {
            if (token.Type == TokenType.Word)
            {
                ProcessWordToken(token);
            }
        }


        private void ProcessWordToken(Token token)
        {
            string word = token.Word!;

            _ = Words.TryGetValue(word, out int value);
            Words[word] = value + 1;
        }


        public void WriteOut(TextWriter writer)
        {
            var sortedWords = new SortedDictionary<string, int>(Words);

            foreach (KeyValuePair<string, int> pair in sortedWords)
            {
                string line = $"{pair.Key}: {pair.Value}";
                writer.WriteLine(line);
            }
        }
    }
}
