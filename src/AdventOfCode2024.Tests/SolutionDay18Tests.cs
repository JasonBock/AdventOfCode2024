using NUnit.Framework;

namespace AdventOfCode2024.Day18.Tests;

public static class SolutionDay18Tests
{
	[Test]
	public static void Part1()
	{
		var input =
			"""
			5,4
			4,2
			4,5
			3,0
			2,1
			6,3
			2,4
			1,5
			0,6
			3,3
			2,6
			5,1
			""";

		Assert.That(SolutionDay18.RunPart1([.. input.Split(Environment.NewLine)], 12, 7), Is.EqualTo(22));
	}

	[Test]
	public static void Part2()
	{
		var input =
			"""
			5,4
			4,2
			4,5
			3,0
			2,1
			6,3
			2,4
			1,5
			0,6
			3,3
			2,6
			5,1
			1,2
			5,5
			2,5
			6,5
			1,4
			0,4
			6,4
			1,1
			6,1
			1,0
			0,5
			1,6
			2,0
			""";

		Assert.That(SolutionDay18.RunPart2([.. input.Split(Environment.NewLine)], 7), Is.EqualTo("6,1"));
	}
}