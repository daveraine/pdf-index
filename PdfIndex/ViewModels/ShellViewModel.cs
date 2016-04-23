using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using MahApps.Metro.Controls.Dialogs;
using PdfIndex.Core;

namespace PdfIndex.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<ShowMessageEvent>
    {
        private readonly IPdfRecordRepository _repository;
        private readonly IDialogCoordinator _dialogs;
        private readonly RecordsViewModelFactory _viewModelFactory;
        private IEnumerable<PdfRecord> _allRecords;

        public ShellViewModel(IPdfRecordRepository repository, IDialogCoordinator dialogs,
            IEventAggregator events,
            RecordsViewModelFactory viewModelFactory)
        {
            events.Subscribe(this);
            _repository = repository;
            _dialogs = dialogs;
            _viewModelFactory = viewModelFactory;            

            DisplayName = "PDF Index";
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

        protected override void OnViewLoaded(object view)
        {
            try
            {
                _allRecords = _repository.GetRecords().ToArray();
                Categories = _allRecords.Select(x => x.Category).Distinct();
            }
            catch (DataAccessException ex)
            {
                _dialogs.ShowMessageAsync(this, "Error", ex.Message);
            }
        }

        public IEnumerable<string> Categories { get; set; }

        public void Handle(ShowMessageEvent message)
        {
            _dialogs.ShowMessageAsync(this, message.Title, message.Message);
        }
    }
}
