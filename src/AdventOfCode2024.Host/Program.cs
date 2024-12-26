using AdventOfCode2024.Day1;

RunDay1();

static void RunDay1()
{
	Console.WriteLine(nameof(RunDay1));
	Console.WriteLine(SolutionDay1.Run([.. File.ReadAllLines("Day1Input.txt")]));
}