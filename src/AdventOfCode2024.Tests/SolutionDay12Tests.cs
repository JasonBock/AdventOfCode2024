using NUnit.Framework;

namespace AdventOfCode2024.Day12.Tests;

public static class SolutionDay12Tests
{
	[Test]
	public static void Part1Small()
	{
		var input =
			"""
			AAAA
			BBCD
			BBCC
			EEEC
			""";

		Assert.That(SolutionDay12.RunPart1([.. input.Split(Environment.NewLine)]), Is.EqualTo(140));
	}

	[Test]
	public static void Part1Medium()
	{
		var input =
			"""
			OOOOO
			OXOXO
			OOOOO
			OXOXO
			OOOOO
			""";

		Assert.That(SolutionDay12.RunPart1([.. input.Split(Environment.NewLine)]), Is.EqualTo(772));
	}

	[Test]
	public static void Part1Large()
	{
		var input =
			"""
			RRRRIICCFF
			RRRRIICCCF
			VVRRRCCFFF
			VVRCCCJFFF
			VVVVCJJCFE
			VVIVCCJJEE
			VVIIICJJEE
			MIIIIIJJEE
			MIIISIJEEE
			MMMISSJEEE
			""";

		Assert.That(SolutionDay12.RunPart1([.. input.Split(Environment.NewLine)]), Is.EqualTo(1930));
	}

	[Test]
	public static void Part2Small()
	{
		var input =
			"""
			AAAA
			BBCD
			BBCC
			EEEC
			""";

		Assert.That(SolutionDay12.RunPart2([.. input.Split(Environment.NewLine)]), Is.EqualTo(80));
	}

	[Test]
	public static void Part2Medium()
	{
		var input =
			"""
			OOOOO
			OXOXO
			OOOOO
			OXOXO
			OOOOO
			""";

		Assert.That(SolutionDay12.RunPart2([.. input.Split(Environment.NewLine)]), Is.EqualTo(436));
	}

	[Test]
	public static void Part2EShape()
	{
		var input =
			"""
			EEEEE
			EXXXX
			EEEEE
			EXXXX
			EEEEE
			""";

		Assert.That(SolutionDay12.RunPart2([.. input.Split(Environment.NewLine)]), Is.EqualTo(236));
	}

	[Test]
	public static void Part22BsInA()
	{
		var input =
			"""
			AAAAAA
			AAABBA
			AAABBA
			ABBAAA
			ABBAAA
			AAAAAA
			""";

		Assert.That(SolutionDay12.RunPart2([.. input.Split(Environment.NewLine)]), Is.EqualTo(368));
	}

	[Test]
	public static void Part2Large()
	{
		var input =
			"""
			RRRRIICCFF
			RRRRIICCCF
			VVRRRCCFFF
			VVRCCCJFFF
			VVVVCJJCFE
			VVIVCCJJEE
			VVIIICJJEE
			MIIIIIJJEE
			MIIISIJEEE
			MMMISSJEEE
			""";

		Assert.That(SolutionDay12.RunPart2([.. input.Split(Environment.NewLine)]), Is.EqualTo(1206));
	}
}