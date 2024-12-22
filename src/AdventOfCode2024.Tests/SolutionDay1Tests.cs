using NUnit.Framework;

namespace AdventOfCode2024.Day1.Tests;

public static class SolutionDay1Tests
{
	[Test]
	public static void Add()
	{
		var x = 3;
		var y = 4;
		Assert.That(x + y, Is.EqualTo(7));
	}
}