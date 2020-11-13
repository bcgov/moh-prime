using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Prime.Services
{
    public static class SoapServiceNamespace
    {
        public const string Value = "urn:hl7-org:v3";
    }

    [ServiceContract(Namespace = SoapServiceNamespace.Value)]
    public interface ISoapService
    {
        [OperationContract(Name = "PRPM_IN301030CA")]
        void AddBcProvider(RequestObject controlActProcess);

        [OperationContract(Name = "PRPM_IN303030CA")]
        void UpdateBcProvider(RequestObject controlActProcess);
    }

    public class SoapService : ISoapService
    {
        public void AddBcProvider(RequestObject request)
        {
            Console.WriteLine("AddBcProvider");
            Console.WriteLine(request);
        }

        public void UpdateBcProvider(RequestObject request)
        {
            Console.WriteLine("UpdateBcProvider");
            Console.WriteLine(request);
        }
    }

    [DataContract]
    public class RequestObject
    {
        [DataMember]
        public XElement controlActProcess { get; set; }
    }
}
