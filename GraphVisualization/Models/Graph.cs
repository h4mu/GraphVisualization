using System.Collections.Generic;

namespace GraphVisualization.Models
{
    public class Graph
    {
        public IEnumerable<Node> Nodes { get; set; }
        public IEnumerable<Edge> Edges { get; set; }
    }
}