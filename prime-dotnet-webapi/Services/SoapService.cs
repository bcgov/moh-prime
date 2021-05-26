using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.ServiceModel;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
        Task AddBcProviderAsync();

        [OperationContract(Name = "PRPM_IN303030CA")]
        Task UpdateBcProviderAsync();
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

        public async Task AddBcProviderAsync()
        {
            _logger.LogInformation("Add BC Provider");

            var plrProvider = ReadDistributionMessage(DocumentRoot.ToString());
            var objectId = await _dbService.CreateOrUpdatePlrProviderAsync(plrProvider);
            _logger.LogDebug("objectId=" + objectId);
        }

        public async Task UpdateBcProviderAsync()
        {
            _logger.LogInformation("Update BC Provider");

            var plrProvider = ReadDistributionMessage(DocumentRoot.ToString());
            var objectId = await _dbService.CreateOrUpdatePlrProviderAsync(plrProvider, true);
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
                _logger.LogError("Mandatory IPC was not found for the message '{id}'", messageId);
                throw new ArgumentNullException("IPC missing.");
            }

            var result = new PlrProvider
            {
                // Primary attributes for PRIME
                Ipc = internalProviderCode,
                // At this point, IdentifierType as OID
                IdentifierType = ReadNodeData($"//{Prefix}:healthCareProvider/{Prefix}:id[not (@root='2.16.840.1.113883.3.40.2.8') and not (@root='2.16.840.1.113883.3.40.2.11')]/@root", documentRoot, messageId),
                CollegeId = ReadNodeData($"//{Prefix}:healthCareProvider/{Prefix}:id[not (@root='2.16.840.1.113883.3.40.2.8') and not (@root='2.16.840.1.113883.3.40.2.11')]/@extension", documentRoot, messageId),
                ProviderRoleType = ReadNodeData($"//{Prefix}:healthCareProvider/{Prefix}:code/@code", documentRoot, messageId),
                FirstName = ReadNodeData($"//{Prefix}:healthCarePrincipalPerson/{Prefix}:name[@use='L']/{Prefix}:given[1]", documentRoot, messageId),
                SecondName = ReadNodeData($"//{Prefix}:healthCarePrincipalPerson/{Prefix}:name[@use='L']/{Prefix}:given[2]", documentRoot, messageId),
                ThirdName = ReadNodeData($"//{Prefix}:healthCarePrincipalPerson/{Prefix}:name[@use='L']/{Prefix}:given[3]", documentRoot, messageId),
                LastName = ReadNodeData($"//{Prefix}:healthCarePrincipalPerson/{Prefix}:name[@use='L']/{Prefix}:family", documentRoot, messageId),
                DateOfBirth = ParseHL7v3DateTime(ReadNodeData($"//{Prefix}:healthCarePrincipalPerson/{Prefix}:birthTime/@value", documentRoot, messageId)),
                StatusCode = ReadNodeData($"//{Prefix}:healthCareProvider/{Prefix}:statusCode/@code", documentRoot, messageId),
                StatusReasonCode = ReadNodeData($"//{Prefix}:healthCareProvider/{Prefix}:subjectOf2/{Prefix}:roleActivation/{Prefix}:reasonCode/@code", documentRoot, messageId),
                ConditionCode = ReadNodeData($"//{Prefix}:healthCareProvider/{Prefix}:responsibleFor/{Prefix}:privilege/{Prefix}:code/@code", documentRoot, messageId),

                // Secondary attributes for PRIME
                Address1Line1 = ReadNodeData($"//{Prefix}:healthCareProvider/{Prefix}:addr[1]/{Prefix}:streetAddressLine[1]", documentRoot, messageId),
                Address1Line2 = ReadNodeData($"//{Prefix}:healthCareProvider/{Prefix}:addr[1]/{Prefix}:streetAddressLine[2]", documentRoot, messageId),
                Address1Line3 = ReadNodeData($"//{Prefix}:healthCareProvider/{Prefix}:addr[1]/{Prefix}:streetAddressLine[3]", documentRoot, messageId),
                City1 = ReadNodeData($"//{Prefix}:healthCareProvider/{Prefix}:addr[1]/{Prefix}:city", documentRoot, messageId),
                Province1 = ReadNodeData($"//{Prefix}:healthCareProvider/{Prefix}:addr[1]/{Prefix}:state", documentRoot, messageId),
                Country1 = ReadNodeData($"//{Prefix}:healthCareProvider/{Prefix}:addr[1]/{Prefix}:country", documentRoot, messageId),
                PostalCode1 = ReadNodeData($"//{Prefix}:healthCareProvider/{Prefix}:addr[1]/{Prefix}:postalCode", documentRoot, messageId),
                Address1StartDate = ParseHL7v3DateTime(ReadNodeData($"//{Prefix}:healthCareProvider/{Prefix}:addr[1]/{Prefix}:useablePeriod/{Prefix}:low/@value", documentRoot, messageId)),

                Address2Line1 = ReadNodeData($"//{Prefix}:healthCareProvider/{Prefix}:addr[2]/{Prefix}:streetAddressLine[1]", documentRoot, messageId),
                Address2Line2 = ReadNodeData($"//{Prefix}:healthCareProvider/{Prefix}:addr[2]/{Prefix}:streetAddressLine[2]", documentRoot, messageId),
                Address2Line3 = ReadNodeData($"//{Prefix}:healthCareProvider/{Prefix}:addr[2]/{Prefix}:streetAddressLine[3]", documentRoot, messageId),
                City2 = ReadNodeData($"//{Prefix}:healthCareProvider/{Prefix}:addr[2]/{Prefix}:city", documentRoot, messageId),
                Province2 = ReadNodeData($"//{Prefix}:healthCareProvider/{Prefix}:addr[2]/{Prefix}:state", documentRoot, messageId),
                Country2 = ReadNodeData($"//{Prefix}:healthCareProvider/{Prefix}:addr[2]/{Prefix}:country", documentRoot, messageId),
                PostalCode2 = ReadNodeData($"//{Prefix}:healthCareProvider/{Prefix}:addr[2]/{Prefix}:postalCode", documentRoot, messageId)
            };

            var dateValue = ReadNodeData($"//{Prefix}:healthCareProvider/{Prefix}:addr[2]/{Prefix}:useablePeriod/{Prefix}:low/@value", documentRoot, messageId);
            if (dateValue != null)
            {
                result.Address2StartDate = ParseHL7v3DateTime(dateValue);
            }

            // According to PLR team, Credentials will have a `reference` child node (i.e. designation text) ...
            result.Credentials = ReadMultiNodeData($"//{Prefix}:healthCareProvider/{Prefix}:relatedTo/{Prefix}:qualifiedEntity/{Prefix}:code[{Prefix}:originalText/{Prefix}:reference]/@code", documentRoot, messageId);
            string emailData = ReadNodeData($"//{Prefix}:healthCareProvider/{Prefix}:telecom[@use='WP' and starts-with(@value, 'mailto')]/@value", documentRoot, messageId);
            if (emailData != null)
            {
                result.Email = RemoveHL7v3TelecomType(emailData);
            }
            // ... but Expertises will never have a `reference` child node
            result.Expertise = ReadMultiNodeData($"//{Prefix}:healthCareProvider/{Prefix}:relatedTo/{Prefix}:qualifiedEntity/{Prefix}:code[not({Prefix}:originalText/{Prefix}:reference)]/@code", documentRoot, messageId);
            string faxNumberData = ReadNodeData($"//{Prefix}:healthCareProvider/{Prefix}:telecom[@use='WP' and starts-with(@value, 'fax')]/@value", documentRoot, messageId);
            if (faxNumberData != null)
            {
                string[] faxNumberParts = SplitHL7v3TelecomNumber(RemoveHL7v3TelecomType(faxNumberData));
                if (faxNumberParts.Length == 2)
                {
                    result.FaxAreaCode = faxNumberParts[0];
                    result.FaxNumber = faxNumberParts[1];
                }
                else
                {
                    result.FaxNumber = faxNumberParts[0];
                }
            }
            result.Gender = ReadNodeData($"//{Prefix}:healthCarePrincipalPerson/{Prefix}:administrativeGenderCode/@code", documentRoot, messageId);
            // result.Languages  // TODO: Verify with Vinder
            result.MspId = ReadNodeData($"//{Prefix}:healthCareProvider/{Prefix}:id[@root='2.16.840.1.113883.3.40.2.11']/@extension", documentRoot, messageId);
            result.NamePrefix = ReadNodeData($"//{Prefix}:healthCarePrincipalPerson/{Prefix}:name[@use='L']/{Prefix}:prefix", documentRoot, messageId);
            result.StatusStartDate = ParseHL7v3DateTime(ReadNodeData($"//{Prefix}:healthCareProvider/{Prefix}:statusCode/{Prefix}:validTime/{Prefix}:low/@value", documentRoot, messageId));
            dateValue = ReadNodeData($"//{Prefix}:healthCareProvider/{Prefix}:statusCode/{Prefix}:validTime/{Prefix}:high/@value", documentRoot, messageId);
            if (dateValue != null)
            {
                result.StatusExpiryDate = ParseHL7v3DateTime(dateValue);
            }
            result.Suffix = ReadNodeData($"//{Prefix}:healthCarePrincipalPerson/{Prefix}:name[@use='L']/{Prefix}:suffix", documentRoot, messageId);
            string telephoneNumData = ReadNodeData($"//{Prefix}:healthCareProvider/{Prefix}:telecom[@use='WP' and starts-with(@value, 'tel')]/@value", documentRoot, messageId);
            if (telephoneNumData != null)
            {
                string[] telephoneNumberParts = SplitHL7v3TelecomNumber(RemoveHL7v3TelecomType(telephoneNumData));
                if (telephoneNumberParts.Length == 2)
                {
                    result.TelephoneAreaCode = telephoneNumberParts[0];
                    result.TelephoneNumber = telephoneNumberParts[1];
                }
                else
                {
                    result.TelephoneNumber = telephoneNumberParts[0];
                }
            }
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

        /// <summary>
        ///
        /// </summary>
        /// <param name="telecomNumber">Expects a 10-digit string but works with other input</param>
        /// <returns>A 2-element array containing area code and local number, if input was a 10-digit string.
        ///     Otherwise, simply returns <c>telecomNumber</c> as the single element in the array</returns>
        public static string[] SplitHL7v3TelecomNumber(string telecomNumber)
        {
            var allDigitsRegex = new Regex("^[0-9]+$");
            if (telecomNumber != null && telecomNumber.Length == 10 && allDigitsRegex.IsMatch(telecomNumber))
            {
                return new string[] { telecomNumber.Substring(0, 3), telecomNumber.Substring(3) };
            }
            else
            {
                return new string[] { telecomNumber };
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

        private string[] ReadMultiNodeData(string xPath, XmlNode documentRoot, string messageId = null)
        {
            XmlNodeList nodes = documentRoot.SelectNodes(xPath, _nsManager);
            if (nodes.Count != 0)
            {
                List<string> results = new List<string>();
                foreach (XmlNode node in nodes)
                {
                    _logger.LogInformation(node.InnerXml);
                    results.Add(node.InnerXml);
                }
                return results.ToArray();
            }
            else
            {
                _logger.LogWarning($"{xPath} did not match anything in the message with ID '{messageId}'");
                return null;
            }
        }
    }
}
