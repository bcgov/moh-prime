using System;

using ExcelDataReader;

using Prime.Models;
using Serilog;

namespace PlrIntakeUtility
{
    public class PlrIntaker
    {
        private static readonly DateTime PlrNullDateTime = new DateTime(9999, 12, 30);


        /// <summary>
        /// Reads from a row in Excel file into a PlrProvider object.
        /// </summary>
        public PlrProvider ReadRow(IExcelDataReader reader)
        {
            PlrProvider provider = new PlrProvider();
            provider.Ipc = reader.GetString(GetIndex("A"));
            provider.Cpn = reader.GetString(GetIndex("B"));
            provider.IdentifierType = reader.GetString(GetIndex("C"));
            provider.CollegeId = reader.GetString(GetIndex("D"));
            provider.ProviderRoleType = reader.GetString(GetIndex("E"));
            provider.MspId = reader.GetString(GetIndex("F"));
            provider.NamePrefix = reader.GetString(GetIndex("G"));
            provider.FirstName = reader.GetString(GetIndex("H"));
            provider.SecondName = reader.GetString(GetIndex("I"));
            provider.ThirdName = reader.GetString(GetIndex("J"));
            provider.LastName = reader.GetString(GetIndex("K"));
            provider.Suffix = reader.GetString(GetIndex("L"));
            provider.Gender = reader.GetString(GetIndex("M"));
            provider.DateOfBirth = TryGetDateTime(reader, "N");
            provider.StatusCode = reader.GetString(GetIndex("O"));
            provider.StatusReasonCode = reader.GetString(GetIndex("P"));
            provider.StatusStartDate = TryGetDateTime(reader, "Q");
            provider.StatusExpiryDate = TryGetDateTime(reader, "R");
            provider.Expertise = GetMultipleElements(reader.GetString(GetIndex("S")));
            // PRIME not collecting Languages
            // provider.Languages = reader.GetString(GetIndex("T"));

            provider.Address1Line1 = reader.GetString(GetIndex("U"));
            provider.Address1Line2 = reader.GetString(GetIndex("V"));
            provider.Address1Line3 = reader.GetString(GetIndex("W"));
            provider.City1 = reader.GetString(GetIndex("X"));
            provider.Province1 = reader.GetString(GetIndex("Y"));
            provider.Country1 = reader.GetString(GetIndex("Z"));
            provider.PostalCode1 = reader.GetString(GetIndex("AA"));
            provider.Address1StartDate = TryGetDateTime(reader, "AB");

            provider.Credentials = GetMultipleElements(reader.GetString(GetIndex("AC")));
            provider.TelephoneAreaCode = reader.GetString(GetIndex("AD"));
            provider.TelephoneNumber = reader.GetString(GetIndex("AE"));
            provider.FaxAreaCode = reader.GetString(GetIndex("AF"));
            provider.FaxNumber = reader.GetString(GetIndex("AG"));
            provider.Email = reader.GetString(GetIndex("AH"));
            provider.ConditionCode = reader.GetString(GetIndex("AI"));
            provider.ConditionStartDate = TryGetDateTime(reader, "AJ");
            provider.ConditionEndDate = TryGetDateTime(reader, "AK");

            return provider;
        }

        /// <summary>
        /// Returns DateTime representing cell value, or `null` if cell is empty
        /// </summary>
        private DateTime? TryGetDateTime(IExcelDataReader reader, string columnId)
        {
            // If cell is empty, `reader.GetDateTime` will cause a `System.NullReferenceException: Object reference not set to an instance of an object.`
            if (reader.IsDBNull(GetIndex(columnId)))
            {
                return (DateTime?)null;
            }
            else
            {
                var dateTime = reader.GetDateTime(GetIndex(columnId));
                // Treat value meant to represent NULL as `null`
                if (dateTime.Equals(PlrNullDateTime))
                {
                    return (DateTime?)null;
                }
                else
                {
                    return dateTime;
                }
            }
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
