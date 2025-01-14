﻿using System.Collections.Immutable;
using System.Globalization;
using System.Numerics;

namespace AdventOfCode2024.Day14;

public static class SolutionDay14
{
	public static BigInteger RunPart1(ImmutableArray<string> input, int xMax, int yMax)
	{
		var robots = SolutionDay14.GetRobots(input);

		for (var i = 0; i < 100; i++)
		{
			for (var r = 0; r < robots.Count; r++)
			{
				var robot = robots[r];
				var newPosition = new Position(
					((robot.Position.X + robot.Velocity.XChange) + xMax) % xMax,
					((robot.Position.Y + robot.Velocity.YChange) + yMax) % yMax);
				robots[r] = robot with { Position = newPosition };
			}
		}

		return
			robots.Count(r =>
				r.Position.X >= 0 && r.Position.X < xMax / 2 &&
				r.Position.Y >= 0 && r.Position.Y < yMax / 2) *
			robots.Count(r =>
				r.Position.X > (xMax / 2) && r.Position.X < xMax &&
				r.Position.Y >= 0 && r.Position.Y < yMax / 2) *
			robots.Count(r =>
				r.Position.X >= 0 && r.Position.X < xMax / 2 &&
				r.Position.Y > (yMax / 2) && r.Position.Y < yMax) *
			robots.Count(r =>
				r.Position.X > (xMax / 2) && r.Position.X < xMax &&
				r.Position.Y > (yMax / 2) && r.Position.Y < yMax);
	}

	public static void RunPart2(ImmutableArray<string> input, int xMax, int yMax)
	{
		var robots = SolutionDay14.GetRobots(input);
		var increment = 0;

		while (true)
		{
			Console.Clear();

			increment++;

			//Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($"Increment: {increment}");

			for (var r = 0; r < robots.Count; r++)
			{
				var robot = robots[r];
				var newPosition = new Position(
					((robot.Position.X + robot.Velocity.XChange) + xMax) % xMax,
					((robot.Position.Y + robot.Velocity.YChange) + yMax) % yMax);
				robots[r] = robot with { Position = newPosition };
			}

			if (increment % xMax == 14 || increment % yMax == 94)
			{
				for (var y = 0; y < yMax; y++)
				{
					var yRobots = robots.Where(r => r.Position.Y == y).ToList();
					var display = new string(' ', xMax).ToCharArray();

					foreach (var yRobot in yRobots)
					{
						display[yRobot.Position.X] = '*';
					}

					Console.WriteLine(new string(display));
				}

				Console.ReadLine();
			}
		}
	}

	private static List<Robot> GetRobots(ImmutableArray<string> input)
	{
		var robots = new List<Robot>();

		foreach (var line in input)
		{
			var parts = line.Split(' ');
			var positionParts = parts[0][(parts[0].IndexOf('=', StringComparison.CurrentCulture) + 1)..].Split(',');
			var velocityParts = parts[1][(parts[1].IndexOf('=', StringComparison.CurrentCulture) + 1)..].Split(',');
			var robot = new Robot(
				new Position(
					int.Parse(positionParts[0], CultureInfo.CurrentCulture),
					int.Parse(positionParts[1], CultureInfo.CurrentCulture)),
				new Velocity(
					int.Parse(velocityParts[0], CultureInfo.CurrentCulture),
					int.Parse(velocityParts[1], CultureInfo.CurrentCulture)));
			robots.Add(robot);
		}

		return robots;
	}
}

public sealed record Position(int X, int Y);
public sealed record Velocity(int XChange, int YChange);
public sealed record Robot(Position Position, Velocity Velocity);