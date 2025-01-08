using NUnit.Framework;
using System.Numerics;

namespace AdventOfCode2024.Day16.Tests;

public static class SolutionDay16Tests
{
	[Test]
	public static void Part1Small()
	{
		var input =
			"""
			###############
			#.......#....E#
			#.#.###.#.###.#
			#.....#.#...#.#
			#.###.#####.#.#
			#.#.#.......#.#
			#.#.#####.###.#
			#...........#.#
			###.#.#####.#.#
			#...#.....#.#.#
			#.#.#.###.#.#.#
			#.....#...#.#.#
			#.###.#.#.#.#.#
			#S..#.....#...#
			###############
			""";

		Assert.That(SolutionDay16.RunPart1([.. input.Split(Environment.NewLine)]), Is.EqualTo(7_036L));
	}

	[Test]
	public static void Part1Large()
	{
		var input =
			"""
			#################
			#...#...#...#..E#
			#.#.#.#.#.#.#.#.#
			#.#.#.#...#...#.#
			#.#.#.#.###.#.#.#
			#...#.#.#.....#.#
			#.#.#.#.#.#####.#
			#.#...#.#.#.....#
			#.#.#####.#.###.#
			#.#.#.......#...#
			#.#.###.#####.###
			#.#.#...#.....#.#
			#.#.#.#####.###.#
			#.#.#.........#.#
			#.#.#.#########.#
			#S#.............#
			#################
			""";

		Assert.That(SolutionDay16.RunPart1([.. input.Split(Environment.NewLine)]), Is.EqualTo(11_048L));
	}
}