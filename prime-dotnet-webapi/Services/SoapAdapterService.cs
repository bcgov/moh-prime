using System;
using System.IO;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using SoapCore.Extensibility;

namespace Prime.Services
{
    public class SoapServiceOperationTuner : IServiceOperationTuner
    {
        public void Tune(HttpContext httpContext, object serviceInstance,
            SoapCore.ServiceModel.OperationDescription operation)
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

        [OperationContract(Name = "PRPM_IN000000CA")]
        void Test(RequestObject controlActProcess);
    }

    public class SoapService : ISoapService
    {
        public HttpRequest Request { get; set; }

        public async void AddBcProvider()
        {
            // var reader = new StreamReader(Request.Body);
            // var text = await reader.ReadToEndAsync();
            //
            // Console.WriteLine(text);
            Console.WriteLine(Request.Body);
        }

        public async void UpdateBcProvider()
        {
            Console.WriteLine(Request.Body);
        }

        public async void Test(RequestObject controlActProcess)
        {
            Console.WriteLine(controlActProcess); // Always null
        }
    }

    [DataContract]
    public class RequestObject
    {
        [DataMember]
        public XElement controlActProcess { get; set; } // Deeply nested XML body
    }
}
