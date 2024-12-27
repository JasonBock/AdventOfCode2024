using NUnit.Framework;

namespace AdventOfCode2024.Day4.Tests;

public static class SolutionDay4Tests
{
	[Test]
	public static void Part1()
	{
		var input =
			"""
			MMMSXXMASM
			MSAMXMSMSA
			AMXSXMAAMM
			MSAMASMSMX
			XMASAMXAMM
			XXAMMXXAMA
			SMSMSASXSS
			SAXAMASAAA
			MAMMMXMMMM
			MXMXAXMASX
			""";

		Assert.That(SolutionDay4.RunPart1([.. input.Split(Environment.NewLine)]), Is.EqualTo(18));
	}

	[Test]
	public static void Part2()
	{
		var input =
			"""
			MMMSXXMASM
			MSAMXMSMSA
			AMXSXMAAMM
			MSAMASMSMX
			XMASAMXAMM
			XXAMMXXAMA
			SMSMSASXSS
			SAXAMASAAA
			MAMMMXMMMM
			MXMXAXMASX
			""";

		Assert.That(SolutionDay4.RunPart2([.. input.Split(Environment.NewLine)]), Is.EqualTo(9));
	}
}