using System;
using System.Collections.Generic;
using System.Windows;
using Caliburn.Micro;
using MahApps.Metro.Controls.Dialogs;
using PdfIndex.Core;
using PdfIndex.Infrastructure;
using PdfIndex.ViewModels;

namespace PdfIndex
{
    public class Bootstrapper : BootstrapperBase
    {
        private SimpleContainer _container = new SimpleContainer();

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            _container.Singleton<IWindowManager, WindowManager>();
            _container.Singleton<IEventAggregator, EventAggregator>();
            _container.Singleton<IPdfRecordRepository, ExcelIndexPdfRecordRepository>();
            _container.Singleton<IPdfRecordReader, WebBrowserPdfRecordReader>();
            _container.Instance<IDialogCoordinator>(DialogCoordinator.Instance);
            _container.PerRequest<ShellViewModel>();
            _container.Singleton<RecordsViewModelFactory>();
            _container.PerRequest<RecordsViewModel>();
        }

        protected override object GetInstance(Type serviceType, string key)
        {
            return _container.GetInstance(serviceType, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return _container.GetAllInstances(serviceType);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }
    }
}
