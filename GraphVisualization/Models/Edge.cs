using System;

namespace GraphVisualization.Models
{
    public class Edge : IEquatable<Edge>
    {
        public int From { get; set; }
        public int To { get; set; }

        public bool Equals(Edge other)
        {
            return From == other.From && To == other.To;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash = (hash * 16777619) ^ From.GetHashCode();
                hash = (hash * 16777619) ^ To.GetHashCode();
                return hash;
            }
        }
    }
}