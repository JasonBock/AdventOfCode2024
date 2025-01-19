using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace AdventOfCode2024.Day20;

public static class SolutionDay20
{
	private const char End = 'E';
	private const char Path = '.';
	private const char Start = 'S';
	private const char Wall = '#';

	public static int RunPart1(ImmutableArray<string> input, int minimumSaving)
	{
		var (path, cheats) = SolutionDay20.GetPath(input);

		// Key is savings
		// Value is the count
		var cheatSavings = 0;

		foreach (var cheat in cheats)
		{
			var startingIndex = path.IndexOf(cheat.Item1);
			var endingIndex = path.IndexOf(cheat.Item2);

			var savings = endingIndex - startingIndex - 2;

			if (savings >= minimumSaving)
			{
				cheatSavings++;
			}
		}

		return cheatSavings;
	}

	private static (ImmutableArray<Position>, ImmutableArray<(Position, Position)>) GetPath(ImmutableArray<string> input)
	{
		Position? startPosition = null;

		for (var y = 0; y < input.Length; y++)
		{
			var x = input[y].IndexOf(SolutionDay20.Start, StringComparison.CurrentCulture);

			if (x >= 0)
			{
				startPosition = new Position(x, y);
				break;
			}
		}

		var path = new List<Position> { startPosition! };
		var cheats = new HashSet<(Position, Position)>();

		var maxX = input[0].Length;
		var maxY = input.Length;

		var currentPosition = startPosition!;

		while (true)
		{
			// At our current position:
			// * Find all the cheats
			// * Find the next position, and if that equals endPosition, we're done and we break

			// Look East (x + 1)
			var eastPosition = currentPosition with { X = currentPosition.X + 1 };
			var eastCharacter = input[eastPosition.Y][eastPosition.X];

			if (eastCharacter == SolutionDay20.Wall)
			{
				var secondCharacter = input[eastPosition.Y][(eastPosition.X + 1 + maxX) % maxX];

				if (secondCharacter == SolutionDay20.Path || secondCharacter == SolutionDay20.End)
				{
					var endCheatPosition = currentPosition with { X = currentPosition.X + 2 };

					if (!path.Contains(endCheatPosition))
					{
						cheats.Add(new(currentPosition, endCheatPosition));
					}
				}
			}
			else if (!path.Contains(eastPosition))
			{
				path.Add(eastPosition);

				if (eastCharacter == SolutionDay20.End)
				{
					break;
				}
			}

			// Look South (y + 1)
			var southPosition = currentPosition with { Y = currentPosition.Y + 1 };
			var southCharacter = input[southPosition.Y][southPosition.X];

			if (southCharacter == SolutionDay20.Wall)
			{
				var secondCharacter = input[(southPosition.Y + 1 + maxY) % maxY][southPosition.X];

				if (secondCharacter == SolutionDay20.Path || secondCharacter == SolutionDay20.End)
				{
					var endCheatPosition = currentPosition with { Y = currentPosition.Y + 2 };

					if (!path.Contains(endCheatPosition))
					{
						cheats.Add(new(currentPosition, endCheatPosition));
					}
				}
			}
			else if (!path.Contains(southPosition))
			{
				path.Add(southPosition);

				if (southCharacter == SolutionDay20.End)
				{
					break;
				}
			}

			// Look West (x - 1)
			var westPosition = currentPosition with { X = currentPosition.X - 1 };
			var westCharacter = input[westPosition.Y][westPosition.X];

			if (westCharacter == SolutionDay20.Wall)
			{
				var secondCharacter = input[westPosition.Y][(westPosition.X - 1 + maxX) % maxX];

				if (secondCharacter == SolutionDay20.Path || secondCharacter == SolutionDay20.End)
				{
					var endCheatPosition = currentPosition with { X = currentPosition.X - 2 };

					if (!path.Contains(endCheatPosition))
					{
						cheats.Add(new(currentPosition, endCheatPosition));
					}
				}
			}
			else if (!path.Contains(westPosition))
			{
				path.Add(westPosition);

				if (westCharacter == SolutionDay20.End)
				{
					break;
				}
			}

			// Look North (y - 1)
			var northPosition = currentPosition with { Y = currentPosition.Y - 1 };
			var northCharacter = input[northPosition.Y][northPosition.X];

			if (northCharacter == SolutionDay20.Wall)
			{
				var secondCharacter = input[(northPosition.Y - 1 + maxY) % maxY][northPosition.X];

				if (secondCharacter == SolutionDay20.Path || secondCharacter == SolutionDay20.End)
				{
					var endCheatPosition = currentPosition with { Y = currentPosition.Y - 2 };

					if (!path.Contains(endCheatPosition))
					{
						cheats.Add(new(currentPosition, endCheatPosition));
					}
				}
			}
			else if (!path.Contains(northPosition))
			{
				path.Add(northPosition);

				if (northCharacter == SolutionDay20.End)
				{
					break;
				}
			}

			currentPosition = path[^1];
		}

		return ([.. path], [.. cheats]);
	}
}

public sealed record Position(int X, int Y);