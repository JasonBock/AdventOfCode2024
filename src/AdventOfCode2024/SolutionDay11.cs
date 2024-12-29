using System.Globalization;
using System.Numerics;

namespace AdventOfCode2024.Day11;

public static class SolutionDay11
{
	public static long RunPart1(string input)
	{
		ArgumentNullException.ThrowIfNull(input);

		var stones = input.Split(' ')
			.Select(_ => BigInteger.Parse(_, CultureInfo.CurrentCulture))
			.ToList();

		for (var i = 0; i < 25; i++)
		{
			Console.WriteLine($"(Current Iteration, Number of Stones): ({i}, {stones.Count})");

			for (var s = stones.Count - 1; s >= 0; s--)
			{
				var stone = stones[s];

				if (stone == BigInteger.Zero)
				{
					stones[s] = BigInteger.One;
				}
				else
				{
					var stoneDigits = (int)Math.Floor(BigInteger.Log10(stone) + 1);

					if (stoneDigits % 2 == 0)
					{
						var splitter = BigInteger.Pow(10, stoneDigits / 2);
						var (quotient, remainder) = BigInteger.DivRem(stone, splitter);
						stones[s] = remainder;
						stones.Insert(s, quotient);
					}
					else
					{
						stones[s] = stone * 2_024;
					}
				}
			}
		}

		Console.WriteLine($"Maximum Value: {stones.Max()}");

		return stones.Count;
	}

	public static long RunPart2(string input)
	{
		ArgumentNullException.ThrowIfNull(input);

		var totalStoneCount = input.Length;

		return totalStoneCount;
	}
}