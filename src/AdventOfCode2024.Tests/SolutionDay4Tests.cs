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

	//[Test]
	//public static void Part2()
	//{
	//	var input = "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))";

	//	Assert.That(SolutionDay3.RunPart2([input]), Is.EqualTo(48));
	//}
}