using NUnit.Framework;

namespace AdventOfCode2024.Day11.Tests;

public static class SolutionDay11Tests
{
	[Test]
	public static void Iterations5()
	{
		var input = "125 17";

		Assert.That(SolutionDay11.Run(input, 5), Is.EqualTo(13));
	}

	[Test]
	public static void Iterations10()
	{
		var input = "125 17";

		Assert.That(SolutionDay11.Run(input, 10), Is.EqualTo(109));
	}

	[Test]
	public static void Iterations25()
	{
		var input = "125 17";

		Assert.That(SolutionDay11.Run(input, 25), Is.EqualTo(55312));
	}
}