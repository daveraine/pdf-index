using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using MahApps.Metro.Controls.Dialogs;
using PdfIndex.Core;

namespace PdfIndex.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<ShowMessageEvent>
    {
        private readonly IEnumerable<PdfRecord> _allRecords;
        private readonly IDialogCoordinator _dialogs;
        private readonly RecordsViewModelFactory _viewModelFactory;

        public ShellViewModel(IPdfRecordRepository repository, IDialogCoordinator dialogs,
            IEventAggregator events,
            RecordsViewModelFactory viewModelFactory)
        {
            events.Subscribe(this);
            _dialogs = dialogs;
            _viewModelFactory = viewModelFactory;

            _allRecords = repository.GetRecords();

            DisplayName = "PDF Index";
            Categories = _allRecords.Select(x => x.Category).Distinct();
        }

        private string _selectedCategory;
        public string SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                NotifyOfPropertyChange(() => SelectedCategory);

                ActivateItem(_viewModelFactory.Create(_allRecords.Where(x => x.Category == _selectedCategory)));
            }
        }

        public IEnumerable<string> Categories { get; set; }

        public void Handle(ShowMessageEvent message)
        {
            _dialogs.ShowMessageAsync(this, message.Title, message.Message);
        }
    }
}
