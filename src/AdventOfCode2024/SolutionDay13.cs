using System.Collections.Immutable;
using System.Globalization;
using System.Numerics;

namespace AdventOfCode2024.Day13;

public static class SolutionDay13
{
	public static BigInteger RunPart1(ImmutableArray<string> input)
	{
		var machines = SolutionDay13.GetMachines(input);

		var totalTokens = BigInteger.Zero;

		foreach (var machine in machines)
		{
			var aNumerator = ((machine.Reward.Y * machine.B.XIncrement) - (machine.B.YIncrement * machine.Reward.X));
			var aDenominator = ((machine.A.YIncrement * machine.B.XIncrement) - (machine.B.YIncrement * machine.A.XIncrement));
			var (aQuotient, aRemainder) = BigInteger.DivRem(aNumerator, aDenominator);

			if (aQuotient > 0 && aRemainder == 0)
			{
				var bNumerator = (machine.Reward.X - (machine.A.XIncrement * aQuotient));
				var (bQuotient, bRemainder) = BigInteger.DivRem(bNumerator, machine.B.XIncrement);

				if (bQuotient > 0 && bRemainder == 0)
				{
					totalTokens += (3 * aQuotient) + bQuotient;
				}
			}
		}

		return totalTokens;
	}

	public static BigInteger RunPart2(ImmutableArray<string> input)
	{
		var prizeOffset = new BigInteger(10_000_000_000_000L);

		var machines = SolutionDay13.GetMachines(input);

		var totalTokens = BigInteger.Zero;

		foreach (var machine in machines)
		{
			var aNumerator = (((machine.Reward.Y + prizeOffset) * machine.B.XIncrement) - (machine.B.YIncrement * (machine.Reward.X + prizeOffset)));
			var aDenominator = ((machine.A.YIncrement * machine.B.XIncrement) - (machine.B.YIncrement * machine.A.XIncrement));
			var (aQuotient, aRemainder) = BigInteger.DivRem(aNumerator, aDenominator);

			if (aQuotient > 0 && aRemainder == 0)
			{
				var bNumerator = ((machine.Reward.X + prizeOffset) - (machine.A.XIncrement * aQuotient));
				var (bQuotient, bRemainder) = BigInteger.DivRem(bNumerator, machine.B.XIncrement);

				if (bQuotient > 0 && bRemainder == 0)
				{
					totalTokens += (3 * aQuotient) + bQuotient;
				}
			}
		}

		return totalTokens;
	}

	private static ImmutableArray<Machine> GetMachines(ImmutableArray<string> input)
	{
		var machines = new List<Machine>();
		Button? a = null;
		Button? b = null;
		Prize? prize = null;

		for (var i = 0; i < input.Length; i++)
		{
			if (i % 4 != 3)
			{
				var data = input[i];

				if (i % 4 == 0)
				{
					// Button A
					var increments = data[(data.IndexOf(':', StringComparison.CurrentCulture) + 1)..].Trim().Split(',').Select(_ => _.Trim()).ToArray();
					var xIncrement = int.Parse(
						increments[0][(increments[0].Trim().IndexOf('+', StringComparison.CurrentCulture) + 1)..], CultureInfo.CurrentCulture);
					var yIncrement = int.Parse(
						increments[1][(increments[1].Trim().IndexOf('+', StringComparison.CurrentCulture) + 1)..], CultureInfo.CurrentCulture);
					a = new Button(xIncrement, yIncrement);
				}
				else if (i % 4 == 1)
				{
					// Button B
					var increments = data[(data.IndexOf(':', StringComparison.CurrentCulture) + 1)..].Trim().Split(',').Select(_ => _.Trim()).ToArray();
					var xIncrement = int.Parse(
						increments[0][(increments[0].Trim().IndexOf('+', StringComparison.CurrentCulture) + 1)..], CultureInfo.CurrentCulture);
					var yIncrement = int.Parse(
						increments[1][(increments[1].Trim().IndexOf('+', StringComparison.CurrentCulture) + 1)..], CultureInfo.CurrentCulture);
					b = new Button(xIncrement, yIncrement);
				}
				else if (i % 4 == 2)
				{
					// Prize
					var locations = data[(data.IndexOf(':', StringComparison.CurrentCulture) + 1)..].Trim().Split(',').Select(_ => _.Trim()).ToArray();
					var xLocation = int.Parse(
						locations[0][(locations[0].Trim().IndexOf('=', StringComparison.CurrentCulture) + 1)..], CultureInfo.CurrentCulture);
					var yLocation = int.Parse(
						locations[1][(locations[1].Trim().IndexOf('=', StringComparison.CurrentCulture) + 1)..], CultureInfo.CurrentCulture);
					prize = new Prize(xLocation, yLocation);
				}
			}
			else
			{
				machines.Add(new Machine(a!, b!, prize!));
				a = null;
				b = null;
				prize = null;
			}
		}

		if (a is not null && b is not null && prize is not null)
		{
			machines.Add(new Machine(a!, b!, prize!));
		}

		return [.. machines];
	}
}

public sealed record Button(BigInteger XIncrement, BigInteger YIncrement);
public sealed record Prize(BigInteger X, BigInteger Y);
public sealed record Machine(Button A, Button B, Prize Reward);