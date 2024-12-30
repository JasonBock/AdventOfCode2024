using NUnit.Framework;
using System.Numerics;

namespace AdventOfCode2024.Day11.Tests;

public static class SolutionDay11Tests
{
	[TestCase("125 17", 5, 13L)]
	[TestCase("125 17", 10, 109L)]
	[TestCase("125 17", 25, 55_312L)]
	[TestCase("125 17", 30, 445_882L)]
	[TestCase("125 17", 35, 3_604_697L)]
	[TestCase("125 17", 40, 29_115_525L)]
	public static void Run(string input, int iterations, long expectedStoneCount) => 
		Assert.That(SolutionDay11.Run(input, iterations),
		   Is.EqualTo(new BigInteger(expectedStoneCount)));
}