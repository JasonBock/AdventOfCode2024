using System.Collections.Immutable;
using System.Numerics;

namespace AdventOfCode2024.Day16;

public static class SolutionDay16
{
	private const char End = 'E';
	private const char Start = 'S';
   private const char Wall = '#';

   public static BigInteger RunPart1(ImmutableArray<string> input)
	{
		var mapItems = SolutionDay16.ParseInput(input);

		var minimalCost = 0;

		return minimalCost;
	}

	private static ImmutableArray<MapItem> ParseInput(ImmutableArray<string> input)
	{
		var mapItems = new List<MapItem>();

		for (var y = 0; y < input.Length; y++)
		{
			var data = input[y];

			for (var x = 0; x < data.Length; x++)
			{
				var mapType = data[x];

				if (mapType == Start || mapType == End || mapType == Wall)
				{
					mapItems.Add(new MapItem(mapType, new Position(x, y)));
				}
			}
		}

		return [.. mapItems];
	}
}

public enum Direction { West, North, East, South }
public sealed record Position(int X, int Y);
public sealed record MapItem(char Type, Position Position);
public sealed record Reindeer(Direction CurrentDirection, Position Position);