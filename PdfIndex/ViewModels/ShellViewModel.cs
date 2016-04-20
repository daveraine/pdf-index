using Caliburn.Micro;
using PdfIndex.Models;
using PdfIndex.Services;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace PdfIndex.ViewModels
{
    public class ShellViewModel : PropertyChangedBase
    {
        private readonly IEnumerable<PdfRecord> _allRecords;

        public ShellViewModel(IPdfRecordRepository repository)
        {
            _allRecords = repository.GetRecords();
            Counties = _allRecords.Select(x => x.County).Distinct();
        }

        public void RowSelect(PdfRecord record)
        {
            var fileName = string.Format("{0}.{1}", record.Reference, "pdf");
            if (File.Exists(fileName))
            {
                Process.Start(fileName);
            }
        }

        private string _selectedCounty;
        public string SelectedCounty
        {
            get { return _selectedCounty; }
            set
            {
                _selectedCounty = value;
                NotifyOfPropertyChange(() => SelectedCounty);

                Records = _allRecords.Where(x => x.County == _selectedCounty);
            }
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

        public IEnumerable<string> Counties { get; set; }
    }
}
