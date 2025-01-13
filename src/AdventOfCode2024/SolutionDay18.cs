using System.Collections.Immutable;
using System.Globalization;

namespace AdventOfCode2024.Day18;

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional
public static class SolutionDay18
{
	public static int RunPart1(ImmutableArray<string> input, int bytesToProcess, int mazeSize)
	{
		var corruptions = new HashSet<Position>();

		for (var i = 0; i < bytesToProcess; i++)
		{
			var position = input[i].Split(',');
			corruptions.Add(new Position(
				int.Parse(position[0], CultureInfo.CurrentCulture),
				int.Parse(position[1], CultureInfo.CurrentCulture)));
		}

		var maze = new int[mazeSize, mazeSize];

		for (var x = 0; x < mazeSize; x++)
		{
			for (var y = 0; y < mazeSize; y++)
			{
				maze[x, y] = corruptions.Contains(new Position(x, y)) ? 0 : 1;
			}
		}

		return PathFindingUsingBFS.BFS(maze, new(0, 0), new(mazeSize - 1, mazeSize - 1));
	}

	public static string RunPart2(ImmutableArray<string> input, int mazeSize)
	{
		var corruptions = new HashSet<Position>();

		for (var i = 0; i < input.Length; i++)
		{
			var position = input[i].Split(',');
			corruptions.Add(new Position(
				int.Parse(position[0], CultureInfo.CurrentCulture),
				int.Parse(position[1], CultureInfo.CurrentCulture)));

			var maze = new int[mazeSize, mazeSize];

			for (var x = 0; x < mazeSize; x++)
			{
				for (var y = 0; y < mazeSize; y++)
				{
					maze[x, y] = corruptions.Contains(new Position(x, y)) ? 0 : 1;
				}
			}

			if (PathFindingUsingBFS.BFS(maze, new(0, 0), new(mazeSize - 1, mazeSize - 1)) == -1)
			{
				return input[i];
			}
		}

		throw new NotSupportedException();
	}
}

public sealed record Position(int X, int Y);

// Lifted from: https://www.geeksforgeeks.org/shortest-path-in-a-binary-maze/
#pragma warning disable CA1034 // Nested types should not be visible
#pragma warning disable CA1051 // Do not declare visible instance fields
public static class PathFindingUsingBFS
{
	// To store matrix cell coordinates
	public class Point
	{
		public int x;
		public int y;

		public Point(int x, int y)
		{
			this.x = x;
			this.y = y;
		}
	};

	// A Data Structure for queue used in BFS
	public class queueNode
	{
		// The coordinates of a cell
		public Point pt;

		// cell's distance of from the source
		public int dist;

		public queueNode(Point pt, int dist)
		{
			this.pt = pt;
			this.dist = dist;
		}
	};

	// check whether given cell (row, col) 
	// is a valid cell or not.
	static bool isValid(int row, int col, int matrixLength) =>
		// return true if row number and 
		// column number is in range
		(row >= 0) && (row < matrixLength) &&
		(col >= 0) && (col < matrixLength);

	// These arrays are used to get row and column
	// numbers of 4 neighbours of a given cell
	static int[] rowNum = { -1, 0, 0, 1 };
	static int[] colNum = { 0, -1, 1, 0 };

	// function to find the shortest path between
	// a given source cell to a destination cell.
	public static int BFS(int[,] matrix,
		Point source, Point destination)
	{
		ArgumentNullException.ThrowIfNull(matrix);
		ArgumentNullException.ThrowIfNull(source);
		ArgumentNullException.ThrowIfNull(destination);

		// check source and destination cell
		// of the matrix have value 1
		if (matrix[source.x, source.y] != 1 ||
			 matrix[destination.x, destination.y] != 1)
			return -1;

		var matrixLength = (int)Math.Sqrt(matrix.Length);
		var visited = new bool[matrixLength, matrixLength];

		// Mark the source cell as visited
		visited[source.x, source.y] = true;

		// Create a queue for BFS
		var q = new Queue<queueNode>();

		// Distance of source cell is 0
		var s = new queueNode(source, 0);
		q.Enqueue(s); // Enqueue source cell

		// Do a BFS starting from source cell
		while (q.Count != 0)
		{
			var curr = q.Peek();
			var pt = curr.pt;

			// If we have reached the destination cell,
			// we are done
			if (pt.x == destination.x && pt.y == destination.y)
				return curr.dist;

			// Otherwise dequeue the front cell 
			// in the queue and enqueue
			// its adjacent cells
			q.Dequeue();

			for (var i = 0; i < 4; i++)
			{
				var row = pt.x + rowNum[i];
				var col = pt.y + colNum[i];

				// if adjacent cell is valid, has path 
				// and not visited yet, enqueue it.
				if (isValid(row, col, matrixLength) &&
						  matrix[row, col] == 1 &&
					!visited[row, col])
				{
					// mark cell as visited and enqueue it
					visited[row, col] = true;
					var Adjcell = new queueNode
								  (new Point(row, col),
										 curr.dist + 1);
					q.Enqueue(Adjcell);
				}
			}
		}

		// Return -1 if destination cannot be reached
		return -1;
	}
}