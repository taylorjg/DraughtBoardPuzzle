
# DraughtBoardPuzzle

My nieces have this puzzle which I have tried to solve many times without success. It consists of 14 pieces which are meant to fit together to form an 8x8 draughtboard.

![Picture of the box](https://github.com/taylorjg/DraughtBoardPuzzle/raw/master/Images/Box.jpg)

I thought it would be an interesting exercise to write a program to solve the puzzle. I used this as vehicle for practising TDD. So I have the following three projects:

- DraughtBoardPuzzle.Tests - C# class library containing the unit tests that drove the development
- DraughtBoardPuzzle - C# class library containing the code to solve the puzzle
- DraughtBoardPuzzleApp - C# console application which prints the solution to the console

## Approach and Sample Output

I have taken a simple brute force approach to solving the puzzle. It tries all permutations of all pieces in each orientation (N,S,E,W) and tries to layout the pieces on a board. I tried it on a smaller version of the puzzle consisting of 4 pieces to form a 4x4 draughtboard. The output for this smaller version of the puzzle is shown below.

	Solution found after 769 iterations.
	AN CN DN BN
	
	+----+----+----+----+
	| Bw | Bb | Bw | Db |
	+----+----+----+----+
	| Bb | Cw | Cb | Dw |
	+----+----+----+----+
	| Aw | Cb | Dw | Db |
	+----+----+----+----+
	| Ab | Aw | Ab | Aw |
	+----+----+----+----+

I am currently running the program for the full 8x8 version. I am still waiting for it to finish!

So I think I need to do some research to try to figure out a better approach to solving the puzzle.
