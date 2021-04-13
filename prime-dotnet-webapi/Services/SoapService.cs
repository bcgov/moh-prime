using System;
using System.Globalization;
using System.IO;
using System.ServiceModel;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Extensions.Logging;
using Prime.Models;

namespace Prime.Services
{
    [ServiceContract(Namespace = SoapServiceOperationTuner.Uri)]
    public interface ISoapService
    {
        [OperationContract(Name = "PRPM_IN301030CA")]
        void AddBcProvider();

        [OperationContract(Name = "PRPM_IN303030CA")]
        void UpdateBcProvider();
    }

    public class SoapService : ISoapService
    {
        public const string Prefix = "plr";

        private readonly ILogger<SoapService> _logger;

        private readonly IPlrProviderService _dbService;

        private readonly XmlNamespaceManager _nsManager;


        public SoapService(
            ILogger<SoapService> logger,
            IPlrProviderService dbService)
        {
            _logger = logger;
            _dbService = dbService;

            _nsManager = new XmlNamespaceManager(new NameTable());
            _nsManager.AddNamespace(Prefix, "urn:hl7-org:v3");
        }

        public XElement DocumentRoot { get; set; }

        public void AddBcProvider()
        {
            _logger.LogInformation("Add BC Provider");

            var plrProvider = ReadDistributionMessage(DocumentRoot.ToString());
            // var objectId = await _dbService.CreateOrUpdatePlrProviderAsync(plrProvider);
            var objectId = _dbService.CreateOrUpdatePlrProvider(plrProvider);
            _logger.LogDebug("objectId=" + objectId);
        }

        public void UpdateBcProvider()
        {
            _logger.LogInformation("Update BC Provider");

            var plrProvider = ReadDistributionMessage(DocumentRoot.ToString());
            var objectId = _dbService.CreateOrUpdatePlrProvider(plrProvider, true);
            _logger.LogDebug("objectId=" + objectId);

        }

        public void LogWarning(string warningMessage)
        {
            _logger.LogWarning(warningMessage);
        }

        private PlrProvider ReadDistributionMessage(string messageContent)
        {
            XmlDocument doc = new XmlDocument();
            var strReader = new StringReader(messageContent);
            doc.Load(strReader);
            XmlNode documentRoot = doc.DocumentElement;

            string messageId = ReadNodeData($"//{Prefix}:id[@root='2.16.840.1.113883.3.40.1.5']/@extension", documentRoot);
            if (messageId == null)
            {
                _logger.LogError("No ID was found for the message {messageContent}", messageContent);
                throw new ArgumentNullException("Message id missing.");
            }
            string internalProviderCode = ReadNodeData($"//{Prefix}:healthCareProvider/{Prefix}:id[@root='2.16.840.1.113883.3.40.2.8']/@extension", documentRoot, messageId);
            if (internalProviderCode == null)
            {
                _logger.LogError("Mandatory IPC was not found for the message {id}'", messageId);
                throw new ArgumentNullException("IPC missing.");
            }

            PlrProvider result = new PlrProvider();

            // Primary attributes for PRIME
            result.Ipc = internalProviderCode;
            // TODO: Translate from OID
            result.IdentifierType = ReadNodeData($"//{Prefix}:healthCareProvider/{Prefix}:id[not (@root='2.16.840.1.113883.3.40.2.8')]/@root", documentRoot, messageId);
            result.CollegeId = ReadNodeData($"//{Prefix}:healthCareProvider/{Prefix}:id[not (@root='2.16.840.1.113883.3.40.2.8')]/@extension", documentRoot, messageId);
            result.ProviderRoleType = ReadNodeData($"//{Prefix}:healthCareProvider/{Prefix}:code/@code", documentRoot, messageId);
            result.FirstName = ReadNodeData($"//{Prefix}:healthCarePrincipalPerson/{Prefix}:name[@use='L']/{Prefix}:given[1]", documentRoot, messageId);
            result.SecondName = ReadNodeData($"//{Prefix}:healthCarePrincipalPerson/{Prefix}:name[@use='L']/{Prefix}:given[2]", documentRoot, messageId);
            result.ThirdName = ReadNodeData($"//{Prefix}:healthCarePrincipalPerson/{Prefix}:name[@use='L']/{Prefix}:given[3]", documentRoot, messageId);
            result.LastName = ReadNodeData($"//{Prefix}:healthCarePrincipalPerson/{Prefix}:name[@use='L']/{Prefix}:family", documentRoot, messageId);
            result.DateOfBirth = ParseHL7v3DateTime(ReadNodeData($"//{Prefix}:healthCarePrincipalPerson/{Prefix}:birthTime/@value", documentRoot, messageId));
            result.StatusCode = ReadNodeData($"//{Prefix}:healthCareProvider/{Prefix}:statusCode/@code", documentRoot, messageId);
            result.StatusReasonCode = ReadNodeData($"//{Prefix}:healthCareProvider/{Prefix}:subjectOf2/{Prefix}:roleActivation/{Prefix}:reasonCode/@code", documentRoot, messageId);
            result.ConditionCode = ReadNodeData($"//{Prefix}:healthCareProvider/{Prefix}:responsibleFor/{Prefix}:privilege/{Prefix}:code/@code", documentRoot, messageId);

            // Secondary attributes for PRIME
            result.Address1Line1 = ReadNodeData($"//{Prefix}:healthCareProvider/{Prefix}:addr[1]/{Prefix}:streetAddressLine[1]", documentRoot, messageId);
            result.Address1Line2 = ReadNodeData($"//{Prefix}:healthCareProvider/{Prefix}:addr[1]/{Prefix}:streetAddressLine[2]", documentRoot, messageId);
            result.Address1Line3 = ReadNodeData($"//{Prefix}:healthCareProvider/{Prefix}:addr[1]/{Prefix}:streetAddressLine[3]", documentRoot, messageId);
            result.City1 = ReadNodeData($"//{Prefix}:healthCareProvider/{Prefix}:addr[1]/{Prefix}:city", documentRoot, messageId);
            result.Province1 = ReadNodeData($"//{Prefix}:healthCareProvider/{Prefix}:addr[1]/{Prefix}:state", documentRoot, messageId);
            result.Country1 = ReadNodeData($"//{Prefix}:healthCareProvider/{Prefix}:addr[1]/{Prefix}:country", documentRoot, messageId);
            result.PostalCode1 = ReadNodeData($"//{Prefix}:healthCareProvider/{Prefix}:addr[1]/{Prefix}:postalCode", documentRoot, messageId);
            result.Address1StartDate = ParseHL7v3DateTime(ReadNodeData($"//{Prefix}:healthCareProvider/{Prefix}:addr[1]/{Prefix}:useablePeriod/{Prefix}:low/@value", documentRoot, messageId));

            result.Address2Line1 = ReadNodeData($"//{Prefix}:healthCareProvider/{Prefix}:addr[2]/{Prefix}:streetAddressLine[1]", documentRoot, messageId);
            result.Address2Line2 = ReadNodeData($"//{Prefix}:healthCareProvider/{Prefix}:addr[2]/{Prefix}:streetAddressLine[2]", documentRoot, messageId);
            result.Address2Line3 = ReadNodeData($"//{Prefix}:healthCareProvider/{Prefix}:addr[2]/{Prefix}:streetAddressLine[3]", documentRoot, messageId);
            result.City2 = ReadNodeData($"//{Prefix}:healthCareProvider/{Prefix}:addr[2]/{Prefix}:city", documentRoot, messageId);
            result.Province2 = ReadNodeData($"//{Prefix}:healthCareProvider/{Prefix}:addr[2]/{Prefix}:state", documentRoot, messageId);
            result.Country2 = ReadNodeData($"//{Prefix}:healthCareProvider/{Prefix}:addr[2]/{Prefix}:country", documentRoot, messageId);
            result.PostalCode2 = ReadNodeData($"//{Prefix}:healthCareProvider/{Prefix}:addr[2]/{Prefix}:postalCode", documentRoot, messageId);
            var dateValue = ReadNodeData($"//{Prefix}:healthCareProvider/{Prefix}:addr[2]/{Prefix}:useablePeriod/{Prefix}:low/@value", documentRoot, messageId);
            if (dateValue != null)
            {
                result.Address2StartDate = ParseHL7v3DateTime(dateValue);
            }

            // result.Credentials  // TODO: Verify with Vinder
            result.Email = RemoveHL7v3TelecomType(ReadNodeData($"//{Prefix}:healthCareProvider/{Prefix}:telecom[@use='WP' and starts-with(@value, 'mailto')]/@value", documentRoot, messageId));
            // result.Expertise  // TODO: Verify with Vinder
            string[] faxNumberParts = SplitHL7v3TelecomNumber(RemoveHL7v3TelecomType(ReadNodeData($"//{Prefix}:healthCareProvider/{Prefix}:telecom[@use='WP' and starts-with(@value, 'fax')]/@value", documentRoot, messageId)));
            result.FaxAreaCode = faxNumberParts[0];
            result.FaxNumber = faxNumberParts[1];
            result.Gender = ReadNodeData($"//{Prefix}:healthCarePrincipalPerson/{Prefix}:administrativeGenderCode/@code", documentRoot, messageId);
            // result.Languages  // TODO: Verify with Vinder
            // result.MspId  // TODO: Verify with Vinder
            result.NamePrefix = ReadNodeData($"//{Prefix}:healthCarePrincipalPerson/{Prefix}:name[@use='L']/{Prefix}:prefix", documentRoot, messageId);
            // result.StatusStartDate  // TODO: Verify with Vinder
            // result.StatusExpiryDate  // TODO: Verify with Vinder
            result.Suffix = ReadNodeData($"//{Prefix}:healthCarePrincipalPerson/{Prefix}:name[@use='L']/{Prefix}:suffix", documentRoot, messageId);
            string[] telephoneNumberParts = SplitHL7v3TelecomNumber(RemoveHL7v3TelecomType(ReadNodeData($"//{Prefix}:healthCareProvider/{Prefix}:telecom[@use='WP' and starts-with(@value, 'tel')]/@value", documentRoot, messageId)));
            result.TelephoneAreaCode = telephoneNumberParts[0];
            result.TelephoneNumber = telephoneNumberParts[1];
            return result;
        }

        public static DateTime ParseHL7v3DateTime(string dateString)
        {
            return DateTime.ParseExact(dateString, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
        }

        public static string RemoveHL7v3TelecomType(string telecomValue)
        {
            var colonIndex = telecomValue.IndexOf(':');
            return (colonIndex != -1) ? telecomValue[(colonIndex + 1)..] : telecomValue;
        }

        public static string[] SplitHL7v3TelecomNumber(string telecomNumber)
        {
            var allDigitsRegex = new Regex("^[0-9]+$");
            if (telecomNumber != null && telecomNumber.Length == 10 && allDigitsRegex.IsMatch(telecomNumber))
            {
                return new string[] { telecomNumber.Substring(0, 3), telecomNumber.Substring(3) };
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(telecomNumber), telecomNumber, "Not a 10 digit string");
            }
        }

        private string ReadNodeData(string xPath, XmlNode documentRoot, string messageId = null)
        {
            XmlNode node = documentRoot.SelectSingleNode(xPath, _nsManager);
            if (node != null)
            {
                _logger.LogInformation(node.InnerXml);
                return node.InnerXml;
            }
            else
            {
                _logger.LogWarning($"{xPath} did not match anything in the message with ID '{messageId}'");
                return null;
            }
        }
    }
}
