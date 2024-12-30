using System.Globalization;
using System.Numerics;

namespace AdventOfCode2024.Day11;

public static class SolutionDay11
{
	public static long Run(string input, int iterations)
	{
		ArgumentNullException.ThrowIfNull(input);

		var stones = input.Split(' ')
			.Select(_ => BigInteger.Parse(_, CultureInfo.CurrentCulture))
			.ToList();

		var stoneCount = 0;
		var stoneIndex = 0;

		foreach (var stone in stones)
		{
			Console.WriteLine($"At {stoneIndex} of {stones.Count}");

			stoneCount += GetStoneCount(stone, iterations);
			stoneIndex++;
		}

		return stoneCount;
	}

	private static int GetStoneCount(BigInteger seed, int iterations)
	{
		if (iterations == 0)
		{
			return 0;
		}

		var stones = new List<BigInteger> { seed };

		for (var i = 0; i < 5; i++)
		{
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

		if (iterations == 5)
		{
			return stones.Count;
		}

		var totalStoneCount = 0;

		foreach (var stone in stones)
		{
			totalStoneCount += GetStoneCount(stone, iterations - 5);
		}

		return totalStoneCount;
	}
}