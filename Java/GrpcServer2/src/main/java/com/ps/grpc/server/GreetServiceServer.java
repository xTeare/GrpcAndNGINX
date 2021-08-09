/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.ps.grpc.server;

import io.grpc.Server;
import io.grpc.ServerBuilder;
import io.grpc.ServerInterceptors;
import io.grpc.ServerServiceDefinition;
import io.grpc.netty.shaded.io.grpc.netty.NettyServerBuilder;
import java.io.File;
import java.net.InetSocketAddress;
import java.net.SocketAddress;

/**
 *
 * @author T.Toepfer
 */
public class GreetServiceServer {
    public static void main(String[] args) {
        try {
            GreetServiceServer greetServiceServer = new GreetServiceServer();
            greetServiceServer.start();
        }
        catch (Exception ex) {        
            System.err.println(ex);
        }
    }
    
    private Server server;
    
    private void start() throws Exception {
        final int port = 5001;
        
        File cert = new File("certs/cert-self.crt");
        File key = new File("certs/cert-self.key");
        
        InetSocketAddress socketAddress = new InetSocketAddress("localhost", port);
        
        GreetService greetService = new GreetService();        
        ServerServiceDefinition serviceDef = ServerInterceptors.interceptForward(greetService, new HeaderServerInterceptor());        
        
        server = NettyServerBuilder
                .forAddress(socketAddress)                
                .useTransportSecurity(cert, key)  
                .addService(serviceDef)                
                .build()
                .start();
        System.out.println("Listening on port " + port);
        
        Runtime.getRuntime().addShutdownHook(new Thread(){
            @Override
            public void run() {
                System.out.println("Shutting down server");
                GreetServiceServer.this.stop();
            }
        });
        
        server.awaitTermination();
    }
    
    private void stop() {
        if (server != null) {
            server.shutdown();
        }
    }
}
