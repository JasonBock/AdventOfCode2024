using System.Collections.Immutable;
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
}

public sealed record Item(char Type, int X, int Y);