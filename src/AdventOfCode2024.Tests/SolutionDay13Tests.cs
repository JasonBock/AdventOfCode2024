using NUnit.Framework;
using System.Numerics;

namespace AdventOfCode2024.Day13.Tests;

public static class SolutionDay13Tests
{
	[Test]
	public static void Part1()
	{
		var input =
			"""
			Button A: X+94, Y+34
			Button B: X+22, Y+67
			Prize: X=8400, Y=5400

			Button A: X+26, Y+66
			Button B: X+67, Y+21
			Prize: X=12748, Y=12176

			Button A: X+17, Y+86
			Button B: X+84, Y+37
			Prize: X=7870, Y=6450

			Button A: X+69, Y+23
			Button B: X+27, Y+71
			Prize: X=18641, Y=10279
			""";

		Assert.That(SolutionDay13.RunPart1([.. input.Split(Environment.NewLine)]), Is.EqualTo(new BigInteger(480)));
	}

	[Test]
	public static void Part2()
	{
		var input =
			"""
			Button A: X+94, Y+34
			Button B: X+22, Y+67
			Prize: X=8400, Y=5400

			Button A: X+26, Y+66
			Button B: X+67, Y+21
			Prize: X=12748, Y=12176

			Button A: X+17, Y+86
			Button B: X+84, Y+37
			Prize: X=7870, Y=6450

			Button A: X+69, Y+23
			Button B: X+27, Y+71
			Prize: X=18641, Y=10279
			""";

		Assert.That(SolutionDay13.RunPart2([.. input.Split(Environment.NewLine)]), Is.EqualTo(new BigInteger(875318608908)));
	}
}