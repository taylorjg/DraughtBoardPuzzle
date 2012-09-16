
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

## UPDATE (16th September 2012)

I recently came back to this project and did some research. I came across Donald Knuth's "Algorithm X" and his "Dancing Links" implementation of this algorithm (DLX). The original paper can be found here:

* [Dancing Links](http://arxiv.org/pdf/cs/0011047v1.pdf)

This algorithm can be used to solve various puzzles like [Sudoku](http://en.wikipedia.org/wiki/Sudoku) and [Polyomino](http://en.wikipedia.org/wiki/Polyomino) puzzles. The problem to be solved is represented by a matrix of 0s and 1s. The algorithm finds all solutions where each solution consists of a set of rows in the original matrix such that each column contains a single 1.

Based on this paper and various other articles, I wrote my own implementation of the algorithm to try to find all solutions to the original problem. These can be seen below. In my case, I have 78 columns in the matrix. The first 14 columns correspond to the 14 pieces of the puzzle. The next 64 columns correspond to the squares on the draughtboard. Each row represents the placement of a single piece on the board. There are rows for each legal position of the piece on the board and for each orientation of the piece. Each solution identifies 14 rows in the matrix that describe the placement of the 14 pieces such that the puzzle is solved. The program finds 37 solutions in 16 seconds or so.

### References

* [Dancing Links](http://arxiv.org/pdf/cs/0011047v1.pdf)
* [Solving Polyomino and Polycube Puzzles](http://www.mattbusche.org/blog/article/polycube/)
* [Generic Distributed Exact Cover Solver](http://janmagnet.files.wordpress.com/2008/07/decs-draft.pdf)
* [Solve the Pentomino puzzle with C++ and dancing links](http://www.codeproject.com/Articles/271634/Puzzle-Solver)

### Solutions

This is the output of the new program that employs DLX:

	Found 37 solutions

	+----+----+----+----+----+----+----+----+
	| Ew | Mb | Mw | Mb | Kw | Kb | Nw | Nb |
	+----+----+----+----+----+----+----+----+
	| Eb | Fw | Fb | Mw | Mb | Kw | Kb | Nw |
	+----+----+----+----+----+----+----+----+
	| Ew | Eb | Fw | Hb | Hw | Hb | Kw | Nb |
	+----+----+----+----+----+----+----+----+
	| Ab | Ew | Fb | Fw | Hb | Gw | Gb | Nw |
	+----+----+----+----+----+----+----+----+
	| Aw | Ab | Jw | Jb | Cw | Db | Gw | Gb |
	+----+----+----+----+----+----+----+----+
	| Ab | Bw | Jb | Cw | Cb | Dw | Db | Dw |
	+----+----+----+----+----+----+----+----+
	| Aw | Bb | Jw | Lb | Lw | Lb | Iw | Db |
	+----+----+----+----+----+----+----+----+
	| Bb | Bw | Jb | Lw | Ib | Iw | Ib | Iw |
	+----+----+----+----+----+----+----+----+

	+----+----+----+----+----+----+----+----+
	| Ew | Mb | Mw | Mb | Gw | Gb | Lw | Lb |
	+----+----+----+----+----+----+----+----+
	| Eb | Fw | Fb | Mw | Mb | Gw | Gb | Lw |
	+----+----+----+----+----+----+----+----+
	| Ew | Eb | Fw | Jb | Jw | Jb | Jw | Lb |
	+----+----+----+----+----+----+----+----+
	| Ab | Ew | Fb | Fw | Kb | Kw | Jb | Cw |
	+----+----+----+----+----+----+----+----+
	| Aw | Ab | Nw | Kb | Kw | Db | Cw | Cb |
	+----+----+----+----+----+----+----+----+
	| Ab | Bw | Nb | Kw | Hb | Dw | Db | Dw |
	+----+----+----+----+----+----+----+----+
	| Aw | Bb | Nw | Hb | Hw | Hb | Iw | Db |
	+----+----+----+----+----+----+----+----+
	| Bb | Bw | Nb | Nw | Ib | Iw | Ib | Iw |
	+----+----+----+----+----+----+----+----+

	+----+----+----+----+----+----+----+----+
	| Ew | Mb | Mw | Mb | Kw | Kb | Nw | Nb |
	+----+----+----+----+----+----+----+----+
	| Eb | Fw | Fb | Mw | Mb | Kw | Kb | Nw |
	+----+----+----+----+----+----+----+----+
	| Ew | Eb | Fw | Hb | Hw | Hb | Kw | Nb |
	+----+----+----+----+----+----+----+----+
	| Db | Ew | Fb | Fw | Hb | Iw | Ab | Nw |
	+----+----+----+----+----+----+----+----+
	| Dw | Db | Dw | Jb | Iw | Ib | Aw | Ab |
	+----+----+----+----+----+----+----+----+
	| Gb | Gw | Db | Jw | Lb | Iw | Ab | Bw |
	+----+----+----+----+----+----+----+----+
	| Cw | Gb | Gw | Jb | Lw | Ib | Aw | Bb |
	+----+----+----+----+----+----+----+----+
	| Cb | Cw | Jb | Jw | Lb | Lw | Bb | Bw |
	+----+----+----+----+----+----+----+----+

	+----+----+----+----+----+----+----+----+
	| Kw | Kb | Gw | Gb | Iw | Ib | Iw | Ib |
	+----+----+----+----+----+----+----+----+
	| Ab | Kw | Kb | Gw | Gb | Iw | Eb | Ew |
	+----+----+----+----+----+----+----+----+
	| Aw | Ab | Kw | Jb | Ew | Eb | Ew | Mb |
	+----+----+----+----+----+----+----+----+
	| Ab | Dw | Db | Jw | Jb | Jw | Jb | Mw |
	+----+----+----+----+----+----+----+----+
	| Aw | Db | Nw | Nb | Fw | Fb | Mw | Mb |
	+----+----+----+----+----+----+----+----+
	| Db | Dw | Hb | Nw | Lb | Fw | Mb | Bw |
	+----+----+----+----+----+----+----+----+
	| Cw | Hb | Hw | Nb | Lw | Fb | Fw | Bb |
	+----+----+----+----+----+----+----+----+
	| Cb | Cw | Hb | Nw | Lb | Lw | Bb | Bw |
	+----+----+----+----+----+----+----+----+

	+----+----+----+----+----+----+----+----+
	| Kw | Kb | Gw | Gb | Iw | Ib | Iw | Ib |
	+----+----+----+----+----+----+----+----+
	| Ab | Kw | Kb | Gw | Gb | Iw | Eb | Ew |
	+----+----+----+----+----+----+----+----+
	| Aw | Ab | Kw | Bb | Ew | Eb | Ew | Mb |
	+----+----+----+----+----+----+----+----+
	| Ab | Dw | Db | Bw | Bb | Bw | Mb | Mw |
	+----+----+----+----+----+----+----+----+
	| Aw | Db | Nw | Nb | Fw | Fb | Mw | Jb |
	+----+----+----+----+----+----+----+----+
	| Db | Dw | Hb | Nw | Lb | Fw | Mb | Jw |
	+----+----+----+----+----+----+----+----+
	| Cw | Hb | Hw | Nb | Lw | Fb | Fw | Jb |
	+----+----+----+----+----+----+----+----+
	| Cb | Cw | Hb | Nw | Lb | Lw | Jb | Jw |
	+----+----+----+----+----+----+----+----+

	+----+----+----+----+----+----+----+----+
	| Lw | Lb | Jw | Jb | Kw | Kb | Nw | Nb |
	+----+----+----+----+----+----+----+----+
	| Ib | Lw | Jb | Gw | Gb | Kw | Kb | Nw |
	+----+----+----+----+----+----+----+----+
	| Iw | Lb | Jw | Db | Gw | Gb | Kw | Nb |
	+----+----+----+----+----+----+----+----+
	| Ib | Iw | Jb | Dw | Db | Dw | Ab | Nw |
	+----+----+----+----+----+----+----+----+
	| Iw | Hb | Fw | Fb | Ew | Db | Aw | Ab |
	+----+----+----+----+----+----+----+----+
	| Hb | Hw | Hb | Fw | Eb | Ew | Ab | Bw |
	+----+----+----+----+----+----+----+----+
	| Cw | Mb | Mw | Fb | Fw | Eb | Aw | Bb |
	+----+----+----+----+----+----+----+----+
	| Cb | Cw | Mb | Mw | Mb | Ew | Bb | Bw |
	+----+----+----+----+----+----+----+----+

	+----+----+----+----+----+----+----+----+
	| Bw | Bb | Iw | Ib | Iw | Ib | Nw | Nb |
	+----+----+----+----+----+----+----+----+
	| Bb | Gw | Gb | Iw | Lb | Lw | Lb | Nw |
	+----+----+----+----+----+----+----+----+
	| Bw | Jb | Gw | Gb | Lw | Mb | Ew | Nb |
	+----+----+----+----+----+----+----+----+
	| Ab | Jw | Jb | Jw | Jb | Mw | Eb | Nw |
	+----+----+----+----+----+----+----+----+
	| Aw | Ab | Fw | Fb | Mw | Mb | Ew | Eb |
	+----+----+----+----+----+----+----+----+
	| Ab | Dw | Db | Fw | Mb | Kw | Kb | Ew |
	+----+----+----+----+----+----+----+----+
	| Aw | Db | Cw | Fb | Fw | Hb | Kw | Kb |
	+----+----+----+----+----+----+----+----+
	| Db | Dw | Cb | Cw | Hb | Hw | Hb | Kw |
	+----+----+----+----+----+----+----+----+

	+----+----+----+----+----+----+----+----+
	| Ew | Mb | Mw | Mb | Gw | Gb | Nw | Nb |
	+----+----+----+----+----+----+----+----+
	| Eb | Fw | Fb | Mw | Mb | Gw | Gb | Nw |
	+----+----+----+----+----+----+----+----+
	| Ew | Eb | Fw | Hb | Hw | Hb | Iw | Nb |
	+----+----+----+----+----+----+----+----+
	| Ab | Ew | Fb | Fw | Hb | Iw | Ib | Nw |
	+----+----+----+----+----+----+----+----+
	| Aw | Ab | Bw | Bb | Bw | Lb | Iw | Jb |
	+----+----+----+----+----+----+----+----+
	| Ab | Dw | Db | Kw | Bb | Lw | Ib | Jw |
	+----+----+----+----+----+----+----+----+
	| Aw | Db | Cw | Kb | Kw | Lb | Lw | Jb |
	+----+----+----+----+----+----+----+----+
	| Db | Dw | Cb | Cw | Kb | Kw | Jb | Jw |
	+----+----+----+----+----+----+----+----+

	+----+----+----+----+----+----+----+----+
	| Ew | Mb | Mw | Mb | Bw | Bb | Bw | Jb |
	+----+----+----+----+----+----+----+----+
	| Eb | Fw | Fb | Mw | Mb | Iw | Bb | Jw |
	+----+----+----+----+----+----+----+----+
	| Ew | Eb | Fw | Ib | Iw | Ib | Iw | Jb |
	+----+----+----+----+----+----+----+----+
	| Ab | Ew | Fb | Fw | Gb | Gw | Jb | Jw |
	+----+----+----+----+----+----+----+----+
	| Aw | Ab | Lw | Lb | Nw | Gb | Gw | Hb |
	+----+----+----+----+----+----+----+----+
	| Ab | Dw | Db | Lw | Nb | Kw | Hb | Hw |
	+----+----+----+----+----+----+----+----+
	| Aw | Db | Cw | Lb | Nw | Kb | Kw | Hb |
	+----+----+----+----+----+----+----+----+
	| Db | Dw | Cb | Cw | Nb | Nw | Kb | Kw |
	+----+----+----+----+----+----+----+----+

	+----+----+----+----+----+----+----+----+
	| Ew | Mb | Mw | Mb | Kw | Kb | Nw | Nb |
	+----+----+----+----+----+----+----+----+
	| Eb | Fw | Fb | Mw | Mb | Kw | Kb | Nw |
	+----+----+----+----+----+----+----+----+
	| Ew | Eb | Fw | Hb | Hw | Hb | Kw | Nb |
	+----+----+----+----+----+----+----+----+
	| Ab | Ew | Fb | Fw | Hb | Lw | Lb | Nw |
	+----+----+----+----+----+----+----+----+
	| Aw | Ab | Bw | Bb | Bw | Ib | Lw | Jb |
	+----+----+----+----+----+----+----+----+
	| Ab | Dw | Db | Gw | Bb | Iw | Lb | Jw |
	+----+----+----+----+----+----+----+----+
	| Aw | Db | Gw | Gb | Cw | Ib | Iw | Jb |
	+----+----+----+----+----+----+----+----+
	| Db | Dw | Gb | Cw | Cb | Iw | Jb | Jw |
	+----+----+----+----+----+----+----+----+

	+----+----+----+----+----+----+----+----+
	| Cw | Hb | Hw | Hb | Iw | Ib | Iw | Ib |
	+----+----+----+----+----+----+----+----+
	| Cb | Cw | Hb | Nw | Nb | Iw | Mb | Bw |
	+----+----+----+----+----+----+----+----+
	| Kw | Kb | Lw | Lb | Nw | Mb | Mw | Bb |
	+----+----+----+----+----+----+----+----+
	| Ab | Kw | Kb | Lw | Nb | Mw | Bb | Bw |
	+----+----+----+----+----+----+----+----+
	| Aw | Ab | Kw | Lb | Nw | Mb | Fw | Jb |
	+----+----+----+----+----+----+----+----+
	| Ab | Dw | Db | Gw | Fb | Fw | Fb | Jw |
	+----+----+----+----+----+----+----+----+
	| Aw | Db | Gw | Gb | Fw | Eb | Ew | Jb |
	+----+----+----+----+----+----+----+----+
	| Db | Dw | Gb | Ew | Eb | Ew | Jb | Jw |
	+----+----+----+----+----+----+----+----+

	+----+----+----+----+----+----+----+----+
	| Iw | Ib | Iw | Ib | Kw | Kb | Nw | Nb |
	+----+----+----+----+----+----+----+----+
	| Bb | Iw | Mb | Mw | Mb | Kw | Kb | Nw |
	+----+----+----+----+----+----+----+----+
	| Bw | Bb | Bw | Lb | Mw | Mb | Kw | Nb |
	+----+----+----+----+----+----+----+----+
	| Ab | Cw | Cb | Lw | Hb | Hw | Hb | Nw |
	+----+----+----+----+----+----+----+----+
	| Aw | Ab | Cw | Lb | Lw | Hb | Fw | Jb |
	+----+----+----+----+----+----+----+----+
	| Ab | Dw | Db | Gw | Fb | Fw | Fb | Jw |
	+----+----+----+----+----+----+----+----+
	| Aw | Db | Gw | Gb | Fw | Eb | Ew | Jb |
	+----+----+----+----+----+----+----+----+
	| Db | Dw | Gb | Ew | Eb | Ew | Jb | Jw |
	+----+----+----+----+----+----+----+----+

	+----+----+----+----+----+----+----+----+
	| Bw | Bb | Ew | Eb | Ew | Kb | Kw | Jb |
	+----+----+----+----+----+----+----+----+
	| Bb | Ew | Eb | Lw | Kb | Kw | Mb | Jw |
	+----+----+----+----+----+----+----+----+
	| Bw | Lb | Lw | Lb | Kw | Mb | Mw | Jb |
	+----+----+----+----+----+----+----+----+
	| Ab | Gw | Gb | Fw | Fb | Mw | Jb | Jw |
	+----+----+----+----+----+----+----+----+
	| Aw | Ab | Gw | Gb | Fw | Mb | Nw | Nb |
	+----+----+----+----+----+----+----+----+
	| Ab | Dw | Db | Cw | Fb | Fw | Hb | Nw |
	+----+----+----+----+----+----+----+----+
	| Aw | Db | Cw | Cb | Iw | Hb | Hw | Nb |
	+----+----+----+----+----+----+----+----+
	| Db | Dw | Ib | Iw | Ib | Iw | Hb | Nw |
	+----+----+----+----+----+----+----+----+

	+----+----+----+----+----+----+----+----+
	| Bw | Bb | Ew | Eb | Ew | Kb | Kw | Jb |
	+----+----+----+----+----+----+----+----+
	| Bb | Ew | Eb | Fw | Kb | Kw | Mb | Jw |
	+----+----+----+----+----+----+----+----+
	| Bw | Fb | Fw | Fb | Kw | Mb | Mw | Jb |
	+----+----+----+----+----+----+----+----+
	| Ab | Fw | Lb | Lw | Lb | Mw | Jb | Jw |
	+----+----+----+----+----+----+----+----+
	| Aw | Ab | Lw | Gb | Gw | Mb | Nw | Nb |
	+----+----+----+----+----+----+----+----+
	| Ab | Dw | Db | Cw | Gb | Gw | Hb | Nw |
	+----+----+----+----+----+----+----+----+
	| Aw | Db | Cw | Cb | Iw | Hb | Hw | Nb |
	+----+----+----+----+----+----+----+----+
	| Db | Dw | Ib | Iw | Ib | Iw | Hb | Nw |
	+----+----+----+----+----+----+----+----+

	+----+----+----+----+----+----+----+----+
	| Ew | Mb | Mw | Mb | Lw | Lb | Nw | Nb |
	+----+----+----+----+----+----+----+----+
	| Eb | Fw | Fb | Mw | Mb | Lw | Jb | Nw |
	+----+----+----+----+----+----+----+----+
	| Ew | Eb | Fw | Gb | Gw | Lb | Jw | Nb |
	+----+----+----+----+----+----+----+----+
	| Ab | Ew | Fb | Fw | Gb | Gw | Jb | Nw |
	+----+----+----+----+----+----+----+----+
	| Aw | Ab | Bw | Bb | Bw | Jb | Jw | Hb |
	+----+----+----+----+----+----+----+----+
	| Ab | Dw | Db | Cw | Bb | Kw | Hb | Hw |
	+----+----+----+----+----+----+----+----+
	| Aw | Db | Cw | Cb | Iw | Kb | Kw | Hb |
	+----+----+----+----+----+----+----+----+
	| Db | Dw | Ib | Iw | Ib | Iw | Kb | Kw |
	+----+----+----+----+----+----+----+----+

	+----+----+----+----+----+----+----+----+
	| Iw | Ib | Iw | Ib | Kw | Kb | Nw | Nb |
	+----+----+----+----+----+----+----+----+
	| Ab | Iw | Mb | Bw | Bb | Kw | Kb | Nw |
	+----+----+----+----+----+----+----+----+
	| Aw | Ab | Mw | Bb | Dw | Db | Kw | Nb |
	+----+----+----+----+----+----+----+----+
	| Ab | Mw | Mb | Bw | Db | Lw | Lb | Nw |
	+----+----+----+----+----+----+----+----+
	| Aw | Mb | Fw | Db | Dw | Gb | Lw | Jb |
	+----+----+----+----+----+----+----+----+
	| Fb | Fw | Fb | Cw | Gb | Gw | Lb | Jw |
	+----+----+----+----+----+----+----+----+
	| Fw | Hb | Cw | Cb | Gw | Eb | Ew | Jb |
	+----+----+----+----+----+----+----+----+
	| Hb | Hw | Hb | Ew | Eb | Ew | Jb | Jw |
	+----+----+----+----+----+----+----+----+

	+----+----+----+----+----+----+----+----+
	| Iw | Ib | Iw | Ib | Cw | Lb | Lw | Lb |
	+----+----+----+----+----+----+----+----+
	| Ab | Iw | Db | Cw | Cb | Lw | Kb | Kw |
	+----+----+----+----+----+----+----+----+
	| Aw | Ab | Dw | Db | Dw | Kb | Kw | Mb |
	+----+----+----+----+----+----+----+----+
	| Ab | Nw | Nb | Ew | Db | Kw | Mb | Mw |
	+----+----+----+----+----+----+----+----+
	| Aw | Gb | Nw | Eb | Fw | Fb | Mw | Jb |
	+----+----+----+----+----+----+----+----+
	| Gb | Gw | Nb | Ew | Eb | Fw | Mb | Jw |
	+----+----+----+----+----+----+----+----+
	| Gw | Hb | Nw | Bb | Ew | Fb | Fw | Jb |
	+----+----+----+----+----+----+----+----+
	| Hb | Hw | Hb | Bw | Bb | Bw | Jb | Jw |
	+----+----+----+----+----+----+----+----+

	+----+----+----+----+----+----+----+----+
	| Iw | Ib | Iw | Ib | Kw | Kb | Nw | Nb |
	+----+----+----+----+----+----+----+----+
	| Ab | Iw | Lb | Lw | Lb | Kw | Kb | Nw |
	+----+----+----+----+----+----+----+----+
	| Aw | Ab | Lw | Mb | Mw | Mb | Kw | Nb |
	+----+----+----+----+----+----+----+----+
	| Ab | Bw | Bb | Bw | Db | Mw | Mb | Nw |
	+----+----+----+----+----+----+----+----+
	| Aw | Gb | Ew | Bb | Dw | Db | Dw | Jb |
	+----+----+----+----+----+----+----+----+
	| Gb | Gw | Eb | Fw | Fb | Cw | Db | Jw |
	+----+----+----+----+----+----+----+----+
	| Gw | Hb | Ew | Eb | Fw | Cb | Cw | Jb |
	+----+----+----+----+----+----+----+----+
	| Hb | Hw | Hb | Ew | Fb | Fw | Jb | Jw |
	+----+----+----+----+----+----+----+----+

	+----+----+----+----+----+----+----+----+
	| Cw | Mb | Mw | Mb | Kw | Kb | Nw | Nb |
	+----+----+----+----+----+----+----+----+
	| Cb | Cw | Db | Mw | Mb | Kw | Kb | Nw |
	+----+----+----+----+----+----+----+----+
	| Lw | Lb | Dw | Db | Dw | Ab | Kw | Nb |
	+----+----+----+----+----+----+----+----+
	| Ib | Lw | Gb | Gw | Db | Aw | Ab | Nw |
	+----+----+----+----+----+----+----+----+
	| Iw | Lb | Ew | Gb | Gw | Ab | Bw | Jb |
	+----+----+----+----+----+----+----+----+
	| Ib | Iw | Eb | Fw | Fb | Aw | Bb | Jw |
	+----+----+----+----+----+----+----+----+
	| Iw | Hb | Ew | Eb | Fw | Bb | Bw | Jb |
	+----+----+----+----+----+----+----+----+
	| Hb | Hw | Hb | Ew | Fb | Fw | Jb | Jw |
	+----+----+----+----+----+----+----+----+

	+----+----+----+----+----+----+----+----+
	| Ew | Lb | Lw | Lb | Fw | Cb | Cw | Jb |
	+----+----+----+----+----+----+----+----+
	| Eb | Lw | Fb | Fw | Fb | Cw | Mb | Jw |
	+----+----+----+----+----+----+----+----+
	| Ew | Eb | Fw | Hb | Nw | Mb | Mw | Jb |
	+----+----+----+----+----+----+----+----+
	| Ab | Ew | Hb | Hw | Nb | Mw | Jb | Jw |
	+----+----+----+----+----+----+----+----+
	| Aw | Ab | Gw | Hb | Nw | Mb | Dw | Db |
	+----+----+----+----+----+----+----+----+
	| Ab | Gw | Gb | Kw | Nb | Nw | Db | Bw |
	+----+----+----+----+----+----+----+----+
	| Aw | Gb | Iw | Kb | Kw | Db | Dw | Bb |
	+----+----+----+----+----+----+----+----+
	| Ib | Iw | Ib | Iw | Kb | Kw | Bb | Bw |
	+----+----+----+----+----+----+----+----+

	+----+----+----+----+----+----+----+----+
	| Bw | Bb | Ew | Eb | Ew | Kb | Kw | Jb |
	+----+----+----+----+----+----+----+----+
	| Bb | Ew | Eb | Fw | Kb | Kw | Gb | Jw |
	+----+----+----+----+----+----+----+----+
	| Bw | Fb | Fw | Fb | Kw | Gb | Gw | Jb |
	+----+----+----+----+----+----+----+----+
	| Ab | Fw | Mb | Nw | Hb | Gw | Jb | Jw |
	+----+----+----+----+----+----+----+----+
	| Aw | Ab | Mw | Nb | Hw | Hb | Lw | Lb |
	+----+----+----+----+----+----+----+----+
	| Ab | Mw | Mb | Nw | Hb | Dw | Db | Lw |
	+----+----+----+----+----+----+----+----+
	| Aw | Mb | Iw | Nb | Nw | Db | Cw | Lb |
	+----+----+----+----+----+----+----+----+
	| Ib | Iw | Ib | Iw | Db | Dw | Cb | Cw |
	+----+----+----+----+----+----+----+----+

	+----+----+----+----+----+----+----+----+
	| Bw | Bb | Ew | Eb | Ew | Hb | Cw | Cb |
	+----+----+----+----+----+----+----+----+
	| Bb | Ew | Eb | Fw | Hb | Hw | Hb | Cw |
	+----+----+----+----+----+----+----+----+
	| Bw | Fb | Fw | Fb | Kw | Kb | Lw | Lb |
	+----+----+----+----+----+----+----+----+
	| Ab | Fw | Mb | Jw | Jb | Kw | Kb | Lw |
	+----+----+----+----+----+----+----+----+
	| Aw | Ab | Mw | Jb | Nw | Db | Kw | Lb |
	+----+----+----+----+----+----+----+----+
	| Ab | Mw | Mb | Jw | Nb | Dw | Db | Dw |
	+----+----+----+----+----+----+----+----+
	| Aw | Mb | Iw | Jb | Nw | Gb | Gw | Db |
	+----+----+----+----+----+----+----+----+
	| Ib | Iw | Ib | Iw | Nb | Nw | Gb | Gw |
	+----+----+----+----+----+----+----+----+

	+----+----+----+----+----+----+----+----+
	| Kw | Kb | Gw | Gb | Iw | Ib | Iw | Ib |
	+----+----+----+----+----+----+----+----+
	| Ab | Kw | Kb | Gw | Gb | Iw | Eb | Ew |
	+----+----+----+----+----+----+----+----+
	| Aw | Ab | Kw | Jb | Ew | Eb | Ew | Mb |
	+----+----+----+----+----+----+----+----+
	| Ab | Nw | Hb | Jw | Jb | Jw | Jb | Mw |
	+----+----+----+----+----+----+----+----+
	| Aw | Nb | Hw | Hb | Fw | Fb | Mw | Mb |
	+----+----+----+----+----+----+----+----+
	| Lb | Nw | Hb | Dw | Db | Fw | Mb | Bw |
	+----+----+----+----+----+----+----+----+
	| Lw | Nb | Nw | Db | Cw | Fb | Fw | Bb |
	+----+----+----+----+----+----+----+----+
	| Lb | Lw | Db | Dw | Cb | Cw | Bb | Bw |
	+----+----+----+----+----+----+----+----+

	+----+----+----+----+----+----+----+----+
	| Kw | Kb | Gw | Gb | Iw | Ib | Iw | Ib |
	+----+----+----+----+----+----+----+----+
	| Ab | Kw | Kb | Gw | Gb | Iw | Eb | Ew |
	+----+----+----+----+----+----+----+----+
	| Aw | Ab | Kw | Bb | Ew | Eb | Ew | Mb |
	+----+----+----+----+----+----+----+----+
	| Ab | Nw | Hb | Bw | Bb | Bw | Mb | Mw |
	+----+----+----+----+----+----+----+----+
	| Aw | Nb | Hw | Hb | Fw | Fb | Mw | Jb |
	+----+----+----+----+----+----+----+----+
	| Lb | Nw | Hb | Dw | Db | Fw | Mb | Jw |
	+----+----+----+----+----+----+----+----+
	| Lw | Nb | Nw | Db | Cw | Fb | Fw | Jb |
	+----+----+----+----+----+----+----+----+
	| Lb | Lw | Db | Dw | Cb | Cw | Jb | Jw |
	+----+----+----+----+----+----+----+----+

	+----+----+----+----+----+----+----+----+
	| Bw | Bb | Bw | Ab | Kw | Kb | Nw | Nb |
	+----+----+----+----+----+----+----+----+
	| Ib | Ew | Bb | Aw | Ab | Kw | Kb | Nw |
	+----+----+----+----+----+----+----+----+
	| Iw | Eb | Ew | Ab | Gw | Gb | Kw | Nb |
	+----+----+----+----+----+----+----+----+
	| Ib | Iw | Eb | Aw | Hb | Gw | Gb | Nw |
	+----+----+----+----+----+----+----+----+
	| Iw | Db | Ew | Hb | Hw | Hb | Fw | Jb |
	+----+----+----+----+----+----+----+----+
	| Lb | Dw | Db | Dw | Fb | Fw | Fb | Jw |
	+----+----+----+----+----+----+----+----+
	| Lw | Mb | Mw | Db | Fw | Cb | Cw | Jb |
	+----+----+----+----+----+----+----+----+
	| Lb | Lw | Mb | Mw | Mb | Cw | Jb | Jw |
	+----+----+----+----+----+----+----+----+

	+----+----+----+----+----+----+----+----+
	| Cw | Hb | Hw | Hb | Kw | Kb | Nw | Nb |
	+----+----+----+----+----+----+----+----+
	| Cb | Cw | Hb | Fw | Mb | Kw | Kb | Nw |
	+----+----+----+----+----+----+----+----+
	| Ew | Fb | Fw | Fb | Mw | Ab | Kw | Nb |
	+----+----+----+----+----+----+----+----+
	| Eb | Fw | Jb | Mw | Mb | Aw | Ab | Nw |
	+----+----+----+----+----+----+----+----+
	| Ew | Eb | Jw | Mb | Gw | Ab | Dw | Db |
	+----+----+----+----+----+----+----+----+
	| Lb | Ew | Jb | Gw | Gb | Aw | Db | Bw |
	+----+----+----+----+----+----+----+----+
	| Lw | Jb | Jw | Gb | Iw | Db | Dw | Bb |
	+----+----+----+----+----+----+----+----+
	| Lb | Lw | Ib | Iw | Ib | Iw | Bb | Bw |
	+----+----+----+----+----+----+----+----+

	+----+----+----+----+----+----+----+----+
	| Iw | Ib | Iw | Ib | Fw | Hb | Hw | Hb |
	+----+----+----+----+----+----+----+----+
	| Ab | Iw | Fb | Fw | Fb | Lw | Hb | Kw |
	+----+----+----+----+----+----+----+----+
	| Aw | Ab | Fw | Lb | Lw | Lb | Kw | Kb |
	+----+----+----+----+----+----+----+----+
	| Ab | Cw | Cb | Dw | Db | Kw | Kb | Nw |
	+----+----+----+----+----+----+----+----+
	| Aw | Mb | Cw | Db | Nw | Nb | Nw | Nb |
	+----+----+----+----+----+----+----+----+
	| Mb | Mw | Db | Dw | Eb | Ew | Gb | Bw |
	+----+----+----+----+----+----+----+----+
	| Mw | Jb | Ew | Eb | Ew | Gb | Gw | Bb |
	+----+----+----+----+----+----+----+----+
	| Mb | Jw | Jb | Jw | Jb | Gw | Bb | Bw |
	+----+----+----+----+----+----+----+----+

	+----+----+----+----+----+----+----+----+
	| Kw | Kb | Iw | Ib | Iw | Ib | Lw | Lb |
	+----+----+----+----+----+----+----+----+
	| Ab | Kw | Kb | Iw | Hb | Fw | Fb | Lw |
	+----+----+----+----+----+----+----+----+
	| Aw | Ab | Kw | Hb | Hw | Hb | Fw | Lb |
	+----+----+----+----+----+----+----+----+
	| Ab | Nw | Nb | Cw | Cb | Ew | Fb | Fw |
	+----+----+----+----+----+----+----+----+
	| Aw | Mb | Nw | Db | Cw | Eb | Ew | Jb |
	+----+----+----+----+----+----+----+----+
	| Mb | Mw | Nb | Dw | Db | Dw | Eb | Jw |
	+----+----+----+----+----+----+----+----+
	| Mw | Bb | Nw | Gb | Gw | Db | Ew | Jb |
	+----+----+----+----+----+----+----+----+
	| Mb | Bw | Bb | Bw | Gb | Gw | Jb | Jw |
	+----+----+----+----+----+----+----+----+

	+----+----+----+----+----+----+----+----+
	| Iw | Ib | Iw | Ib | Lw | Ab | Nw | Nb |
	+----+----+----+----+----+----+----+----+
	| Jb | Iw | Lb | Lw | Lb | Aw | Ab | Nw |
	+----+----+----+----+----+----+----+----+
	| Jw | Jb | Jw | Jb | Fw | Ab | Ew | Nb |
	+----+----+----+----+----+----+----+----+
	| Cb | Cw | Fb | Fw | Fb | Aw | Eb | Nw |
	+----+----+----+----+----+----+----+----+
	| Cw | Mb | Fw | Gb | Dw | Db | Ew | Eb |
	+----+----+----+----+----+----+----+----+
	| Mb | Mw | Gb | Gw | Db | Kw | Kb | Ew |
	+----+----+----+----+----+----+----+----+
	| Mw | Bb | Gw | Db | Dw | Hb | Kw | Kb |
	+----+----+----+----+----+----+----+----+
	| Mb | Bw | Bb | Bw | Hb | Hw | Hb | Kw |
	+----+----+----+----+----+----+----+----+

	+----+----+----+----+----+----+----+----+
	| Cw | Cb | Dw | Db | Ew | Eb | Ew | Hb |
	+----+----+----+----+----+----+----+----+
	| Ib | Cw | Db | Ew | Eb | Fw | Hb | Hw |
	+----+----+----+----+----+----+----+----+
	| Iw | Db | Dw | Fb | Fw | Fb | Lw | Hb |
	+----+----+----+----+----+----+----+----+
	| Ib | Iw | Ab | Fw | Lb | Lw | Lb | Nw |
	+----+----+----+----+----+----+----+----+
	| Iw | Mb | Aw | Ab | Nw | Nb | Nw | Nb |
	+----+----+----+----+----+----+----+----+
	| Mb | Mw | Ab | Kw | Jb | Jw | Jb | Jw |
	+----+----+----+----+----+----+----+----+
	| Mw | Bb | Aw | Kb | Kw | Gb | Gw | Jb |
	+----+----+----+----+----+----+----+----+
	| Mb | Bw | Bb | Bw | Kb | Kw | Gb | Gw |
	+----+----+----+----+----+----+----+----+

	+----+----+----+----+----+----+----+----+
	| Fw | Fb | Ew | Hb | Hw | Hb | Nw | Nb |
	+----+----+----+----+----+----+----+----+
	| Ib | Fw | Eb | Ew | Hb | Lw | Lb | Nw |
	+----+----+----+----+----+----+----+----+
	| Iw | Fb | Fw | Eb | Cw | Cb | Lw | Nb |
	+----+----+----+----+----+----+----+----+
	| Ib | Iw | Ab | Ew | Db | Cw | Lb | Nw |
	+----+----+----+----+----+----+----+----+
	| Iw | Mb | Aw | Ab | Dw | Db | Dw | Jb |
	+----+----+----+----+----+----+----+----+
	| Mb | Mw | Ab | Kw | Gb | Gw | Db | Jw |
	+----+----+----+----+----+----+----+----+
	| Mw | Bb | Aw | Kb | Kw | Gb | Gw | Jb |
	+----+----+----+----+----+----+----+----+
	| Mb | Bw | Bb | Bw | Kb | Kw | Jb | Jw |
	+----+----+----+----+----+----+----+----+

	+----+----+----+----+----+----+----+----+
	| Bw | Bb | Bw | Ab | Cw | Cb | Lw | Lb |
	+----+----+----+----+----+----+----+----+
	| Gb | Gw | Bb | Aw | Ab | Cw | Mb | Lw |
	+----+----+----+----+----+----+----+----+
	| Ew | Gb | Gw | Ab | Dw | Db | Mw | Lb |
	+----+----+----+----+----+----+----+----+
	| Eb | Ew | Jb | Aw | Db | Mw | Mb | Iw |
	+----+----+----+----+----+----+----+----+
	| Nw | Eb | Jw | Db | Dw | Mb | Iw | Ib |
	+----+----+----+----+----+----+----+----+
	| Nb | Ew | Jb | Kw | Kb | Fw | Fb | Iw |
	+----+----+----+----+----+----+----+----+
	| Nw | Jb | Jw | Hb | Kw | Kb | Fw | Ib |
	+----+----+----+----+----+----+----+----+
	| Nb | Nw | Hb | Hw | Hb | Kw | Fb | Fw |
	+----+----+----+----+----+----+----+----+

	+----+----+----+----+----+----+----+----+
	| Cw | Jb | Jw | Jb | Jw | Hb | Hw | Hb |
	+----+----+----+----+----+----+----+----+
	| Cb | Cw | Eb | Ew | Jb | Fw | Hb | Kw |
	+----+----+----+----+----+----+----+----+
	| Ew | Eb | Ew | Fb | Fw | Fb | Kw | Kb |
	+----+----+----+----+----+----+----+----+
	| Mb | Mw | Mb | Fw | Ab | Kw | Kb | Iw |
	+----+----+----+----+----+----+----+----+
	| Nw | Db | Mw | Mb | Aw | Ab | Iw | Ib |
	+----+----+----+----+----+----+----+----+
	| Nb | Dw | Db | Dw | Ab | Bw | Lb | Iw |
	+----+----+----+----+----+----+----+----+
	| Nw | Gb | Gw | Db | Aw | Bb | Lw | Ib |
	+----+----+----+----+----+----+----+----+
	| Nb | Nw | Gb | Gw | Bb | Bw | Lb | Lw |
	+----+----+----+----+----+----+----+----+

	+----+----+----+----+----+----+----+----+
	| Bw | Bb | Bw | Jb | Cw | Cb | Lw | Lb |
	+----+----+----+----+----+----+----+----+
	| Gb | Gw | Bb | Jw | Ab | Cw | Mb | Lw |
	+----+----+----+----+----+----+----+----+
	| Ew | Gb | Gw | Jb | Aw | Ab | Mw | Lb |
	+----+----+----+----+----+----+----+----+
	| Eb | Ew | Jb | Jw | Ab | Mw | Mb | Iw |
	+----+----+----+----+----+----+----+----+
	| Nw | Eb | Dw | Db | Aw | Mb | Iw | Ib |
	+----+----+----+----+----+----+----+----+
	| Nb | Ew | Db | Kw | Kb | Fw | Fb | Iw |
	+----+----+----+----+----+----+----+----+
	| Nw | Db | Dw | Hb | Kw | Kb | Fw | Ib |
	+----+----+----+----+----+----+----+----+
	| Nb | Nw | Hb | Hw | Hb | Kw | Fb | Fw |
	+----+----+----+----+----+----+----+----+

	+----+----+----+----+----+----+----+----+
	| Bw | Bb | Iw | Ib | Iw | Ib | Lw | Lb |
	+----+----+----+----+----+----+----+----+
	| Bb | Kw | Kb | Iw | Ab | Fw | Fb | Lw |
	+----+----+----+----+----+----+----+----+
	| Bw | Hb | Kw | Kb | Aw | Ab | Fw | Lb |
	+----+----+----+----+----+----+----+----+
	| Hb | Hw | Mb | Kw | Ab | Ew | Fb | Fw |
	+----+----+----+----+----+----+----+----+
	| Nw | Hb | Mw | Db | Aw | Eb | Ew | Jb |
	+----+----+----+----+----+----+----+----+
	| Nb | Mw | Mb | Dw | Db | Dw | Eb | Jw |
	+----+----+----+----+----+----+----+----+
	| Nw | Mb | Cw | Gb | Gw | Db | Ew | Jb |
	+----+----+----+----+----+----+----+----+
	| Nb | Nw | Cb | Cw | Gb | Gw | Jb | Jw |
	+----+----+----+----+----+----+----+----+

	+----+----+----+----+----+----+----+----+
	| Bw | Bb | Iw | Ib | Iw | Ib | Lw | Lb |
	+----+----+----+----+----+----+----+----+
	| Bb | Cw | Cb | Iw | Ab | Fw | Fb | Lw |
	+----+----+----+----+----+----+----+----+
	| Bw | Hb | Cw | Mb | Aw | Ab | Fw | Lb |
	+----+----+----+----+----+----+----+----+
	| Hb | Hw | Mb | Mw | Ab | Ew | Fb | Fw |
	+----+----+----+----+----+----+----+----+
	| Nw | Hb | Mw | Db | Aw | Eb | Ew | Jb |
	+----+----+----+----+----+----+----+----+
	| Nb | Kw | Mb | Dw | Db | Dw | Eb | Jw |
	+----+----+----+----+----+----+----+----+
	| Nw | Kb | Kw | Gb | Gw | Db | Ew | Jb |
	+----+----+----+----+----+----+----+----+
	| Nb | Nw | Kb | Kw | Gb | Gw | Jb | Jw |
	+----+----+----+----+----+----+----+----+

	+----+----+----+----+----+----+----+----+
	| Jw | Jb | Ew | Mb | Mw | Mb | Lw | Lb |
	+----+----+----+----+----+----+----+----+
	| Jb | Cw | Eb | Ew | Ab | Mw | Mb | Lw |
	+----+----+----+----+----+----+----+----+
	| Jw | Cb | Cw | Eb | Aw | Ab | Gw | Lb |
	+----+----+----+----+----+----+----+----+
	| Jb | Bw | Bb | Ew | Ab | Gw | Gb | Iw |
	+----+----+----+----+----+----+----+----+
	| Nw | Bb | Dw | Db | Aw | Gb | Iw | Ib |
	+----+----+----+----+----+----+----+----+
	| Nb | Bw | Db | Kw | Kb | Fw | Fb | Iw |
	+----+----+----+----+----+----+----+----+
	| Nw | Db | Dw | Hb | Kw | Kb | Fw | Ib |
	+----+----+----+----+----+----+----+----+
	| Nb | Nw | Hb | Hw | Hb | Kw | Fb | Fw |
	+----+----+----+----+----+----+----+----+
