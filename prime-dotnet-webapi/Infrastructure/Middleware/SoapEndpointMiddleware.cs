using System;
using System.IO;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Prime.Infrastructure.Middleware
{
    public class SoapEndpointMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Type _serviceType;
        private readonly string _endpointPath;
        private readonly MessageEncoder _messageEncoder;
        private readonly ILogger<SoapEndpointMiddleware> _logger;

        public SoapEndpointMiddleware(
            RequestDelegate next,
            Type serviceType,
            string endpointPath,
            MessageEncoder messageEncoder,
            ILogger<SoapEndpointMiddleware> logger)
        {
            _next = next;
            _serviceType = serviceType;
            _endpointPath = endpointPath;
            _messageEncoder = messageEncoder;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Path.Equals(_endpointPath, StringComparison.Ordinal))
            {
                // await httpContext.Response.WriteAsync("SOAP in 1\n");
                // _logger.LogInformation("Test");
                // await _next(httpContext);
                // await httpContext.Response.WriteAsync("SOAP out 1\n");


                // var xml = XmlDictionaryReader
                //     .CreateTextReader( httpContext.Response.Body, new XmlDictionaryReaderQuotas() );
                // xml.ReadStartElement("PRPM_IN301030CA", "urn:hl7-org:v3");
                //
                // await httpContext.Response.WriteAsync(xml.ReadContentAsString());


                using (var reader = new StreamReader(httpContext.Request.Body))
                {
                    var body = await reader.ReadToEndAsync();
                    // await httpContext.Response.WriteAsync(body);
                    _logger.LogInformation(body);

                    // var doc = new XmlDocument();
                    // doc.LoadXml(body);
                    //
                    // var nsmgr = new XmlNamespaceManager(doc.NameTable);
                    // nsmgr.AddNamespace("PLR", "urn:hl7-org:v3");
                    //
                    // var root = doc.DocumentElement;

                    StringBuilder sb = new StringBuilder(1024);
                    var doc = XDocument.Parse(body);
                    // await httpContext.Response.WriteAsync(doc.Document.FirstNode.ToString());
                    // await httpContext.Response.WriteAsync(doc.XPathSelectElement("/PRPM_IN301030CA").ToString());

                    var root = doc.XPathSelectElements("/Customers/Customer");
                    foreach (var el in root)
                    {
                        sb.AppendLine(el.Element("versionCode")?.Value);
                    }
                    await httpContext.Response.WriteAsync(sb.ToString());

                    // var nodeList = root.SelectNodes(
                    //     "descendant::PLR:controlActProcess", nsmgr);
                    // await httpContext.Response.WriteAsync(nodeList);

                    // var xmlBody = doc.GetElementsByTagName("soap:Body");
                    // await httpContext.Response.WriteAsync(xmlBody[0].InnerXml);
                    // await httpContext.Response.WriteAsync(doc.DocumentElement.InnerXml);

                    // await httpContext.Response.WriteAsync(test[0].InnerText);
                    // await httpContext.Response.WriteAsync(test[0].Value);
                    // using (XmlDictionaryReader xmlReader = requestMessage.GetReaderAtBodyContents())
                    // {
                    //     xmlReader.ReadStartElement(operation.Name, operation.Contract.Namespace);
                    // }
                }

                // XDocument doc;
                // var input = httpContext.Request.Body;
                // doc = XDocument.Load(httpContext.Request.Body);
                // Console.WriteLine($"{doc}");
                // using (XmlDictionaryReader xmlReader = requestMessage.GetReaderAtBodyContents())
                // {
                //     xmlReader.ReadStartElement(operation.Name, operation.Contract.Namespace);
                // }

                // using (var stream = httpContext.Request.Body)
                // {
                //     var doc = XDocument.Load(stream);
                //     Console.WriteLine($"{doc.ToString()}");
                // }
            }
            else
            {
                await _next(httpContext);
            }
        }
    }

    public static class SoapEndpointMiddlewareExtension
    {
        public static IApplicationBuilder UseSoapEndpointMiddleware<T>(
            this IApplicationBuilder builder,
            string path,
            MessageEncoder encoder)
        {
            return builder.UseMiddleware<SoapEndpointMiddleware>(typeof(T), path, encoder);
        }

        public static IApplicationBuilder UseSoapEndpointMiddleware<T>(
            this IApplicationBuilder builder,
            string path,
            Binding binding)
        {
            // Automatically extract the encoder based on the binding
            var encoder = binding.CreateBindingElements()
                .Find<MessageEncodingBindingElement>()
                ?.CreateMessageEncoderFactory()
                .Encoder;

            return builder.UseMiddleware<SoapEndpointMiddleware>(typeof(T), path, encoder);
        }
    }
}
