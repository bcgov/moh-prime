using System;
using System.IO;
using ExcelDataReader;
using Prime.Models;
using Xunit;

namespace PlrIntakeUtility.Tests
{
    public class PlrIntakerTests
    {
        private PlrIntaker _tested;

        public PlrIntakerTests()
        {
            _tested = new PlrIntaker();

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        [Fact]
        public void TestGetIndex()
        {
            Assert.Equal(0, PlrIntaker.GetIndex("A"));
            Assert.Equal(25, PlrIntaker.GetIndex("Z"));
            Assert.Equal(26, PlrIntaker.GetIndex("AA"));
            Assert.Equal(27, PlrIntaker.GetIndex("AB"));
            Assert.Equal(52, PlrIntaker.GetIndex("BA"));
        }

        [Fact]
        public void TestReadRow()
        {
            using (var stream = File.Open("PRIME_Test_Data_PLR_IAT20210617_v2.0.xls", FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    // Header row
                    reader.Read();
                    Assert.Equal("IPC", reader.GetString(0));

                    reader.Read();
                    PlrProvider provider = _tested.ReadRow(reader);
                    Assert.Equal("IPC.00166847.BC.PRS", provider.Ipc);
                    Assert.Equal("CPN.00166847.BC.PRS", provider.Cpn);
                    Assert.Equal("CPSID", provider.IdentifierType);
                    Assert.Equal("20101", provider.CollegeId);
                    Assert.Equal("MD", provider.ProviderRoleType);
                    Assert.Equal("PRIMET", provider.FirstName);
                    Assert.Equal("Norval", provider.SecondName);
                    Assert.Equal("TEN", provider.LastName);
                    Assert.Equal("M", provider.Gender);
                    Assert.Equal(new DateTime(2000, 5, 30), provider.DateOfBirth);
                    Assert.Equal("ACTIVE", provider.StatusCode);
                    Assert.Equal("PRAC", provider.StatusReasonCode);
                    Assert.Equal(new DateTime(2020, 1, 10), provider.StatusStartDate);
                    Assert.Equal(null, provider.StatusExpiryDate);
                    Assert.Equal(new string[] { "101" }, provider.Expertise);
                    Assert.Equal("7887 Fallen Circus", provider.Address1Line1);
                    Assert.Equal("Burnaby", provider.City1);
                    Assert.Equal("BC", provider.Province1);
                    Assert.Equal("V3N 3N6", provider.PostalCode1);
                    Assert.Equal(new DateTime(1995, 1, 15), provider.Address1StartDate);
                    Assert.Equal(new string[] { "MD", "PHD" }, provider.Credentials);
                    Assert.Equal("218", provider.TelephoneAreaCode);
                    Assert.Equal("310-6997", provider.TelephoneNumber);
                    Assert.Equal("PRIMETTEN@test.ca", provider.Email);
                    Assert.Equal("ADMIN", provider.ConditionCode);
                    Assert.Equal(new DateTime(2020, 1, 1), provider.ConditionStartDate);
                    Assert.Equal(null, provider.ConditionEndDate);

                    Assert.Null(provider.Address1Line2);

                    reader.Read();
                    PlrProvider provider2 = _tested.ReadRow(reader);
                    Assert.Null(provider2.ConditionCode);
                    Assert.Null(provider2.ConditionStartDate);
                    Assert.Null(provider2.ConditionEndDate);
                }
            }
        }
    }
}
