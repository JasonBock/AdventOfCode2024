using System.Collections.Immutable;
using Map = System.Collections.Generic.List<System.Collections.Generic.List<int>>;

namespace AdventOfCode2024.Day6;

public static class SolutionDay6
{
	public static int RunPart1(ImmutableArray<string> input)
	{
		var (map, guard) = GetMap(input);

		var maxX = input[0].Length - 1;
		var maxY = map.Count - 1;

		return GetGuardPositions(map, guard, maxX, maxY).Count;
	}

	public static int RunPart2(ImmutableArray<string> input)
	{
		// We could create a whole bunch of arbitrary maps
		// where we put obstacles at indexes that don't currently exist.
		// Then, run the map. If the guard escapes, that's not a valid position.
		// If the guard ever retraces their position on their route, that is a valid position.
		var effectiveNewObstacleCount = 0;

		var (map, guard) = GetMap(input);

		var maxX = input[0].Length - 1;
		var maxY = map.Count - 1;

		var guardPositions = GetGuardPositions(map, guard, maxX, maxY);

		foreach (var guardPosition in guardPositions)
		{
			try
			{
				map[guardPosition.Y].Add(guardPosition.X);

				if (!DoesGuardEscape(map, guard, maxX, maxY))
				{
					effectiveNewObstacleCount++;
				}
			}
			finally
			{
				_ = map[guardPosition.Y].Remove(guardPosition.X);
			}
		}

		return effectiveNewObstacleCount;
	}

	private static bool DoesGuardEscape(Map map, Guard guard, int maxX, int maxY)
	{
		var guardPositions = new HashSet<(Guard, Direction)>
		{
			(guard, Direction.Up)
		};

		var direction = Direction.Up;

		while (!(guard.X == 0 || guard.Y == 0 || guard.X >= maxX || guard.Y >= maxY))
		{
			if (direction == Direction.Up)
			{
				if (!map[guard.Y - 1].Contains(guard.X))
				{
					guard = new(guard.X, guard.Y - 1);

					if (!guardPositions.Add((guard, direction)))
					{
						return false;
					}
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

					if (!guardPositions.Add((guard, direction)))
					{
						return false;
					}
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

					if (!guardPositions.Add((guard, direction)))
					{
						return false;
					}
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

					if (!guardPositions.Add((guard, direction)))
					{
						return false;
					}
				}
				else
				{
					direction = Direction.Up;
				}
			}
		}

		return true;
	}

	private static (Map, Guard) GetMap(ImmutableArray<string> input)
	{
		var map = new Map(input.Length);
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

		return guard is null ?
			throw new NotSupportedException("Guard should have been assigned.") :
			((Map, Guard))(map, guard);
	}

	private static HashSet<Guard> GetGuardPositions(Map map, Guard guard, int maxX, int maxY)
	{
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

		return guardPositions;
	}
}

public sealed record Guard(int X, int Y);

public enum Direction { Up, Down, Left, Right }