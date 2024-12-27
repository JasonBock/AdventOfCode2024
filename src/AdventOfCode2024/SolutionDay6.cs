using System.Collections.Immutable;

namespace AdventOfCode2024.Day6;

public static class SolutionDay6
{
	public static int RunPart1(ImmutableArray<string> input)
	{
		var map = new List<int[]>(input.Length);
		Guard? guard = null;

		for (var r = 0; r < input.Length; r++)
		{
			var item = input[r];
			var indexes = new List<int>();

			for (var i = 0; i < item.Length; i++)
			{
				if (item[i] == '#')
				{
					indexes.Add(i);
				}
				else if (item[i] == '^')
				{
					guard = new(i, r);
				}
			}

			map.Add([.. indexes]);
		}

		var maxX = input[0].Length - 1;
		var maxY = map.Count - 1;

		if (guard is null)
		{
			throw new NotSupportedException("Guard should have been assigned.");
		}

		var guardPositions = new HashSet<Guard>
		{
		   guard
		};

		var direction = Direction.Up;

		while (!(guard.X == 0 || guard.Y == 0 || guard.X >= maxX || guard.Y >= maxY))
		{
			if (direction == Direction.Up)
			{
				if (!map[guard.Y - 1].Contains(guard.X))
				{
					guard = new(guard.X, guard.Y - 1);
					_ = guardPositions.Add(guard);
				}
				else
				{
					direction = Direction.Right;
				}
			}
			else if (direction == Direction.Right)
			{
				if (!map[guard.Y].Contains(guard.X + 1))
				{
					guard = new(guard.X + 1, guard.Y);
					_ = guardPositions.Add(guard);
				}
				else
				{
					direction = Direction.Down;
				}
			}
			else if (direction == Direction.Down)
			{
				if (!map[guard.Y + 1].Contains(guard.X))
				{
					guard = new(guard.X, guard.Y + 1);
					_ = guardPositions.Add(guard);
				}
				else
				{
					direction = Direction.Left;
				}
			}
			else if (direction == Direction.Left)
			{
				if (!map[guard.Y].Contains(guard.X - 1))
				{
					guard = new(guard.X - 1, guard.Y);
					_ = guardPositions.Add(guard);
				}
				else
				{
					direction = Direction.Up;
				}
			}
		}

		return guardPositions.Count;
	}

	public static int RunPart2(ImmutableArray<string> input)
	{
		var x = input.Length;

		return x;
	}
}

public sealed record Guard(int X, int Y);

public enum Direction { Up, Down, Left, Right }