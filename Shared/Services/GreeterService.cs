using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;

namespace Shared.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            List<string> results = request.Results.ToList();
            Console.WriteLine(results.Count);
            return Task.FromResult(new HelloReply
            {
                Message = "Successful ? "
            });
        }
    }
}
