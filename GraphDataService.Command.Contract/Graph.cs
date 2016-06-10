using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GraphDataService.Command.Contract
{
    [DataContract]
    public class Graph
    {
        [DataMember]
        public IEnumerable<Vertex> Vertices { get; set; }
    }
}