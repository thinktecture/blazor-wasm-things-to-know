using System;
using System.Runtime.Serialization;

namespace BlazorWasmVirtualization.Shared.DTO
{
    [DataContract]
    public class ConferenceOverview
    {
        [DataMember(Order=1)]
        public Guid ID { get; set; }
        
        [DataMember(Order=2)]
        public string Title { get; set; }
    }
}
