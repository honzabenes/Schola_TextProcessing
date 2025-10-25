using System.Text;

namespace TextProcessing
{
    public class WordFrequencyCounter : ITokenProcessor
    {
        private TextWriter _writer;

        public SortedDictionary<string, int> Words { get; private set; } = new SortedDictionary<string, int>();

        public WordFrequencyCounter(TextWriter writer) 
        {
            _writer = writer;
        }


        public void ProcessToken(Token token)
        {
            if (token.Type == TypeToken.Word)
            {
                string word = token.Word;

                if (Words.ContainsKey(word))
                {
                    Words[word]++;
                }
                else
                {
                    Words.Add(word, 1);
                }
            }
        }


        public void WriteOut()
        {
            foreach (KeyValuePair<string, int> pair in Words)
            {
                string line = $"{pair.Key}: {pair.Value}";
                _writer.WriteLine(line);
            }
        }
    }
}
