using AdventOfCode2024.Day2;
using NUnit.Framework;

namespace AdventOfCode2024.Day1.Tests;

public static class SolutionDay2Tests
{
	[Test]
	public static void Part1()
	{
		var input =
			"""
			7 6 4 2 1
			1 2 7 8 9
			9 7 6 2 1
			1 3 2 4 5
			8 6 4 4 1
			1 3 6 7 9
			""";

		Assert.That(SolutionDay2.RunPart1([.. input.Split(Environment.NewLine)]), Is.EqualTo(2));
	}

	//[Test]
	//public static void Part2()
	//{
	//	var input =
	//		"""
	//		3   4
	//		4   3
	//		2   5
	//		1   3
	//		3   9
	//		3   3
	//		""";

	//	Assert.That(SolutionDay1.RunPart2([.. input.Split(Environment.NewLine)]), Is.EqualTo(31));
	//}
}