using NUnit.Framework;

namespace AdventOfCode2024.Day1.Tests;

public static class SolutionDay1Tests
{
	[Test]
	public static void Add()
	{
		var input =
			"""
			3   4
			4   3
			2   5
			1   3
			3   9
			3   3
			""";

		Assert.That(SolutionDay1.Run([.. input.Split(Environment.NewLine)]), Is.EqualTo(11));
	}
}