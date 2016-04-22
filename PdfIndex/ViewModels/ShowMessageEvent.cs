namespace PdfIndex.ViewModels
{
    public class ShowMessageEvent
    {
        public ShowMessageEvent(string title, string message)
        {
            Title = title;
            Message = message;
        }

        public string Message { get; private set; }

        public string Title { get; private set; }
    }
}
