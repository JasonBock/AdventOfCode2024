using NUnit.Framework;

namespace AdventOfCode2024.Day19.Tests;

public static class SolutionDay19Tests
{
	[Test]
	public static void Part1()
	{
		var input =
			"""
			r, wr, b, g, bwu, rb, gb, br

			brwrr
			bggr
			gbbr
			rrbgbr
			ubwu
			bwurrg
			brgr
			bbrgwb
			""";

		Assert.That(SolutionDay19.RunPart1([.. input.Split(Environment.NewLine)]), Is.EqualTo(6));
	}
}