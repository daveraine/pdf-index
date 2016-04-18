using Caliburn.Micro;
using PdfIndex.Models;
using PdfIndex.Services;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace PdfIndex.ViewModels
{
    public class ShellViewModel : PropertyChangedBase
    {
        public ShellViewModel(IPdfRecordRepository repository)
        {
            Records = repository.GetRecords();
        }

        public void RowSelect(PdfRecord record)
        {
            var fileName = string.Format("{0}.{1}", record.Reference, "pdf");
            if (File.Exists(fileName))
            {
                Process.Start(fileName);
            }
        }

        public IEnumerable<PdfRecord> Records { get; set; }
    }
}
