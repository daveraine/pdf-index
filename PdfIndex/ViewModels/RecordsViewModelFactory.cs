using PdfIndex.Core;
using System.Collections.Generic;

namespace PdfIndex.ViewModels
{
    public class RecordsViewModelFactory
    {
        private readonly RecordsViewModel _viewModel;

        public RecordsViewModelFactory(RecordsViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public RecordsViewModel Create(IEnumerable<PdfRecord> records)
        {
            _viewModel.Load(records);
            return _viewModel;
        }
    }
}
