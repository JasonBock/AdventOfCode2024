using NUnit.Framework;
using System.Numerics;

namespace AdventOfCode2024.Day17.Tests;

public static class SolutionDay17Tests
{
	[Test]
	public static void Part1()
	{
		var input =
			"""
			Register A: 729
			Register B: 0
			Register C: 0

			Program: 0,1,5,4,3,0
			""";

		Assert.That(SolutionDay17.RunPart1([.. input.Split(Environment.NewLine)]), Is.EqualTo("4,6,3,5,6,3,5,2,1,0"));
	}

	[Test]
	public static void Part2()
	{
		var input =
			"""
			Register A: 2024
			Register B: 0
			Register C: 0

			Program: 0,3,5,4,3,0
			""";

		Assert.That(SolutionDay17.RunPart2([.. input.Split(Environment.NewLine)]), Is.EqualTo(new BigInteger(117_440)));
	}
}