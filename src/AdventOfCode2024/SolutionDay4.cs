using System.Collections.Immutable;

namespace AdventOfCode2024.Day4;

public static class SolutionDay4
{
	private const string MasWord = "MAS";

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
					if (x >= 3 && $"{line[x - 1]}{line[x - 2]}{line[x - 3]}" == MasWord)
					{
						xmasCount++;
					}

					// NorthWest.
					if (x >= 3 && y >= 3 && $"{input[y - 1][x - 1]}{input[y - 2][x - 2]}{input[y - 3][x - 3]}" == MasWord)
					{
						xmasCount++;
					}

					// North.
					if (y >= 3 && $"{input[y - 1][x]}{input[y - 2][x]}{input[y - 3][x]}" == MasWord)
					{
						xmasCount++;
					}

					// NorthEast.
					if (x < xMax - 3 && y >= 3 && $"{input[y - 1][x + 1]}{input[y - 2][x + 2]}{input[y - 3][x + 3]}" == MasWord)
					{
						xmasCount++;
					}

					// East.
					if (x < xMax - 3 && $"{line[x + 1]}{line[x + 2]}{line[x + 3]}" == MasWord)
					{
						xmasCount++;
					}

					// SouthEast.
					if (x < xMax - 3 && y < yMax - 3 && $"{input[y + 1][x + 1]}{input[y + 2][x + 2]}{input[y + 3][x + 3]}" == MasWord)
					{
						xmasCount++;
					}

					// South.
					if (y < yMax - 3 && $"{input[y + 1][x]}{input[y + 2][x]}{input[y + 3][x]}" == MasWord)
					{
						xmasCount++;
					}

					// SouthWest.
					if (x >= 3 && y < yMax - 3 && $"{input[y + 1][x - 1]}{input[y + 2][x - 2]}{input[y + 3][x - 3]}" == MasWord)
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
		var xMax = input[0].Length;
		var yMax = input.Length;

		var xmasCount = 0;

		for (var y = 1; y < yMax - 1; y++)
		{
			var line = input[y];

			for (var x = 1; x < xMax - 1; x++)
			{
				if (line[x] == 'A')
				{
					var word0 = $"{input[y - 1][x - 1]}{input[y + 1][x + 1]}";
					var word1 = $"{input[y - 1][x + 1]}{input[y + 1][x - 1]}";

					if ((word0 == "SM" || word0 == "MS") &&
						(word1 == "SM" || word1 == "MS"))
					{
						xmasCount++;
					}
				}
			}
		}

		return xmasCount;
	}
}