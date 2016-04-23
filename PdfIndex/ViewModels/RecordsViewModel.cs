using System.Collections.Generic;
using Caliburn.Micro;
using PdfIndex.Core;

namespace PdfIndex.ViewModels
{
    public class RecordsViewModel : PropertyChangedBase
    {
        private readonly IEventAggregator _events;
        private readonly IPdfRecordReader _reader;

        public RecordsViewModel(IEventAggregator events, IPdfRecordReader reader)
        {
            _events = events;
            _reader = reader;
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

        public void OpenRecord(PdfRecord record)
        {
            if (!_reader.Open(record))
            {
                _events.PublishOnUIThread(new ShowMessageEvent("File not found", string.Format("Could not load the file for {0}", record.Title)));
            }
        }
    }
}
