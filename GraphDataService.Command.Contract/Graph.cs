using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GraphDataService.Command.Contract
{
    [DataContract]
    public class Graph
    {
        [DataMember]
        public IList<Vertex> Vertices { get; set; }
    }
}