using System.ServiceModel;
using UserStorage.Diagnostics.Query;

namespace UserStorage.Diagnostics
{
    [ServiceContract(Name = "Monitor", ConfigurationName = "ServiceMonitor")]
    public interface IServiceMonitor
    {
        [OperationContract(Name = "GetServicesCount")]
        int GetCount();

        [OperationContract(Name = "QueryServices")]
        ServiceInfo[] Query(ServiceQuery queryType);
    }
}
