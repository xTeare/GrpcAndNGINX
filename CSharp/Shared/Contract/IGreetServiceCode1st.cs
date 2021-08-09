using System.ServiceModel;
using System.Threading.Tasks;
using Shared.Model;

namespace Shared.Contract
{
    [ServiceContract]
    public interface IGreetServiceCode1st
    {
        [OperationContract]
        ValueTask<Model.HelloReplyCode1st> SaveResultsAsync(Model.HelloRequestCode1st requestCode);
    }
}
