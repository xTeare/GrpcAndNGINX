using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Shared.Model
{
    [DataContract]
    public class HelloRequestCode1st
    {
        [DataMember(Order = 1)]
        public List<string> Names { get; set; }
    }
}
