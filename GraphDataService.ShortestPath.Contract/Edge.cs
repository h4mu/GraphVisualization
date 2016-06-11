using System.Runtime.Serialization;

namespace GraphDataService.ShortestPath.Contract
{
    [DataContract]
    public class Edge
    {
        [DataMember]
        public int Vertex1 { get; set; }

        [DataMember]
        public int Vertex2 { get; set; }
    }
}