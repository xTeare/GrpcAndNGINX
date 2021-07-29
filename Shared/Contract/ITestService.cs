using System.ServiceModel;
using System.Threading.Tasks;
using Shared.Model;

namespace Shared.Contract
{
    [ServiceContract]
    public interface ITestService
    {
        [OperationContract]
        ValueTask<TestServiceResponse> SaveResultsAsync(TestServiceRequest request);
    }
}
