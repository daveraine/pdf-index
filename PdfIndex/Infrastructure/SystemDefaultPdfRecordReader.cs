using System.Diagnostics;
using System.IO;
using PdfIndex.Core;

namespace PdfIndex.Infrastructure
{
    internal class SystemDefaultPdfRecordReader : IPdfRecordReader
    {
        public bool Open(PdfRecord record)
        {
            var fileName = string.Format("{0}.{1}", record.Reference, "pdf");
            if (File.Exists(fileName))
            {
                Process.Start(fileName);
                return true;
            }

            return false;
        }
    }
}
