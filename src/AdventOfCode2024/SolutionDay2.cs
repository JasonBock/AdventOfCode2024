using System.Collections.Immutable;
using System.Globalization;

namespace AdventOfCode2024.Day2;

public static class SolutionDay2
{
	public static int RunPart1(ImmutableArray<string> input)
	{
		static Direction GetDifferenceDirection(int difference) =>
			difference > 0 ? 
				Direction.Descending :
				difference < 0 ? 
					Direction.Ascending : 
					Direction.Flat;

		static bool IsSafeDifference(int difference) =>
			difference is (>= (-3) and <= (-1)) or
			(>= 1 and <= 3);

		var safeCount = 0;

		foreach (var report in input)
		{
			var isSafe = true;

			var levels = report.Split(' ')
				.Select(_ => int.Parse(_, CultureInfo.CurrentCulture)).ToArray();

			var startingDifference = levels[0] - levels[1];

			if (IsSafeDifference(startingDifference))
			{
				var requiredDirection = GetDifferenceDirection(startingDifference);

				for (var i = 1; i < levels.Length - 1; i++)
				{
					var currentDifference = levels[i] - levels[i + 1];

					if (!(IsSafeDifference(currentDifference) &&
						GetDifferenceDirection(currentDifference) == requiredDirection))
					{
						isSafe = false;
						break;
					}
				}
			}
			else
			{
				isSafe = false;
			}

			if (isSafe)
			{
				safeCount++;
			}
		}

		return safeCount;
	}

	//public static int RunPart2(ImmutableArray<string> input)
	//{
	//}
}

public enum Direction { Ascending, Descending, Flat }