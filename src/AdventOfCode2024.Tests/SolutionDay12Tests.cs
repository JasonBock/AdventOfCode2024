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

	//[Test]
	//public static void Part2()
	//{
	//	var input =
	//		"""
	//		............
	//		........0...
	//		.....0......
	//		.......0....
	//		....0.......
	//		......A.....
	//		............
	//		............
	//		........A...
	//		.........A..
	//		............
	//		............
	//		""";

	//	Assert.That(SolutionDay8.RunPart2([.. input.Split(Environment.NewLine)]), Is.EqualTo(34));
	//}
}