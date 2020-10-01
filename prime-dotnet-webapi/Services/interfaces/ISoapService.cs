using System.ServiceModel;
using Prime.Models;

namespace Prime.Services
{
    [ServiceContract]
    public interface ISoapService
    {
        [OperationContract]
        string Test(string s);

        [OperationContract]
        SoapObject GetModel();

        [OperationContract]
        SoapObject SetModel(int id, string email);

        [OperationContract]
        void XmlMethod(System.Xml.Linq.XElement xml);
    }
}
