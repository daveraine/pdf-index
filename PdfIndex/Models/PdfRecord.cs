namespace PdfIndex.Models
{
    public class PdfRecord
    {
        public string Title { get; set; }

        public string Reference { get; set; }

        public string County { get; set; }

        public string Range { get; set; }

        public int? Page { get; set; }
    }
}
