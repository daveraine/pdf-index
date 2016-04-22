using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Caliburn.Micro;
using MahApps.Metro.Controls.Dialogs;
using PdfIndex.Core;

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
            Categories = _allRecords.Select(x => x.Category).Distinct();
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

        private string _selectedCategory;
        public string SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                NotifyOfPropertyChange(() => SelectedCategory);

                Records = _allRecords.Where(x => x.Category == _selectedCategory);
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

        public IEnumerable<string> Categories { get; set; }
    }
}
