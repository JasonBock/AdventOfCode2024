using System.Collections.Immutable;

namespace AdventOfCode2024.Day4;

public static class SolutionDay4
{
	private const string TargetWord = "MAS";

	public static int RunPart1(ImmutableArray<string> input)
	{
		var xMax = input[0].Length;
		var yMax = input.Length;

		var xmasCount = 0;

		for (var y = 0; y < yMax; y++)
		{
			var line = input[y];

			for (var x = 0; x < xMax; x++)
			{
				if (line[x] == 'X')
				{
					// West.
					if (x >= 3 && $"{line[x - 1]}{line[x - 2]}{line[x - 3]}" == TargetWord)
					{
						xmasCount++;
					}

					// NorthWest.
					if (x >= 3 && y >= 3 && $"{input[y - 1][x - 1]}{input[y - 2][x - 2]}{input[y - 3][x - 3]}" == TargetWord)
					{
						xmasCount++;
					}

					// North.
					if (y >= 3 && $"{input[y - 1][x]}{input[y - 2][x]}{input[y - 3][x]}" == TargetWord)
					{
						xmasCount++;
					}

					// NorthEast.
					if (x < xMax - 3 && y >= 3 && $"{input[y - 1][x + 1]}{input[y - 2][x + 2]}{input[y - 3][x + 3]}" == TargetWord)
					{
						xmasCount++;
					}

					// East.
					if (x < xMax - 3 && $"{line[x + 1]}{line[x + 2]}{line[x + 3]}" == TargetWord)
					{
						xmasCount++;
					}

					// SouthEast.
					if (x < xMax - 3 && y < yMax - 3 && $"{input[y + 1][x + 1]}{input[y + 2][x + 2]}{input[y + 3][x + 3]}" == TargetWord)
					{
						xmasCount++;
					}

					// South.
					if (y < yMax - 3 && $"{input[y + 1][x]}{input[y + 2][x]}{input[y + 3][x]}" == TargetWord)
					{
						xmasCount++;
					}

					// SouthWest.
					if (x >= 3 && y < yMax - 3 && $"{input[y + 1][x - 1]}{input[y + 2][x - 2]}{input[y + 3][x - 3]}" == TargetWord)
					{
						xmasCount++;
					}
				}
			}
		}

		return xmasCount;
	}

	public static int RunPart2(ImmutableArray<string> input)
	{
		var xmasCount = input.Length;

		return xmasCount;
	}
}