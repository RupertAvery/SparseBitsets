using System.Linq;
using BenchmarkDotNet.Running;
using Benchmarks.MicroBenchmarks;

namespace Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            //var types = typeof(MicroBenchmark).Assembly.GetTypes().Where(t => !t.IsAbstract && typeof(MicroBenchmark).IsAssignableFrom(t)).ToList();
            var types = new[]
            {
                typeof(MicroBenchmarkCensusIncome), 
                typeof(MicroBenchmarkCensus1881), 
                typeof(MicroBenchmarkWikileaksNoQuotes),
            };

            foreach (var type in types)
            {
                BenchmarkRunner.Run(type);
            }
        }
    }
}
