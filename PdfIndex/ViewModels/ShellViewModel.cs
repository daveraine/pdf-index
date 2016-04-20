using Caliburn.Micro;
using MahApps.Metro.Controls.Dialogs;
using PdfIndex.Models;
using PdfIndex.Services;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PdfIndex.ViewModels
{
    public class ShellViewModel : PropertyChangedBase
    {
        private readonly IEnumerable<PdfRecord> _allRecords;
        private readonly IDialogCoordinator _dialogs;

        public ShellViewModel(IPdfRecordRepository repository, IDialogCoordinator dialogs)
        {
            _dialogs = dialogs;
            _allRecords = repository.GetRecords();
            Counties = _allRecords.Select(x => x.County).Distinct();
        }

        public async Task RowSelect(PdfRecord record)
        {
            var fileName = string.Format("{0}.{1}", record.Reference, "pdf");
            if (File.Exists(fileName))
            {
                Process.Start(fileName);
            }
            else
            {
                await _dialogs.ShowMessageAsync(this, "File not found", string.Format("Could not load the file for {0}", record.Title));
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
