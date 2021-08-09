/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.ps.grpc.server;
import com.ps.grpc.Messages.Greet;
import com.ps.grpc.Messages.GreeterGrpc;
import io.grpc.stub.StreamObserver;
/**
 *
 * @author T.Toepfer
 */
public class GreetService extends GreeterGrpc.GreeterImplBase{

    @Override
    public void sayHello(Greet.HelloRequest request, StreamObserver<Greet.HelloReply> responseObserver) {
        
        System.out.println("Greetings from Client came in...");
                
        Greet.HelloReply reply = Greet.HelloReply.newBuilder()  // reply = new Greet.HelloReply();
                        .setMessage("Hello, here is your JAVA Server")
                        .build();
                        
        responseObserver.onNext(reply);
        responseObserver.onCompleted();        
    }
    
        
}
