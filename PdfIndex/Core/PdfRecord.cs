namespace PdfIndex.Core
{
    public class PdfRecord
    {
        public string Title { get; set; }

        public string Reference { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

        public int? Page { get; set; }
    }
}
