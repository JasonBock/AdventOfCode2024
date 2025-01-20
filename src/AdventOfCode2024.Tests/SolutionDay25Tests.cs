using NUnit.Framework;

namespace AdventOfCode2024.Day25.Tests;

public static class SolutionDay25Tests
{
	[Test]
	public static void Part1()
	{
		var input =
			"""
			#####
			.####
			.####
			.####
			.#.#.
			.#...
			.....

			#####
			##.##
			.#.##
			...##
			...#.
			...#.
			.....

			.....
			#....
			#....
			#...#
			#.#.#
			#.###
			#####

			.....
			.....
			#.#..
			###..
			###.#
			###.#
			#####

			.....
			.....
			.....
			#....
			#.#..
			#.#.#
			#####
			""";

		Assert.That(
			SolutionDay25.RunPart1([.. input.Split(Environment.NewLine)]), Is.EqualTo(3));
	}
}