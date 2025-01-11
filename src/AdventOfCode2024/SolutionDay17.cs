using System.Collections.Immutable;
using System.Globalization;
using System.Numerics;

namespace AdventOfCode2024.Day17;

public static class SolutionDay17
{
	public static string RunPart1(ImmutableArray<string> input)
	{
		// Parse the input.
		var registerA = BigInteger.Parse(input[0].Split(':')[1].Trim(), CultureInfo.CurrentCulture);
		var registerB = BigInteger.Parse(input[1].Split(':')[1].Trim(), CultureInfo.CurrentCulture);
		var registerC = BigInteger.Parse(input[2].Split(':')[1].Trim(), CultureInfo.CurrentCulture);

		var program = input[4].Split(':')[1].Trim().Split(',')
			.Select(_ => BigInteger.Parse(_, CultureInfo.CurrentCulture)).ToImmutableArray();

		var computerInput = new ComputerInput(registerA, registerB, registerC, program);

		var computer = new Computer(computerInput);

		return computer.Output;
	}

	//public static BigInteger RunPart2(ImmutableArray<string> input)
	//{
	//}
}

public sealed record ComputerInput(BigInteger RegisterA, BigInteger RegisterB, BigInteger RegisterC,
	ImmutableArray<BigInteger> Program);

public sealed class Computer
{
	private static class Instructions
	{
		public static readonly BigInteger Adv = BigInteger.Zero;
		public static readonly BigInteger Bxl = BigInteger.One;
		public static readonly BigInteger Bst = 2;
		public static readonly BigInteger Jnz = 3;
		public static readonly BigInteger Bxc = 4;
		public static readonly BigInteger Out = 5;
		public static readonly BigInteger Bdv = 6;
		public static readonly BigInteger Cdv = 7;
	}

	public Computer(ComputerInput input)
	{
		(this.RegisterA, this.RegisterB, this.RegisterC, this.Program) = input;
		this.Output = this.Process();
	}

	public BigInteger GetComboOperand(BigInteger operand)
	{
		if (operand >= BigInteger.Zero && operand < 4)
		{
			return operand;
		}
		else if (operand == 4)
		{
			return this.RegisterA;
		}
		else if (operand == 5)
		{
			return this.RegisterB;
		}
		else if (operand == 6)
		{
			return this.RegisterC;
		}
		else
		{
			throw new NotSupportedException($"Invalid operand: {operand}");
		}
	}

	private string Process()
	{
		var result = new List<BigInteger>();

		while (this.InstructionPointer <= this.Program.Length - 2)
		{
			var opcode = this.Program[this.InstructionPointer];
			var operand = this.Program[this.InstructionPointer + 1];
			var didJump = false;

			if (opcode == Computer.Instructions.Adv)
			{
				var (quotient, _) = BigInteger.DivRem(
					this.RegisterA, BigInteger.Pow(2, (int)this.GetComboOperand(operand)));
				this.RegisterA = quotient;
			}
			else if (opcode == Computer.Instructions.Bxl)
			{
				this.RegisterB ^= operand;
			}
			else if (opcode == Computer.Instructions.Bst)
			{
				this.RegisterB = this.GetComboOperand(operand) % 8;
			}
			else if (opcode == Computer.Instructions.Jnz)
			{
				if (this.RegisterA != 0)
				{
					this.InstructionPointer = (int)operand;
					didJump = true;
				}
			}
			else if (opcode == Computer.Instructions.Bxc)
			{
				this.RegisterB ^= this.RegisterC;
			}
			else if (opcode == Computer.Instructions.Out)
			{
				result.Add(this.GetComboOperand(operand) % 8);
			}
			else if (opcode == Computer.Instructions.Bdv)
			{
				var (quotient, _) = BigInteger.DivRem(
					this.RegisterA, BigInteger.Pow(2, (int)this.GetComboOperand(operand)));
				this.RegisterB = quotient;
			}
			else if (opcode == Computer.Instructions.Cdv)
			{
				var (quotient, _) = BigInteger.DivRem(
					this.RegisterA, BigInteger.Pow(2, (int)this.GetComboOperand(operand)));
				this.RegisterC = quotient;
			}
			else
			{
				throw new NotSupportedException($"Invalid opcode: {opcode}");
			}

			if (!didJump)
			{
				this.InstructionPointer += 2;
			}
		}

		return string.Join(',', result);
	}

	public int InstructionPointer { get; private set; }
	public string Output { get; }
	public ImmutableArray<BigInteger> Program { get; }
	public BigInteger RegisterA { get; private set; }
	public BigInteger RegisterB { get; private set; }
	public BigInteger RegisterC { get; private set; }
}