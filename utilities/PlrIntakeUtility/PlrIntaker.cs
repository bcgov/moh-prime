using System;

using ExcelDataReader;

using Prime.Models;


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
            provider.FirstName = reader.GetString(GetIndex("E"));
            provider.LastName = reader.GetString(GetIndex("H"));
            provider.Gender = reader.GetString(GetIndex("J"));
            provider.DateOfBirth = reader.GetDateTime(GetIndex("K"));
            provider.StatusCode = reader.GetString(GetIndex("L"));
            provider.StatusReasonCode = reader.GetString(GetIndex("M"));
            provider.StatusStartDate = reader.GetDateTime(GetIndex("N"));
            provider.StatusExpiryDate = reader.GetDateTime(GetIndex("O"));
            provider.Address1_Line1 = reader.GetString(GetIndex("R"));
            provider.City1 = reader.GetString(GetIndex("U"));
            provider.Province1 = reader.GetString(GetIndex("V"));
            provider.PostalCode1 = reader.GetString(GetIndex("X"));
            provider.Address1StartDate = reader.GetDateTime(GetIndex("Y"));
            provider.TelephoneAreaCode = reader.GetString(GetIndex("AI"));
            provider.TelephoneNumber = reader.GetString(GetIndex("AJ"));

            return provider;
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
