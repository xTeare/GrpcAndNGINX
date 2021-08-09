using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Client.Properties;
using Grpc.Net.Client;
using ProtoBuf.Grpc.Client;
using Shared.Contract;
using Shared.Model;

namespace Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await WaitForStartup().ConfigureAwait(false);
            await CallServer(500).ConfigureAwait(false); // 500 work for me
            await CallServer(1000).ConfigureAwait(false); // 1000 dont work for me - will timeout after 90 seconds (nginx.conf -> grpc_send_timeout 90s;)
            Console.Read();
        }
        
        private static async Task CallServer(int entryCount)
        {
            LogMessage("> Start calling server");
            Stopwatch sw = new Stopwatch();
            sw.Start();

            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            handler.ClientCertificates.Add(new X509Certificate2(Resources.cert_self, "Password123!"));
            LogMessage("-- > Created HttpClientHandler with certificate");

            // port 5001 - the server it self
            // port 5002 - nginx configured to call the server on 5001
            GrpcChannel channel = GrpcChannel.ForAddress("https://localhost:5002", new GrpcChannelOptions() { HttpHandler = handler }); //, new GrpcChannelOptions(){HttpHandler = handler}
            IGreetServiceCode1st greetService = channel.CreateGrpcService<IGreetServiceCode1st>();
            LogMessage("-- > Created GrpcChannel and GreetServiceCode1St");

            int entries = entryCount;
            List<string> list = new List<string>(entries);

            for(int i = 0; i < entries; i++)
                list.Add($"This is a long string for testing purposes. It should reach quite some size. I am Entry No.: {i}");

            HelloRequestCode1st requestCode1St = new HelloRequestCode1st() { Names = list };
            LogMessage($"-- > Created test data ({entries} entries)");


            LogMessage("-- > Start calling GreetServiceCode1St.");
            HelloReplyCode1st response = await greetService.SaveResultsAsync(requestCode1St).ConfigureAwait(false);
            LogMessage($"-- > Finished calling GreetServiceCode1St. Success: {response.Success}");

            sw.Stop();
            LogMessage($"> End calling server. Duration: {sw.Elapsed}");
            Console.WriteLine();
        }

        private static void LogMessage(string message) => Console.WriteLine($"{DateTime.Now:o} | {message}");
        private static async Task WaitForStartup() => await Task.Delay(5000).ConfigureAwait(false);
    }
}
