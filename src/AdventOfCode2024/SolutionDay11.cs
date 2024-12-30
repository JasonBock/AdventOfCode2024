using System.Globalization;
using System.Numerics;

namespace AdventOfCode2024.Day11;

public static class SolutionDay11
{
	private static readonly Dictionary<BigInteger, BigInteger> Precomputes25Iterations = new()
	{
		{ 0L, 19778L },
		{ 1L, 29165L },
		{ 2L, 27842L },
		{ 3L, 27569L },
		{ 4L, 26669L },
		{ 5L, 23822L },
		{ 6L, 25469L },
		{ 7L, 25071L },
		{ 8L, 24212L },
		{ 9L, 25793L },
		{ 20L, 31055L },
		{ 24L, 36669L },
		{ 40L, 30300L },
		{ 48L, 33975L },
		{ 2024L, 43726L },
		{ 4048L, 42646L },
	};

	private static readonly Dictionary<BigInteger, BigInteger> Precomputes50Iterations = new()
	{
		{ 0L, 663251546L },
		{ 1L, 1010392024L },
		{ 2L, 967190364L },
		{ 3L, 967436144L },
		{ 4L, 939523808L },
		{ 5L, 830902728L },
		{ 6L, 884539345L },
		{ 7L, 870467992L },
		{ 8L, 841069902L },
		{ 9L, 897592763L },
		{ 20L, 1072629280L },
		{ 24L, 1254513380L },
		{ 40L, 1056089110L },
		{ 48L, 1174092474L },
		{ 2024L, 1529921658L },
		{ 4048L, 1464254721L },
	};

	public static BigInteger Run(string input, int iterations)
	{
		ArgumentNullException.ThrowIfNull(input);

		var stones = input.Split(' ')
			.Select(_ => BigInteger.Parse(_, CultureInfo.CurrentCulture))
			.ToList();

		var stoneCountTasks = new List<Task<BigInteger>>();

		foreach (var stone in stones)
		{
			stoneCountTasks.Add(Task.Run(() => GetStoneCount(stone, iterations)));
		}

		Task.WaitAll(stoneCountTasks);

		var stoneCount = BigInteger.Zero;

		foreach (var stoneCountTask in stoneCountTasks)
		{
			stoneCount += stoneCountTask.Result;
		}

		return stoneCount;
	}

	private static BigInteger GetStoneCount(BigInteger stone, int iterations)
	{
		if (iterations == 25 && Precomputes25Iterations.TryGetValue(stone, out var precompute25))
		{
			return precompute25;
		}

		if (iterations == 50 && Precomputes50Iterations.TryGetValue(stone, out var precompute50))
		{
			return precompute50;
		}

		if (stone == BigInteger.Zero)
		{
			if (iterations == 1)
			{
				return 1;
			}

			stone = BigInteger.One;
			return GetStoneCount(stone, iterations - 1);
		}
		else
		{
			var stoneDigits = (int)Math.Floor(BigInteger.Log10(stone) + 1);

			if (stoneDigits % 2 == 0)
			{
				if (iterations == 1)
				{
					return 2;
				}

				var splitter = BigInteger.Pow(10, stoneDigits / 2);
				var (quotient, remainder) = BigInteger.DivRem(stone, splitter);

				return GetStoneCount(quotient, iterations - 1) +
					GetStoneCount(remainder, iterations - 1);
			}
			else
			{
				if (iterations == 1)
				{
					return 1;
				}

				stone *= 2_024;
				return GetStoneCount(stone, iterations - 1);
			}
		}
	}
}