using System;
using System.ServiceModel;
using System.Xml.Linq;

namespace Prime.Services
{
    [ServiceContract(Namespace = "urn:hl7-org:v3")]
    public interface ISoapService
    {
        [OperationContract(Name = "PRPM_IN301030CA")]
        void AddBcProvider(XElement controlActProcess);

        [OperationContract(Name = "PRPM_IN303030CA")]
        void UpdateBcProvider(XElement controlActProcess);
    }

    public class SoapService : ISoapService
    {
        public void AddBcProvider(XElement controlActProcess)
        {
            Console.WriteLine("AddBcProvider");
            // TODO querying the object with Xpath or LINQ
            Console.WriteLine(controlActProcess);
        }

        public void UpdateBcProvider(XElement controlActProcess)
        {
            Console.WriteLine("UpdateBcProvider");
            // TODO querying the object with Xpath or LINQ
            Console.WriteLine(controlActProcess);
        }
    }
}
