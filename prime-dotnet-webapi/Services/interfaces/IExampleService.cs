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
        void XmlMethod(System.Xml.Linq.XElement xml);

        [OperationContract]
        ExampleModel TestExampleModel(ExampleModel model);
    }
}
