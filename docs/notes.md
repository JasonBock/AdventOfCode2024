# Advent of Code 2024 Notes

These are my notes for each day's solutions, if I had any.

## Current Plan

Right now I have 42 stars. I'm not sure how much more I can get done, espeically with time constraints on things, and just wanting to move on to other things. Here's what I think the plan is and in the order I think is reasonable (1 being pretty reasonable to 10 being oh my god I have no clue):

* Day 23, Part 2 - 4 - This is simple to understand, but I know this could get out of hand rather quickly if I don't find a trick.
* Day 20, Part 2 - 4 - I get what needs to be done, but I'm not sure I can figure out a trick to prevent it from exploding in complexity.
* Day 17, Part 2 - 5 - I still don't know what the "trick" is, but I don't think all hope is lost here.
* Day 24, Part 2 - 6 - I don't get the problem statement, but I think once I do, it might be doable.
* Day 21, Parts 1 and 2 - 9 - LOL. I get the problem statement, but I have no idea how to optimize Part 1, and I've Part 2 is even harder.

Note that Part 2 of Day 25 isn't even doable until I get all of these.

## Day 7

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

## Day 11

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

## Day 12

### Part 2
I'm dumb. I make a new line if I find more than one line, but I never add that new line to the line list. This doesn't come up in the unit tests, but in the main problem, it does. Oops. Need to fix that.

## Day 13

### Part 1

94A + 22B = 8400
34A + 67B = 5400

### Part 2

10_000_000_000_000
Possible idea: https://faculty.uml.edu/dklain/optimization.pdf

Minimize: 
  3a + b
Constraints:
  AXa + BXb = RX
  AYa + BYb = RY

Doing some transforming...

Maximize:
  -3a - b
Constraints:
  AXa + BXb <= RX
  -AXa - BXb <= -RX
  AYa + BYb <= RY
  -AYa - BYb <= -RY

Whatever the answer is (if there is one), then reverse the sign.

Or...

94*A+22*B=8400
34*A+67*B=5400

22B = 8400 - 94A
B = (8400 - 94A) / 22

34A + 67((8400 - 94A) / 22) = 5400

34A + 67(8400/22) - 67(94A / 22) = 5400

34A + 562800/22 - 6298A / 22 = 5400

748A + 562800 - 6298A = 118800

562800 - 5550A = 118800

-5550A = -444000

A = 80

To generalize:

AXa + BXb = RX
AYa + BYb = RY

BXb = RX - AXa
b = (RX - AXa) / BX

AYa + BY((RX - AXa) / BX) = RY

AYa + BY(RX/BX) - BY(AXa / BX) = RY

AYa + ((BY * RX) / BX) - ((BY * AXa) / BX) = RY

(AY * BX)a + (BY * RX) - (BY * AXa) = (RY * BX)

(AY * BX)a - (BY * AX)a = (RY * BX) - (BY * RX)

a = ((RY * BX) - (BY * RX)) / ((AY * BX) - (BY * AX))

Therefore:

a = ((RY * BX) - (BY * RX)) / ((AY * BX) - (BY * AX))
b = (RX - AXa) / BX

Could change this for Part 1. Basically, a and b have to be positive with no remainder after division. I'm assuming now there can be only zero or one solution. If this gives the same answer, then Part 2 is the same thing, except the prize's X and Y values are added by 10 trillion (or whatever that big value is).

## Day 14

### Part 1

[8] = {Robot { Position = Position { X = 0, Y = 2 }, Velocity = Velocity { XChange = 2, YChange = 3 } }}
[5] = {Robot { Position = Position { X = 1, Y = 3 }, Velocity = Velocity { XChange = -2, YChange = -2 } }}
[4] = {Robot { Position = Position { X = 1, Y = 6 }, Velocity = Velocity { XChange = 1, YChange = 3 } }}
[7] = {Robot { Position = Position { X = 2, Y = 3 }, Velocity = Velocity { XChange = -1, YChange = -2 } }}
[0] = {Robot { Position = Position { X = 3, Y = 5 }, Velocity = Velocity { XChange = 3, YChange = -3 } }}
[3] = {Robot { Position = Position { X = 4, Y = 5 }, Velocity = Velocity { XChange = 2, YChange = -1 } }}
[10] = {Robot { Position = Position { X = 4, Y = 5 }, Velocity = Velocity { XChange = 2, YChange = -3 } }}
[1] = {Robot { Position = Position { X = 5, Y = 4 }, Velocity = Velocity { XChange = -1, YChange = -3 } }}
[6] = {Robot { Position = Position { X = 6, Y = 0 }, Velocity = Velocity { XChange = -1, YChange = -3 } }}
[9] = {Robot { Position = Position { X = 6, Y = 0 }, Velocity = Velocity { XChange = -1, YChange = 2 } }}
[11] = {Robot { Position = Position { X = 6, Y = 6 }, Velocity = Velocity { XChange = -3, YChange = -3 } }}
[2] = {Robot { Position = Position { X = 9, Y = 0 }, Velocity = Velocity { XChange = -1, YChange = 2 } }}

Vertical Clumps: 14, 115, 216, 317,
Horizontal Clumps: 94, 197, 300, 

## Day 15

### Part 2

[][]
[]
@

[]
[][]
@


   #
[][]
[]
@

[] #
[][]
@

## Day 16

### Part 1

Need to create a `Path`, which contains the number of traversed positions, the number of turns, the current position, the current direction, the current cost, and a flag signaling if the path has completed. Have a method called `GetNextPaths()`, which returns an `ImmutableArray<Path>`. 

We have a list of `Path`s to evaluate. In a `while` loop, once we no longer have paths to evaluate, we break out.

We go through each path. We check to see if this is equal to or higher than the current lowest score. If it's not, we call `GetNextPaths()`. This will return 0 to 3 paths. Each new path is evaluated for either finishing or what its current score is. If it finishes, and it's lower than the current lowest score, we set that. The remaining new paths are evaluated equal to or higher than the current lowest score, and discared if they are equal or higher. Finished paths are discarded.

To find a new path, we look in each direction other than the one 180o from where we are going (e.g. if we're going `North`, we don't look `South`). If there is not a wall in that direction, we create a new `Path`:
* Incrementing the number of traversed positions
* Updating the current position
* If we change direction, we increment the number of turns
* We set the current direction.
* We update the cost.
* If the new position is the end, we also signal that we've finished.

More ideas:

DONE - Maybe we change `Map` to `ImmutableDictionary<Position, MapItemType>`. Because we're going to do so many search on where things are (or aren't), using that as the key may speed things up.

DONE - `public enum MapItemType { Start, End, Wall }`

We also keep a list of previous junction visits: what the position is, what the direction is, and what the cost is. Call it a `Junction`. This is "global" for the entire search, just like the map is, but it's dynamic (probably a `HashSet<Junction>`). 

When we search in a direction, we want to find in that direction (incrementally) either
  * An existing `Junction` **where we are and in that direction** that is equal to or less than our cost. Just stop processing.
  * A `MapItem`
  * A junction - that is, there's another opening 90o in either direction.

We either end up:
* In a dead end. In that case, no new `Path`s are created. 
  * Idea: we **could** create a list of "dead end" `Junction`s. That is, a `Path` keeps a list of `Junction`s found that only went one way and required a turn (e.g. I was going North and then I could only go West). Not sure how helpful this would be though.
* At the end. We return a `Path` that is updated with new info and `IsFinished` is `true`.
* At the start. Probably unlikely, but that's the same as hitting a wall, and we should terminate that path.
* At a junction. We create new `Junction`s. For each `Junction`, we determine if any exist that are at the same position and direction. If so, and an existing one is less than or equal to our cost, we don't create a `Path` for that `Junction`. Otherwise, we create a new `Path` where **we do not move ahead**, but we calculate the moves it took to get there, and also add a turn if is needed for that new direction. We also add that `Junction` to the global list.

We should keep track of all the `Junction`s that we've visited. If we ever hit a visited `Junction`s position, we immediately stop that `Path`. Doesn't matter which direction we were going there. 



* It is possible to return to the start. In the big map, it's not too big of a deal, because you'll end up going in to a dead-end rather quickly. But we can stop those paths from continuing if we do run into that case.
* Only return from `GetNextPaths()` when we've hit a position going in our direction that has an opening to the "left" or "right" (depending on what direction we're going). That way, when we're going down a hall, we don't keep creating just one path to iterate over. We only return new paths when we've either hit that fork, or we dead-end, or we finish. But that should minimize iterating over `GetNextPaths()` a bit.
* When we create new paths, mark what our cost is to get to that position. If another path hits that, we could potentially cut that path off from continuing if we know we're going to 


Any time we find a junction - that is, we can go more than one direction - 

9223372036854775807
9223372036854775807

* We need to create start paths, **plural**. You could go East and/or North. Or technically neither, but that would suck.
* If we end with the cost equaling `long.MaxValue`, that means the map doesn't let you finish. Maybe throw an exception - `BlockedMapException`.

As we're evaluating new paths, we only add a new path to `newPaths` if:

If a new path's `CurrentPosition` is in another's `VisitedJunction`s:
* With the cost at that junction equal to or greater than, we don't add the new path. 
* Else, we removing the existing path.

So we have to add the cost and the direction when we hit a junction

## Day 17

### Part 2

2,4,1,5,7,5,0,3,1,6,4,3,5,5,3,0

Find X

2,4 => 
  A = X
  B = X % 8
  C = 0
  R = []
1,5 => 
  A = X
  B = (X % 8) XOR 5
  C = 0
  R = []
7,5 => 
  A = X
  B = (X % 8) XOR 5
  C = (X / (2 ^ ((X % 8) XOR 5)))
  R = []
0,3 => 
  A = X
  B = (X % 8) XOR 5
  C = (X / (2 ^ ((X % 8) XOR 5)))
  R = []
1,6 => 
  A = (X / (2 ^ (X / (2 ^ ((X % 8) XOR 5)))))
  B = (X % 8) XOR 5
  C = (X / (2 ^ ((X % 8) XOR 5)))
  R = []
4,3 => 
  A = (X / (2 ^ (X / (2 ^ ((X % 8) XOR 5)))))
  B = ((X % 8) XOR 5) XOR (X / (2 ^ ((X % 8) XOR 5)))
  C = (X / (2 ^ ((X % 8) XOR 5)))
  R = []
5,5 => 
  A = (X / (2 ^ (X / (2 ^ ((X % 8) XOR 5)))))
  B = ((X % 8) XOR 5) XOR (X / (2 ^ ((X % 8) XOR 5)))
  C = (X / (2 ^ ((X % 8) XOR 5)))
  R = [((X % 8) XOR 5) XOR (X / (2 ^ ((X % 8) XOR 5))) % 8]
3,0 => 
  A = (X / (2 ^ (X / (2 ^ ((X % 8) XOR 5)))))
  B = ((X % 8) XOR 5) XOR (X / (2 ^ ((X % 8) XOR 5)))
  C = (X / (2 ^ ((X % 8) XOR 5)))
  R = [((X % 8) XOR 5) XOR (X / (2 ^ ((X % 8) XOR 5))) % 8], jump to the beginning

2,4 => 

We need this to run through 16 times, but then the last "3,0" instruction needs A = 0.

16 * 8 = 128 maximum. Any more would print out too much.

 35_000_000_000_000 => 120 executions
300_000_000_000_000 => 136 executions

 35_184_372_088_832, this is the first number that does 128 executions
281_474_976_710_655, this is the last number that does 128 executions

Interesting, (last / first) gives exactly 7. I have no idea if that helps or not :)

Reverse this

  A = < 8
  B = B
  C = C
  R = [2,4,1,5,7,5,0,3,1,6,4,3,5,5,3]
2,4 => 
  A = < 8
  B = (< 8) % 8
  C = C
  R = [2,4,1,5,7,5,0,3,1,6,4,3,5,5,3]
1,5 => 
  A = < 8
  B = ((< 8) % 8) ^ 5
  C = C
  R = [2,4,1,5,7,5,0,3,1,6,4,3,5,5,3]
7,5 => 
  A = < 8
  B = ((< 8) % 8) % 5
  C = (< 8) / (2 ^ (((< 8) % 8) ^ 5))
  R = [2,4,1,5,7,5,0,3,1,6,4,3,5,5,3]
0,3 => 
  A = 0
  B = ((< 8) % 8) % 5
  C = (< 8) / (2 ^ (((< 8) % 8) ^ 5))
  R = [2,4,1,5,7,5,0,3,1,6,4,3,5,5,3]
1,6 => 
  A = 0
  B = (((< 8) % 8) % 5) ^ 6
  C = (< 8) / (2 ^ (((< 8) % 8) ^ 5))
  R = [2,4,1,5,7,5,0,3,1,6,4,3,5,5,3]
4,3 =>
  A = 0
  B = (((A % 8) % 5) ^ 6) ^ (A / (2 ^ ((A % 8) ^ 5)))
  C = (< 8) / (2 ^ (((< 8) % 8) ^ 5))
  R = [2,4,1,5,7,5,0,3,1,6,4,3,5,5,3]
5,5 =>
  A = 0
  B = ((((< 8) % 8) % 5) ^ 6) ^ ((< 8) / (2 ^ (((< 8) % 8) ^ 5))) (must be a multiple of 8)
  C = (< 8) / (2 ^ (((< 8) % 8) ^ 5))
  R = [2,4,1,5,7,5,0,3,1,6,4,3,5,5,3,0]
3,0 =>
  A = 0
  B = ((((< 8) % 8) % 5) ^ 6) ^ ((< 8) / (2 ^ (((< 8) % 8) ^ 5))) (must be a multiple of 8)
  C = (< 8) / (2 ^ (((< 8) % 8) ^ 5))
  R = [2,4,1,5,7,5,0,3,1,6,4,3,5,5,3,0]
TERMINATION

## Day 18

### Part 1

This feels like cheating, but finding the shortest path between two points is a well-known problem, so...why recreate the wheel?

* https://en.wikipedia.org/wiki/A*_search_algorithm
* https://www.geeksforgeeks.org/shortest-path-in-a-binary-maze/
* https://stackoverflow.com/questions/61218945/best-algorithm-for-maze-solving

Don't use backtracking, use BFS. (Of course, duh!)

## Day 19

### Part 1

Maybe I use a `ImmutableDictionary<char, ImmutableArray<string>>` for the tokens. That way you can find those that might match based on the first character quicker, rather than looking at each token.

So, `r, wr, b, g, bwu, rb, gb, br` turns into:

* `'r' : "r", "rb"`
* `'w' : "wr"`
* `'b' : "b", "bwu", "br"`
* `'g' : "g", "gb"`

First word is `"brwrr"`. We can create an extension method on `string` called `bool CanBeMade(Tokens tokens)`. We start at index 0:

```c#
var indexes = new SortedSet<int> { 0 };

while(indexes.Count > 0)
{
  var currentIndex = indexes[0];

  if(tokens.TryGetValue(self[currentIndex], out startingTokens))
  {
    foreach(var startingToken in startingTokens)
    {
      var nextIndex = currentIndex + startingToken.Length;

      if((self.Length <= (nextIndex) &&
        self.AsSpan(currentIndex, startingToken.Length).SequenceEqual(token.AsSpan())))
      {
        if(self.Length == nextIndex)
        {
          return true;
        }

        indexes.Add(nextIndex);
      }
    }
  }

  indexes.Remove(currentIndex);
}

return false;
```

We look at the character at 0, and it's `'b'`. For all the `'b'`s, two give a match: `"b", and "br"`, so we add 1 and 2 to `indexes`. We can start looking at those indexes since they would have to be after the index we are looking at. We can also remove the 0th index, so we're always looking at the 0th element in `indexes` until there are no more index values, or we found a completion.

So `indexes` would be `[1, 2]`
Let's order the indexes because that could matter.

For index 1, `'r'` has one match: `"r"`, so we have a new index: 2. **But**, we have that in our current indexes, so we'll basically ignore it.
For index 2, `'w'` has one match: `"wr`, so we have a new index: 4.
For index 4, `'r'` has one match: `"r"`. This puts us to the end of the string, so we've found a towel pattern, return true.

"ubwu" is supposedly not workable:

Start at index 0. There are no keys in the token dictionary, so we'll remove 0 as an index, there will be no more in the set, so we return false.

"bbrgwb" is also impossible.

Start at index 0. This would add 1.
Index 1 would add 2 and 3.
Index 2 would add 3, but we already have that.
Index 3 would add 4.
Index 4 wouldn't add any indexes, so we have no more indexes, and we return false.

## Part 2

I think we need to preserve valid branch counts for previously computed indexes

## Day 20

### Part 1

S => Line 78, Ch 70
E => Line 70, Ch 51

Total file size is 20K, so not that big.

When parsing the file, just get the positions of the path.

Then, at the start, store the solution with (Position, Direction). (May not need to store the direction, but it might be a convenience.)

Let's do a simple maze:

#########
#.......#
#.#####.#
#...#...#
###.#.###
###S#.###
#####E###
#########

That's 18 picoseconds.

Now, walk the solution, and look in each direction other than the one you're going. If there's a wall that way, and there's no wall 1 position after that (mod x or y, depending on which direction you're looking), that's a possible cut. 

At (2, 3), there's one shortcut. The starting index of the cheat is 3. The end is 7. This would save 2 picoseconds. So, theory: 

If (endIndex > startIndex), then (endIndex - startIndex) - 2

(7 - 3) - 2 = 2

At (5, 1), there's a shortcut. The starting index is 10. The end is 16. This would save 4.

(16 - 10) - 2 = 4

Seems to hold.

Now, let's say that we go to (5, 3), there's a shortcut, but it's the wrong way. Starting index is 16, end is 10, so don't need to consider it.

Cheats are stored as Dictionary<int, HashSet<(int, int)>>. The key is the savings, and the value are the unique start and end indexes.

If we don't store the direction when we find the path, we can just look all directions at every point. That'll be a bit more work each time, but it'll make the search a bit simpler.

...

Maybe we don't need to do 2 passes. As we build the path, we look to see if a cheat exists at each point. We can't evaluate it, but we can capture the start/end positions. Once we get the path, we can evaluate each cheat

(7, 7) in the example is where the cheat of 64 savings occurs. Why do we think it's 63? I think I just answered my statement.

(4, 30) is giving 29, not 30. But, (2, 44) is giving the correct answer.

## Day 21

### Part 1

## Day 23

### Part 1

x|y

x|z     z|x

z|y


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
* Day 16
  * Maybe get the shortest path using A* (is that it?), find its cost, and then use that as a baseline as others find the true "best" path.
* Day 18
  * Part 2 - A small optimization would be to only find if a path exists after a certain point in the file. Meaning, the quickest way to block off the grid would be to create a diagonal across the grid. So, basically you'd build up the collisions, but you'd only check to see if it's block at index `(int)Math.Sqrt(size ^^ 2 + size ^^ 2)`, more or less. For the 71 size example, that's 100.
* Day 19
  * Part 1 - Is the `Span<>` approach really "better" than `Substring()`?