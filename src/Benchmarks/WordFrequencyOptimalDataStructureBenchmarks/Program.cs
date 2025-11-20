using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace WordFrequencyOptimalDataStructureBenchmarks
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<IncrementCountInDataStructure_KeyNotFound>();
            BenchmarkRunner.Run<IncrementCountInDataStructure_KeyFound>();
            BenchmarkRunner.Run<BuildAndSortDictionaryAtTheEnd>();
            BenchmarkRunner.Run<ConsoleOutputBenchmark>();
        }
    }


    public class IncrementCountInDataStructure_KeyNotFound
    {
        private SortedList<string, int> _sortedList = new SortedList<string, int>();
        private SortedDictionary<string, int> _sortedDictionary = new SortedDictionary<string, int>();
        private Dictionary<string, int> _dictionary = new Dictionary<string, int>();

        private string[] _keys;

        private const int _operationsPerIteration = 100_000;

        [GlobalSetup]
        public void GlobalSetup()
        {
            _keys = new string[_operationsPerIteration];

            for (int i = 0; i < _operationsPerIteration; i++)
            {
                _keys[i] = "key" + i;
            }
        }

        [IterationSetup]
        public void IterationSetup()
        {
            _sortedList.Clear();
            _sortedDictionary.Clear();
            _dictionary.Clear();
        }


        [Benchmark(OperationsPerInvoke = _operationsPerIteration)]
        public void IncrementWordCount_SortedList()
        {
            for (int i = 0; i < _operationsPerIteration; i++)
            {
                string key = _keys[i];

                _ = _sortedList.TryGetValue(key, out int value);
                value++;
                _sortedList[key] = value;
            }
        }


        [Benchmark(OperationsPerInvoke = _operationsPerIteration)]
        public void IncrementWordCount_SortedDictionary()
        {
            for (int i = 0; i < _operationsPerIteration; i++)
            {
                string key = _keys[i];

                _ = _sortedDictionary.TryGetValue(key, out int value);
                value++;
                _sortedDictionary[key] = value;
            }
        }


        [Benchmark(OperationsPerInvoke = _operationsPerIteration)]
        public void IncrementWordCount_Dictionary()
        {
            for (int i = 0; i < _operationsPerIteration; i++)
            {
                string key = _keys[i];

                _ = _dictionary.TryGetValue(key, out int value);
                value++;
                _dictionary[key] = value;
            }
        }
    }


    public class IncrementCountInDataStructure_KeyFound
    {
        private SortedList<string, int> _sortedList = new SortedList<string, int>();
        private SortedDictionary<string, int> _sortedDictionary = new SortedDictionary<string, int>();
        private Dictionary<string, int> _dictionary = new Dictionary<string, int>();

        private string[] _keys;

        private const int _operationsPerIteration = 100_000;
        private const int _keysCountInDataStructure = 10_000;

        [GlobalSetup]
        public void GlobalSetup()
        {
            _keys = new string[_keysCountInDataStructure];
        }

        [IterationSetup]
        public void IterationSetup()
        {
            _sortedList.Clear();
            _sortedDictionary.Clear();
            _dictionary.Clear();

            for (int i = 0; i < _keys.Length; i++)
            {
                _keys[i] = "key" + i;

                _sortedList.Add(_keys[i], 1);
                _sortedDictionary.Add(_keys[i], 1);
                _dictionary.Add(_keys[i], 1);
            }
        }


        [Benchmark(OperationsPerInvoke = _operationsPerIteration)]
        public void IncrementWordCount_SortedList()
        {
            for (int i = 0; i < _operationsPerIteration; i++)
            {
                string key = _keys[i % _keysCountInDataStructure];

                _ = _sortedList.TryGetValue(key, out int value);
                value++;
                _sortedList[key] = value;
            }
        }


        [Benchmark(OperationsPerInvoke = _operationsPerIteration)]
        public void IncrementWordCount_SortedDictionary()
        {
            for (int i = 0; i < _operationsPerIteration; i++)
            {
                string key = _keys[i % _keysCountInDataStructure];

                _ = _sortedDictionary.TryGetValue(key, out int value);
                value++;
                _sortedDictionary[key] = value;
            }
        }


        [Benchmark(OperationsPerInvoke = _operationsPerIteration)]
        public void IncrementWordCount_Dictionary()
        {
            for (int i = 0; i < _operationsPerIteration; i++)
            {
                string key = _keys[i % _keysCountInDataStructure];

                _ = _dictionary.TryGetValue(key, out int value);
                value++;
                _dictionary[key] = value;
            }
        }
    }


    public class BuildAndSortDictionaryAtTheEnd
    {
        private string[] _keys;

        private const int _operationsPerIteration = 100_000;
        private const int _keysCountInDictionary = 10_000;

        [IterationSetup]
        public void IterationSetup()
        {
            var random = new Random();

            // Create array of shuffled numbers
            HashSet<int> numbersSet = new HashSet<int>();
            while (numbersSet.Count < _keysCountInDictionary)
            {
                numbersSet.Add(random.Next(0, _keysCountInDictionary));
            }
            int[] numbers = numbersSet.ToArray();


            _keys = new string[_keysCountInDictionary];

            for (int i = 0; i < _keys.Length; i++)
            {
                _keys[i] = "key" + numbers[i];
            }
        }


        [Benchmark(OperationsPerInvoke = _operationsPerIteration)]
        public void Sort_Dictionary()
        {
            var dictionary = new Dictionary<string, int>();

            for (int i = 0; i < _operationsPerIteration; i++)
            {
                string key = _keys[i % _keysCountInDictionary];

                _ = dictionary.TryGetValue(key, out int value);
                value++;
                dictionary[key] = value;
            }

            var sortedDictionary = new SortedDictionary<string, int>(dictionary);
        }
    }


    public class ConsoleOutputBenchmark()
    {
        private SortedList<string, int> _sortedList = new SortedList<string, int>();
        private SortedDictionary<string, int> _sortedDictionary = new SortedDictionary<string, int>();
        private Dictionary<string, int> _dictionary = new Dictionary<string, int>();

        private const int _keysCountInDataStructure = 10_000;

        [GlobalSetup]
        public void GlobalSetup()
        {
            for (int i = 0; i < _keysCountInDataStructure; i++)
            {
                string key = "key" + i;

                _sortedList.Add(key, 1);
                _sortedDictionary.Add(key, 1);
                _dictionary.Add(key, 1);
            }

            Console.SetOut(TextWriter.Null);
        }


        [Benchmark]
        public void PrintSortedList()
        {
            foreach (KeyValuePair<string, int> pair in _sortedList)
            {
                string line = $"{pair.Key}: {pair.Value}";
                Console.WriteLine(line);
            }
        }


        [Benchmark]
        public void PrintSortedDictionary()
        {
            foreach (KeyValuePair<string, int> pair in _sortedDictionary)
            {
                string line = $"{pair.Key}: {pair.Value}";
                Console.WriteLine(line);
            }
        }


        [Benchmark]
        public void PrintDictionary()
        {
            foreach (KeyValuePair<string, int> pair in _dictionary)
            {
                string line = $"{pair.Key}: {pair.Value}";
                Console.WriteLine(line);
            }
        }
    }
}
