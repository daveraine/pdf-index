using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Excel;
using PdfIndex.Core;
using System.Data;

namespace PdfIndex.Infrastructure
{
    internal class ExcelIndexPdfRecordRepository : IPdfRecordRepository
    {
        public IEnumerable<PdfRecord> GetRecords()
        {
            try
            {
                using (var stream = File.Open("index.xlsx", FileMode.Open, FileAccess.Read))
                using (var reader = ExcelReaderFactory.CreateOpenXmlReader(stream))
                {
                    reader.IsFirstRowAsColumnNames = true;

                    return reader.AsDataSet().Tables[0].AsEnumerable().Select(x => new PdfRecord
                    {
                        Title = x.Field<string>("TITLE"),
                        Category = x.Field<string>("CATEGORY"),
                        Description = x.Field<string>("DESCRIPTION"),
                        Reference = x.Field<string>("REFERENCE"),
                        Page = x.Field<double?>("PAGE").HasValue ? Convert.ToInt32(x.Field<double?>("PAGE").Value) : (int?)null
                    });
                }
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