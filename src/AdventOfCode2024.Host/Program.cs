using AdventOfCode2024.Day1;
using AdventOfCode2024.Day2;
using AdventOfCode2024.Day3;

//RunDay1();
//RunDay2();
RunDay3();

static void RunDay1()
{
	Console.WriteLine(nameof(RunDay1));
	Console.WriteLine($"Part 1 = {SolutionDay1.RunPart1([.. File.ReadAllLines("Day1Input.txt")])}");
	Console.WriteLine($"Part 2 = {SolutionDay1.RunPart2([.. File.ReadAllLines("Day1Input.txt")])}");
}

static void RunDay2()
{
	Console.WriteLine(nameof(RunDay2));
	Console.WriteLine($"Part 1 = {SolutionDay2.RunPart1([.. File.ReadAllLines("Day2Input.txt")])}");
	Console.WriteLine($"Part 2 = {SolutionDay2.RunPart2([.. File.ReadAllLines("Day2Input.txt")])}");
}

static void RunDay3()
{
	Console.WriteLine(nameof(RunDay3));
	Console.WriteLine($"Part 1 = {SolutionDay3.RunPart1([.. File.ReadAllLines("Day3Input.txt")])}");
	Console.WriteLine($"Part 2 = {SolutionDay3.RunPart2([.. File.ReadAllLines("Day3Input.txt")])}");
}