using System.IO;
using System.Linq;
using BenchmarkDotNet.Attributes;
using Collections.Special;
using SparseBitsets;

namespace Benchmarks.MicroBenchmarks
{
    public abstract class MicroBenchmark
    {
        private readonly SparseBitset[] m_Bitsets;
        private readonly RoaringBitmap[] r_Bitsets;

        protected MicroBenchmark(string fileName)
        {
            var m_Path = @"Data";

            using (var provider = new ZipRealDataProvider(Path.Combine(m_Path, fileName)))
            {
                m_Bitsets = provider.ToArray();
            }
            using (var provider = new ZipRealDataProvider2(Path.Combine(m_Path, fileName)))
            {
                r_Bitsets = provider.ToArray();
            }
        }

        [Benchmark]
        public long Or()
        {
            var total = 0L;
            for (var k = 0; k < m_Bitsets.Length - 1; k++)
            {
                total += (m_Bitsets[k] | m_Bitsets[k + 1]).GetPopCount();
            }
            return total;
        }

        [Benchmark]
        public long RoaringOr()
        {
            var total = 0L;
            for (var k = 0; k < r_Bitsets.Length - 1; k++)
            {
                total += (r_Bitsets[k] | r_Bitsets[k + 1]).Cardinality;
            }
            return total;
        }

        //[Benchmark]
        //public long Xor()
        //{
        //    var total = 0L;
        //    for (var k = 0; k < m_Bitsets.Length - 1; k++)
        //    {
        //        total += (m_Bitsets[k] ^ m_Bitsets[k + 1]).GetPopCount();
        //    }
        //    return total;
        //}

        [Benchmark]
        public long And()
        {
            var total = 0L;
            for (var k = 0; k < m_Bitsets.Length - 1; k++)
            {
                total += (m_Bitsets[k] & m_Bitsets[k + 1]).GetPopCount();
            }
            return total;
        }

        [Benchmark]
        public long RoaringAnd()
        {
            var total = 0L;
            for (var k = 0; k < r_Bitsets.Length - 1; k++)
            {
                total += (r_Bitsets[k] & r_Bitsets[k + 1]).Cardinality;
            }
            return total;
        }

        //[Benchmark]
        //public long AndNot()
        //{
        //    var total = 0L;
        //    for (var k = 0; k < m_Bitsets.Length - 1; k++)
        //    {
        //        total += m_Bitsets[k].AndNot(m_Bitsets[k + 1]).GetPopCount();
        //    }
        //    return total;
        //}


        [Benchmark]
        public long Iterate()
        {
            var total = 0L;
            foreach (var bitset in m_Bitsets)
            {
                foreach (var @int in bitset.GetValues())
                {
                    unchecked
                    {
                        total += @int;
                    }
                }
            }
            return total;
        }
    }

    public abstract class RoaringMicroBenchmark
    {
        private readonly RoaringBitmap[] r_Bitsets;

        protected RoaringMicroBenchmark(string fileName)
        {
            var m_Path = @"Data";

            using (var provider = new ZipRealDataProvider2(Path.Combine(m_Path, fileName)))
            {
                r_Bitsets = provider.ToArray();
            }
        }

        [Benchmark]
        public long RoaringOr()
        {
            var total = 0L;
            for (var k = 0; k < r_Bitsets.Length - 1; k++)
            {
                total += (r_Bitsets[k] | r_Bitsets[k + 1]).Cardinality;
            }
            return total;
        }

        [Benchmark]
        public long RoaringAnd()
        {
            var total = 0L;
            for (var k = 0; k < r_Bitsets.Length - 1; k++)
            {
                total += (r_Bitsets[k] & r_Bitsets[k + 1]).Cardinality;
            }
            return total;
        }

        //[Benchmark]
        //public long AndNot()
        //{
        //    var total = 0L;
        //    for (var k = 0; k < m_Bitsets.Length - 1; k++)
        //    {
        //        total += m_Bitsets[k].AndNot(m_Bitsets[k + 1]).GetPopCount();
        //    }
        //    return total;
        //}


        //[Benchmark]
        //public long Iterate()
        //{
        //    var total = 0L;
        //    foreach (var bitset in r_Bitsets)
        //    {
        //        foreach (var @int in bitset.GetValues())
        //        {
        //            unchecked
        //            {
        //                total += @int;
        //            }
        //        }
        //    }
        //    return total;
        //}
    }
}