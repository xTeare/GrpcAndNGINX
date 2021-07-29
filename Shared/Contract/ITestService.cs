using System;
using System.Collections.Generic;
using System.Text;
using Shared.Model;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Shared.Contract
{
    [ServiceContract]
    public interface ITestService
    {
        ValueTask<TestServiceResponse> SaveDiscoveryResultAsync(TestServiceRequest request);
    }
}
