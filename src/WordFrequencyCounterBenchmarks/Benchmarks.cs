using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;


namespace WordFrequencyCounterBenchmarks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //BenchmarkRunner.Run<IncrementNewWordInDictionary>();
            BenchmarkRunner.Run<IncrementExistingWordInDictionary>();
        }
    }


    public class IncrementNewWordInDictionary
    {
        private IDictionary<string, int> _wordFrequencies = new Dictionary<string, int>();
        private string[] _words;

        [Params(10_000, 100_000)]
        public int NumberOfWords;

        [GlobalSetup]
        public void GlobalSetup()
        {
            _words = new string[NumberOfWords];

            for (int i = 0; i < NumberOfWords; i++)
            {
                _words[i] = "word" + i;
            }
        }

        [IterationSetup]
        public void IterationSetup()
        {
            _wordFrequencies.Clear();
        }


        [Benchmark]
        public void IncrementWordCount_V1()
        {
            foreach (string word in _words)
            {
                try
                {
                    _wordFrequencies[word]++;
                }
                catch (KeyNotFoundException)
                {
                    _wordFrequencies[word] = 1;
                }
            }
        }


        [Benchmark]
        public void IncrementWordCount_V2()
        {
            foreach (string word in _words)
            {
                if (_wordFrequencies.ContainsKey(word))
                {
                    _wordFrequencies[word]++;
                }
                else
                {
                    _wordFrequencies[word] = 1;
                }
            }
        }


        [Benchmark]
        public void IncrementWordCount_V3()
        {
            foreach (string word in _words)
            {
                _ = _wordFrequencies.TryGetValue(word, out int value);     // If not found, value == default(int) == 0
                value++;
                _wordFrequencies[word] = value;
            }
        }
    }


    public class IncrementExistingWordInDictionary
    {
        private IDictionary<string, int> _wordFrequencies = new Dictionary<string, int>();
        private string word = "benchmark";

        [Benchmark]
        public void IncrementWordCount_V1()
        {
            try
            {
                _wordFrequencies[word]++;
            }
            catch (KeyNotFoundException)
            {
                _wordFrequencies[word] = 1;
            }
        }


        [Benchmark]
        public void IncrementWordCount_V2()
        {
            if (_wordFrequencies.ContainsKey(word))
            {
                _wordFrequencies[word]++;
            }
            else
            {
                _wordFrequencies[word] = 1;
            }
        }


        [Benchmark]
        public void IncrementWordCount_V3()
        {
            _ = _wordFrequencies.TryGetValue(word, out int value);     // If not found, value == default(int) == 0
            value++;
            _wordFrequencies[word] = value;
        }
    }
}
