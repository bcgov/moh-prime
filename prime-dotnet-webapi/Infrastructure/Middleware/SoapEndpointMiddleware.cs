using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
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
                using var reader = new StreamReader(httpContext.Request.Body);
                var requestBody = await reader.ReadToEndAsync();

                // Example 1 (XDocument and XPath)
                var xDocument = XDocument.Parse(requestBody);

                var xmlnsManager = new XmlNamespaceManager(new NameTable());
                xmlnsManager.AddNamespace("hl7", "urn:hl7-org:v3");

                var root = xDocument.XPathSelectElements("//hl7:PRPM_IN301030CA", xmlnsManager);

                if (root != null)
                {
                    var sb = new StringBuilder(1024);
                    foreach (var el in root)
                    {
                        sb.Append(el);
                    }

                    await httpContext.Response.WriteAsync(sb.ToString());
                }
                else
                {
                    await httpContext.Response.WriteAsync("Not Found");
                }

                // Example 2 (XmlDocument and XPath)
                // var xmlDocument = new XmlDocument();
                // xmlDocument.LoadXml(requestBody);
                //
                // var xmlnsManager = new XmlNamespaceManager(xmlDocument.NameTable);
                // xmlnsManager.AddNamespace("hl7", "urn:hl7-org:v3");
                //
                // var root = xmlDocument.SelectSingleNode("//hl7:PRPM_IN301030CA", xmlnsManager);
                //
                // if (root != null)
                // {
                //     await httpContext.Response.WriteAsync(root.InnerXml);
                // }
                // else
                // {
                //     await httpContext.Response.WriteAsync("Not Found");
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
