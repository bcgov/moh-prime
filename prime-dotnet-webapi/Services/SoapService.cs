using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using SoapCore.Extensibility;

namespace Prime.Services
{
    public class SoapServiceOperationTuner : IServiceOperationTuner
    {
        public const string Prefix = "plr";
        public const string Uri = "urn:hl7-org:v3";

        public void Tune(HttpContext httpContext, object serviceInstance, SoapCore.ServiceModel.OperationDescription operation)
        {
            // This must be the header used by proxies that first receive the client certificate
            // (and pass on via HTTP Header)
            const string CLIENT_CERT_HEADER = "X-SSL-CERT";

            if (serviceInstance is SoapService service)
            {
                string urlEncoded;
                StringValues paramValue;
                if (httpContext.Request.Headers.TryGetValue(CLIENT_CERT_HEADER, out paramValue))
                {
                    urlEncoded = paramValue[0];
                }
                else
                {
                    throw new ArgumentException("Client certificate expected to be provided.");
                }

                if (PrimeEnvironment.PlrIntegration.ClientCertThumbprint == null)
                {
                    throw new SystemException("Receiving system is not configured properly; please advise system administrator.");
                }

                string decoded = HttpUtility.UrlDecode(urlEncoded);
                // https://stackoverflow.com/questions/65349878/c-sharp-convert-certificate-string-into-x509-certificate
                byte[] certAsBytes = Encoding.ASCII.GetBytes(decoded);
                var clientCert = new X509Certificate2(certAsBytes);
                if (clientCert.Thumbprint.Equals(PrimeEnvironment.PlrIntegration.ClientCertThumbprint))
                {
                    service.DocumentRoot = GetRequestBody(httpContext, Prefix, Uri, operation.Name);
                }
                else
                {
                    throw new ArgumentException($"The certificate with thumbprint {clientCert.Thumbprint} is invalid to the receiving system.");
                }
            }
        }

        public static XElement GetRequestBody(HttpContext httpContext, string prefix, string uri, string bodyElement)
        {
            // Rewinding seems a bit expensive, but can't figure out why the stream
            // is partially read at this point in the request, but it appears to
            // occur in SoapCore as custom inlined middleware indicates it is not
            // partially read prior to entering SoapCore's middleware
            httpContext.Request.Body.Seek(0, SeekOrigin.Begin);

            // Produces a graph of XNode objects, which depending on our use case
            // could be considered expensive with HL7 XML documents appearing to
            // be quite large and deeply nested
            var xDocument = XDocument.Load(httpContext.Request.Body);
            var query = $"//{prefix}:{bodyElement}";
            return xDocument.XPathSelectElement(query, GetXmlNamespaceManager(prefix, uri));
        }

        private static XmlNamespaceManager GetXmlNamespaceManager(string prefix, string uri)
        {
            var xmlnsManager = new XmlNamespaceManager(new NameTable());
            xmlnsManager.AddNamespace(prefix, uri);

            return xmlnsManager;
        }
    }

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
        private readonly ILogger<SoapService> _logger;

        public SoapService(
            ILogger<SoapService> logger)
        {
            _logger = logger;
        }

        public XElement DocumentRoot { get; set; }

        public void AddBcProvider()
        {
            _logger.LogInformation("Add BC Provider");
            Console.WriteLine(DocumentRoot.ToString());
        }

        public void UpdateBcProvider()
        {
            _logger.LogInformation("Update BC Provider");
            Console.WriteLine(DocumentRoot.ToString());
        }
    }
}
