using SparseBitsets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparseBitsetUnitTests
{
    public class BitsetHelpers
    {
        public static IEnumerable<ulong> ToValues(ulong start, string expression)
        {
            expression = expression.Replace(" ", "", StringComparison.InvariantCulture);
            int ptr = 0;
            while (ptr < expression.Length)
            {
                if (expression[ptr] == '*')
                {
                    yield return start + (ulong)ptr;
                }
                ptr++;
            }
        }

        public static ulong ToValue(string expression)
        {
            expression = expression.Replace(" ", "", StringComparison.InvariantCulture);
            expression = new string(expression.Reverse().ToArray());
            ulong value = 0;
            int ptr = 0;
            while (ptr < expression.Length)
            {
                if (expression[ptr] == '*')
                {
                    value |= ((ulong)1 << ptr);
                }
                ptr++;
            }

            return value;
        }

        public static IEnumerable<Run> ToRuns(long startKey, string expression)
        {
            return ToRuns(startKey, expression, null);
        }

        public static IEnumerable<Run> ToRuns(long startKey, string expression, Dictionary<char, ulong> valueLookup)
        {
            expression = expression.Replace(" ", "", StringComparison.InvariantCulture);
            int ptr = 0;
            Run currentRun = null;
            ulong[] buffer = new ulong[256];
            var bufferPtr = 0;
            var sb = new StringBuilder();
            while (ptr < expression.Length)
            {
                if (expression[ptr] != '-')
                {

                    if (currentRun == null)
                    {
                        currentRun = new Run()
                        {
                            Start = startKey + ptr,
                        };
                    }

                    ulong value;
                    if (expression[ptr] == '*')
                    {
                        value = ulong.MaxValue;
                    }
                    else
                    {
                        value = valueLookup[expression[ptr]];
                    }
                    buffer[bufferPtr] = value;
                    bufferPtr++;

                }
                else if (expression[ptr] == '-')
                {
                    if (currentRun != null)
                    {
                        currentRun.Values = new ulong[bufferPtr];
                        Array.Copy(buffer, currentRun.Values, bufferPtr);
                        currentRun.End = startKey + ptr - 1;
                        yield return currentRun;
                        currentRun = null;
                        bufferPtr = 0;
                    }
                }
                ptr++;
            }

            if (currentRun != null)
            {
                currentRun.Values = new ulong[bufferPtr];
                Array.Copy(buffer, currentRun.Values, bufferPtr);
                currentRun.End = startKey + ptr - 1;
                yield return currentRun;
            }
        }
    }
}