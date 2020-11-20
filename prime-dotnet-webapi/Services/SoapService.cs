using System;
using System.IO;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Prime.Helpers;
using SoapCore.Extensibility;

namespace Prime.Services
{
    public class SoapServiceOperationTuner : IServiceOperationTuner
    {
        public void Tune(HttpContext httpContext,  object serviceInstance,  SoapCore.ServiceModel.OperationDescription operation)
        {
            if (serviceInstance is SoapService service)
            {
                service.Request = httpContext.Request;
            }
        }
    }

    [ServiceContract(Namespace = "urn:hl7-org:v3")]
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

        public HttpRequest Request { get; set; }

        public async void AddBcProvider()
        {
        }

        public async void UpdateBcProvider()
        {
        }
    }
}
