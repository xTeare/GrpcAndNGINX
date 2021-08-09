/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.ps.grpc.server;

import io.grpc.Metadata;
import io.grpc.ServerCall;
import io.grpc.ServerCallHandler;
import io.grpc.ServerInterceptor;

/**
 *
 * @author T.Toepfer
 */
public class HeaderServerInterceptor implements ServerInterceptor {

    @Override
    public <ReqT, RespT> ServerCall.Listener<ReqT> interceptCall(
            ServerCall<ReqT, RespT> sc, 
            Metadata md, 
            ServerCallHandler<ReqT, RespT> next) {
        
        System.out.println("Has called " + sc.getMethodDescriptor().getFullMethodName());
        
        if (sc.getMethodDescriptor().getFullMethodName().equalsIgnoreCase("greet.Greeter/SayHello")) {
        
            for(String key : md.keys()) { 
                System.out.println(key + ": " + md.get(Metadata.Key.of(key, Metadata.ASCII_STRING_MARSHALLER)));
            }
        }      
        
        return next.startCall(sc, md);        
    }
    
}
