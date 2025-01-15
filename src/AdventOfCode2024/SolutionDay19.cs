using System.Collections.Immutable;
using System.Numerics;

namespace AdventOfCode2024.Day19;

public static class SolutionDay19
{
	public static int RunPart1(ImmutableArray<string> input)
	{
		// First line contains the tokens.
		var tokenParts = input[0].Split(", ");
		var tokens = new Dictionary<char, HashSet<string>>();

		foreach (var tokenPart in tokenParts)
		{
			if (tokens.TryGetValue(tokenPart[0], out var tokenValues))
			{
				tokenValues.Add(tokenPart);
			}
			else
			{
				tokens.Add(tokenPart[0], [tokenPart]);
			}
		}

		var possibleDesignCount = 0;

		for (var i = 2; i < input.Length; i++)
		{
			possibleDesignCount = input[i].CanBeMade(tokens) ?
				possibleDesignCount + 1 : possibleDesignCount;
		}

		return possibleDesignCount;
	}

	public static BigInteger RunPart2(ImmutableArray<string> input)
	{
		// First line contains the tokens.
		var tokenParts = input[0].Split(", ");
		var tokens = new Dictionary<char, HashSet<string>>();

		foreach (var tokenPart in tokenParts)
		{
			if (tokens.TryGetValue(tokenPart[0], out var tokenValues))
			{
				tokenValues.Add(tokenPart);
			}
			else
			{
				tokens.Add(tokenPart[0], [tokenPart]);
			}
		}

		var possibleOptionsCount = BigInteger.Zero;

		for (var i = 2; i < input.Length; i++)
		{
			Console.WriteLine(input[i]);
			possibleOptionsCount += input[i].GetValidBranches(tokens, 0, []);
		}

		return possibleOptionsCount;
	}

	private static bool CanBeMade(this string self, Dictionary<char, HashSet<string>> tokens)
	{
		var indexes = new SortedSet<int> { 0 };

		while (indexes.Count > 0)
		{
			var currentIndex = indexes.First();

			if (tokens.TryGetValue(self[currentIndex], out var startingTokens))
			{
				foreach (var startingToken in startingTokens)
				{
					var nextIndex = currentIndex + startingToken.Length;

					if ((self.Length >= (nextIndex) &&
						self.AsSpan(currentIndex, startingToken.Length).SequenceEqual(startingToken.AsSpan())))
					{
						if (self.Length == nextIndex)
						{
							return true;
						}

						indexes.Add(nextIndex);
					}
				}
			}

			indexes.Remove(currentIndex);
		}

		return false;
	}

	private static BigInteger GetValidBranches(this string self, Dictionary<char, HashSet<string>> tokens, 
		int currentIndex, Dictionary<int, BigInteger> computedIndexes)
	{
		var validBranches = BigInteger.Zero;

		if (computedIndexes.TryGetValue(currentIndex, out var computedValidBranches))
		{
			return computedValidBranches;
		}

		if (tokens.TryGetValue(self[currentIndex], out var startingTokens))
		{
			foreach (var startingToken in startingTokens)
			{
				var nextIndex = currentIndex + startingToken.Length;

				if ((self.Length >= (nextIndex) &&
					self.AsSpan(currentIndex, startingToken.Length).SequenceEqual(startingToken.AsSpan())))
				{
					if (self.Length == nextIndex)
					{
						validBranches++;
					}
					else
					{
						validBranches += self.GetValidBranches(tokens, nextIndex, computedIndexes);
					}
				}
			}
		}

		computedIndexes.TryAdd(currentIndex, validBranches);
		return validBranches;
	}
}
