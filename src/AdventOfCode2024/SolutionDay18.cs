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

		//return PathFindingUsingBacktracking.findShortestPathLength(maze, [0, 0], [mazeSize - 1, mazeSize - 1]);
		return PathFindingUsingBFS.BFS(maze, new(0, 0), new(mazeSize - 1, mazeSize - 1));
	}
}

public sealed record Position(int X, int Y);

// Lifted from: https://www.geeksforgeeks.org/shortest-path-in-a-binary-maze/
public static class PathFindingUsingBacktracking
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
	static bool[,] visited;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

	// Check if it is possible to go to (x, y) from the
	// current position. The function returns false if the
	// cell has value 0 or already visited
	private static bool isSafe(int[,] matrix, bool[,] visited, int x, int y) =>
		(x >= 0 && x < matrix.GetLength(0) && y >= 0 && y < matrix.GetLength(1) &&
			matrix[x, y] == 1 && !visited[x, y]);

	private static int findShortestPath(int[,] mat, int i, int j,
		int x, int y, int min_dist,
		int dist)
	{

		if (i == x && j == y)
		{
			min_dist = Math.Min(dist, min_dist);
			return min_dist;
		}

		// set (i, j) cell as visited
		visited[i, j] = true;
		// go to the bottom cell
		if (isSafe(mat, visited, i + 1, j))
		{
			min_dist = findShortestPath(mat, i + 1, j, x, y,
												 min_dist, dist + 1);
		}
		// go to the right cell
		if (isSafe(mat, visited, i, j + 1))
		{
			min_dist = findShortestPath(mat, i, j + 1, x, y,
												 min_dist, dist + 1);
		}
		// go to the top cell
		if (isSafe(mat, visited, i - 1, j))
		{
			min_dist = findShortestPath(mat, i - 1, j, x, y,
												 min_dist, dist + 1);
		}
		// go to the left cell
		if (isSafe(mat, visited, i, j - 1))
		{
			min_dist = findShortestPath(mat, i, j - 1, x, y,
												 min_dist, dist + 1);
		}
		// backtrack: remove (i, j) from the visited matrix
		visited[i, j] = false;
		return min_dist;
	}

	// Wrapper over findShortestPath() function
	public static int findShortestPathLength(
		int[,] matrix, int[] source, int[] destination)
	{
		ArgumentNullException.ThrowIfNull(matrix);
		ArgumentNullException.ThrowIfNull(source);
		ArgumentNullException.ThrowIfNull(destination);

		if (matrix.GetLength(0) == 0
			 || matrix[source[0], source[1]] == 0
			 || matrix[destination[0], destination[1]] == 0)
			return -1;

		var row = matrix.GetLength(0);
		var col = matrix.GetLength(1);

		// construct an `M × N` matrix to keep track of
		// visited cells
		visited = new bool[row, col];
		for (var i = 0; i < row; i++)
		{
			for (var j = 0; j < col; j++)
				visited[i, j] = false;
		}

		var dist = Int32.MaxValue;
		dist = findShortestPath(matrix, source[0], source[1],
										destination[0], destination[1], dist, 0);

		if (dist != Int32.MaxValue)
			return dist;
		return -1;
	}
}

#pragma warning disable CA1034 // Nested types should not be visible
#pragma warning disable CA1051 // Do not declare visible instance fields
public static class PathFindingUsingBFS
{
	//static int ROW = 9;
	//static int COL = 10;

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