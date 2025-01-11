using System.Collections.Immutable;
using Map = System.Collections.Immutable.ImmutableDictionary<AdventOfCode2024.Day16.Position, AdventOfCode2024.Day16.MapItemType>;

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

		var startLocation = map.Single(_ => _.Value == MapItemType.Start);

		var pathsToEvaluate = new List<Path>()
		{
			new(map, 0, 0, startLocation.Key, Direction.East, false, []),
			new(map, 0, 1, startLocation.Key, Direction.North, false, []),
		};

		while (pathsToEvaluate.Count > 0)
		{
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
			
			// TODO: We need to do some pruning.
			pathsToEvaluate = newPaths;
		}

		return minimalCost;
	}

	private static Map ParseInput(ImmutableArray<string> input)
	{
		var mapItems = new Dictionary<Position, MapItemType>();

		for (var y = 0; y < input.Length; y++)
		{
			var data = input[y];

			for (var x = 0; x < data.Length; x++)
			{
				var mapType = data[x];

				if (mapType == Start || mapType == End || mapType == Wall)
				{
					mapItems.Add(new Position(x, y),
						mapType switch
						{
							Start => MapItemType.Start,
							End => MapItemType.End,
							Wall => MapItemType.Wall,
							_ => throw new NotSupportedException()
						});
				}
			}
		}

		return mapItems.ToImmutableDictionary();
	}
}

public enum Direction { West, North, East, South }

public sealed record Path(Map Map, int TraversedPositionCount, int NumberOfTurns,
	Position CurrentPosition, Direction CurrentDirection, bool IsFinished,
	ImmutableHashSet<Position> VisitedJunctions)
{
	public ImmutableArray<Path> GetNextPaths()
	{
		var newPaths = new List<Path>();

		if (this.CurrentDirection == Direction.East)
		{
			// East
			var nextPosition = this.CurrentPosition with { X = this.CurrentPosition.X + 1 };
			var nextPositionCount = 1;

			while (true)
			{
				var foundWall = false;

				if (this.Map.TryGetValue(nextPosition, out var nextMapItem))
				{
					if (nextMapItem == MapItemType.End)
					{
						newPaths.Add(this with
						{
							VisitedJunctions = this.VisitedJunctions.Add(nextPosition),
							TraversedPositionCount = this.TraversedPositionCount + nextPositionCount,
							NumberOfTurns = this.NumberOfTurns,
							CurrentPosition = nextPosition,
							CurrentDirection = Direction.East,
							IsFinished = true
						});

						break;
					}

					foundWall = true;
				}

				// look for paths North, South, and maybe East
				if (!this.Map.ContainsKey(nextPosition with { Y = nextPosition.Y - 1 }))
				{
					if (!this.VisitedJunctions.Contains(nextPosition))
					{
						newPaths.Add(this with
						{
							VisitedJunctions = this.VisitedJunctions.Add(nextPosition),
							TraversedPositionCount = this.TraversedPositionCount + nextPositionCount,
							NumberOfTurns = this.NumberOfTurns + 1,
							CurrentPosition = nextPosition,
							CurrentDirection = Direction.North,
							IsFinished = false
						});
					}
				}

				if (!this.Map.ContainsKey(nextPosition with { Y = nextPosition.Y + 1 }))
				{
					if (!this.VisitedJunctions.Contains(nextPosition))
					{
						newPaths.Add(this with
						{
							VisitedJunctions = this.VisitedJunctions.Add(nextPosition),
							TraversedPositionCount = this.TraversedPositionCount + nextPositionCount,
							NumberOfTurns = this.NumberOfTurns + 1,
							CurrentPosition = nextPosition,
							CurrentDirection = Direction.South,
							IsFinished = false
						});
					}
				}

				if (!foundWall && newPaths.Count > 0)
				{
					if (!this.Map.ContainsKey(nextPosition with { X = nextPosition.X + 1 }))
					{
						if (!this.VisitedJunctions.Contains(nextPosition))
						{
							newPaths.Add(this with
							{
								VisitedJunctions = this.VisitedJunctions.Add(nextPosition),
								TraversedPositionCount = this.TraversedPositionCount + nextPositionCount,
								NumberOfTurns = this.NumberOfTurns,
								CurrentPosition = nextPosition,
								CurrentDirection = Direction.East,
								IsFinished = false
							});
						}
					}
				}

				if (newPaths.Count > 0)
				{
					break;
				}

				nextPosition = nextPosition with { X = nextPosition.X + 1 };
				nextPositionCount++;
			}
		}
		else if (this.CurrentDirection == Direction.South)
		{
			// South
			var nextPosition = this.CurrentPosition with { Y = this.CurrentPosition.Y + 1 };
			var nextPositionCount = 1;

			while (true)
			{
				var foundWall = false;

				if (this.Map.TryGetValue(nextPosition, out var nextMapItem))
				{
					// We'll never hit the end going South,
					// so we don't need to check for that,
					// but...I guess it's possible to hit the start.
					if (nextMapItem == MapItemType.Start)
					{
						break;
					}

					foundWall = true;
				}

				// look for paths East, West, and maybe South
				if (!this.Map.ContainsKey(nextPosition with { X = nextPosition.X + 1 }))
				{
					if (!this.VisitedJunctions.Contains(nextPosition))
					{
						newPaths.Add(this with
						{
							VisitedJunctions = this.VisitedJunctions.Add(nextPosition),
							TraversedPositionCount = this.TraversedPositionCount + nextPositionCount,
							NumberOfTurns = this.NumberOfTurns + 1,
							CurrentPosition = nextPosition,
							CurrentDirection = Direction.East,
							IsFinished = false
						});
					}
				}

				if (!this.Map.ContainsKey(nextPosition with { X = nextPosition.X - 1 }))
				{
					if (!this.VisitedJunctions.Contains(nextPosition))
					{
						newPaths.Add(this with
						{
							VisitedJunctions = this.VisitedJunctions.Add(nextPosition),
							TraversedPositionCount = this.TraversedPositionCount + nextPositionCount,
							NumberOfTurns = this.NumberOfTurns + 1,
							CurrentPosition = nextPosition,
							CurrentDirection = Direction.West,
							IsFinished = false
						});
					}
				}

				if (!foundWall && newPaths.Count > 0)
				{
					if (!this.Map.ContainsKey(nextPosition with { Y = nextPosition.Y + 1 }))
					{
						if (!this.VisitedJunctions.Contains(nextPosition))
						{
							newPaths.Add(this with
							{
								VisitedJunctions = this.VisitedJunctions.Add(nextPosition),
								TraversedPositionCount = this.TraversedPositionCount + nextPositionCount,
								NumberOfTurns = this.NumberOfTurns,
								CurrentPosition = nextPosition,
								CurrentDirection = Direction.South,
								IsFinished = false
							});
						}
					}
				}

				if (newPaths.Count > 0)
				{
					break;
				}

				nextPosition = nextPosition with { Y = nextPosition.Y + 1 };
				nextPositionCount++;
			}
		}
		else if (this.CurrentDirection == Direction.West)
		{
			// West
			var nextPosition = this.CurrentPosition with { X = this.CurrentPosition.X - 1 };
			var nextPositionCount = 1;

			while (true)
			{
				var foundWall = false;

				if (this.Map.TryGetValue(nextPosition, out var nextMapItem))
				{
					// We'll never hit the end going West,
					// so we don't need to check for that,
					// but...I guess it's possible to hit the start.
					if (nextMapItem == MapItemType.Start)
					{
						break;
					}

					foundWall = true;
				}

				// look for paths North, South, and maybe West
				if (!this.Map.ContainsKey(nextPosition with { Y = nextPosition.Y - 1 }))
				{
					if (!this.VisitedJunctions.Contains(nextPosition))
					{
						newPaths.Add(this with
						{
							VisitedJunctions = this.VisitedJunctions.Add(nextPosition),
							TraversedPositionCount = this.TraversedPositionCount + nextPositionCount,
							NumberOfTurns = this.NumberOfTurns + 1,
							CurrentPosition = nextPosition,
							CurrentDirection = Direction.North,
							IsFinished = false
						});
					}
				}

				if (!this.Map.ContainsKey(nextPosition with { Y = nextPosition.Y + 1 }))
				{
					if (!this.VisitedJunctions.Contains(nextPosition))
					{
						newPaths.Add(this with
						{
							VisitedJunctions = this.VisitedJunctions.Add(nextPosition),
							TraversedPositionCount = this.TraversedPositionCount + nextPositionCount,
							NumberOfTurns = this.NumberOfTurns + 1,
							CurrentPosition = nextPosition,
							CurrentDirection = Direction.South,
							IsFinished = false
						});
					}
				}

				if (!foundWall && newPaths.Count > 0)
				{
					if (!this.Map.ContainsKey(nextPosition with { X = nextPosition.X - 1 }))
					{
						if (!this.VisitedJunctions.Contains(nextPosition))
						{
							newPaths.Add(this with
							{
								VisitedJunctions = this.VisitedJunctions.Add(nextPosition),
								TraversedPositionCount = this.TraversedPositionCount + nextPositionCount,
								NumberOfTurns = this.NumberOfTurns,
								CurrentPosition = nextPosition,
								CurrentDirection = Direction.West,
								IsFinished = false
							});
						}
					}
				}

				if (newPaths.Count > 0)
				{
					break;
				}

				nextPosition = nextPosition with { X = nextPosition.X - 1 };
				nextPositionCount++;
			}
		}
		else
		{
			// North
			var nextPosition = this.CurrentPosition with { Y = this.CurrentPosition.Y - 1 };
			var nextPositionCount = 1;

			while (true)
			{
				var foundWall = false;

				if (this.Map.TryGetValue(nextPosition, out var nextMapItem))
				{
					if (nextMapItem == MapItemType.End)
					{
						this.VisitedJunctions.Contains(nextPosition);

						newPaths.Add(this with
						{
							VisitedJunctions = this.VisitedJunctions.Add(nextPosition),
							TraversedPositionCount = this.TraversedPositionCount + nextPositionCount,
							NumberOfTurns = this.NumberOfTurns,
							CurrentPosition = nextPosition,
							CurrentDirection = Direction.North,
							IsFinished = true
						});

						break;
					}

					foundWall = true;
				}

				// look for paths East, West, and maybe North
				if (!this.Map.ContainsKey(nextPosition with { X = nextPosition.X + 1 }))
				{
					if (!this.VisitedJunctions.Contains(nextPosition))
					{
						newPaths.Add(this with
						{
							VisitedJunctions = this.VisitedJunctions.Add(nextPosition),
							TraversedPositionCount = this.TraversedPositionCount + nextPositionCount,
							NumberOfTurns = this.NumberOfTurns + 1,
							CurrentPosition = nextPosition,
							CurrentDirection = Direction.East,
							IsFinished = false
						});
					}
				}

				if (!this.Map.ContainsKey(nextPosition with { X = nextPosition.X - 1 }))
				{
					if (!this.VisitedJunctions.Contains(nextPosition))
					{
						newPaths.Add(this with
						{
							VisitedJunctions = this.VisitedJunctions.Add(nextPosition),
							TraversedPositionCount = this.TraversedPositionCount + nextPositionCount,
							NumberOfTurns = this.NumberOfTurns + 1,
							CurrentPosition = nextPosition,
							CurrentDirection = Direction.West,
							IsFinished = false
						});
					}
				}

				if (!foundWall && newPaths.Count > 0)
				{
					if (!this.Map.ContainsKey(nextPosition with { Y = nextPosition.Y - 1 }))
					{
						if (!this.VisitedJunctions.Contains(nextPosition))
						{
							newPaths.Add(this with
							{
								VisitedJunctions = this.VisitedJunctions.Add(nextPosition),
								TraversedPositionCount = this.TraversedPositionCount + nextPositionCount,
								NumberOfTurns = this.NumberOfTurns,
								CurrentPosition = nextPosition,
								CurrentDirection = Direction.North,
								IsFinished = false
							});
						}
					}
				}

				if (newPaths.Count > 0)
				{
					break;
				}

				nextPosition = nextPosition with { Y = nextPosition.Y - 1 };
				nextPositionCount++;
			}
		}

		return [.. newPaths];
	}

	public long CurrentCost => this.TraversedPositionCount + (1_000L * this.NumberOfTurns);
}

public enum MapItemType { Start, End, Wall }
public sealed record Position(int X, int Y);
public sealed record MapItem(MapItemType Type, Position Position);
public sealed record Reindeer(Direction CurrentDirection, Position Position);