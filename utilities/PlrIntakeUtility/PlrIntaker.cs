using System;

using CsvHelper;
using Serilog;

using Prime.Models;

namespace PlrIntakeUtility
{
    public class PlrIntaker
    {
        private static readonly DateTime PlrNullDateTime = new DateTime(9999, 12, 30);


        /// <summary>
        /// Reads from a row in Excel file into a PlrProvider object.
        /// </summary>
        public PlrProvider ReadRow(CsvReader reader)
        {
            PlrProvider provider = new PlrProvider();
            provider.Ipc = GetString(reader, GetIndex("A"));
            provider.Cpn = GetString(reader, GetIndex("B"));
            provider.IdentifierType = GetString(reader, GetIndex("C"));
            provider.CollegeId = GetString(reader, GetIndex("D"));
            provider.ProviderRoleType = GetString(reader, GetIndex("E"));
            provider.MspId = GetString(reader, GetIndex("F"));
            provider.NamePrefix = GetString(reader, GetIndex("G"));
            provider.FirstName = GetString(reader, GetIndex("H"));
            provider.SecondName = GetString(reader, GetIndex("I"));
            provider.ThirdName = GetString(reader, GetIndex("J"));
            provider.LastName = GetString(reader, GetIndex("K"));
            provider.Suffix = GetString(reader, GetIndex("L"));
            provider.Gender = GetString(reader, GetIndex("M"));
            provider.DateOfBirth = TryGetDateTime(reader, "N");
            provider.StatusCode = GetString(reader, GetIndex("O"));
            provider.StatusReasonCode = GetString(reader, GetIndex("P"));
            provider.StatusStartDate = TryGetDateTime(reader, "Q");
            provider.StatusExpiryDate = TryGetDateTime(reader, "R");
            provider.Expertise = GetMultipleElements(GetString(reader, GetIndex("S")));
            // PRIME not collecting Languages
            // provider.Languages = GetString(reader, GetIndex("T"));

            provider.Address1Line1 = GetString(reader, GetIndex("U"));
            provider.Address1Line2 = GetString(reader, GetIndex("V"));
            provider.Address1Line3 = GetString(reader, GetIndex("W"));
            provider.City1 = GetString(reader, GetIndex("X"));
            provider.Province1 = GetString(reader, GetIndex("Y"));
            provider.Country1 = GetString(reader, GetIndex("Z"));
            provider.PostalCode1 = GetString(reader, GetIndex("AA"));
            provider.Address1StartDate = TryGetDateTime(reader, "AB");

            provider.Credentials = GetMultipleElements(GetString(reader, GetIndex("AC")));
            provider.TelephoneAreaCode = GetString(reader, GetIndex("AD"));
            provider.TelephoneNumber = GetString(reader, GetIndex("AE"));
            provider.FaxAreaCode = GetString(reader, GetIndex("AF"));
            provider.FaxNumber = GetString(reader, GetIndex("AG"));
            provider.Email = GetString(reader, GetIndex("AH"));
            provider.ConditionCode = GetString(reader, GetIndex("AI"));
            provider.ConditionStartDate = TryGetDateTime(reader, "AJ");
            provider.ConditionEndDate = TryGetDateTime(reader, "AK");

            return provider;
        }


        /// <summary>
        /// Returns `null` if cell is empty
        /// </summary>
        private string GetString(CsvReader reader, int colIndex)
        {
            string value = reader.GetField<string>(colIndex);
            return (String.IsNullOrEmpty(value) ? null : value);
        }


        /// <summary>
        /// Returns DateTime representing cell value, or `null` if cell is empty
        /// </summary>
        private DateTime? TryGetDateTime(CsvReader reader, string columnId)
        {
            var dateTime = reader.GetField<DateTime?>(GetIndex(columnId));
            if (dateTime == null)
            {
                return null;
            }
            // Treat value meant to represent NULL as `null`
            else if (dateTime.Equals(PlrNullDateTime))
            {
                return (DateTime?)null;
            }
            else
            {
                return dateTime;
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
