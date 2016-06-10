using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GraphDataService.Command.Contract
{
    [DataContract]
    public class Vertex
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public IEnumerable<int> AdjacentNodeIds { get; set; }
    }
}