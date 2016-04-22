using System;
using System.Collections.Generic;
using System.IO;
using Excel;
using PdfIndex.Core;

namespace PdfIndex.Infrastructure
{
    internal class ExcelIndexPdfRecordRepository : IPdfRecordRepository
    {
        public IEnumerable<PdfRecord> GetRecords()
        {
            try
            {
                var records = new List<PdfRecord>();

                using (var stream = File.Open("index.xlsx", FileMode.Open, FileAccess.Read))
                using (var reader = ExcelReaderFactory.CreateOpenXmlReader(stream))
                {
                    while (reader.Read())
                    {
                        var page = reader.GetValue(4);

                        records.Add(new PdfRecord
                        {
                            Title = reader.GetString(0),
                            Category = reader.GetString(1),
                            Description = reader.GetString(2),
                            Reference = reader.GetString(3),
                            Page = page == null ? null : Convert.ToInt32(page) as int?
                        });
                    }
                }

                return records;
            }
            catch (FileNotFoundException ex)
            {
                throw new DataAccessException("Could not find index.xlsx", ex);
            }
            catch (IOException ex)
            {
                // File already open?
                throw new DataAccessException("Could not load from index.xlsx. Make sure the file is not already open and try again.", ex);
            }
        }
    }
}