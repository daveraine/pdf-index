using PdfIndex.Models;
using System.Collections.Generic;

namespace PdfIndex.Services
{
    public interface IPdfRecordRepository
    {
        IEnumerable<PdfRecord> GetRecords();
    }
}
