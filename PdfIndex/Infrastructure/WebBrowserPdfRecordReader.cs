using PdfIndex.Core;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace PdfIndex.Infrastructure
{
    internal class WebBrowserPdfRecordReader : IPdfRecordReader
    {
        public bool Open(PdfRecord record)
        {
            var fileName = string.Format("{0}.{1}", record.Reference, "pdf");
            if (File.Exists(fileName))
            {
                var uri = new StringBuilder("file:///");
                uri.Append(Path.GetFullPath(fileName));
                if (record.Page.HasValue)
                {
                    uri.AppendFormat("#page={0}", record.Page.Value);
                }
                Process.Start(uri.ToString());
                return true;
            }

            return false;
        }
    }
}
