using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Caliburn.Micro;
using PdfIndex.Core;

namespace PdfIndex.ViewModels
{
    public class RecordsViewModel : PropertyChangedBase
    {
        private readonly IEventAggregator _events;

        public RecordsViewModel(IEventAggregator events)
        {
            _events = events;
        }

        private IEnumerable<PdfRecord> _records;
        public IEnumerable<PdfRecord> Records
        {
            get { return _records; }
            set
            {
                _records = value;
                NotifyOfPropertyChange(() => Records);
            }
        }

        internal void Load(IEnumerable<PdfRecord> records)
        {
            Records = records;
        }

        public void RowSelect(PdfRecord record)
        {
            var fileName = string.Format("{0}.{1}", record.Reference, "pdf");
            if (File.Exists(fileName))
            {
                Process.Start(fileName);
            }
            else
            {
                _events.PublishOnUIThread(new ShowMessageEvent("File not found", string.Format("Could not load the file for {0}", record.Title)));
            }
        }
    }
}
