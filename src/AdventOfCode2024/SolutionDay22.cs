using System.Collections.Immutable;
using System.Globalization;
using System.Linq;

namespace AdventOfCode2024.Day22;

public static class SolutionDay22
{
	public static long RunPart1(ImmutableArray<string> input)
	{
		var secretNumbersSum = 0L;

		foreach (var item in input)
		{
			var initialSecret = long.Parse(item, CultureInfo.CurrentCulture);
			secretNumbersSum += SolutionDay22.GetSecret(initialSecret, 2_000);
		}

		return secretNumbersSum;
	}

	public static int RunPart2(ImmutableArray<string> input)
	{
		var maximumBananaCount = 0;

		var buyers = new List<Buyer>();

		foreach (var item in input)
		{
			buyers.Add(new Buyer(long.Parse(item, CultureInfo.CurrentCulture)));
		}

		var changeListCodes = new HashSet<int>();

		for (var b = 0; b < buyers.Count; b++)
		{
			Console.WriteLine($"At buyer {b}");

			var buyer = buyers[b];

			for (var i = 0; i < buyer.Changes.Length - 3; i++)
			{
				var buyerChanges = buyer.Changes.AsSpan(i, 4);

				if (changeListCodes.Add(SolutionDay22.GetSpanCode(buyerChanges)))
				{
					var currentBananaCount = buyer.Prices[i + 3];

					foreach (var otherBuyer in buyers.Where(_ => _ != buyer))
					{
						var otherBuyerIndex = otherBuyer.Changes.AsSpan().IndexOf(buyerChanges);

						if (otherBuyerIndex != -1)
						{
							currentBananaCount += otherBuyer.Prices[otherBuyerIndex + 3];
						}
					}

					if (currentBananaCount > maximumBananaCount)
					{
						maximumBananaCount = currentBananaCount;
					}
				}
			}
		}

		return maximumBananaCount;
	}

	private static int GetSpanCode(ReadOnlySpan<int> span)
	{
		var spanCode = (span[3] + 10);
		spanCode <<= 5;
		spanCode |= (span[2] + 10);
		spanCode <<= 5;
		spanCode |= (span[1] + 10);
		spanCode <<= 5;
		spanCode |= (span[0] + 10);

		return spanCode;
	}

	public static long GetSecret(long initialValue, int iterations)
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

public sealed class Buyer
{
	public Buyer(long secretNumber)
	{
		this.InitialSecretNumber = secretNumber;

		var changes = new int[2000];
		var prices = new int[2001];
		prices[0] = (int)(secretNumber % 10);

		for (var i = 0; i < 2000; i++)
		{
			var newSecret = SolutionDay22.GetSecret(secretNumber, 1);

			prices[i + 1] = (int)(newSecret % 10);
			changes[i] = prices[i + 1] - prices[i];

			secretNumber = newSecret;
		}

		(this.Changes, this.Prices) = ([.. changes], [.. prices.AsSpan(1, 2000)]);
	}

	public long InitialSecretNumber { get; }
	public ImmutableArray<int> Changes { get; }
	public ImmutableArray<int> Prices { get; }
}