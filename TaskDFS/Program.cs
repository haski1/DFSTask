using System;
using System.IO;
using System.Linq;

namespace TaskDFS
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length == 0)
                return;

            using var inputFile = File.OpenText(args[0]);

            var size = int.Parse(inputFile.ReadLine() ?? throw new ArgumentException());
            var matrix = new int[size, size];

            for (var i = 0; i < size; i++)
            {
                var line = inputFile.ReadLine();
                if (line == null) continue;
                var numbers = line
                    .Split(' ')
                    .Select(int.Parse)
                    .ToArray();

                for (var j = 0; j < size; j++) matrix[i, j] = numbers[j];
            }

            using var outputFile = File.CreateText(args[1]);
            var graph = new Graph(size, matrix);

            if (graph.IsBipartite())
            {
                outputFile.WriteLine("Y");
                var (upper, lower) = graph.GetParts();
                outputFile.WriteLine(Graph.PartToLine(upper));
                outputFile.WriteLine(Graph.PartToLine(lower));
            }
            else
            {
                outputFile.WriteLine("N");
            }
        }
    }
}