namespace TextProcessing
{
    public class WordFrequencyCounter : ITokenProcessor
    {
        public SortedDictionary<string, int> Words { get; private set; } = new SortedDictionary<string, int>();

        public void ProcessToken(Token token)
        {
            if (token.Type == TypeToken.Word)
            {
                ProcessWordToken(token);
            }
        }


        private void ProcessWordToken(Token token)
        {
            string word = token.Word!;

            if (Words.ContainsKey(word))
            {
                Words[word]++;
            }
            else
            {
                Words.Add(word, 1);
            }
        }


        public void WriteOut(TextWriter writer)
        {
            foreach (KeyValuePair<string, int> pair in Words)
            {
                string line = $"{pair.Key}: {pair.Value}";
                writer.WriteLine(line);
            }
        }
    }
}
