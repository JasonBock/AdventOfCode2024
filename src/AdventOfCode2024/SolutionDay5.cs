using System.Collections.Immutable;
using System.Globalization;

namespace AdventOfCode2024.Day5;

public static class SolutionDay5
{
	public static int RunPart1(ImmutableArray<string> input)
	{
		var middlePageSum = 0;
		var rules = new List<Rule>();
		var updates = new List<int[]>();

		foreach (var item in input)
		{
			if (item.Contains('|', StringComparison.CurrentCultureIgnoreCase))
			{
				var ruleValues = item.Split('|');
				rules.Add(
					new Rule(
						int.Parse(ruleValues[0], CultureInfo.CurrentCulture),
						int.Parse(ruleValues[1], CultureInfo.CurrentCulture)));
			}
			else if (item.Contains(',', StringComparison.CurrentCultureIgnoreCase))
			{
				var updateValues = item.Split(",").Select(_ => int.Parse(_, CultureInfo.CurrentCulture)).ToArray();
				updates.Add(updateValues);
			}
		}

		foreach (var update in updates)
		{
			var isValidUpdate = true;

			for (var i = 0; i < update.Length; i++)
			{
				if (i > 0)
				{
					// We can look to the left.
					for (var b = 0; b < i; b++)
					{
						if (!rules.Any(_ => _ == new Rule(update[b], update[i])))
						{
							isValidUpdate = false;
							goto do_add;
						}
					}
				}
				else if (i < update.Length - 1)
				{
					// We can look to the right.
					for (var a = i + 1; a < update.Length; a++)
					{
						if (!rules.Any(_ => _ == new Rule(update[i], update[a])))
						{
							isValidUpdate = false;
							goto do_add;
						}
					}
				}
			}

		do_add:
			if (isValidUpdate)
			{
				middlePageSum += update[update.Length / 2];
			}
		}

		return middlePageSum;
	}

	public static int RunPart2(ImmutableArray<string> input)
	{
		var middlePageSum = 0;
		var rules = new List<Rule>();
		var updates = new List<int[]>();

		foreach (var item in input)
		{
			if (item.Contains('|', StringComparison.CurrentCultureIgnoreCase))
			{
				var ruleValues = item.Split('|');
				rules.Add(
					new Rule(
						int.Parse(ruleValues[0], CultureInfo.CurrentCulture),
						int.Parse(ruleValues[1], CultureInfo.CurrentCulture)));
			}
			else if (item.Contains(',', StringComparison.CurrentCultureIgnoreCase))
			{
				var updateValues = item.Split(",").Select(_ => int.Parse(_, CultureInfo.CurrentCulture)).ToArray();
				updates.Add(updateValues);
			}
		}

		foreach (var update in updates)
		{
			var isValidUpdate = true;

			for (var i = 0; i < update.Length; i++)
			{
				if (i > 0)
				{
					// We can look to the left.
					for (var b = 0; b < i; b++)
					{
						var before = update[b];
						var after = update[i];

						if (!rules.Any(_ => _ == new Rule(update[b], update[i])))
						{
							isValidUpdate = false;
							update[b] = after;
							update[i] = before;
						}
					}
				}
				else if (i < update.Length - 1)
				{
					// We can look to the right.
					for (var a = i + 1; a < update.Length; a++)
					{
						var before = update[i];
						var after = update[a];

						if (!rules.Any(_ => _ == new Rule(update[i], update[a])))
						{
							isValidUpdate = false;
							update[i] = after;
							update[a] = before;
						}
					}
				}
			}

			if (!isValidUpdate)
			{
				middlePageSum += update[update.Length / 2];
			}
		}

		return middlePageSum;
	}
}

public record struct Rule(int Before, int After);
