using System.Diagnostics;

namespace SparseBitsets
{
    [DebuggerDisplay("Start = {Start}, End = {End}")]
    public class Run
    {
        public long Start { get; set; }
        public long End { get; set; }
        public ulong[] Values { get; set; }
    }
}