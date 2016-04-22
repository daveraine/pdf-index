using System;
using System.Collections.Generic;
using Excel;
using System.IO;
using PdfIndex.Core;

namespace PdfIndex.Infrastructure
{
    internal class ExcelIndexPdfRecordRepository : IPdfRecordRepository
    {
        public IEnumerable<PdfRecord> GetRecords()
        {
            using (var stream = File.Open("index.xlsx", FileMode.Open, FileAccess.Read))
            using (var reader = ExcelReaderFactory.CreateOpenXmlReader(stream))
            {
                while (reader.Read())
                {
                    var page = reader.GetValue(4);

                    yield return new PdfRecord
                    {
                        Title = reader.GetString(0),
                        Category = reader.GetString(1),
                        Description = reader.GetString(2),
                        Reference = reader.GetString(3),
                        Page = page == null ? null : Convert.ToInt32(page) as int?
                    };
                }
            }
        }
    }
}
