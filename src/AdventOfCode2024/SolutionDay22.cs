using System.Collections.Immutable;
using System.Globalization;
using System.Numerics;

namespace AdventOfCode2024.Day22;

public static class SolutionDay22
{
	public static BigInteger RunPart1(ImmutableArray<string> input)
	{
		var secretNumbersSum = BigInteger.Zero;

		foreach (var item in input)
		{
			var initialSecret = BigInteger.Parse(item, CultureInfo.CurrentCulture);
			secretNumbersSum += SolutionDay22.GetSecret(initialSecret, 2_000);
		}

		return secretNumbersSum;
	}

	public static BigInteger GetSecret(BigInteger initialValue, int iterations)
	{
		var newSecret = initialValue;

		for (var i = 0; i < iterations; i++)
		{
			// Part 1
			var part1Result = ((newSecret * 64) ^ newSecret) % 16777216;

			// Part 2
			var part2Result = ((part1Result / 32) ^ part1Result) % 16777216;

			// Part 3
			var part3Result = ((part2Result * 2048) ^ part2Result) % 16777216;

			newSecret = part3Result;
		}

		return newSecret;
	}
}