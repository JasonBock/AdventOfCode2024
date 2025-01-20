using System.Collections.Immutable;

namespace AdventOfCode2024.Day25;

public static class SolutionDay25
{
	const int MaxHeight = 5;

	public static int RunPart1(ImmutableArray<string> input)
	{
		var (keys, locks) = SolutionDay25.ParseInput(input);

		var fits = 0;

		foreach (var @lock in locks)
		{
			foreach (var key in keys)
			{
				if(@lock.Height0 + key.Height0 <= MaxHeight &&
					@lock.Height1 + key.Height1 <= MaxHeight &&
					@lock.Height2 + key.Height2 <= MaxHeight &&
					@lock.Height3 + key.Height3 <= MaxHeight &&
					@lock.Height4 + key.Height4 <= MaxHeight)
				{
					fits++;
				}
			}
		}

		return fits;
	}

	private static (ImmutableArray<Key>, ImmutableArray<Lock>) ParseInput(ImmutableArray<string> input)
	{
		static Key ParseKey(ImmutableArray<string> input, int index)
		{
			var (height0, height1, height2, height3, height4) = (0, 0, 0, 0, 0);

			for (var i = index + 5; i >= index + 1; i--)
			{
				var item = input[i];

				height0 = item[0] == '#' ? height0 + 1 : height0;
				height1 = item[1] == '#' ? height1 + 1 : height1;
				height2 = item[2] == '#' ? height2 + 1 : height2;
				height3 = item[3] == '#' ? height3 + 1 : height3;
				height4 = item[4] == '#' ? height4 + 1 : height4;
			}

			return new Key(height0, height1, height2, height3, height4);
		}

		static Lock ParseLock(ImmutableArray<string> input, int index)
		{
			var (height0, height1, height2, height3, height4) = (0, 0, 0, 0, 0);

			for (var i = index + 1; i < index + 6; i++)
			{
				var item = input[i];

				height0 = item[0] == '#' ? height0 + 1 : height0;
				height1 = item[1] == '#' ? height1 + 1 : height1;
				height2 = item[2] == '#' ? height2 + 1 : height2;
				height3 = item[3] == '#' ? height3 + 1 : height3;
				height4 = item[4] == '#' ? height4 + 1 : height4;
			}

			return new Lock(height0, height1, height2, height3, height4);
		}

		var keys = new List<Key>();
		var locks = new List<Lock>();

		for (var i = 0; i < input.Length; i++)
		{
			if (input[i] == "#####")
			{
				locks.Add(ParseLock(input, i));
			}
			else
			{
				keys.Add(ParseKey(input, i));
			}

			i += 7;
		}

		return ([.. keys], [.. locks]);
	}
}

public sealed record Key(int Height0, int Height1, int Height2, int Height3, int Height4);

public sealed record Lock(int Height0, int Height1, int Height2, int Height3, int Height4);