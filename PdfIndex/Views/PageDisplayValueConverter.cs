using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace PdfIndex.Views
{
    internal class PageDisplayValueConverter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var page = value as int?;

            if (page.HasValue)
            {
                return string.Format("Page {0}", page.Value);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
