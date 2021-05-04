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
            provider.Ipc = reader.GetString(GetIndex("A"));
            provider.IdentifierType = reader.GetString(GetIndex("B"));
            provider.CollegeId = reader.GetString(GetIndex("C"));
            provider.ProviderRoleType = reader.GetString(GetIndex("D"));
            provider.MspId = reader.GetString(GetIndex("E"));
            provider.NamePrefix = reader.GetString(GetIndex("F"));
            provider.FirstName = reader.GetString(GetIndex("G"));
            provider.SecondName = reader.GetString(GetIndex("H"));
            provider.ThirdName = reader.GetString(GetIndex("I"));
            provider.LastName = reader.GetString(GetIndex("J"));
            provider.Suffix = reader.GetString(GetIndex("K"));
            provider.Gender = reader.GetString(GetIndex("L"));
            provider.DateOfBirth = TryGetDateTime(reader, "M");
            provider.StatusCode = reader.GetString(GetIndex("N"));
            provider.StatusReasonCode = reader.GetString(GetIndex("O"));
            provider.StatusStartDate = TryGetDateTime(reader, "P");
            provider.StatusExpiryDate = TryGetDateTime(reader, "Q");
            provider.Expertise = GetMultipleElements(reader.GetString(GetIndex("R")));
            provider.Languages = reader.GetString(GetIndex("S"));

            provider.Address1Line1 = reader.GetString(GetIndex("T"));
            provider.Address1Line2 = reader.GetString(GetIndex("U"));
            provider.Address1Line3 = reader.GetString(GetIndex("V"));
            provider.City1 = reader.GetString(GetIndex("W"));
            provider.Province1 = reader.GetString(GetIndex("X"));
            provider.Country1 = reader.GetString(GetIndex("Y"));
            provider.PostalCode1 = reader.GetString(GetIndex("Z"));
            provider.Address1StartDate = TryGetDateTime(reader, "AA");

            provider.Address2Line1 = reader.GetString(GetIndex("AB"));
            provider.Address2Line2 = reader.GetString(GetIndex("AC"));
            provider.Address2Line3 = reader.GetString(GetIndex("AD"));
            provider.City2 = reader.GetString(GetIndex("AE"));
            provider.Province2 = reader.GetString(GetIndex("AF"));
            provider.Country2 = reader.GetString(GetIndex("AG"));
            provider.PostalCode2 = reader.GetString(GetIndex("AH"));
            provider.Address2StartDate = TryGetDateTime(reader, "AI");

            provider.Credentials = GetMultipleElements(reader.GetString(GetIndex("AJ")));

            provider.TelephoneAreaCode = reader.GetString(GetIndex("AK"));
            provider.TelephoneNumber = reader.GetString(GetIndex("AL"));
            provider.FaxAreaCode = reader.GetString(GetIndex("AM"));
            provider.FaxNumber = reader.GetString(GetIndex("AN"));
            provider.Email = reader.GetString(GetIndex("AO"));
            provider.ConditionCode = reader.GetString(GetIndex("AP"));

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


        /// <param name="aValue">A pipe-delimited string</param>
        /// <returns>Array containing parsed elements or <c>null</c> if given input is <c>null</c>.</returns>
        private string[] GetMultipleElements(string aValue)
        {
            if (aValue != null)
            {
                return aValue.Split('|');
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Check fields that are expected to be populated, logging error if not populated.
        /// </summary>
        public void CheckData(PlrProvider provider, int rowNum)
        {
            CheckRequiredField(provider.Ipc, nameof(provider.Ipc), rowNum);
            CheckRequiredField(provider.IdentifierType, nameof(provider.IdentifierType), rowNum);
            CheckRequiredField(provider.CollegeId, nameof(provider.CollegeId), rowNum);
            CheckRequiredField(provider.ProviderRoleType, nameof(provider.ProviderRoleType), rowNum);
            CheckRequiredField(provider.FirstName, nameof(provider.FirstName), rowNum);
            CheckRequiredField(provider.LastName, nameof(provider.LastName), rowNum);
            CheckRequiredField(provider.Gender, nameof(provider.Gender), rowNum);
            CheckRequiredField(provider.DateOfBirth, nameof(provider.DateOfBirth), rowNum);
            CheckRequiredField(provider.StatusCode, nameof(provider.StatusCode), rowNum);
            CheckRequiredField(provider.StatusReasonCode, nameof(provider.StatusReasonCode), rowNum);
            CheckRequiredField(provider.StatusStartDate, nameof(provider.StatusStartDate), rowNum);
            CheckRequiredField(provider.StatusExpiryDate, nameof(provider.StatusExpiryDate), rowNum);
            CheckRequiredField(provider.Address1Line1, nameof(provider.Address1Line1), rowNum);
            CheckRequiredField(provider.City1, nameof(provider.City1), rowNum);
            CheckRequiredField(provider.Province1, nameof(provider.Province1), rowNum);
            CheckRequiredField(provider.PostalCode1, nameof(provider.PostalCode1), rowNum);
            CheckRequiredField(provider.Address1StartDate, nameof(provider.Address1StartDate), rowNum);
            CheckRequiredField(provider.TelephoneAreaCode, nameof(provider.TelephoneAreaCode), rowNum);
            CheckRequiredField(provider.TelephoneNumber, nameof(provider.TelephoneNumber), rowNum);
            CheckRequiredField(provider.Email, nameof(provider.Email), rowNum);
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
