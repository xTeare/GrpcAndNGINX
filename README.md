# UPDATE
This issue has been fixed: https://trac.nginx.org/nginx/ticket/2229
It was indeed a bug within nginx.


# C# GrpcAndNGINX

Grpc Requests time out if the request is too big. Neither changes in the server's config nor in the nginx config seem to fix this issue.
But the ```grpc_send_timeout 90s;``` nginx setting does effect it in the way, that it will timeout after the specified amount. But it will always fail, 
even if set to a large timeout

[Stackoverflow](https://stackoverflow.com/questions/68547210/c-sharp-nginx-upstream-timed-out)

# Errors

### Grpc.Core.RpcException
```Grpc.Core.RpcException: 'Status(StatusCode="Unavailable", Detail="Bad gRPC response. HTTP status code: 504")'```

### nginx error.log
```
2021/07/29 12:29:26 [error] 20604#30660: *36 upstream timed out (10060: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond) while sending request to upstream, client: 127.0.0.1, server: , request: "POST /Shared.Contract.TestService/SaveDiscoveryResult HTTP/2.0", upstream: "grpcs://127.0.0.1:5001", host: "localhost:5002"
```

nginx is listening on port 50051

Server is hosting on port 50052

# Used Packages

### Server

[Grpc.AspNetCore](https://www.nuget.org/packages/Grpc.AspNetCore/2.27.0)

[Grpc.AspNetCore.Server](https://www.nuget.org/packages/Grpc.AspNetCore.Server/2.38.0)

[protobuf-net.Grpc.AspNetCore](https://www.nuget.org/packages/protobuf-net.Grpc.AspNetCore/1.0.152)

[protobuf-net.Grpc.AspNetCore.Reflection](https://www.nuget.org/packages/protobuf-net.Grpc.AspNetCore.Reflection/1.0.152)


### Client

[Grpc.Net.Client](https://www.nuget.org/packages/Grpc.Net.Client/2.38.0)

[protobuf-net.Grpc.Native](https://www.nuget.org/packages/protobuf-net.Grpc.Native/1.0.152)

# Certificate

The Certificate was generated with openssl and the following commands:

### Generate CRT + KEY
```openssl req -x509 -nodes -days 365 -newkey rsa:2048 -keyout cert-self.key -out cert-self.crt```


### Generate PFX file
```openssl pkcs12 -export -out cert-self.pfx -inkey cert-self.key -in cert-self.crt```
