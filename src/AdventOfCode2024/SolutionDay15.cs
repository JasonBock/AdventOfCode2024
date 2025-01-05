using System.Collections.Immutable;
using System.Diagnostics;
using System.Numerics;

namespace AdventOfCode2024.Day15;

public static class SolutionDay15
{
	private const char Wall = '#';
	private const char Box = 'O';
	private const char Robot = '@';

	public static BigInteger RunPart1(ImmutableArray<string> input)
	{
		var (items, movements) = SolutionDay15.ParseInput(input);

		foreach (var movement in movements)
		{
			foreach (var movementCommand in movement)
			{
				var robot = items.Single(r => r.Type == Robot);

				var itemsToMove = new List<Item>();

				if (movementCommand == '^')
				{
					var currentItem = robot;

					while (true)
					{
						itemsToMove.Add(currentItem);
						var nextItem = items.SingleOrDefault(t => t.X == currentItem.X && t.Y == currentItem.Y - 1);

						if (nextItem is null)
						{
							break;
						}
						else if (nextItem.Type == Box)
						{
							currentItem = nextItem;
						}
						else
						{
							itemsToMove.Clear();
							break;
						}
					}

					if (itemsToMove.Count > 0)
					{
						foreach (var itemToMove in itemsToMove)
						{
							items.Remove(itemToMove);
							items.Add(itemToMove with { Y = itemToMove.Y - 1 });
						}
					}
				}
				else if (movementCommand == '>')
				{
					var currentItem = robot;

					while (true)
					{
						itemsToMove.Add(currentItem);
						var nextItem = items.SingleOrDefault(t => t.X == currentItem.X + 1 && t.Y == currentItem.Y);

						if (nextItem is null)
						{
							break;
						}
						else if (nextItem.Type == Box)
						{
							currentItem = nextItem;
						}
						else
						{
							itemsToMove.Clear();
							break;
						}
					}

					if (itemsToMove.Count > 0)
					{
						foreach (var itemToMove in itemsToMove)
						{
							items.Remove(itemToMove);
							items.Add(itemToMove with { X = itemToMove.X + 1 });
						}
					}
				}
				else if (movementCommand == 'v')
				{
					var currentItem = robot;

					while (true)
					{
						itemsToMove.Add(currentItem);
						var nextItem = items.SingleOrDefault(t => t.X == currentItem.X && t.Y == currentItem.Y + 1);

						if (nextItem is null)
						{
							break;
						}
						else if (nextItem.Type == Box)
						{
							currentItem = nextItem;
						}
						else
						{
							itemsToMove.Clear();
							break;
						}
					}

					if (itemsToMove.Count > 0)
					{
						foreach (var itemToMove in itemsToMove)
						{
							items.Remove(itemToMove);
							items.Add(itemToMove with { Y = itemToMove.Y + 1 });
						}
					}
				}
				else
				{
					var currentItem = robot;

					while (true)
					{
						itemsToMove.Add(currentItem);
						var nextItem = items.SingleOrDefault(t => t.X == currentItem.X - 1 && t.Y == currentItem.Y);

						if (nextItem is null)
						{
							break;
						}
						else if (nextItem.Type == Box)
						{
							currentItem = nextItem;
						}
						else
						{
							itemsToMove.Clear();
							break;
						}
					}

					if (itemsToMove.Count > 0)
					{
						foreach (var itemToMove in itemsToMove)
						{
							items.Remove(itemToMove);
							items.Add(itemToMove with { X = itemToMove.X - 1 });
						}
					}
				}
			}
		}

		var gpsSum = BigInteger.Zero;

		foreach (var box in items.Where(i => i.Type == Box))
		{
			gpsSum += box.X + (100 * box.Y);
		}

		return gpsSum;
	}

	public static BigInteger RunPart2(ImmutableArray<string> input)
	{
		var (entities, movements) = SolutionDay15.ParseDoubleInput(input);

		foreach (var movement in movements)
		{
			foreach (var movementCommand in movement)
			{
			}
		}

		var gpsSum = BigInteger.Zero;

		foreach (var box in entities.OfType<Box>())
		{
			gpsSum += box.LeftPosition.X + (100 * box.LeftPosition.Y);
		}

		return gpsSum;
	}

	private static (List<Item>, ImmutableArray<string>) ParseInput(ImmutableArray<string> input)
	{
		var items = new List<Item>();

		for (var y = 0; y < input.Length; y++)
		{
			var data = input[y];

			if (data.Length == 0)
			{
				return (items, input[(y + 1)..]);
			}
			else
			{
				for (var x = 0; x < data.Length; x++)
				{
					var type = data[x];

					if (type == Wall || type == Box || type == Robot)
					{
						items.Add(new Item(type, x, y));
					}
				}
			}
		}

		return (items, []);
	}

	private static (List<Entity>, ImmutableArray<string>) ParseDoubleInput(ImmutableArray<string> input)
	{
		var entities = new List<Entity>();

		for (var y = 0; y < input.Length; y++)
		{
			var data = input[y];

			if (data.Length == 0)
			{
				return (entities, input[(y + 1)..]);
			}
			else
			{
				for (var x = 0; x < data.Length; x++)
				{
					var type = data[x];

					if (type == Wall)
					{
						entities.Add(new Wall(new Position(x * 2, y)));
						entities.Add(new Wall(new Position((x * 2) + 1, y)));
					}
					else if (type == Box)
					{
						entities.Add(new Box(new Position(x * 2, y), new Position((x * 2) + 1, y)));
					}
					else if (type == Robot)
					{
						entities.Add(new Robot(new Position(x * 2, y)));
					}
				}
			}
		}

		return (entities, []);
	}
}

public sealed record Item(char Type, int X, int Y);

public sealed record Position(int X, int Y);

public abstract class Entity;

[DebuggerDisplay("Wall - ({Position.X}, {Position.Y})")]
public sealed class Wall
	: Entity
{
	public Wall(Position position) => this.Position = position;

	public Position Position { get; init; }
}

[DebuggerDisplay("Robot - ({Position.X}, {Position.Y})")]
public sealed class Robot
	: Entity
{
	public Robot(Position position) => this.Position = position;

	public Position Position { get; init; }
}

[DebuggerDisplay("Box - ({LeftPosition.X}, {LeftPosition.Y}), ({RightPosition.X}, {RightPosition.Y})")]
public sealed class Box
	: Entity
{
	public Box(Position leftPosition, Position rightPosition) =>
		(this.LeftPosition, this.RightPosition) =
			(leftPosition, rightPosition);

	public Position LeftPosition { get; init; }
	public Position RightPosition { get; init; }
}
