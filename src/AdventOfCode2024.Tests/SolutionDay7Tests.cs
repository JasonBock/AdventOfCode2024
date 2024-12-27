using NUnit.Framework;

namespace AdventOfCode2024.Day7.Tests;

public static class SolutionDay7Tests
{
	[Test]
	public static void Part1()
	{
		var input =
			"""
			190: 10 19
			3267: 81 40 27
			83: 17 5
			156: 15 6
			7290: 6 8 6 15
			161011: 16 10 13
			192: 17 8 14
			21037: 9 7 18 13
			292: 11 6 16 20
			""";

		Assert.That(SolutionDay7.RunPart1([.. input.Split(Environment.NewLine)]), Is.EqualTo(3749));
	}

	[Test]
	public static void Part2()
	{
	}
}