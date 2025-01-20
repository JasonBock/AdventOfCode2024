using System.Collections.Immutable;

namespace AdventOfCode2024.Day23;

public static class SolutionDay23
{
	public static long RunPart1(ImmutableArray<string> input)
	{
		var threeSets = new HashSet<string>();
		var connections = new List<Connection>();

		foreach (var line in input)
		{
			var lineParts = line.Split('-');
			connections.Add(
				new Connection(new Computer(lineParts[0]), new Computer(lineParts[1])));
		}

		foreach (var connection in connections)
		{
			var leftConnections = connections.Where(
				_ => _ != connection &&
					(_.Left == connection.Left || _.Right == connection.Left));

			foreach (var leftConnection in leftConnections)
			{
				var completeConnections = connections.Where(
					_ => (_ != leftConnection && _ != connection) &&
						(_.Left == connection.Right && _.Right == (leftConnection.Left == connection.Left ? leftConnection.Right : leftConnection.Left)) ||
						(_.Right == connection.Right && _.Left == (leftConnection.Left == connection.Left ? leftConnection.Right : leftConnection.Left)));

				foreach (var completeConnection in completeConnections)
				{
					var threeSet = new SortedSet<string>
					{
						connection.Left.Name, connection.Right.Name,
						leftConnection.Left.Name, leftConnection.Right.Name,
						completeConnection.Left.Name, completeConnection.Right.Name
					};

					if (threeSet.Any(_ => _.StartsWith('t')))
					{
						threeSets.Add(string.Join('-', threeSet));
					}
				}
			}

			// TODO...?: need right side as well.
		}

		return threeSets.Count;
	}
}

public sealed record Computer(string Name);
public sealed record Connection(Computer Left, Computer Right);