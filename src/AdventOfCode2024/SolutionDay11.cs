using System.Collections.Immutable;
using System.Globalization;
using System.Numerics;
using Precomputes = System.Collections.Generic.Dictionary<(System.Numerics.BigInteger, int), System.Numerics.BigInteger>;

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

		var stoneCount = BigInteger.Zero;
		Precomputes precomputes = [];

		foreach (var stone in stones)
		{
			var generator = new StoneCountGenerator(stone, iterations, precomputes);
			precomputes = generator.Precomputes;
			stoneCount += generator.Count;
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

public sealed class StoneCountGenerator
{
	private readonly ImmutableArray<BigInteger> targets =
	[
		0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 
		20, 24, 26, 28, 32, 36, 40, 48, 
		56, 57, 60, 67, 72, 77, 80, 84, 
		86, 91, 94, 96, 2024, 2048, 2457, 
		2608, 2867, 2880, 3277, 3686, 4048, 
		6032, 6072, 8096, 9184, 9456, 10120, 
		12144, 14168, 16192, 18216, 20482880, 
		24579456, 28676032, 32772608, 36869184
	];

   public StoneCountGenerator(BigInteger stone, int iterations, Precomputes precomputes)
   {
		this.Precomputes = precomputes;
		(this.Iterations, this.Count) = (iterations, this.GetStoneCount(stone, iterations));
   }

   private BigInteger GetStoneCount(BigInteger stone, int iterations)
	{
		if (this.Precomputes.TryGetValue((stone, iterations), out var precompute))
		{
			return precompute;
		}

		if (stone == BigInteger.Zero)
		{
			if (iterations == 1)
			{
				_ = this.Precomputes.TryAdd((stone, 1), 1);
				return 1;
			}

			stone = BigInteger.One;

			var count = this.GetStoneCount(stone, iterations - 1);
			_ = this.Precomputes.TryAdd((stone, iterations - 1), count);
			return count;
		}
		else
		{
			var stoneDigits = (int)Math.Floor(BigInteger.Log10(stone) + 1);

			if (stoneDigits % 2 == 0)
			{
				if (iterations == 1)
				{
					if (this.targets.Contains(stone))
					{
						_ = this.Precomputes.TryAdd((stone, 1), 2);
					}

					return 2;
				}

				var splitter = BigInteger.Pow(10, stoneDigits / 2);
				var (quotient, remainder) = BigInteger.DivRem(stone, splitter);

				var quotientCount = this.GetStoneCount(quotient, iterations - 1);
				var remainderCount = this.GetStoneCount(remainder, iterations - 1);

				if (this.targets.Contains(quotient))
				{
					_ = this.Precomputes.TryAdd((quotient, iterations - 1), quotientCount);
				}

				if (this.targets.Contains(remainder))
				{
					_ = this.Precomputes.TryAdd((remainder, iterations - 1), remainderCount);
				}

				return quotientCount + remainderCount;
			}
			else
			{
				if (iterations == 1)
				{
					_ = this.Precomputes.TryAdd((stone, 1), 1);
					return 1;
				}

				stone *= 2_024;
				return this.GetStoneCount(stone, iterations - 1);
			}
		}
	}

	public BigInteger Count { get; }
	public int Iterations { get; }
	public Precomputes Precomputes { get; }
}

public sealed class StoneListGenerator
{
	public StoneListGenerator(BigInteger stone, int iterations)
	{
		var stones = new List<BigInteger>() { stone };

		for (var i = 0; i < iterations; i++)
		{
			for (var j = stones.Count - 1; j >= 0; j--)
			{
				var iterationStone = stones[j];

				if (iterationStone == BigInteger.Zero)
				{
					stones[j] = BigInteger.One;
				}
				else
				{
					var iterationStoneDigits = (int)Math.Floor(BigInteger.Log10(iterationStone) + 1);

					if (iterationStoneDigits % 2 == 0)
					{
						var splitter = BigInteger.Pow(10, iterationStoneDigits / 2);
						var (quotient, remainder) = BigInteger.DivRem(iterationStone, splitter);

						stones[j] = quotient;
						stones.Insert(j, remainder);
					}
					else
					{
						stones[j] *= 2_024;
					}
				}
			}
		}

		this.Stones = [.. stones];
	}

	public ImmutableArray<BigInteger> Stones { get; }

	public int Iterations { get; }
}