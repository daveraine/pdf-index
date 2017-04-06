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
                using (var stream = File.Open("index.xlsx", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var reader = ExcelReaderFactory.CreateOpenXmlReader(stream))
                {
                    reader.IsFirstRowAsColumnNames = true;

                    return reader.AsDataSet().Tables[0].AsEnumerable().Select(ParseRow);
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

        private static PdfRecord ParseRow(DataRow row)
        {
            try
            {
                var page = row.Field<double?>("PAGE");

                return new PdfRecord
                {
                    Title = row.Field<string>("TITLE"),
                    Category = row.Field<string>("CATEGORY"),
                    Description = row.Field<string>("DESCRIPTION"),
                    Reference = row.Field<string>("REFERENCE"),
                    Page = page.HasValue ? Convert.ToInt32(page.Value) : (int?)null
                };
            }
            catch (ArgumentException ex)
            {
                throw new DataAccessException("Invalid column names in index.xlsx", ex);
            }
            catch (InvalidCastException ex)
            {
                throw new DataAccessException("Invalid data in index.xlsx", ex);
            }
        }
    }
}