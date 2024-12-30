# Advent of Code 2024 Notes

These are my notes for each day's solutions.

## Day 1:

## Day 7:

    3^2  3^1  3^0
0:    A    A    A
1:    A    A    M
2:    A    A    C
3:    A    M    A
4:    A    M    M
5:    A    M    C
6:    A    C    A
7:    A    C    M
8:    A    C    C
9:    M    A    A
10:   M    A    M

(Operation)(index / "power")

## Day 11:

9223372036854775807
       395106562048

x x

x x x x x

  x x x x

    x x x

    x x

    x x x x

    x x x x x

  x x x

  x x x x x

  x x

  x x x x x x x

4 + 3 + 5 + 2 + 7


# TODOs
* Day 1
    * Part 1 - Do a comparison on the difference between using a `List<>` and an `int[]`
    * Part 2 - Should the arrays be sorted, or do something to handle previously discovered IDs?
* Day 3
    * Can I make the regexs any better, especially for Part 2?
* Day 9
    * Part 1 - Took a bit of time to finish. Where was the bottleneck?
* Day 10
    * Part 1 - Would be nice if `string` had a `IndexesOf()` - that is, gives you a collection of indexes for a given character. Maybe a Spackle feature?
    * Part 1 - Parallelization would probably make this quicker.
* Day 11
    * Part 1 - `int digits = (int)Math.Floor(BigInteger.Log10(x) + 1);` - this may be a bit quicker, also to use for getting the "left" and "right" values.