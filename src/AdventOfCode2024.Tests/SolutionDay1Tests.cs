using NUnit.Framework;

namespace AdventOfCode2024.Day1.Tests;

public static class SolutionDay1Tests
{
	[Test]
	public static void Part1()
	{
		var input =
			"""
			3   4
			4   3
			2   5
			1   3
			3   9
			3   3
			""";

		Assert.That(SolutionDay1.RunPart1([.. input.Split(Environment.NewLine)]), Is.EqualTo(11));
	}

	[Test]
	public static void Part2()
	{
		var input =
			"""
			3   4
			4   3
			2   5
			1   3
			3   9
			3   3
			""";

		Assert.That(SolutionDay1.RunPart2([.. input.Split(Environment.NewLine)]), Is.EqualTo(31));
	}
}