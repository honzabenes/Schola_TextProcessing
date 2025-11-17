using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

// Při psaní benchmarků pro případ, že se slovo, u kterého chceme zvyšovat počet výskytů, ještě ve slovníku nenachází,
// jsem narazil na problém, jak zaručit, aby se v každé operaci při běhu benchmarku přidávané slovo opravdu
// ve slovníku nenacházelo. Ńejprve jsem sám problém vyřešil tím, že jsem v každé ze tří funkcí nejprve slovník
// vyprázdnil. To mi ale přijde jako špatné řešení, pak by se do času jednoho běhu přičítalo i vypradňování slovníku.
// Pak jsem našel v dokumentaci BenchmarkDotNet, že lze pomocí Atributu IterationSetup vyprázdnit slovník
// před každou iterací. Jenže to, jak jsem zjistil donutí benchmark, aby v každé iteraci prováděl jen jednu operaci.
// Z toho, si myslím, by nevycházely dobré výsledky vzhledem k malému počtu spouštění funkce. Poté jsem se AI zeptal,
// jak by to jinak šlo vyřešit a ta mi nabídla způsob, podle kterého jsem nakonec benchmarky implementoval.
// https://aistudio.google.com/app/prompts?state=%7B%22ids%22:%5B%221QEquw94qHFVfXt3HABZWROdfSx-TH5sh%22%5D,%22action%22:%22open%22,%22userId%22:%22111991003136347724951%22,%22resourceKeys%22:%7B%7D%7D&usp=sharing

namespace DictionaryBenchmarks
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            //BenchmarkRunner.Run<IncrementValueInDictionary_KeyNotFound>();
            BenchmarkRunner.Run<IncrementValueInDictionary_KeyFound>();
        }
    }


    public class IncrementValueInDictionary_KeyNotFound
    {
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
            _dictionary.Clear();
        }


        [Benchmark(OperationsPerInvoke = _operationsPerIteration)]
        public void IncrementWordCount_V1()
        {
            for (int i = 0; i < _operationsPerIteration; i++)
            {
                string key = _keys[i];

                try
                {
                    _dictionary[key]++;
                }
                catch (KeyNotFoundException)
                {
                    _dictionary[key] = 1;
                }
            }
        }


        [Benchmark(OperationsPerInvoke = _operationsPerIteration)]
        public void IncrementWordCount_V2()
        {
            for (int i = 0; i < _operationsPerIteration; i++)
            {
                string key = _keys[i];

                if (_dictionary.ContainsKey(key))
                {
                    _dictionary[key]++;
                }
                else
                {
                    _dictionary[key] = 1;
                }
            }
        }


        [Benchmark(OperationsPerInvoke = _operationsPerIteration)]
        public void IncrementWordCount_V3()
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


    public class IncrementValueInDictionary_KeyFound
    {
        private Dictionary<string, int> _dictionary= new Dictionary<string, int>();

        private string[] _keys;

        private const int _operationsPerIteration = 100_000;
        private const int _keysCountInDictionary = 10_000;

        [GlobalSetup]
        public void GlobalSetup()
        {
            _keys = new string[_keysCountInDictionary];

            for (int i = 0; i < _keys.Length; i++)
            {
                _keys[i] = "key" + i;

                _dictionary.Add(_keys[i], 1);
            }
        }


        [Benchmark(OperationsPerInvoke = _operationsPerIteration)]
        public void IncrementWordCount_V1()
        {
            for (int i = 0; i < _operationsPerIteration; i++)
            {
                string key = _keys[i % _keysCountInDictionary];

                try
                {
                    _dictionary[key]++;
                }
                catch (KeyNotFoundException)
                {
                    _dictionary[key] = 1;
                }
            }
        }


        [Benchmark(OperationsPerInvoke = _operationsPerIteration)]
        public void IncrementWordCount_V2()
        {
            for (int i = 0; i < _operationsPerIteration; i++)
            {
                string key = _keys[i % _keysCountInDictionary];

                if (_dictionary.ContainsKey(key))
                {
                    _dictionary[key]++;
                }
                else
                {
                    _dictionary[key] = 1;
                }
            }
        }


        [Benchmark(OperationsPerInvoke = _operationsPerIteration)]
        public void IncrementWordCount_V3()
        {
            for (int i = 0; i < _operationsPerIteration; i++)
            {
                string key = _keys[i % _keysCountInDictionary];

                _ = _dictionary.TryGetValue(key, out int value);
                value++;
                _dictionary[key] = value;
            }
        }
    }
}
