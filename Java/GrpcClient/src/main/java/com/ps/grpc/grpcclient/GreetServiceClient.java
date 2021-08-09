/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.ps.grpc.grpcclient;

import com.ps.grpc.Messages.Greet;
import com.ps.grpc.Messages.GreeterGrpc;
import io.grpc.Channel;
import io.grpc.ClientInterceptors;
import io.grpc.ManagedChannel;
import io.grpc.Metadata;
import io.grpc.netty.shaded.io.grpc.netty.GrpcSslContexts;
import io.grpc.netty.shaded.io.grpc.netty.NettyChannelBuilder;
import io.grpc.stub.MetadataUtils;
import java.io.File;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.util.concurrent.TimeUnit;

/**
 *
 * @author T.Toepfer
 */
public class GreetServiceClient {
    public static void main(String[] args) throws Exception {
        
        // port 5001 - the server it self
        // port 5002 - nginx configured to call the server on 5001
        ManagedChannel channel = NettyChannelBuilder
                        .forAddress("localhost", 5002)
                        .sslContext(
                                GrpcSslContexts.forClient()
                                .trustManager(new File("certs/cert-self.crt")).build())
                .build();
        
        GreeterGrpc.GreeterBlockingStub blockingClient = 
              GreeterGrpc.newBlockingStub(channel);
        
        GreeterGrpc.GreeterStub nonblockingClient = 
              GreeterGrpc.newStub(channel);
        
        //sendMetadata(blockingClient);

        
        
        sendHelloRequest(blockingClient, 500);  // works work with Nginx 
        sendHelloRequest(blockingClient, 1000); // does not work with Nginx 
        
        
        Thread.sleep(1000);        
        channel.shutdown();        
        channel.awaitTermination(5, TimeUnit.SECONDS);
        
    }

    private static void sendMetadata(GreeterGrpc.GreeterBlockingStub blockingClient) {
        Metadata metadata = new Metadata();
        
        metadata.put(Metadata.Key.of("username", Metadata.ASCII_STRING_MARSHALLER), "John.Doe");
        
        metadata.put(Metadata.Key.of("password", Metadata.ASCII_STRING_MARSHALLER), "very secret");
        
        Channel channel = ClientInterceptors.intercept(blockingClient.getChannel(), MetadataUtils.newAttachHeadersInterceptor(metadata));
        
        
        Greet.HelloReply sayHello = blockingClient.withChannel(channel).sayHello(Greet.HelloRequest.newBuilder().build());
    }

    private static void sendHelloRequest(GreeterGrpc.GreeterBlockingStub blockingClient, int numberOfEntries) {
        final Greet.HelloRequest.Builder requestBuilder = Greet.HelloRequest.newBuilder();
        LogMessage("> Start calling server");
        
        for (int i = 0; i < numberOfEntries; i++) {
            requestBuilder.addResults("This is a long test string from Java Client. It should reach quite some size. I am Entry No.: " + i);
        }       
        
        Greet.HelloRequest request = requestBuilder.build();
                
        Greet.HelloReply reply = blockingClient.sayHello(request);
        
        LogMessage("-- > Finished calling greet.Greeter/SayHello. Reply: " + reply.getMessage());
        
        LogMessage("> End calling server. Duration: xxx");
    }
    
    
    private static void LogMessage(String message) {
            
        System.out.println(LocalDateTime.now().format(DateTimeFormatter.ISO_LOCAL_DATE_TIME) + " | " + message); 
    }
}
