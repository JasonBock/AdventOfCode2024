using System.Collections.Immutable;
using Region = System.Collections.Generic.HashSet<AdventOfCode2024.Day12.Plot>;
using Line = System.Collections.Generic.List<(AdventOfCode2024.Day12.Plot Plot, AdventOfCode2024.Day12.Side Side)>;

namespace AdventOfCode2024.Day12;

public static class SolutionDay12
{
	public static long RunPart1(ImmutableArray<string> input)
	{
		var maxX = input[0].Length;
		var maxY = input.Length;

		var regions = new List<Region>();

		for (var y = 0; y < input.Length; y++)
		{
			var line = input[y];

			for (var x = 0; x < input.Length; x++)
			{
				var plant = line[x];

				if (!regions.Any(
					region => region.Any(
						plot => plot.X == x && plot.Y == y)))
				{
					var plot = new Plot(plant, x, y);
					var newRegion = new Region() { plot };

					var plotsToAnalyze = new List<Plot>() { plot };

					while (true)
					{
						var newPlotsToAnalyze = new List<Plot>();

						foreach (var plotToAnalyze in plotsToAnalyze)
						{
							// Left
							if (plotToAnalyze.X > 0 && input[plotToAnalyze.Y][plotToAnalyze.X - 1] == plant)
							{
								var newPlotToAnalyze = new Plot(plant, plotToAnalyze.X - 1, plotToAnalyze.Y);
								if (newRegion.Add(newPlotToAnalyze))
								{
									newPlotsToAnalyze.Add(newPlotToAnalyze);
								}
							}

							// Up
							if (plotToAnalyze.Y > 0 && input[plotToAnalyze.Y - 1][plotToAnalyze.X] == plant)
							{
								var newPlotToAnalyze = new Plot(plant, plotToAnalyze.X, plotToAnalyze.Y - 1);
								if (newRegion.Add(newPlotToAnalyze))
								{
									newPlotsToAnalyze.Add(newPlotToAnalyze);
								}
							}

							// Right
							if (plotToAnalyze.X < maxX - 1 && input[plotToAnalyze.Y][plotToAnalyze.X + 1] == plant)
							{
								var newPlotToAnalyze = new Plot(plant, plotToAnalyze.X + 1, plotToAnalyze.Y);
								if (newRegion.Add(newPlotToAnalyze))
								{
									newPlotsToAnalyze.Add(newPlotToAnalyze);
								}
							}

							// Down
							if (plotToAnalyze.Y < maxY - 1 && input[plotToAnalyze.Y + 1][plotToAnalyze.X] == plant)
							{
								var newPlotToAnalyze = new Plot(plant, plotToAnalyze.X, plotToAnalyze.Y + 1);
								if (newRegion.Add(newPlotToAnalyze))
								{
									newPlotsToAnalyze.Add(newPlotToAnalyze);
								}
							}
						}

						if (newPlotsToAnalyze.Count == 0)
						{
							break;
						}

						plotsToAnalyze = newPlotsToAnalyze;
					}

					regions.Add(newRegion);
				}
			}
		}

		// Now we have all the regions. Go through each one,
		// get the area and perimeter,
		// calculate the price,
		// and add it to the total price.

		var price = 0;

		foreach (var region in regions)
		{
			var area = region.Count;

			var perimeter = 0;

			foreach (var plot in region)
			{
				// Left
				if (plot.X == 0 || input[plot.Y][plot.X - 1] != plot.Plant)
				{
					perimeter++;
				}

				// Up
				if (plot.Y == 0 || input[plot.Y - 1][plot.X] != plot.Plant)
				{
					perimeter++;
				}

				// Right
				if (plot.X == maxX - 1 || input[plot.Y][plot.X + 1] != plot.Plant)
				{
					perimeter++;
				}

				// Down
				if (plot.Y == maxY - 1 || input[plot.Y + 1][plot.X] != plot.Plant)
				{
					perimeter++;
				}
			}

			price += area * perimeter;
		}

		return price;
	}

	public static long RunPart2(ImmutableArray<string> input)
	{
		var maxX = input[0].Length;
		var maxY = input.Length;

		var regions = new List<Region>();

		for (var y = 0; y < input.Length; y++)
		{
			var line = input[y];

			for (var x = 0; x < input.Length; x++)
			{
				var plant = line[x];

				if (!regions.Any(
					region => region.Any(
						plot => plot.X == x && plot.Y == y)))
				{
					var plot = new Plot(plant, x, y);
					var newRegion = new Region() { plot };

					var plotsToAnalyze = new List<Plot>() { plot };

					while (true)
					{
						var newPlotsToAnalyze = new List<Plot>();

						foreach (var plotToAnalyze in plotsToAnalyze)
						{
							// Left
							if (plotToAnalyze.X > 0 && input[plotToAnalyze.Y][plotToAnalyze.X - 1] == plant)
							{
								var newPlotToAnalyze = new Plot(plant, plotToAnalyze.X - 1, plotToAnalyze.Y);
								if (newRegion.Add(newPlotToAnalyze))
								{
									newPlotsToAnalyze.Add(newPlotToAnalyze);
								}
							}

							// Up
							if (plotToAnalyze.Y > 0 && input[plotToAnalyze.Y - 1][plotToAnalyze.X] == plant)
							{
								var newPlotToAnalyze = new Plot(plant, plotToAnalyze.X, plotToAnalyze.Y - 1);
								if (newRegion.Add(newPlotToAnalyze))
								{
									newPlotsToAnalyze.Add(newPlotToAnalyze);
								}
							}

							// Right
							if (plotToAnalyze.X < maxX - 1 && input[plotToAnalyze.Y][plotToAnalyze.X + 1] == plant)
							{
								var newPlotToAnalyze = new Plot(plant, plotToAnalyze.X + 1, plotToAnalyze.Y);
								if (newRegion.Add(newPlotToAnalyze))
								{
									newPlotsToAnalyze.Add(newPlotToAnalyze);
								}
							}

							// Down
							if (plotToAnalyze.Y < maxY - 1 && input[plotToAnalyze.Y + 1][plotToAnalyze.X] == plant)
							{
								var newPlotToAnalyze = new Plot(plant, plotToAnalyze.X, plotToAnalyze.Y + 1);
								if (newRegion.Add(newPlotToAnalyze))
								{
									newPlotsToAnalyze.Add(newPlotToAnalyze);
								}
							}
						}

						if (newPlotsToAnalyze.Count == 0)
						{
							break;
						}

						plotsToAnalyze = newPlotsToAnalyze;
					}

					regions.Add(newRegion);
				}
			}
		}

		// Now we have all the regions. Go through each one,
		// get the area and perimeter,
		// calculate the price,
		// and add it to the total price.

		var price = 0;

		foreach (var region in regions)
		{
			var area = region.Count;

			var lines = new List<Line>();

			foreach (var plot in region)
			{
				// Left
				if (plot.X == 0 || input[plot.Y][plot.X - 1] != plot.Plant)
				{
					var existingLines = lines.Where(
						l => l.Any(p =>
							p.Side == Side.Left &&
							p.Plot.X == plot.X && (p.Plot.Y == plot.Y + 1 || p.Plot.Y == plot.Y - 1))).ToList();

					if (existingLines.Count > 1)
					{
						var newLine = new Line() { (plot, Side.Left) };

						foreach (var existingLine in existingLines)
						{
							newLine.AddRange(existingLine);
							_ = lines.Remove(existingLine);
						}

						lines.Add(newLine);
					}
					else if (existingLines.Count == 1)
					{
						existingLines[0].Add((plot, Side.Left));
					}
					else
					{
						lines.Add([(plot, Side.Left)]);
					}
				}

				// Up
				if (plot.Y == 0 || input[plot.Y - 1][plot.X] != plot.Plant)
				{
					var existingLines = lines.Where(
						l => l.Any(p =>
							p.Side == Side.Top &&
							p.Plot.Y == plot.Y && (p.Plot.X == plot.X - 1 || p.Plot.X == plot.X + 1))).ToList();

					if (existingLines.Count > 1)
					{
						var newLine = new Line() { (plot, Side.Top) };

						foreach (var existingLine in existingLines)
						{
							newLine.AddRange(existingLine);
							_ = lines.Remove(existingLine);
						}

						lines.Add(newLine);
					}
					else if (existingLines.Count == 1)
					{
						existingLines[0].Add((plot, Side.Top));
					}
					else
					{
						lines.Add([(plot, Side.Top)]);
					}
				}

				// Right
				if (plot.X == maxX - 1 || input[plot.Y][plot.X + 1] != plot.Plant)
				{
					var existingLines = lines.Where(
						l => l.Any(p =>
							p.Side == Side.Right &&
							p.Plot.X == plot.X && (p.Plot.Y == plot.Y + 1 || p.Plot.Y == plot.Y - 1))).ToList();

					if (existingLines.Count > 1)
					{
						var newLine = new Line() { (plot, Side.Right) };

						foreach (var existingLine in existingLines)
						{
							newLine.AddRange(existingLine);
							_ = lines.Remove(existingLine);
						}

						lines.Add(newLine);
					}
					else if (existingLines.Count == 1)
					{
						existingLines[0].Add((plot, Side.Right));
					}
					else
					{
						lines.Add([(plot, Side.Right)]);
					}
				}

				// Down
				if (plot.Y == maxY - 1 || input[plot.Y + 1][plot.X] != plot.Plant)
				{
					var existingLines = lines.Where(
						l => l.Any(p =>
							p.Side == Side.Bottom &&
							p.Plot.Y == plot.Y && (p.Plot.X == plot.X - 1 || p.Plot.X == plot.X + 1))).ToList();

					if (existingLines.Count > 1)
					{
						var newLine = new Line() { (plot, Side.Bottom) };

						foreach (var existingLine in existingLines)
						{
							newLine.AddRange(existingLine);
							_ = lines.Remove(existingLine);
						}

						lines.Add(newLine);
					}
					else if (existingLines.Count == 1)
					{
						existingLines[0].Add((plot, Side.Bottom));
					}
					else
					{
						lines.Add([(plot, Side.Bottom)]);
					}
				}
			}

			price += area * lines.Count;
		}

		return price;
	}
}

public sealed record Plot(char Plant, int X, int Y);

public enum Side { Left, Right, Top, Bottom }