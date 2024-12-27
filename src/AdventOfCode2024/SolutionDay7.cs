using System.Collections.Immutable;
using System.Globalization;

namespace AdventOfCode2024.Day7;

public static class SolutionDay7
{
	public static long RunPart1(ImmutableArray<string> input)
	{
		var finalCalibrationResult = 0L;
		var calibrations = new List<Calibration>(input.Length);

		foreach (var calibration in input)
		{
			var parts = calibration.Split(':');
			calibrations.Add(new(
				long.Parse(parts[0], CultureInfo.CurrentCulture),
				parts[1].Trim().Split(' ').Select(_ => long.Parse(_, CultureInfo.CurrentCulture)).ToImmutableArray()));
		}

		foreach (var calibration in calibrations)
		{
			var operationCount = Math.Pow(2, calibration.Values.Length - 1);
			var wasSolutionFound = false;

			for (var operationPattern = 0; operationPattern < operationCount; operationPattern++)
			{
				var testSolution = calibration.Values[0];

				for (var j = 1; j < calibration.Values.Length; j++)
				{
					testSolution = (operationPattern & (1 << (j - 1))) == 0 ?
						testSolution + calibration.Values[j] :
						testSolution * calibration.Values[j];
				}

				if (testSolution == calibration.Solution)
				{
					wasSolutionFound = true;
					break;
				}
			}

			if (wasSolutionFound)
			{
				finalCalibrationResult += calibration.Solution;
			}
		}

		return finalCalibrationResult;
	}

	public static int RunPart2(ImmutableArray<string> input)
	{
		var finalCalibrationResult = input.Length;

		return finalCalibrationResult;
	}
}

public sealed record Calibration(long Solution, ImmutableArray<long> Values);