using System.Runtime.Serialization;

namespace GraphDataService.Query.Contract
{
    [DataContract]
    public class Vertex
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Label { get; set; }
    }
}