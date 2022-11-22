using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Collections.Special;
using SparseBitsets;

namespace Benchmarks
{
    public class ZipRealDataProvider : IEnumerable<SparseBitset>, IDisposable
    {
        private readonly ZipArchive m_Archive;

        public ZipRealDataProvider(string path)
        {
            var fs = File.OpenRead(path);
            m_Archive = new ZipArchive(fs, ZipArchiveMode.Read);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerator<SparseBitset> GetEnumerator()
        {
            foreach (var zipArchiveEntry in m_Archive.Entries)
            {
                using (var stream = zipArchiveEntry.Open())
                {
                    using (var stringReader = new StreamReader(stream))
                    {
                        var split = stringReader.ReadLine().Split(',');
                        var values = split.Select(int.Parse).ToList();
                        var bitmap = new SparseBitset(values);
                        bitmap.Pack();
                        yield return bitmap;
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        ~ZipRealDataProvider()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                m_Archive.Dispose();
            }
        }
    }

    public class ZipRealDataProvider2: IEnumerable<RoaringBitmap>, IDisposable
    {
        private readonly ZipArchive m_Archive;

        public ZipRealDataProvider2(string path)
        {
            var fs = File.OpenRead(path);
            m_Archive = new ZipArchive(fs, ZipArchiveMode.Read);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerator<RoaringBitmap> GetEnumerator()
        {
            foreach (var zipArchiveEntry in m_Archive.Entries)
            {
                using (var stream = zipArchiveEntry.Open())
                {
                    using (var stringReader = new StreamReader(stream))
                    {
                        var split = stringReader.ReadLine().Split(',');
                        var values = split.Select(int.Parse).ToList();
                        var bitmap = RoaringBitmap.Create(values);
                        yield return bitmap;
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        ~ZipRealDataProvider2()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                m_Archive.Dispose();
            }
        }
    }
}