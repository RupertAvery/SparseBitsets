using NUnit.Framework;

namespace SparseBitsetUnitTests
{
    [TestFixture]
    public class SparseBitsetValuesAndNotTests
    {
        [TestCase()]
        public void AndNot_With_Empty_Matching_Values()
        {
            var ____left = BitsetHelpers.ToValues(0, "----------------------------");
            var ___right = BitsetHelpers.ToValues(0, "----------------------------");
            var __result = BitsetHelpers.ToValues(0, "----------------------------");
            var ____full = BitsetHelpers.ToValues(0, "****************************");


            var leftBitset = ____left.ToOptimizedBitset();
            var rightBitset = ___right.ToOptimizedBitset();
            var fullBitset = ____full.ToOptimizedBitset();
            var actual = leftBitset.AndNot(rightBitset, fullBitset).GetValues();
            var expected = __result.ToOptimizedBitset().GetValues();

            CollectionAssert.AreEqual(actual, expected);
        }

        [TestCase()]
        public void AndNot_With_Full_Matching_Values()
        {
            var ____left = BitsetHelpers.ToValues(0, "****************************");
            var ___right = BitsetHelpers.ToValues(0, "****************************");
            var __result = BitsetHelpers.ToValues(0, "----------------------------");
            var ____full = BitsetHelpers.ToValues(0, "****************************");


            var leftBitset = ____left.ToOptimizedBitset();
            var rightBitset = ___right.ToOptimizedBitset();
            var fullBitset = ____full.ToOptimizedBitset();
            var actual = leftBitset.AndNot(rightBitset, fullBitset).GetValues();
            var expected = __result.ToOptimizedBitset().GetValues();



            CollectionAssert.AreEqual(actual, expected);
        }

        [TestCase()]
        public void AndNot_With_Left_Empty_Right_Full_Matching_Values()
        {
            var ____left = BitsetHelpers.ToValues(0, "----------------------------");
            var ___right = BitsetHelpers.ToValues(0, "****************************");
            var __result = BitsetHelpers.ToValues(0, "----------------------------");
            var ____full = BitsetHelpers.ToValues(0, "****************************");


            var leftBitset = ____left.ToOptimizedBitset();
            var rightBitset = ___right.ToOptimizedBitset();
            var fullBitset = ____full.ToOptimizedBitset();
            var actual = leftBitset.AndNot(rightBitset, fullBitset).GetValues();
            var expected = __result.ToOptimizedBitset().GetValues();


            CollectionAssert.AreEqual(actual, expected);
        }

        [TestCase()]
        public void AndNot_With_Left_Full_Right_Empty_Matching_Values()
        {
            var ____left = BitsetHelpers.ToValues(0, "****************************");
            var ___right = BitsetHelpers.ToValues(0, "----------------------------");
            var __result = BitsetHelpers.ToValues(0, "****************************");
            var ____full = BitsetHelpers.ToValues(0, "****************************");


            var leftBitset = ____left.ToOptimizedBitset();
            var rightBitset = ___right.ToOptimizedBitset();
            var fullBitset = ____full.ToOptimizedBitset();
            var actual = leftBitset.AndNot(rightBitset, fullBitset).GetValues();
            var expected = __result.ToOptimizedBitset().GetValues();


            CollectionAssert.AreEqual(actual, expected);
        }


        [TestCase()]
        public void AndNot_With_Left_Enclosing_Value()
        {
            var ____left = BitsetHelpers.ToValues(0, "****************************");
            var ___right = BitsetHelpers.ToValues(0, "-------**************-------");
            var __result = BitsetHelpers.ToValues(0, "*******--------------*******");
            var ____full = BitsetHelpers.ToValues(0, "****************************");


            var leftBitset = ____left.ToOptimizedBitset();
            var rightBitset = ___right.ToOptimizedBitset();
            var fullBitset = ____full.ToOptimizedBitset();
            var actual = leftBitset.AndNot(rightBitset, fullBitset).GetValues();
            var expected = __result.ToOptimizedBitset().GetValues();


            CollectionAssert.AreEqual(actual, expected);
        }

        [TestCase()]
        public void AndNot_With_Right_Enclosing_Value()
        {
            var ____left = BitsetHelpers.ToValues(0, "-------**************-------");
            var ___right = BitsetHelpers.ToValues(0, "****************************");
            var __result = BitsetHelpers.ToValues(0, "----------------------------");
            var ____full = BitsetHelpers.ToValues(0, "****************************");


            var leftBitset = ____left.ToOptimizedBitset();
            var rightBitset = ___right.ToOptimizedBitset();
            var fullBitset = ____full.ToOptimizedBitset();
            var actual = leftBitset.AndNot(rightBitset, fullBitset).GetValues();
            var expected = __result.ToOptimizedBitset().GetValues();


            CollectionAssert.AreEqual(actual, expected);
        }

        [TestCase()]
        public void AndNot_With_Various_Values()
        {
            var ____left = BitsetHelpers.ToValues(0, "-----------------------------***************************-------------********----------*************-----------------************-----***--**");
            var ___right = BitsetHelpers.ToValues(0, "---------*****************************-------------**************************------*****----*------***------***------******---------***-----*");
            var __result = BitsetHelpers.ToValues(0, "--------------------------------------*************-------------------------------------****-******------------------------******------**--*-");
            var ____full = BitsetHelpers.ToValues(0, "*********************************************************************************************************************************************");


            var leftBitset = ____left.ToOptimizedBitset();
            var rightBitset = ___right.ToOptimizedBitset();
            var fullBitset = ____full.ToOptimizedBitset();
            var actual = leftBitset.AndNot(rightBitset, fullBitset).GetValues();
            var expected = __result.ToOptimizedBitset().GetValues();


            CollectionAssert.AreEqual(actual, expected);
        }


        [TestCase()]
        public void AndNot_With_Non_Overlapping_Values_Returns_Empty()
        {
            var ____left = BitsetHelpers.ToValues(0, "---------------*************");
            var ___right = BitsetHelpers.ToValues(0, "***************-------------");
            var __result = BitsetHelpers.ToValues(0, "---------------*************");
            var ____full = BitsetHelpers.ToValues(0, "****************************");


            var leftBitset = ____left.ToOptimizedBitset();
            var rightBitset = ___right.ToOptimizedBitset();
            var fullBitset = ____full.ToOptimizedBitset();
            var actual = leftBitset.AndNot(rightBitset, fullBitset).GetValues();
            var expected = __result.ToOptimizedBitset().GetValues();


            CollectionAssert.AreEqual(actual, expected);
        }

        [TestCase()]
        public void AndNot_With_Left_Bridging_Value()
        {
            var ____left = BitsetHelpers.ToValues(0, "---------********************------------");
            var ___right = BitsetHelpers.ToValues(0, "**************--------*******************");
            var __result = BitsetHelpers.ToValues(0, "--------------********-------------------");
            var ____full = BitsetHelpers.ToValues(0, "*****************************************");


            var leftBitset = ____left.ToOptimizedBitset();
            var rightBitset = ___right.ToOptimizedBitset();
            var fullBitset = ____full.ToOptimizedBitset();
            var actual = leftBitset.AndNot(rightBitset, fullBitset).GetValues();
            var expected = __result.ToOptimizedBitset().GetValues();


            CollectionAssert.AreEqual(actual, expected);
        }

        [TestCase()]
        public void AndNot_With_Right_Bridging_Value()
        {
            var ____left = BitsetHelpers.ToValues(0, "**************--------*******************");
            var ___right = BitsetHelpers.ToValues(0, "---------********************------------");
            var __result = BitsetHelpers.ToValues(0, "*********--------------------************");
            var ____full = BitsetHelpers.ToValues(0, "*****************************************");


            var leftBitset = ____left.ToOptimizedBitset();
            var rightBitset = ___right.ToOptimizedBitset();
            var fullBitset = ____full.ToOptimizedBitset();
            var actual = leftBitset.AndNot(rightBitset, fullBitset).GetValues();
            var expected = __result.ToOptimizedBitset().GetValues();


            CollectionAssert.AreEqual(actual, expected);
        }



        [TestCase()]
        public void AndNot_With_Alternate_Bridging_Values()
        {
            var ____left = BitsetHelpers.ToValues(0, "****--********--------*******************----------");
            var ___right = BitsetHelpers.ToValues(0, "---------********************--------*******---****");
            var __result = BitsetHelpers.ToValues(0, "****--***--------------------********--------------");
            var ____full = BitsetHelpers.ToValues(0, "***************************************************");


            var leftBitset = ____left.ToOptimizedBitset();
            var rightBitset = ___right.ToOptimizedBitset();
            var fullBitset = ____full.ToOptimizedBitset();
            var actual = leftBitset.AndNot(rightBitset, fullBitset).GetValues();
            var expected = __result.ToOptimizedBitset().GetValues();


            CollectionAssert.AreEqual(actual, expected);
        }


        [TestCase()]
        public void AndNot_With_Left_Leading_Values()
        {
            var ____left = BitsetHelpers.ToValues(0, "--****---*******----*********************");
            var ___right = BitsetHelpers.ToValues(0, "--------------------------***************");
            var __result = BitsetHelpers.ToValues(0, "--****---*******----******---------------");
            var ____full = BitsetHelpers.ToValues(0, "*****************************************");


            var leftBitset = ____left.ToOptimizedBitset();
            var rightBitset = ___right.ToOptimizedBitset();
            var fullBitset = ____full.ToOptimizedBitset();
            var actual = leftBitset.AndNot(rightBitset, fullBitset).GetValues();
            var expected = __result.ToOptimizedBitset().GetValues();


            CollectionAssert.AreEqual(actual, expected);
        }

        [TestCase()]
        public void AndNot_With_Right_Leading_Values()
        {
            var ____left = BitsetHelpers.ToValues(0, "--------------------------***************");
            var ___right = BitsetHelpers.ToValues(0, "--****---*******----*********************");
            var __result = BitsetHelpers.ToValues(0, "-----------------------------------------");
            var ____full = BitsetHelpers.ToValues(0, "*****************************************");


            var leftBitset = ____left.ToOptimizedBitset();
            var rightBitset = ___right.ToOptimizedBitset();
            var fullBitset = ____full.ToOptimizedBitset();
            var actual = leftBitset.AndNot(rightBitset, fullBitset).GetValues();
            var expected = __result.ToOptimizedBitset().GetValues();


            CollectionAssert.AreEqual(actual, expected);
        }


        [TestCase()]
        public void AndNot_With_Left_Advanced_Overlapping_Value()
        {
            var ____left = BitsetHelpers.ToValues(0, "--****************----------");
            var ___right = BitsetHelpers.ToValues(0, "---------*******************");
            var __result = BitsetHelpers.ToValues(0, "--*******-------------------");
            var ____full = BitsetHelpers.ToValues(0, "****************************");


            var leftBitset = ____left.ToOptimizedBitset();
            var rightBitset = ___right.ToOptimizedBitset();
            var fullBitset = ____full.ToOptimizedBitset();
            var actual = leftBitset.AndNot(rightBitset, fullBitset).GetValues();
            var expected = __result.ToOptimizedBitset().GetValues();


            CollectionAssert.AreEqual(actual, expected);
        }

        [TestCase()]
        public void AndNot_With_Right_Advanced_Overlapping_Value()
        {
            var ____left = BitsetHelpers.ToValues(0, "---------*******************");
            var ___right = BitsetHelpers.ToValues(0, "--****************----------");
            var __result = BitsetHelpers.ToValues(0, "------------------**********");
            var ____full = BitsetHelpers.ToValues(0, "****************************");


            var leftBitset = ____left.ToOptimizedBitset();
            var rightBitset = ___right.ToOptimizedBitset();
            var fullBitset = ____full.ToOptimizedBitset();
            var actual = leftBitset.AndNot(rightBitset, fullBitset).GetValues();
            var expected = __result.ToOptimizedBitset().GetValues();


            CollectionAssert.AreEqual(actual, expected);
        }

        [TestCase()]
        public void AndNot_With_Start_Synchronized_Left_Advanced_Ending_Overlapping_Value()
        {
            var ____left = BitsetHelpers.ToValues(0, "***************--------------");
            var ___right = BitsetHelpers.ToValues(0, "*********************--------");
            var __result = BitsetHelpers.ToValues(0, "-----------------------------");
            var ____full = BitsetHelpers.ToValues(0, "*****************************");


            var leftBitset = ____left.ToOptimizedBitset();
            var rightBitset = ___right.ToOptimizedBitset();
            var fullBitset = ____full.ToOptimizedBitset();
            var actual = leftBitset.AndNot(rightBitset, fullBitset).GetValues();
            var expected = __result.ToOptimizedBitset().GetValues();


            CollectionAssert.AreEqual(actual, expected);
        }

        [TestCase()]
        public void AndNot_With_Start_Synchronized_Right_Advanced_Ending_Overlapping_Value()
        {
            var ____left = BitsetHelpers.ToValues(0, "*********************--------");
            var ___right = BitsetHelpers.ToValues(0, "***************--------------");
            var __result = BitsetHelpers.ToValues(0, "---------------******--------");
            var ____full = BitsetHelpers.ToValues(0, "*****************************");


            var leftBitset = ____left.ToOptimizedBitset();
            var rightBitset = ___right.ToOptimizedBitset();
            var fullBitset = ____full.ToOptimizedBitset();
            var actual = leftBitset.AndNot(rightBitset, fullBitset).GetValues();
            var expected = __result.ToOptimizedBitset().GetValues();


            CollectionAssert.AreEqual(actual, expected);
        }

        [TestCase()]
        public void AndNot_With_End_Synchronized_Left_Advanced_Overlapping_Value()
        {
            var ____left = BitsetHelpers.ToValues(0, "--------------***************");
            var ___right = BitsetHelpers.ToValues(0, "*****************************");
            var __result = BitsetHelpers.ToValues(0, "-----------------------------");
            var ____full = BitsetHelpers.ToValues(0, "****************************");


            var leftBitset = ____left.ToOptimizedBitset();
            var rightBitset = ___right.ToOptimizedBitset();
            var fullBitset = ____full.ToOptimizedBitset();
            var actual = leftBitset.AndNot(rightBitset, fullBitset).GetValues();
            var expected = __result.ToOptimizedBitset().GetValues();


            CollectionAssert.AreEqual(actual, expected);
        }

        [TestCase()]
        public void AndNot_With_End_Synchronized_Right_Advanced_Overlapping_Value()
        {
            var ____left = BitsetHelpers.ToValues(0, "*****************************");
            var ___right = BitsetHelpers.ToValues(0, "--------------***************");
            var __result = BitsetHelpers.ToValues(0, "**************---------------");
            var ____full = BitsetHelpers.ToValues(0, "****************************");


            var leftBitset = ____left.ToOptimizedBitset();
            var rightBitset = ___right.ToOptimizedBitset();
            var fullBitset = ____full.ToOptimizedBitset();
            var actual = leftBitset.AndNot(rightBitset, fullBitset).GetValues();
            var expected = __result.ToOptimizedBitset().GetValues();


            CollectionAssert.AreEqual(actual, expected);
        }


        [TestCase()]
        public void AndNot_With_End_Synchronized_Left_Advanced_Overlapping_Value_With_Trailing_Values()
        {
            var ____left = BitsetHelpers.ToValues(0, "--------------***************----***----**-");
            var ___right = BitsetHelpers.ToValues(0, "*****************************--------------");
            var __result = BitsetHelpers.ToValues(0, "---------------------------------***----**-");
            var ____full = BitsetHelpers.ToValues(0, "*******************************************");


            var leftBitset = ____left.ToOptimizedBitset();
            var rightBitset = ___right.ToOptimizedBitset();
            var fullBitset = ____full.ToOptimizedBitset();
            var actual = leftBitset.AndNot(rightBitset, fullBitset).GetValues();
            var expected = __result.ToOptimizedBitset().GetValues();


            CollectionAssert.AreEqual(actual, expected);
        }

        [TestCase()]
        public void AndNot_With_End_Synchronized_Right_Advanced_Overlapping_Value_With_Trailing_Values()
        {
            var ____left = BitsetHelpers.ToValues(0, "*****************************----***----**-");
            var ___right = BitsetHelpers.ToValues(0, "--------------***************--------------");
            var __result = BitsetHelpers.ToValues(0, "**************-------------------***----**-");
            var ____full = BitsetHelpers.ToValues(0, "*******************************************");


            var leftBitset = ____left.ToOptimizedBitset();
            var rightBitset = ___right.ToOptimizedBitset();
            var fullBitset = ____full.ToOptimizedBitset();
            var actual = leftBitset.AndNot(rightBitset, fullBitset).GetValues();
            var expected = __result.ToOptimizedBitset().GetValues();


            CollectionAssert.AreEqual(actual, expected);
        }

    }
}