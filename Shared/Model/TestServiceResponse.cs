﻿using System.Runtime.Serialization;

namespace Shared.Model
{
    [DataContract]
    public class TestServiceResponse
    {
        [DataMember(Order = 1)] public string Message { get; set; }

        [DataMember(Order = 2)] public bool Success { get; set; }
    }
}
