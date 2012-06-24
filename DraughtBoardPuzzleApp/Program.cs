using System;
using System.Collections.Generic;
using DraughtBoardPuzzle;

namespace DraughtBoardPuzzleApp
{
    internal static class Program
    {
        private static void Main()
        {
            SolveBoard(4, Piece.TestPieces);
            SolveBoard(8, Piece.RealPieces);
        }

        private static void SolveBoard(int boardSize, Piece[] pieces)
        {
            var board = new Board(boardSize);
            var solver = new Solver();
            var pieceFeeder = new PieceFeeder(pieces);
            var solutionDetails = solver.SolvePuzzle(board, pieceFeeder);

            if (solutionDetails.Solved) {
                var consolePrintTarget = new ConsolePrintTarget();
                var boardPrinter = new BoardPrinter(consolePrintTarget);
                Console.WriteLine("Solution found after {0} iterations.", solutionDetails.Iterations);
                PrintPermutation(solutionDetails.Solution);
                Console.WriteLine();
                boardPrinter.Print(board);
            }
            else
            {
                Console.WriteLine("Failed to find a solution after {0} iterations.", solutionDetails.Iterations);
            }

            Console.WriteLine();
        }

        private static void PrintPermutation(IEnumerable<RotatedPiece> permutation)
        {
            foreach (var rotatedPiece in permutation)
                PrintRotatedPiece(rotatedPiece);

            Console.WriteLine();
        }

        private static void PrintRotatedPiece(RotatedPiece rotatedPiece)
        {
            string orientationInitialLetter = rotatedPiece.Orientation.ToString().Substring(0, 1);
            Console.Write("{0}{1} ", rotatedPiece.Piece.Name, orientationInitialLetter);
        }
    }
}
