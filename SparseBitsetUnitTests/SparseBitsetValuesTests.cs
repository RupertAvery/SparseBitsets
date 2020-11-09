using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SparseBitsets;

namespace SparseBitsetUnitTests
{
    [TestFixture]
    public class SparseBitsetValuesTests
    {
        private SparseBitset _overallBitset;

        [SetUp]
        public void Setup()
        {
            var bitValues = Enumerable.Range(0, 1000);
            _overallBitset = new SparseBitset();

            foreach (var bitValue in bitValues)
            {
                _overallBitset.Add((ulong)bitValue);
            }

            _overallBitset.Pack();
        }

        [Test]
        public void OverallBitsetGetPopCount_ShouldBe1000_True()
        {
            Assert.IsTrue(_overallBitset.GetPopCount() == 1000);
        }

        [TestCase(ExpectedResult = 128)]
        public long Crossing64BitBoundaries()
        {
            return Enumerable.Range(1152 * 64 + 5, 128).Select(x => (ulong)x).ToOptimizedBitset().GetPopCount();
        }



        [TestCase(new ulong[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, ExpectedResult = 10)]
        [TestCase(new ulong[] { 1, 2, 3, 4, 5 }, ExpectedResult = 5)]
        [TestCase(new ulong[] { 1, 2 }, ExpectedResult = 2)]
        public long OverallAndGetPopCount_ShouldBe_True(IEnumerable<ulong> valuesToCompare)
        {
            var breakdown = _overallBitset.And(valuesToCompare.ToOptimizedBitset());
            return breakdown.GetPopCount();
        }


        [TestCase(new ulong[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, ExpectedResult = 990)]
        [TestCase(new ulong[] { 1, 2, 3, 4, 5 }, ExpectedResult = 995)]
        [TestCase(new ulong[] { 1, 2 }, ExpectedResult = 998)]
        public long OverallAndNotGetPopCount_ShouldBe_True(IEnumerable<ulong> valuesToCompare)
        {
            var breakdown = _overallBitset.AndNot(valuesToCompare.ToOptimizedBitset(), _overallBitset);
            return breakdown.GetPopCount();
        }


        [TestCase(new ulong[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, ExpectedResult = 990)]
        [TestCase(new ulong[] { 1, 2, 3, 4, 5 }, ExpectedResult = 995)]
        [TestCase(new ulong[] { 1, 2 }, ExpectedResult = 998)]
        public long OverallNotGetPopCount_ShouldBe_True(IEnumerable<ulong> valuesToCompare)
        {
            var breakdown = valuesToCompare.ToOptimizedBitset().Not(_overallBitset);
            return breakdown.GetPopCount();
        }

        [TestCase(new ulong[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, ExpectedResult = 1000)]
        [TestCase(new ulong[] { 1, 2, 3, 4, 5 }, ExpectedResult = 1000)]
        [TestCase(new ulong[] { 1, 2 }, ExpectedResult = 1000)]
        public long OverallOrGetPopCount_ShouldBe_True(IEnumerable<ulong> valuesToCompare)
        {
            var breakdown = _overallBitset.Or(valuesToCompare.ToOptimizedBitset());
            return breakdown.GetPopCount();
        }
        
        [TestCase(new ulong[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, new ulong[] { 1, 2, 3, 4, 5, 6, 7 }, ExpectedResult = new ulong[] { 1, 2, 3, 4, 5, 6, 7 })]
        [TestCase(new ulong[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, new ulong[] { 5, 6, 7, 8, 9, 10 }, ExpectedResult = new ulong[] { 5, 6, 7, 8, 9, 10 })]
        [TestCase(new ulong[] { 5, 6, 7, 8, 9, 10, 11, 12 }, new ulong[] { 1, 2, 3, 4, 5, 6, 7, 8 }, ExpectedResult = new ulong[] { 5, 6, 7, 8 })]
        [TestCase(new ulong[] { 1, 2, 3, 4, 5, 6, 7, 8 }, new ulong[] { 5, 6, 7, 8, 9, 10, 11, 12 }, ExpectedResult = new ulong[] { 5, 6, 7, 8 })]
        [TestCase(new ulong[] { 5, 6, 7, 8, 9, 10 }, new ulong[] { 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 }, ExpectedResult = new ulong[] { })]
        [TestCase(new ulong[] { 1, 2, 3, 4, 5, 6, 7, 8 }, new ulong[] { 1, 2, 3, 4, 5, 6, 7, 8 }, ExpectedResult = new ulong[] { 1, 2, 3, 4, 5, 6, 7, 8 })]
        [TestCase(new ulong[] { 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, }, new ulong[] { 1, 2, 3, 4, 5, 6, 7, 8, 15, 16, 17, 18 }, ExpectedResult = new ulong[] { 5, 6, 7, 8, 15, 16, 17, 18 })]
        [TestCase(new ulong[] { 60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71 }, new ulong[] { 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67 }, ExpectedResult = new ulong[] { 60, 61, 62, 63, 64, 65, 66, 67 })]
        [TestCase(new ulong[] { }, new ulong[] { 1, 2, 3, 4, 5, 6, 7, 8 }, ExpectedResult = new ulong[] { })]
        [TestCase(new ulong[] { 1, 2, 3, 4, 5, 6, 7, 8 }, new ulong[] { }, ExpectedResult = new ulong[] { })]
        public long[] AndGetPopCount_ShouldBe_True(IEnumerable<ulong> leftPopulation, IEnumerable<ulong> rightPopulation)
        {
            var leftBitset = leftPopulation.ToOptimizedBitset();
            var rightBitset = rightPopulation.ToOptimizedBitset();

            return leftBitset.And(rightBitset).GetValues().ToArray();
        }

        [TestCase(new ulong[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, new ulong[] { 1, 2, 3, 4, 5 }, ExpectedResult = new ulong[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 })]
        [TestCase(new ulong[] { 4, 5, 6, 7, 8 }, new ulong[] { 6, 7, 8, 9, 10 }, ExpectedResult = new ulong[] { 4, 5, 6, 7, 8, 9, 10 }, Description = "Overlap with lower Left")]
        [TestCase(new ulong[] { 6, 7, 8, 9, 10 }, new ulong[] { 4, 5, 6, 7, 8 }, ExpectedResult = new ulong[] { 4, 5, 6, 7, 8, 9, 10 }, Description = "Overlap with lower Right")]
        [TestCase(new ulong[] { 6, 7, 8, 9, 10 }, new ulong[] { 1, 2, 3, 4, 5 }, ExpectedResult = new ulong[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, Description = "No Overlap")]
        [TestCase(new ulong[] { 1, 2, 3, 4, 5 }, new ulong[] { 1, 2, 3, 4, 5 }, ExpectedResult = new ulong[] { 1, 2, 3, 4, 5 }, Description = "Full overlap")]
        public long[] OrGetPopCount_ShouldBe_True(IEnumerable<ulong> leftPopulation, IEnumerable<ulong> rightPopulation)
        {
            var leftBitset = leftPopulation.ToOptimizedBitset();
            var rightBitset = rightPopulation.ToOptimizedBitset();

            return leftBitset.Or(rightBitset).GetValues().ToArray();
        }

        [TestCase(new ulong[] { 6, 7, 8, 9, 10, 11 }, ExpectedResult = new ulong[] { 6, 7, 8, 9, 10, 11 })]
        [TestCase(new ulong[] { 62, 63, 64, 65, 66, 67, 68, 69, 70, 71 }, ExpectedResult = new ulong[] { 62, 63, 64, 65, 66, 67, 68, 69, 70, 71 }, Description = "Crosses 64-bit boundary")]
        public long[] GetValues_ShouldBe_True(IEnumerable<ulong> leftPopulation)
        {
            var leftBitset = leftPopulation.ToOptimizedBitset();
            return leftBitset.GetValues().ToArray();
        }

        [TestCase(new ulong[] { 6, 7, 8, 9, 10, 11 }, new ulong[] { 6, 7, 8, 9, 10, 11 }, ExpectedResult = new ulong[] { })]
        [TestCase(new ulong[] { 6, 7, 8, 9, 10 }, new ulong[] { 6, 7, 8, 9, 10, 11 }, ExpectedResult = new ulong[] { 11 })]
        [TestCase(new ulong[] { 1 }, new ulong[] { 1, 2, 3, 4, 6, 7, 8, 9, 10 }, ExpectedResult = new ulong[] { 2, 3, 4, 6, 7, 8, 9, 10 })]
        [TestCase(new ulong[] { }, new ulong[] { 1, 2, 3, 4, 6, 7, 8, 9, 10 }, ExpectedResult = new ulong[] { 1, 2, 3, 4, 6, 7, 8, 9, 10 })]
        [TestCase(new ulong[] { 1, 2, 3, 4, 6, 7, 8, 9, 10 }, new ulong[] { 1, 2, 3, 4, 6, 7, 8, 9, 10 }, ExpectedResult = new ulong[] { })]
        public long[] Not_ShouldBe_True(IEnumerable<ulong> leftPopulation, IEnumerable<ulong> fullPopulation)
        {
            var leftBitset = leftPopulation.ToOptimizedBitset();
            var fullBitset = fullPopulation.ToOptimizedBitset();

            return leftBitset.Not(fullBitset).GetValues().ToArray();
        }

        [TestCase(new ulong[] { 6, 7, 8, 9, 10, 11 }, new ulong[] { 1, 2, 3, 4, 5, 11 }, ExpectedResult = 5)]
        [TestCase(new ulong[] { 6, 7, 8, 9, 10 }, new ulong[] { 6, 7, 8, 9, 10 }, ExpectedResult = 0)]
        public long AndNotGetPopCount_ShouldBe_True(IEnumerable<ulong> leftPopulation, IEnumerable<ulong> rightPopulation)
        {
            var leftBitset = leftPopulation.ToOptimizedBitset();
            var rightBitset = rightPopulation.ToOptimizedBitset();
            var fullBitset = leftBitset.Or(rightBitset);

            return leftBitset.AndNot(rightBitset, fullBitset).GetPopCount();
        }

        [TestCase(new ulong[] { 1, 2, 3, 4, 5 }, new ulong[] { 2, 5 }, new ulong[] { 6, 7, 8 }, ExpectedResult = 5)]
        [TestCase(new ulong[] { 1, 2, 3, 4, 5 }, new ulong[] { 10, 11, 12 }, new ulong[] { 6, 7, 8 }, ExpectedResult = 3)]
        public long AndOrGetPopCount_ShouldBe_True(IEnumerable<ulong> andPopulation1, IEnumerable<ulong> andPopulation2, IEnumerable<ulong> orPopulation)
        {
            var bitSet = andPopulation1.ToOptimizedBitset()
                .And(andPopulation2.ToOptimizedBitset())
                .Or(orPopulation.ToOptimizedBitset());

            return bitSet.GetPopCount();
        }

        [TestCase(new ulong[] { 1, 2, 3, 4, 5 }, new ulong[] { 2, 5 }, new ulong[] { 6, 7, 8 }, new ulong[] { 2, 5 }, ExpectedResult = 3)]
        public long MixedOperationGetPopCount_ShouldBe_True(IEnumerable<ulong> andPopulation1, IEnumerable<ulong> andPopulation2, IEnumerable<ulong> orPopulation, IEnumerable<ulong> notPopulation)
        {
            var fullBitset = andPopulation1.ToOptimizedBitset().Or(andPopulation2.ToOptimizedBitset()).Or(orPopulation.ToOptimizedBitset())
                .Or(notPopulation.ToOptimizedBitset());

            var bitSet = andPopulation1.ToOptimizedBitset()
                .And(andPopulation2.ToOptimizedBitset())
                .Or(orPopulation.ToOptimizedBitset())
                .AndNot(notPopulation.ToOptimizedBitset(), fullBitset);

            return bitSet.GetPopCount();
        }
    }
}