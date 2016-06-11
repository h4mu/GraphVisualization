using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GraphDataService.Command.Contract
{
    [DataContract]
    public class Vertex
    {
        [DataMember]
        public string Label { get; set; }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public IList<int> AdjacentNodeIds { get; set; }
    }
}