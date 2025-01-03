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
45 => 00:00:05.0301904
50 => 00:00:39.3754173
55 => 00:05:42.0317381

60 iterations, "0" => 43369895096 stones, 00:07:30.0919207

"Hint" from the gallery:

> If you start with just "0", there are 54 different kinds of stones after 22 blinks. There are the same 54 kinds of stones after 23, 24, 50, or 60 blinks.
>
> All of the values in your precompute map were of those 54; if you had all 54 of them, you could compute one precomp map from the prior iteration.

So, let's start with 0

* 0
* 1
* 2024
* 20 24
* 2 0 2 4
* 4048 1 4048 8096
* 40 48 2024 40 48 80 96
* 4 0 4 8 20 24 4 0 4 8 8 0 9 6
* 8096 1 8096 16192 2 0 2 4 8096 1 8096 16192 16192 1 18216 12144
* 80 96 2024 80 96 32772608 4048 1 4048 8096 80 96 2024 80 96 32772608 32772608 2024 36869184 24579456
* 8 0 9 6 20 24 8 0 9 6 3277 2608 40 48 2024 40 48 80 96 8 0 9 6 20 24 8 0 9 6 3277 2608 3277 2608 20 24 3686 9184 2457 9456

0 is "cyclical" in that a 0 will produce another 0 every 4th iteration at least. Of course, it's more than that as this progresses.

In other words, given a stone `S`, save all the counts for each iteration, and then store those in a map. As you make more, the map will fill in with lots of precalculated values.

But we don't want to map everything, because that could get out of hand. We definitely want:
* 0 to 9
* 2024
* 20
* 24
* 4048
* 8096
* 40
* 48
* 80
* 96
* 32772608
* 36869184
* 24579456
* 3277
* 2608
* 2024
* 3277
* 2608
* I mean...basically any number from the "54" that the guy gave me a hint of. Which is why we need to calculate the list of stones for an initial seed of 0 after 25 iterations.

The reason why is that a number with an odd number of digits, like 6831043, iterates like this:

* 6831043
* 13826031032
* 27983886808768
* 2798388 6808768
* 5663937312 13780946432
* 56639 37312 27892635578368
* 114637336 75519488 2789263 5578368 (At this point, 75519488 will "decompose" into 8 1-digit numbers)
* 232025968064 7551 9488 5645468312 11290616832
* 232025 968064 75 51 94 88 56454 68312 22852208467968
* 232 25 968 64 7 5 5 1 9 4 8 8 114262896 138263488 2285220 8467968

So, numbers seem to, at some point, "decompose" down to these elemental starting points.

Here's another thing: The position of the stones **does not matter**. So if we reorder the list like this:

0 1 22 2790 528 572556 10725 4679021

We will precompute lots of elemental values with 0, 1, 2 (from 22), and 7 and 9 (from the 2790 split)

That gets us a lot out of the gate. And we will probably precompute even more elemental values as we iterate over these 5 elemental numbers.

We should also get stone counts favoring numbers with a number of digits that is equal to a power of 2. For example, if we have 232025, that gives 232 and 25. Note that 25 will give 2 and 5, values that we would want first. So we'd want to get the stone count of 25 first, in case we haven't done 2 and 5 just yet. We don't really know where 232 is going to go.

More ideas:
* Add the "`GetStones()` method, which takes an initial list, and the number of iterations. This will literally create the list, so this is only for debugging purposes.
* **MAYBE** change the current implementation to increment iterations, not decrement. Just a bit easier to reason about. Not entirely sure about that.
* Create **one** pre-compute map, and make it a `Dictionary<(BigInteger, int), BigInteger>`, and remove task/parallel processing for now. The key is stone value and the number of remaining iterations, and the value is how many stone will be created with that.
* Find the magic 54 values from the "hint". Any time that is computed, add it to the map. That should give `54 * 75 = 4050`. 4K key/values is not a big deal.
* Consider doing persistence around the map.
* Consider reintroducing tasks and change to `ConcurrentDictionary<(BigInteger, int), BigInteger>`.

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
