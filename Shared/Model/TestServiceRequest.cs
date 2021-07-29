using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Shared.Model
{
    [DataContract]
    public class TestServiceRequest
    {
        [DataMember(Order = 1)] public List<string> LongList { get; set; }
    }
}
