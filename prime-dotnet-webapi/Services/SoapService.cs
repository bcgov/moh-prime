using System;
using System.IO;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Xml;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SoapCore.Extensibility;

namespace Prime.Services
{
    [ServiceContract(Namespace = "urn:hl7-org:v3")]
    public interface ISoapService
    {
        // [OperationContract(Name = "PRPM_IN301030CA")]
        // void AddBcProvider();
        //
        // [OperationContract(Name = "PRPM_IN303030CA")]
        // void UpdateBcProvider();
        //
        // [OperationContract(Name = "PRPM_IN000000CA")]
        // void Example(int id, string email);
    }

    public class SoapService : ISoapService
    {
        private readonly ILogger<SoapService> _logger;

        public SoapService(
            ILogger<SoapService> logger)
        {
            _logger = logger;
        }

        // public HttpRequest Request { get; set; }

        // public async void AddBcProvider()
        // {
        //     Console.WriteLine(Request.Body);
        // }

        // public async void UpdateBcProvider()
        // {
        //     Console.WriteLine(Request.Body);
        // }

        // public async void PRPM_IN000000CA(ExampleModel example)
        // {
        //     Console.WriteLine($"Hello World!"); // Always null
        // }
    }

    // [DataContract]
    // public class RequestObject
    // {
    //     // [DataMember]
    //     // public XElement controlActProcess { get; set; }
    // }

    // [MessageContract(IsWrapped = false)]
    // public class ExampleModel
    // {
    //     [MessageBodyMember]
    //     public int Id { get; set; }
    //
    //     [MessageBodyMember]
    //     public string Email { get; set; }
    // }
}
