using System.Collections.Immutable;
using System.Globalization;

namespace AdventOfCode2024.Day24;

public static class SolutionDay24
{
	public static ulong RunPart1(ImmutableArray<string> input)
	{
		var wires = new List<Wire>();
		var equations = new List<Equation>();

		var splitIndex = input.IndexOf(string.Empty);

		// Get wires
		for (var w = 0; w < splitIndex; w++)
		{
			var wireParts = input[w].Split(':', StringSplitOptions.TrimEntries);
			wires.Add(new Wire(wireParts[0], int.Parse(wireParts[1], CultureInfo.CurrentCulture)));
		}

		// Get equations
		for (var e = splitIndex + 1; e < input.Length; e++)
		{
			var equationParts = input[e].Split("->", StringSplitOptions.TrimEntries);
			var expressionParts = equationParts[0].Split(' ');

			var left = wires.SingleOrDefault(_ => _.Name == expressionParts[0]);

			if (left is null)
			{
				left = new Wire(expressionParts[0], -1);
				wires.Add(left);
			}

			var right = wires.SingleOrDefault(_ => _.Name == expressionParts[2]);

			if (right is null)
			{
				right = new Wire(expressionParts[2], -1);
				wires.Add(right);
			}

			var @operator = expressionParts[1] == "AND" ?
				Operation.And :
				expressionParts[1] == "OR" ?
					Operation.Or :
					Operation.Xor;

			var result = wires.SingleOrDefault(_ => _.Name == equationParts[1]);

			if (result is null)
			{
				result = new Wire(equationParts[1], -1);
				wires.Add(result);
			}

			equations.Add(new Equation(left, @operator, right, result));
		}

		while (true)
		{
			var evaluationOccurred = false;

			foreach (var equation in equations)
			{
				if (equation.Left.Value != -1 && equation.Right.Value != -1)
				{
					if (equation.Result.Value == -1)
					{
						evaluationOccurred = true;
						equation.Result.Value =
							equation.Operation switch
							{
								Operation.And => equation.Left.Value & equation.Right.Value,
								Operation.Or => equation.Left.Value | equation.Right.Value,
								_ => equation.Left.Value ^ equation.Right.Value
							};
					}
				}
			}

			if (!evaluationOccurred)
			{
				break;
			}
		}

		var systemNumber = 0UL;

		foreach (var zWire in wires.Where(_ => _.Name.StartsWith('z')).OrderByDescending(x => x.Name))
		{
			systemNumber <<= 1;
			systemNumber |= zWire.Value == 1 ? (ulong)1 : 0;
		}

		return systemNumber;
	}
}

public enum Operation { And, Or, Xor }

public sealed class Wire
{
	public Wire(string name, int value) =>
		(this.Name, this.Value) = (name, value);

	public string Name { get; }
	public int Value { get; set; }
}

public sealed record Equation(Wire Left, Operation Operation, Wire Right, Wire Result);
