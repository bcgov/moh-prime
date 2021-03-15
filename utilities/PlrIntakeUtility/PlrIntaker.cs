using System;

using ExcelDataReader;

using Prime.Models;
using Serilog;

namespace PlrIntakeUtility
{
    public class PlrIntaker
    {
        /// <summary>
        /// Reads from a row in Excel file into a PlrProvider object.
        /// </summary>
        public PlrProvider ReadRow(IExcelDataReader reader)
        {
            PlrProvider provider = new PlrProvider();
            provider.CollegeId = reader.GetString(GetIndex("A"));
            provider.ProviderType = reader.GetString(GetIndex("B"));
            provider.MspId = reader.GetString(GetIndex("C"));
            provider.NamePrefix = reader.GetString(GetIndex("D"));
            provider.FirstName = reader.GetString(GetIndex("E"));
            provider.SecondName = reader.GetString(GetIndex("F"));
            provider.ThirdName = reader.GetString(GetIndex("G"));
            provider.LastName = reader.GetString(GetIndex("H"));
            provider.Suffix = reader.GetString(GetIndex("I"));
            provider.Gender = reader.GetString(GetIndex("J"));
            provider.DateOfBirth = TryGetDateTime(reader, "K");
            provider.StatusCode = reader.GetString(GetIndex("L"));
            provider.StatusReasonCode = reader.GetString(GetIndex("M"));
            provider.StatusStartDate = TryGetDateTime(reader, "N");
            provider.StatusExpiryDate = TryGetDateTime(reader, "O");
            provider.Expertise = reader.GetString(GetIndex("P"));
            provider.Languages = reader.GetString(GetIndex("Q"));

            provider.Address1_Line1 = reader.GetString(GetIndex("R"));
            provider.Address1_Line2 = reader.GetString(GetIndex("S"));
            provider.Address1_Line3 = reader.GetString(GetIndex("T"));
            provider.City1 = reader.GetString(GetIndex("U"));
            provider.Province1 = reader.GetString(GetIndex("V"));
            provider.Country1 = reader.GetString(GetIndex("W"));
            provider.PostalCode1 = reader.GetString(GetIndex("X"));
            provider.Address1StartDate = TryGetDateTime(reader, "Y");

            provider.Address2_Line1 = reader.GetString(GetIndex("Z"));
            provider.Address2_Line2 = reader.GetString(GetIndex("AA"));
            provider.Address2_Line3 = reader.GetString(GetIndex("AB"));
            provider.City2 = reader.GetString(GetIndex("AC"));
            provider.Province2 = reader.GetString(GetIndex("AD"));
            provider.Country2 = reader.GetString(GetIndex("AE"));
            provider.PostalCode2 = reader.GetString(GetIndex("AF"));
            provider.Address2StartDate = TryGetDateTime(reader, "AG");

            provider.Credentials = reader.GetString(GetIndex("AH"));

            provider.TelephoneAreaCode = reader.GetString(GetIndex("AI"));
            provider.TelephoneNumber = reader.GetString(GetIndex("AJ"));
            provider.FaxAreaCode = reader.GetString(GetIndex("AK"));
            provider.FaxNumber = reader.GetString(GetIndex("AL"));

            return provider;
        }

        /// <summary>
        /// Returns DateTime representing cell value, or `DateTime.MinValue` if cell is empty
        /// </summary>
        private DateTime TryGetDateTime(IExcelDataReader reader, string columnId)
        {
            // If cell is empty, `reader.GetDateTime` will cause a `System.NullReferenceException: Object reference not set to an instance of an object.`
            return (reader.IsDBNull(GetIndex(columnId)) ? DateTime.MinValue : reader.GetDateTime(GetIndex(columnId)));
        }

        /// <summary>
        /// Check fields that are expected to be populated, logging error if not populated.
        /// </summary>
        public void CheckData(PlrProvider provider, int rowNum)
        {
            CheckRequiredField(provider.CollegeId, nameof(provider.CollegeId), rowNum);
            CheckRequiredField(provider.ProviderType, nameof(provider.ProviderType), rowNum);
            CheckRequiredField(provider.FirstName, nameof(provider.FirstName), rowNum);
            CheckRequiredField(provider.LastName, nameof(provider.LastName), rowNum);
            CheckRequiredField(provider.Gender, nameof(provider.Gender), rowNum);
            CheckRequiredField(provider.DateOfBirth, nameof(provider.DateOfBirth), rowNum);
            CheckRequiredField(provider.StatusCode, nameof(provider.StatusCode), rowNum);
            CheckRequiredField(provider.StatusReasonCode, nameof(provider.StatusReasonCode), rowNum);
            CheckRequiredField(provider.StatusStartDate, nameof(provider.StatusStartDate), rowNum);
            CheckRequiredField(provider.StatusExpiryDate, nameof(provider.StatusExpiryDate), rowNum);
            CheckRequiredField(provider.Address1_Line1, nameof(provider.Address1_Line1), rowNum);
            CheckRequiredField(provider.City1, nameof(provider.City1), rowNum);
            CheckRequiredField(provider.Province1, nameof(provider.Province1), rowNum);
            CheckRequiredField(provider.PostalCode1, nameof(provider.PostalCode1), rowNum);
            CheckRequiredField(provider.Address1StartDate, nameof(provider.Address1StartDate), rowNum);
            CheckRequiredField(provider.TelephoneAreaCode, nameof(provider.TelephoneAreaCode), rowNum);
            CheckRequiredField(provider.TelephoneNumber, nameof(provider.TelephoneNumber), rowNum);
        }

        private bool CheckRequiredField(object fieldValue, string fieldName, int rowNum)
        {
            if (fieldValue == null || fieldValue.Equals(DateTime.MinValue))
            {
                Log.Warning($"'{fieldName}' was empty at row number {rowNum}.");
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Algorithm courtesy of https://stackoverflow.com/questions/667802/what-is-the-algorithm-to-convert-an-excel-column-letter-into-its-number
        /// </summary>
        /// <param name="excelColumn">For example, "AC"</param>
        /// <returns>Zero-based index, e.g. 28</returns>
        public static int GetIndex(string excelColumn)
        {
            if (string.IsNullOrEmpty(excelColumn)) throw new ArgumentNullException("excelColumn");

            excelColumn = excelColumn.ToUpperInvariant();

            int sum = 0;

            for (int i = 0; i < excelColumn.Length; i++)
            {
                sum *= 26;
                sum += (excelColumn[i] - 'A' + 1);
            }

            // Zero-based indexing
            return sum - 1;
        }
    }
}
