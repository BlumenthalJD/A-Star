using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AStar
{
	class Program
	{
		static void Main(string[] args)
		{
			List<List<int>> map = new List<List<int>>
			{
				new List<int>{0, 0, 0, 0, 0, 0, 0, 0, 0},
				new List<int>{0, 1, 1, 1, 0, 1, 1, 1, 0},
				new List<int>{0, 1, 1, 1, 0, 1, 1, 1, 0},
				new List<int>{0, 1, 1, 1, 0, 1, 1, 1, 0},
				new List<int>{0, 1, 1, 1, 0, 1, 1, 1, 0},
				new List<int>{0, 1, 1, 1, 0, 1, 1, 1, 0},
				new List<int>{0, 1, 1, 1, 0, 1, 1, 1, 0},
				new List<int>{0, 1, 1, 1, 0, 1, 1, 1, 0},
				new List<int>{0, 0, 0, 0, 0, 0, 0, 0, 0}
			};

			Vertex start = new Vertex(null, new List<int> { 0, 0 });
			Vertex end = new Vertex(null, new List<int> { 8, 8 });

			List<List<int>> path = AStar(map, start, end);

			for (int i = 0; i < path.Count; i++)
			{
				Console.WriteLine($"({path[i][0]}, {path[i][1]})");
			}

		}

		static List<List<int>> AStar(List<List<int>> map, Vertex start, Vertex end)
		{
			//start stopwatch to get runtime
			var watch = Stopwatch.StartNew();

			List<Vertex> openList = new List<Vertex>();
			List<Vertex> closedList = new List<Vertex>();
			List<List<int>> positions = new List<List<int>>
				{
					new List<int>{0, -1},
					new List<int>{0, 1},
					new List<int>{-1, 0},
					new List<int>{1, 0},
					new List<int>{-1, -1},
					new List<int>{-1, 1},
					new List<int>{1, -1},
					new List<int>{1, 1}
				};

			openList.Add(start);

			while (openList.Count > 0)
			{
				var currentVertex = openList[0];
				var currentIndex = 0;

				for (int i = 0; i < openList.Count; i++)
				{
					if (openList[i].fScore < currentVertex.fScore)
					{
						currentVertex = openList[i];
						currentIndex = i;
					}
				}

				openList.RemoveAt(currentIndex);
				closedList.Add(currentVertex);

				if (currentVertex.position.SequenceEqual(end.position))
				{
					List<List<int>> path = new List<List<int>>();
					var current = currentVertex;
					while (current != null)
					{
						path.Add(current.position);
						current = current.parent;
					}
					path.Reverse();

					//stop runtime stopwatch
					watch.Stop();

					//write elapsed ms of A*
					Console.WriteLine($"\nRuntime: {watch.Elapsed.TotalMilliseconds}ms");

					return path;
				}

				List<Vertex> children = new List<Vertex>();

				foreach (var position in positions)
				{
					var vertexPosition = new List<int> { currentVertex.position[0] + position[0], currentVertex.position[1] + position[1] };

					if ((vertexPosition[0] > map.Count - 1) || (vertexPosition[0] < 0) || (vertexPosition[1] > map[0].Count - 1) || (vertexPosition[1] < 0)) continue;

					if (map[vertexPosition[0]][vertexPosition[1]] != 0) continue;

					var newVertex = new Vertex(currentVertex, vertexPosition);

					children.Add(newVertex);
				}

				foreach (var child in children)
				{
					foreach (var closed in closedList)
					{
						if (child.position.SequenceEqual(closed.position)) continue;
					}

					child.gScore = currentVertex.gScore + 1;
					child.hScore = Math.Pow(child.position[0] - end.position[0], 2) + Math.Pow(child.position[1] - end.position[1], 2);
					child.fScore = child.gScore + child.hScore;

					foreach (var open in openList)
					{
						if (child.position.SequenceEqual(open.position) && child.gScore > open.gScore) continue;
					}

					openList.Add(child);
				}
			}

			return new List<List<int>>();
		}
	}
}
