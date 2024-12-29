using System.Collections.Immutable;
using HikingTrail = System.Collections.Generic.List<AdventOfCode2024.Day10.MapPosition>;

namespace AdventOfCode2024.Day10;

public static class SolutionDay10
{
	public static long RunPart1(ImmutableArray<string> input)
	{
		var trailHeadScoreSummation = 0L;

		var maxX = input[0].Length;
		var maxY = input.Length;

		for (var y = 0; y < input.Length; y++)
		{
			var line = input[y];

			for (var x = 0; x < line.Length; x++)
			{
				var height = line[x] - '0';

				if (height == 0)
				{
					trailHeadScoreSummation += FindScore(new MapPosition(0, x, y), input, maxX, maxY);
				}
			}
		}

		return trailHeadScoreSummation;
	}

	private static int FindScore(MapPosition position, ImmutableArray<string> input, int maxX, int maxY)
	{
		var hikingTrails = new List<HikingTrail>() { new() { position } };

		while (hikingTrails.Count > 0)
		{
			var currentHikingTrailsCount = hikingTrails.Count;
			var completions = 0;

			for (var i = currentHikingTrailsCount - 1; i >= 0; i--)
			{
				var currentHikingTrail = hikingTrails[i];
				var currentHikingTrailEnd = currentHikingTrail[^1];

				if (currentHikingTrailEnd.Value == 9)
				{
					completions++;
				}
				else
				{
					// Need to look in all directions and determine if there
					// are valid continuations.

					// Up
					if (currentHikingTrailEnd.Y > 0)
					{
						var nextValue = input[currentHikingTrailEnd.Y - 1][currentHikingTrailEnd.X] - '0';

						if (nextValue - currentHikingTrailEnd.Value == 1)
						{
							var newHikingTrail = new HikingTrail(currentHikingTrail)
							{
								new MapPosition(nextValue, currentHikingTrailEnd.X, currentHikingTrailEnd.Y - 1)
							};

							hikingTrails.Add(newHikingTrail);
						}
					}

					// Right
					if (currentHikingTrailEnd.X < maxX - 1)
					{
						var nextValue = input[currentHikingTrailEnd.Y][currentHikingTrailEnd.X + 1] - '0';

						if (nextValue - currentHikingTrailEnd.Value == 1)
						{
							var newHikingTrail = new HikingTrail(currentHikingTrail)
							{
								new MapPosition(nextValue, currentHikingTrailEnd.X + 1, currentHikingTrailEnd.Y)
							};

							hikingTrails.Add(newHikingTrail);
						}
					}

					// Down
					if (currentHikingTrailEnd.Y < maxY - 1)
					{
						var nextValue = input[currentHikingTrailEnd.Y + 1][currentHikingTrailEnd.X] - '0';

						if (nextValue - currentHikingTrailEnd.Value == 1)
						{
							var newHikingTrail = new HikingTrail(currentHikingTrail)
							{
								new MapPosition(nextValue, currentHikingTrailEnd.X, currentHikingTrailEnd.Y + 1)
							};

							hikingTrails.Add(newHikingTrail);
						}
					}

					// Left
					if (currentHikingTrailEnd.X > 0)
					{
						var nextValue = input[currentHikingTrailEnd.Y][currentHikingTrailEnd.X - 1] - '0';

						if (nextValue - currentHikingTrailEnd.Value == 1)
						{
							var newHikingTrail = new HikingTrail(currentHikingTrail)
							{
								new MapPosition(nextValue, currentHikingTrailEnd.X - 1, currentHikingTrailEnd.Y)
							};

							hikingTrails.Add(newHikingTrail);
						}
					}

					// Remove it regardless if we found new paths or not.
					hikingTrails.RemoveAt(i);
				}
			}

			if (completions == hikingTrails.Count)
			{
				break;
			}
		}

		var uniqueEnds = new HashSet<MapPosition>();

		foreach (var hikingTrail in hikingTrails)
		{
			_ = uniqueEnds.Add(hikingTrail[^1]);
		}

		return uniqueEnds.Count;
	}

	public static long RunPart2(ImmutableArray<string> input)
	{
		var x = input.Length;

		return x;
	}
}

public sealed record MapPosition(int Value, int X, int Y);