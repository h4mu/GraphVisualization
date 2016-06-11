using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GraphDataService.Query.Contract
{
    [DataContract]
    public class Graph
    {
        [DataMember]
        public IList<Vertex> Vertices { get; set; }
        [DataMember]
        public IList<Edge> Edges { get; set; }
    }
}