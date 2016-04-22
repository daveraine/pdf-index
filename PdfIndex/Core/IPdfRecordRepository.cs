using PdfIndex.Core;
using System.Collections.Generic;

namespace PdfIndex.Core
{
    public interface IPdfRecordRepository
    {
        IEnumerable<PdfRecord> GetRecords();
    }
}
