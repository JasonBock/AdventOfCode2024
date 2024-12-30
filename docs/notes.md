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

x x x

  x x x x

    x x x x x

    x x x x

    x x x x x x x

    x x x x x

  x x x

    x x x x x

    x x x x

    x x x

  x x x x x

    x x x x x
    
    x x x x x x x

    x x x x x x x x

    x x x x x

    x x x x x

  x x x

    x x x

    x x x x x

  x x x x x x x

    x x x x x x x x x x

    x x x x x x x x x

    x x x x x x x x x x x

    x x x x x x x x x x x x

    x x x x x x x

    x x x x x x x
    
    x x x x x x x x x

25 => 00:00:00.0177952
30 => 00:00:00.0525373
35 => 00:00:00.3191811
40 => 00:00:01.2586611
45 => 00:00:09.5249220
50 => 00:01:12.4605297

For 25 iterations, what's the count for values 0 to 9, 20, 24, and 4048?

Value |      Count
------------------
    0 | 19778
    1 | 29165
    2 | 27842
    3 | 27569
    4 | 26669
    5 | 23822
    6 | 25469
    7 | 25071
    8 | 24212
    9 | 25793
   20 | 31055
   24 | 36669
   40 | 30300
   48 | 33975
 2024 | 43726
 4048 | 42646

For 50 iterations, what's the count for values 0 to 9, 20, 24, and 4048?

Value |      Count
------------------
    0 |  663251546
    1 | 1010392024
    2 |  967190364
    3 |  967436144
    4 |  939523808
    5 |  830902728
    6 |  884539345
    7 |  870467992
    8 |  841069902
    9 |  897592763
   20 | 1072629280
   24 | 1254513380
   40 | 1056089110
   48 | 1174092474
 2024 | 1529921658 
 4048 | 1464254721

25 => 
30 => 
35 => 
40 => 
45 => 
50 => 

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
