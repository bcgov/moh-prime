using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using FakeItEasy;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Prime.Services;
using Xunit;

namespace PrimeTests.UnitTests
{
    public class SoapServiceTests
    {
        public static SoapService CreateService(
            ILogger<SoapService> logger = null,
            IPlrProviderService plrProviderService = null)
        {
            return new SoapService(
                logger ?? A.Fake<ILogger<SoapService>>(),
                plrProviderService ?? A.Fake<IPlrProviderService>()
            );
        }


        [Theory]
        [InlineData("./Integration/PRPM_IN301030CA - missing message id.xml")]
        [InlineData("./Integration/PRPM_IN301030CA - missing IPC.xml")]
        // [InlineData("./README.md")] ... needs to be tested outside of this unit test since `XDocument.Load` will fail
        public async Task TestHandlingMalformedInput(string filePath)
        {
            var service = CreateService();
            Assert.True(File.Exists(filePath));
            service.DocumentRoot = XDocument.Load(filePath).Root;

            await Assert.ThrowsAnyAsync<Exception>(() => service.AddBcProviderAsync());
        }

        [Fact]
        public void TestParseHL7v3DateTime()
        {
            Assert.Equal(new DateTime(1957, 3, 5, 20, 20, 20), SoapService.ParseHL7v3DateTime("19570305202020"));
            Assert.Null(SoapService.ParseHL7v3DateTime("19570305XXXXXX"));
        }

        [Fact]
        public void TestRemoveHL7v3TelecomType()
        {
            Assert.Equal("terry_albert_test@gmail.com", SoapService.RemoveHL7v3TelecomType("mailto:terry_albert_test@gmail.com"));
            Assert.Equal("terry_albert_test@gmail.com", SoapService.RemoveHL7v3TelecomType("terry_albert_test@gmail.com"));
        }

        [Fact]
        public void TestSplitTelecomNumber()
        {
            Assert.Equal(new string[] { "604", "6665555" }, SoapService.SplitTelecomNumber("6046665555"));
            Assert.Equal(new string[] { "1234567" }, SoapService.SplitTelecomNumber("1234567"));
            Assert.Equal(new string[] { "(604)666-5555" }, SoapService.SplitTelecomNumber("(604)666-5555"));
        }
    }
}
