using AdventOfCode2024.Day20;
using NUnit.Framework;
using System.Numerics;

namespace AdventOfCode2024.Day22.Tests;

public static class SolutionDay22Tests
{
	[TestCase(15887950, 1, 16495136)]
	[TestCase(15887950, 2, 527345)]
	[TestCase(15887950, 3, 704524)]
	[TestCase(15887950, 4, 1553684)]
	[TestCase(15887950, 5, 12683156)]
	[TestCase(15887950, 6, 11100544)]
	[TestCase(15887950, 7, 12249484)]
	[TestCase(15887950, 8, 7753432)]
	[TestCase(15887950, 9, 5908254)]
	public static void GetSecret(long initialSecret, int iterations, long expectedSecret) =>
		Assert.That(SolutionDay22.GetSecret(initialSecret, iterations), Is.EqualTo(new BigInteger(expectedSecret)));

	[Test]
	public static void Part1()
	{
		var input =
			"""
			1
			10
			100
			2024
			""";

		Assert.That(
			SolutionDay22.RunPart1([.. input.Split(Environment.NewLine)]), Is.EqualTo(new BigInteger(37327623)));
	}
}