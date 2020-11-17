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

        // [OperationContract(Name = "PRPM_IN000000CA")]
        // void Example(int id, string email);

        [OperationContract]
        void PRPM_IN000000CA(ExampleModel example);
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

        // TODO WORKS NO TOUCHY
        // public async void PRPM_IN000000CA(ExampleModel example)
        // {
        //     Console.WriteLine($"Hello World!"); // Always null
        // }

        public async void PRPM_IN000000CA(ExampleModel example)
        {
            Console.WriteLine($"Hello World!"); // Always null
        }
    }

    [DataContract]
    public class RequestObject
    {
        // [DataMember]
        // public XElement controlActProcess { get; set; }
    }

    // TODO WORKS NO TOUCHY
    // [MessageContract(IsWrapped = false)]
    // public class ExampleModel
    // {
    //     [MessageBodyMember]
    //     public int Id { get; set; }
    //
    //     [MessageBodyMember]
    //     public string Email { get; set; }
    // }

    [MessageContract(IsWrapped = false)]
    public class ExampleModel
    {
        [MessageBodyMember]
        public int Id { get; set; }

        [MessageBodyMember]
        public string Email { get; set; }

        // [MessageBodyMember]
        // public XElement controlActProcess { get; set; }
    }
}

// TODO WORKS NO TOUCHY
// <?xml version="1.0" encoding="utf-8"?>
// <soap:Envelope xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
//     <soap:Body>
//         <PRPM_IN000000CA ITSVersion="XML_1.0" xmlns="urn:hl7-org:v3">
//             <Id>1</Id>
//             <Email>tide@pods.com</Email>
//         </PRPM_IN000000CA>
//     </soap:Body>
// </soap:Envelope>
