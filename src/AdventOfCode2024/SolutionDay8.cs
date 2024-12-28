using System.Collections.Immutable;

namespace AdventOfCode2024.Day8;

public static class SolutionDay8
{
	public static long RunPart1(ImmutableArray<string> input)
	{
		var antennas = new List<Antenna>();

		for (var y = 0; y < input.Length; y++)
		{
			var line = input[y];

			for (var x = 0; x < line.Length; x++)
			{
				var frequency = line[x];

				if (frequency != '.')
				{
					antennas.Add(new(frequency, new(x, y)));
				}
			}
		}

		var maxX = input[0].Length;
		var maxY = input.Length;

		var uniqueAntinodes = new HashSet<Node>();

		foreach (var antenna in antennas)
		{
			var pairedAntennas = antennas.Where(
				_ => _.Frequency == antenna.Frequency &&
					_ != antenna);

			foreach (var pairedAntenna in pairedAntennas)
			{
				var difference = new Node(
					antenna.Position.X - pairedAntenna.Position.X,
					antenna.Position.Y - pairedAntenna.Position.Y);

				var firstAntinode = new Node(
					pairedAntenna.Position.X - difference.X,
					pairedAntenna.Position.Y - difference.Y);

				if (firstAntinode.X >= 0 && firstAntinode.X < maxX &&
					firstAntinode.Y >= 0 && firstAntinode.Y < maxY)
				{
					_ = uniqueAntinodes.Add(firstAntinode);
				}

				var secondAntinode = new Node(
					antenna.Position.X + difference.X,
					antenna.Position.Y + difference.Y);

				if (secondAntinode.X >= 0 && secondAntinode.X < maxX &&
					secondAntinode.Y >= 0 && secondAntinode.Y < maxY)
				{
					_ = uniqueAntinodes.Add(secondAntinode);
				}
			}
		}

		return uniqueAntinodes.Count;
	}

	public static long RunPart2(ImmutableArray<string> input)
	{
		var antennas = new List<Antenna>();

		for (var y = 0; y < input.Length; y++)
		{
			var line = input[y];

			for (var x = 0; x < line.Length; x++)
			{
				var frequency = line[x];

				if (frequency != '.')
				{
					antennas.Add(new(frequency, new(x, y)));
				}
			}
		}

		var maxX = input[0].Length;
		var maxY = input.Length;

		var uniqueAntinodes = new HashSet<Node>();

		foreach (var antenna in antennas)
		{
			_ = uniqueAntinodes.Add(antenna.Position);
			var pairedAntennas = antennas.Where(
				_ => _.Frequency == antenna.Frequency &&
					_ != antenna);

			foreach (var pairedAntenna in pairedAntennas)
			{
				_ = uniqueAntinodes.Add(pairedAntenna.Position);

				var difference = new Node(
					antenna.Position.X - pairedAntenna.Position.X,
					antenna.Position.Y - pairedAntenna.Position.Y);

				var firstAntinode = new Node(
					pairedAntenna.Position.X,
					pairedAntenna.Position.Y);

				while (true)
				{
					firstAntinode = new Node(
						firstAntinode.X - difference.X,
						firstAntinode.Y - difference.Y);

					if (firstAntinode.X >= 0 && firstAntinode.X < maxX &&
						firstAntinode.Y >= 0 && firstAntinode.Y < maxY)
					{
						_ = uniqueAntinodes.Add(firstAntinode);
					}
					else
					{
						break;
					}
				}

				var secondAntinode = new Node(
					antenna.Position.X,
					antenna.Position.Y);

				while (true)
				{
					secondAntinode = new Node(
						secondAntinode.X + difference.X,
						secondAntinode.Y + difference.Y);
				
					if (secondAntinode.X >= 0 && secondAntinode.X < maxX &&
						secondAntinode.Y >= 0 && secondAntinode.Y < maxY)
					{
						_ = uniqueAntinodes.Add(secondAntinode);
					}
					else
					{
						break;
					}
				}
			}
		}

		return uniqueAntinodes.Count;
	}
}

public sealed record Antenna(char Frequency, Node Position);
public sealed record Node(int X, int Y);