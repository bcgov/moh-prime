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
            provider.Ipc = reader.GetField(GetIndex("A"));
            provider.Cpn = reader.GetField(GetIndex("B"));
            provider.IdentifierType = reader.GetField(GetIndex("C"));
            provider.CollegeId = reader.GetField(GetIndex("D"));
            provider.ProviderRoleType = reader.GetField(GetIndex("E"));
            provider.MspId = reader.GetField(GetIndex("F"));
            provider.NamePrefix = reader.GetField(GetIndex("G"));
            provider.FirstName = reader.GetField(GetIndex("H"));
            provider.SecondName = reader.GetField(GetIndex("I"));
            provider.ThirdName = reader.GetField(GetIndex("J"));
            provider.LastName = reader.GetField(GetIndex("K"));
            provider.Suffix = reader.GetField(GetIndex("L"));
            provider.Gender = reader.GetField(GetIndex("M"));
            provider.DateOfBirth = TryGetDateTime(reader, "N");
            provider.StatusCode = reader.GetField(GetIndex("O"));
            provider.StatusReasonCode = reader.GetField(GetIndex("P"));
            provider.StatusStartDate = TryGetDateTime(reader, "Q");
            provider.StatusExpiryDate = TryGetDateTime(reader, "R");
            provider.Expertise = GetMultipleElements(reader.GetField(GetIndex("S")));
            // PRIME not collecting Languages
            // provider.Languages = reader.GetField(GetIndex("T"));

            provider.Address1Line1 = reader.GetField(GetIndex("U"));
            provider.Address1Line2 = reader.GetField(GetIndex("V"));
            provider.Address1Line3 = reader.GetField(GetIndex("W"));
            provider.City1 = reader.GetField(GetIndex("X"));
            provider.Province1 = reader.GetField(GetIndex("Y"));
            provider.Country1 = reader.GetField(GetIndex("Z"));
            provider.PostalCode1 = reader.GetField(GetIndex("AA"));
            provider.Address1StartDate = TryGetDateTime(reader, "AB");

            provider.Credentials = GetMultipleElements(reader.GetField(GetIndex("AC")));
            provider.TelephoneAreaCode = reader.GetField(GetIndex("AD"));
            provider.TelephoneNumber = reader.GetField(GetIndex("AE"));
            provider.FaxAreaCode = reader.GetField(GetIndex("AF"));
            provider.FaxNumber = reader.GetField(GetIndex("AG"));
            provider.Email = reader.GetField(GetIndex("AH"));
            provider.ConditionCode = reader.GetField(GetIndex("AI"));
            provider.ConditionStartDate = TryGetDateTime(reader, "AJ");
            provider.ConditionEndDate = TryGetDateTime(reader, "AK");

            return provider;
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
