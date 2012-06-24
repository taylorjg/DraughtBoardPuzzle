using System;
using System.Collections.Generic;
using System.Linq;

namespace DraughtBoardPuzzle
{
    public class Solver
    {
        public SolutionDetails SolvePuzzle(Board board, IPieceFeeder pieceFeeder)
        {
            if (!SanityCheck(board, pieceFeeder))
                throw new InvalidOperationException("Failed sanity check!");

            var solved = false;
            var iterations = 0;
            IEnumerable<RotatedPiece> solution = null;

            foreach (var permutation in pieceFeeder.Permutations) {

                iterations++;

                board.Reset();

                // ReSharper disable PossibleMultipleEnumeration
                if (permutation.All(board.LayoutNextPiece))
                {
                    solved = true;
                    solution = permutation;
                    break;
                }
                // ReSharper restore PossibleMultipleEnumeration
            }

            return new SolutionDetails(solved, iterations, solution);
        }

        public bool SanityCheck(Board board, IPieceFeeder pieceFeeder)
        {
            var totalSquaresExpected = board.BoardSize * board.BoardSize;
            var totalWhiteSquaresExpected = totalSquaresExpected / 2;
            var totalBlackSquaresExpected = totalSquaresExpected / 2;

            var actualSquares = 0;
            var actualWhiteSquares = 0;
            var actualBlackSquares = 0;

            foreach (var piece in pieceFeeder.Pieces)
            {
                for (var x = 0; x < piece.Width; x++)
                {
                    for (var y = 0; y < piece.Height; y++)
                    {
                        var square = piece.SquareAt(x, y);
                        if (square != null)
                        {
                            actualSquares++;

                            if (square.Colour == Colour.White)
                                actualWhiteSquares++;

                            if (square.Colour == Colour.Black)
                                actualBlackSquares++;
                        }
                    }
                }
            }

            return (
                actualSquares == totalSquaresExpected &&
                actualWhiteSquares == totalWhiteSquaresExpected &&
                actualBlackSquares == totalBlackSquaresExpected);
        }
    }
}
