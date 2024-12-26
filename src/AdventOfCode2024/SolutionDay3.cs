using System.Collections.Immutable;
using System.Globalization;
using System.Text.RegularExpressions;

namespace AdventOfCode2024.Day3;

public static partial class SolutionDay3
{
	public static int RunPart1(ImmutableArray<string> input)
	{
		var mulSummation = 0;

		foreach (var instructions in input)
		{
			var mulMatches = GetMulExpression().Matches(instructions);

			for (var i = 0; i < mulMatches.Count; i++)
			{
				var mul = mulMatches[i];
				mulSummation += int.Parse(mul.Groups[1].Value, CultureInfo.CurrentCulture) *
					int.Parse(mul.Groups[2].Value, CultureInfo.CurrentCulture);
			}
		}

		return mulSummation;
	}

	//public static void RunPart2(ImmutableArray<string> input)
	//{
	//}

	[GeneratedRegex(@"mul[(](\d{1,3}),(\d{1,3})[)]")]
	private static partial Regex GetMulExpression();
}