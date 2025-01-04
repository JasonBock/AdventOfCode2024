using System.Collections.Immutable;
using System.Globalization;

namespace AdventOfCode2024.Day13;

public static class SolutionDay13
{
	public static long RunPart1(ImmutableArray<string> input)
	{
		var machines = SolutionDay13.GetMachines(input);

		var totalTokens = 0L;

		foreach (var machine in machines)
		{
			var tokenCosts = new List<long>();

			for (var a = 1; a <= 100; a++)
			{
				for (var b = 1; b <= 100; b++)
				{
					if ((machine.A.XIncrement * a) + (machine.B.XIncrement * b) == machine.Reward.X &&
						(machine.A.YIncrement * a) + (machine.B.YIncrement * b) == machine.Reward.Y)
					{
						tokenCosts.Add((3 * a) + b);
					}
				}
			}

			if (tokenCosts.Count > 0)
			{
				totalTokens += tokenCosts.Min();
			}
		}

		return totalTokens;
	}

	public static long RunPart2(ImmutableArray<string> input)
	{
		var x = input.Length;

		return x;
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

public sealed record Button(int XIncrement, int YIncrement);
public sealed record Prize(int X, int Y);
public sealed record Machine(Button A, Button B, Prize Reward);