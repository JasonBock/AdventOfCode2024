using NUnit.Framework;

namespace AdventOfCode2024.Day10.Tests;

public static class SolutionDay10Tests
{
	[Test]
	public static void Part1()
	{
		var input =
			"""
			89010123
			78121874
			87430965
			96549874
			45678903
			32019012
			01329801
			10456732
			""";

		Assert.That(SolutionDay10.RunPart1([.. input.Split(Environment.NewLine)]), Is.EqualTo(36));
	}

	[Test]
	public static void Part2()
	{
		var input =
			"""
			89010123
			78121874
			87430965
			96549874
			45678903
			32019012
			01329801
			10456732
			""";

		Assert.That(SolutionDay10.RunPart2([.. input.Split(Environment.NewLine)]), Is.EqualTo(81));
	}
}