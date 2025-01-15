using NUnit.Framework;
using System.Numerics;

namespace AdventOfCode2024.Day19.Tests;

public static class SolutionDay19Tests
{
	[Test]
	public static void Part1()
	{
		var input =
			"""
			r, wr, b, g, bwu, rb, gb, br

			brwrr
			bggr
			gbbr
			rrbgbr
			ubwu
			bwurrg
			brgr
			bbrgwb
			""";


		Assert.That(SolutionDay19.RunPart1([.. input.Split(Environment.NewLine)]), Is.EqualTo(6));
	}

	[Test]
	public static void Part2()
	{
		var input =
			"""
			r, wr, b, g, bwu, rb, gb, br

			brwrr
			bggr
			gbbr
			rrbgbr
			ubwu
			bwurrg
			brgr
			bbrgwb
			""";

		Assert.That(SolutionDay19.RunPart2([.. input.Split(Environment.NewLine)]), Is.EqualTo(new BigInteger(16)));
	}

	[Test]
	public static void Part2Case1()
	{
		var input =
			"""
			grg, rwguw, bbw, gbrruug, ubbbbw, bggwu, gwbwbb, urwbrwr, wwww, rwbubwr, wrggb, ruuw, gub, wgbr, gur, gugw, gwbbg, rrg, gbgrbbbb, ugu, rrbbgruu, urub, rwuw, brrw, rwr, ugbbbbb, uwr, rgbbw, rgrgg, bwwgw, rg, ur, wgrrr, rbubgu, gwu, uwrgb, brb, rgrgrw, brggu, bbrubuwg, uwgrrbug, bwwbrrrb, ruuwgr, guw, gbr, guwu, uuubbuwb, guu, g, grbwb, gggbbbu, ugugw, wwg, wggr, bwr, wwr, ubuwbbw, gwwr, gugbuww, rwb, ugbb, bwbwwuw, bbr, rgb, ruu, ubbb, bwrgurgb, rubg, rggb, rbuwb, uguurb, gwrw, wwuwr, rrur, grwggw, bgwr, rgugb, wbg, wwb, wr, wbur, ugbbr, wgwurru, ug, ggbwgg, bwu, wuug, gwbwr, bbbbgrr, bw, ubbg, urwuw, uwrrbgw, bwwwr, wgrwww, wg, burwbu, gurrrru, ugub, gwrrg, ubrw, gruwrw, ubr, gbbb, gbu, rbu, ggw, rrgbb, wuubbw, wggbuur, uuw, urg, wbuu, uruug, wrw, wurgbw, gbgbgb, gwb, bwb, bwug, ggub, uug, bww, grr, uurrr, uw, ubb, bgrg, rgbbr, rrb, rr, bgr, bugrbg, bgubur, ggbb, bub, gwruuw, rbg, uuuwbbr, wgr, rbuurrw, bwuwwb, ugb, gbgr, gubu, bgb, uguru, uwg, wgwwugrg, gwwrb, wgu, rru, rwbbwg, gwwuur, ggwgwrr, uwbr, uuu, wbb, wggg, buu, rbuggb, wuuggg, urgg, u, ww, uuwuub, gr, rbgbb, bwburuw, rug, bubwb, wb, wgw, brr, wgrrw, urr, gu, bbrw, grbb, uwgwu, rrr, rwu, urw, gurrb, grw, bugub, rurrww, wrwuugr, wgggrr, brrgguu, rgww, wgg, wuwwwuw, ubruwuwr, brgw, wbu, rgw, uwgurg, rgrb, rrw, ubwggr, uruw, uru, urwgb, gw, bgrbuu, gbrrwwru, gbrgr, bbbgbrgb, bgg, rww, urbggb, wgrr, bbrrww, gb, rbbb, gbbrrw, rubwb, wrbrwrww, ubuuw, bgbww, rgg, rbww, rwgu, uwrw, uwrr, gggr, wbbgrwbg, uggrggub, uww, wu, wuu, uwbbur, ggww, bubwr, rgwb, bwg, gugr, burgw, rgur, wrbg, rwrgrr, gug, wbbuw, bbg, guwub, uruu, gurg, ruw, rbrbgrrw, bwurg, gwww, gbbu, ggu, wgwbw, guuww, uwbbrbr, bggbuu, rbrg, rwww, bbubb, ubg, rurbb, ugugwrb, wrg, wubwu, uubw, r, gbuuuu, ugrgg, rgu, rwwr, gbgu, ugwwrg, ru, bwbbbgu, bug, ugg, rwwwrwb, rgwgbr, rugbguub, rwub, uugrbw, rbr, rbb, wwguw, bb, ubuwr, ggbwrb, ubu, bwgb, ugw, rwrrgw, bbrrg, wgbggw, rrwuwrwu, urgggr, gwwggw, bu, wbw, rggrb, wbrru, brw, gww, gwg, ubuuuw, wwrg, wwuwrug, grwgrb, gbb, uwwb, wbgr, wwwwg, rugr, bwrb, gg, ubggg, gbrwru, brbr, ub, gbrbug, gguwurw, bggr, bbb, wubbbu, wub, brgbwb, wbrbg, wwrw, bgw, uwwg, rrrrgw, uwb, urubg, gwr, wrgrg, ggr, wbubbu, uwu, bgrbb, ggbg, brg, rwwrb, uwug, bwwww, grb, wgwrwgb, wgrbbr, wrr, rwwwr, gwwwu, buw, gbw, wug, wugw, wuw, ggbww, wrggrbw, wru, wwrguwu, wwbb, bguw, wrbb, rub, rbbr, gwuw, gbbggb, rw, gwgb, wrubbb, wbgur, bwbwbgg, rgbb, wbrr, www, bgbg, ubwg, wrggggr, wgbuug, gwub, buubuug, brbb, ggugwbu, rguwb, bgu, gubuurgb, bgub, uwbbrg, gbru, wwu, grbgbbw, rbw, ggg, wrb, rb, wbwgrurw, wgrgu, wgb, uuubrwg, gwgu, wubww, guubw, rgggrwb, uurw, w, rbuu, ugwwu, buwwubg, ubgrgb, ugr, uggwbru, ggb, bbu, rur, wuuuwbu, gru, rbgwu, ugbwbgr, ggrrb, rgr, rugg, uu, ugrgb, wbr, gguwrrwb, rbrgg, wgbwwg, uur, ggbr, rrww, bur, wuwgur, urbb, uugr, ugwr, ubrgu, wbgggg, uugrb, bgggr, rwg, bwgw, wbwg, uwgbb, wrwu, gbg, ggrbu, rwubr
			
			wrgwrwgrbbgwrugbbugbgggubbguwrugrbrggurwuwrrww
			""";

		Assert.That(SolutionDay19.RunPart2([.. input.Split(Environment.NewLine)]), Is.EqualTo(new BigInteger(57_847_513_816)));
	}
}