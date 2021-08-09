using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Contract;
using Shared.Model;

namespace Shared.Implementation
{
    public class GreetServiceCode1St : IGreetServiceCode1st
    {
        public async ValueTask<Model.HelloReplyCode1st> SaveResultsAsync(Model.HelloRequestCode1st requestCode1St)
        {
            Model.HelloReplyCode1st response = new Model.HelloReplyCode1st();

            try
            {
                List<string> results = requestCode1St.Names;
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
