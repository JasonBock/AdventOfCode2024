using System.Collections.Immutable;
using System.Globalization;

namespace AdventOfCode2024.Day1;

public static class SolutionDay1
{
	public static int RunPart1(ImmutableArray<string> input)
	{
		var leftLocationIds = new int[input.Length];
		var rightLocationIds = new int[input.Length];

		for (var i = 0; i < input.Length; i++)
		{
			var item = input[i];
			var ids = item.Split("   ");
			leftLocationIds[i] = int.Parse(ids[0], CultureInfo.CurrentCulture);
			rightLocationIds[i] = int.Parse(ids[1], CultureInfo.CurrentCulture);
		}

		Array.Sort(leftLocationIds);
		Array.Sort(rightLocationIds);

		var difference = 0;

		for (var i = 0; i < leftLocationIds.Length; i++)
		{
			difference += int.Abs(leftLocationIds[i] - rightLocationIds[i]);
		}

		return difference;
	}

	public static int RunPart2(ImmutableArray<string> input)
	{
		var leftLocationIds = new int[input.Length];
		var rightLocationIds = new int[input.Length];

		for (var i = 0; i < input.Length; i++)
		{
			var item = input[i];
			var ids = item.Split("   ");
			leftLocationIds[i] = int.Parse(ids[0], CultureInfo.CurrentCulture);
			rightLocationIds[i] = int.Parse(ids[1], CultureInfo.CurrentCulture);
		}

		var similarity = 0;

		foreach (var value in leftLocationIds)
		{
			similarity += value * rightLocationIds.Count(_ => _ == value);
		}

		return similarity;
	}
}