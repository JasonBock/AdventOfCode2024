using AdventOfCode2024.Day1;

RunDay1();

static void RunDay1()
{
	Console.WriteLine(nameof(RunDay1));
	Console.WriteLine($"Part 1 = {SolutionDay1.RunPart1([.. File.ReadAllLines("Day1Input.txt")])}");
	Console.WriteLine($"Part 2 = {SolutionDay1.RunPart2([.. File.ReadAllLines("Day1Input.txt")])}");
}