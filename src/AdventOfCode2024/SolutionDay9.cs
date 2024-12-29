namespace AdventOfCode2024.Day9;

public static class SolutionDay9
{
	public static long RunPart1(string input)
	{
		ArgumentNullException.ThrowIfNull(input);

		var identifier = 0L;
		var index = 0;
		var blocks = new List<Block>();

		while (true)
		{
			var format = (index + 1) < input.Length ?
				new Format(input[index] - '0', input[index + 1] - '0') :
				new Format(input[index] - '0', 0);

			for (var i = 0; i < format.FileSize; i++)
			{
				blocks.Add(new(new(identifier)));
			}

			for (var i = 0; i < format.FreeSpaceSize; i++)
			{
				blocks.Add(new(null));
			}

			index += 2;

			if (index >= input.Length)
			{
				break;
			}

			identifier++;
		}

		var blockIndex = blocks.Count - 1;

		while (true)
		{
			var block = blocks[blockIndex];

			if (block.Owner is not null)
			{
				var freeBlock = blocks.First(_ => _.Owner is null);
				var freeBlockIndex = blocks.IndexOf(freeBlock);

				if (freeBlockIndex < blockIndex)
				{
					blocks[freeBlockIndex] = block;
					blocks[blockIndex] = freeBlock;
				}
				else
				{
					break;
				}
			}

			blockIndex--;
		}

		var checkSum = 0L;

		for (var i = 0; i < blocks.Count; i++)
		{
			var block = blocks[i];

			if (block.Owner is null)
			{
				break;
			}

			checkSum += i * block.Owner.Id;
		}

		return checkSum;
	}

	public static long RunPart2(string input)
	{
		ArgumentNullException.ThrowIfNull(input);

		var identifier = 0L;
		var index = 0;
		var sections = new List<Section>();

		while (true)
		{
			sections.Add(new Section(new Identifier(identifier), input[index] - '0'));

			if ((index + 1) < input.Length)
			{
				sections.Add(new Section(null, input[index + 1] - '0'));
			}

			index += 2;

			if (index >= input.Length)
			{
				break;
			}

			identifier++;
		}

		var sectionIndex = sections.Count - 1;

		while (sectionIndex >= 0)
		{
			var section = sections[sectionIndex];

			if (section.Owner is not null)
			{
				var freeSection = sections.FirstOrDefault(
					_ => _.Owner is null && _.Size >= section.Size);

				if (freeSection is not null)
				{
					var freeSectionIndex = sections.IndexOf(freeSection);

					if (freeSectionIndex < sectionIndex)
					{
						sections[sectionIndex] = new Section(null, section.Size);

						if (freeSection.Size < section.Size)
						{
							sections[freeSectionIndex] = section;
						}
						else
						{
							sections[freeSectionIndex] = new Section(null, freeSection.Size - section.Size);
							sections.Insert(freeSectionIndex, section);
							sectionIndex++;
						}
					}
				}
			}

			sectionIndex--;
		}

		var checkSum = 0L;
		var blockSize = 0;

		for (var i = 0; i < sections.Count; i++)
		{
			var section = sections[i];

			if (section.Owner is not null)
			{
				for (var j = 0; j < section.Size; j++)
				{
					checkSum += blockSize * section.Owner.Id;
					blockSize++;
				}
			}
			else
			{
				blockSize += section.Size;
			}
		}

		return checkSum;
	}
}

public sealed record Section(Identifier? Owner, int Size);

public sealed record Block(Identifier? Owner);

public sealed record Identifier(long Id);

public sealed record Format(int FileSize, int FreeSpaceSize);