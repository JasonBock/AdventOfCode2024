using NUnit.Framework;

namespace AdventOfCode2024.Day3.Tests;

public static class SolutionDay3Tests
{
	[Test]
	public static void Part1()
	{
		var input = "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))";

		Assert.That(SolutionDay3.RunPart1([input]), Is.EqualTo(161));
	}

	[Test]
	public static void Part2()
	{
		var input = "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))";

		Assert.That(SolutionDay3.RunPart2([input]), Is.EqualTo(48));
	}
}