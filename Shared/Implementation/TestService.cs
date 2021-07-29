using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Contract;
using Shared.Model;

namespace Shared.Implementation
{
    public class TestService : ITestService
    {
        public async ValueTask<TestServiceResponse> SaveResultsAsync(TestServiceRequest request)
        {
            TestServiceResponse response = new TestServiceResponse();

            try
            {
                List<string> results = request.LongList;
                int resultCount = results?.Count ?? 0;

                if (resultCount > 0)
                {
                    Console.WriteLine($"Got List with {resultCount} entries.");
                }

                response.Success = true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
