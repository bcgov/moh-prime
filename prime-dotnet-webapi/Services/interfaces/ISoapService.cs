using System.ServiceModel;
using System.Runtime.Serialization;

namespace Prime.Services
{
    [ServiceContract(Namespace = "urn:hl7-org:v3")]
    public interface ISoapService
    {
        [OperationContract(Name = "PRPM_IN301030CA")]
        void Request(System.Xml.Linq.XElement controlActProcess);

    }


}
