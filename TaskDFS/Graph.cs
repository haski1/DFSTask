using System;
using System.Collections.Generic;
using System.Text;

namespace TaskDFS
{
    public class Graph
    {
        private readonly Node[] nodes;

        public Graph(int size, int[,] adjacencyMatrix)
        {
            nodes = new Node[size];
            for (var i = 1; i <= size; i++) nodes[i - 1] = new Node(i);

            for (var i = 0; i < size; i++)
            for (var j = 0; j < size; j++)
                if (adjacencyMatrix[i, j] == 1)
                    nodes[i].AddNeighbor(nodes[j]);
        }

        private bool DFS(Node node)
        {
            node.IsVisited = true;
            foreach (var neighbor in node.Neighbors)
                if (neighbor.IsVisited)
                {
                    if (neighbor.Color == node.Color)
                        return false;
                }
                else
                {
                    neighbor.InverseColor();
                    DFS(neighbor);
                }

            return true;
        }

        public bool IsBipartite()
        {
            return DFS(nodes[0]);
        }

        public Tuple<Node[], Node[]> GetParts()
        {
            var upperPart = new List<Node>();
            var lowerPart = new List<Node>();

            var upperColor = nodes[0].Color;

            foreach (var node in nodes)
                if (node.Color == upperColor)
                    upperPart.Add(node);
                else lowerPart.Add(node);

            return new Tuple<Node[], Node[]>(upperPart.ToArray(), lowerPart.ToArray());
        }

        public static string PartToLine(Node[] nodes)
        {
            var builder = new StringBuilder();
            foreach (var node in nodes)
            {
                builder.Append(node);
                builder.Append(' ');
            }
            builder.Append('0');
            return builder.ToString();
        }
    }

    public class Node
    {
        public Color Color;
        public bool IsVisited;
        public readonly List<Node> Neighbors;
        private readonly int number;

        public Node(int number)
        {
            this.number = number;
            Neighbors = new List<Node>();
            Color = Color.Black;
        }

        public void AddNeighbor(Node neighbor)
        {
            Neighbors.Add(neighbor);
        }

        public void InverseColor()
        {
            Color = Color == Color.Black ? Color.White : Color.Black;
        }

        public override string ToString()
        {
            return number.ToString();
        }
    }

    public enum Color
    {
        Black,
        White
    }
}