using NUnit.Framework;

namespace AdventOfCode2024.Day23.Tests;

public static class SolutionDay23Tests
{
	[Test]
	public static void Part1()
	{
		var input =
			"""
			kh-tc
			qp-kh
			de-cg
			ka-co
			yn-aq
			qp-ub
			cg-tb
			vc-aq
			tb-ka
			wh-tc
			yn-cg
			kh-ub
			ta-co
			de-co
			tc-td
			tb-wq
			wh-td
			ta-ka
			td-qp
			aq-cg
			wq-ub
			ub-vc
			de-ta
			wq-aq
			wq-vc
			wh-yn
			ka-de
			kh-ta
			co-tc
			wh-qp
			tb-vc
			td-yn
			""";

		Assert.That(
			SolutionDay23.RunPart1([.. input.Split(Environment.NewLine)]), Is.EqualTo(7));
	}
}