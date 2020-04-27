using System.ServiceModel;
using Prime.Models;

namespace Prime.Services
{
    [ServiceContract]
    public interface IExampleService
    {
        [OperationContract]
        string Test(string s);

        [OperationContract]
        ExampleModel GetModel();

        [OperationContract]
        ExampleModel SetModel(int id, string email);

        [OperationContract]
        void XmlMethod(System.Xml.Linq.XElement xml);
    }
}
