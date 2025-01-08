using System.Collections.Immutable;
using System.Numerics;
using Map = System.Collections.Immutable.ImmutableArray<AdventOfCode2024.Day16.MapItem>;

namespace AdventOfCode2024.Day16;

public static class SolutionDay16
{
	public const char End = 'E';
	public const char Start = 'S';
	public const char Wall = '#';

	public static long RunPart1(ImmutableArray<string> input)
	{
		var map = SolutionDay16.ParseInput(input);

		var minimalCost = long.MaxValue;

		var startLocation = map.Single(_ => _.Type == SolutionDay16.Start);
		var startPath = new Path(map, 0, 0, startLocation.Position, Direction.East, false);

		var pathsToEvaluate = new List<Path>() { startPath };

		while (pathsToEvaluate.Count > 0)
		{
			Console.WriteLine(
				$"Current Path Evaluation Count: {pathsToEvaluate.Count}, Longest Path: {pathsToEvaluate.MaxBy(_ => _.TraversedPositionCount)!.TraversedPositionCount}");
			var newPaths = new List<Path>();

			foreach (var pathToEvaluate in pathsToEvaluate)
			{
				if (pathToEvaluate.CurrentCost < minimalCost)
				{
					var nextPaths = pathToEvaluate.GetNextPaths();
					var minimalFinishedNextPath = nextPaths.Where(_ => _.IsFinished).MinBy(_ => _.CurrentCost);

					if (minimalFinishedNextPath?.CurrentCost < minimalCost)
					{
						minimalCost = minimalFinishedNextPath.CurrentCost;
					}

					newPaths.AddRange(nextPaths.Where(_ => !_.IsFinished && _.CurrentCost < minimalCost));
				}
			}

			pathsToEvaluate = newPaths;
		}

		return minimalCost;
	}

	private static Map ParseInput(ImmutableArray<string> input)
	{
		var mapItems = new List<MapItem>();

		for (var y = 0; y < input.Length; y++)
		{
			var data = input[y];

			for (var x = 0; x < data.Length; x++)
			{
				var mapType = data[x];

				if (mapType == Start || mapType == End || mapType == Wall)
				{
					mapItems.Add(new MapItem(mapType, new Position(x, y)));
				}
			}
		}

		return [.. mapItems];
	}
}

public enum Direction { West, North, East, South }

public sealed record Path(Map Map, int TraversedPositionCount, int NumberOfTurns,
	Position CurrentPosition, Direction CurrentDirection, bool IsFinished)
{
	public ImmutableArray<Path> GetNextPaths()
	{
		var newPaths = new List<Path>();

		if (this.CurrentDirection != Direction.West)
		{
			// Look East
			var nextMapItem = this.Map.SingleOrDefault(
				_ => _.Position.X == this.CurrentPosition.X + 1 && _.Position.Y == this.CurrentPosition.Y);

			if (nextMapItem is null || (nextMapItem.Type != SolutionDay16.Wall && nextMapItem.Type == SolutionDay16.End))
			{
				newPaths.Add(this with
				{
					TraversedPositionCount = this.TraversedPositionCount + 1,
					NumberOfTurns = this.CurrentDirection != Direction.East ? this.NumberOfTurns + 1 : this.NumberOfTurns,
					CurrentPosition = this.CurrentPosition with { X = this.CurrentPosition.X + 1 },
					CurrentDirection = Direction.East,
					IsFinished = nextMapItem is not null
				});
			}
		}

		if (this.CurrentDirection != Direction.North)
		{
			// Look South
			var nextMapItem = this.Map.SingleOrDefault(
				_ => _.Position.X == this.CurrentPosition.X && _.Position.Y == this.CurrentPosition.Y + 1);

			if (nextMapItem is null || (nextMapItem.Type != SolutionDay16.Wall && nextMapItem.Type == SolutionDay16.End))
			{
				newPaths.Add(this with
				{
					TraversedPositionCount = this.TraversedPositionCount + 1,
					NumberOfTurns = this.CurrentDirection != Direction.South ? this.NumberOfTurns + 1 : this.NumberOfTurns,
					CurrentPosition = this.CurrentPosition with { Y = this.CurrentPosition.Y + 1 },
					CurrentDirection = Direction.South,
					IsFinished = nextMapItem is not null
				});
			}
		}

		if (this.CurrentDirection != Direction.East)
		{
			// Look West
			var nextMapItem = this.Map.SingleOrDefault(
				_ => _.Position.X == this.CurrentPosition.X - 1 && _.Position.Y == this.CurrentPosition.Y);

			if (nextMapItem is null || (nextMapItem.Type != SolutionDay16.Wall && nextMapItem.Type == SolutionDay16.End))
			{
				newPaths.Add(this with
				{
					TraversedPositionCount = this.TraversedPositionCount + 1,
					NumberOfTurns = this.CurrentDirection != Direction.West ? this.NumberOfTurns + 1 : this.NumberOfTurns,
					CurrentPosition = this.CurrentPosition with { X = this.CurrentPosition.X - 1 },
					CurrentDirection = Direction.West,
					IsFinished = nextMapItem is not null
				});
			}
		}

		if (this.CurrentDirection != Direction.South)
		{
			// Look North
			var nextMapItem = this.Map.SingleOrDefault(
				_ => _.Position.X == this.CurrentPosition.X && _.Position.Y == this.CurrentPosition.Y - 1);

			if (nextMapItem is null || (nextMapItem.Type != SolutionDay16.Wall && nextMapItem.Type == SolutionDay16.End))
			{
				newPaths.Add(this with
				{
					TraversedPositionCount = this.TraversedPositionCount + 1,
					NumberOfTurns = this.CurrentDirection != Direction.North ? this.NumberOfTurns + 1 : this.NumberOfTurns,
					CurrentPosition = this.CurrentPosition with { Y = this.CurrentPosition.Y - 1 },
					CurrentDirection = Direction.North,
					IsFinished = nextMapItem is not null
				});
			}
		}

		return [.. newPaths];
	}

	public long CurrentCost => this.TraversedPositionCount + (1_000L * this.NumberOfTurns);
}

public sealed record Position(int X, int Y);
public sealed record MapItem(char Type, Position Position);
public sealed record Reindeer(Direction CurrentDirection, Position Position);