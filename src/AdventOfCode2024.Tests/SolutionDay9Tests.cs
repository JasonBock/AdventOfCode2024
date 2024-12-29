using NUnit.Framework;

namespace AdventOfCode2024.Day9.Tests;

public static class SolutionDay9Tests
{
	[Test]
	public static void Part1()
	{
		var input = "2333133121414131402";

		Assert.That(SolutionDay9.RunPart1(input), Is.EqualTo(1928));
	}

	[Test]
	public static void Part2()
	{
		var input = "2333133121414131402";

		Assert.That(SolutionDay9.RunPart2(input), Is.EqualTo(2858));
	}
}