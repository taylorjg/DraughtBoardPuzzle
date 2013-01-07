using System;
using System.Collections.Generic;
using System.Linq;
using DraughtBoardPuzzle;

namespace DraughtBoardPuzzleDlxApp
{
    internal class Program
    {
        private static void Main()
        {
            var pieces = Piece.RealPieces;
            var solver = new SolverUsingDlx(pieces);

            var solutions = solver.FindAllSolutions().ToList();
            solutions = RemoveDuplicateSolutions(solver, solutions);

            var consolePrintTarget = new ConsolePrintTarget();
            var boardPrinter = new BoardPrinter(consolePrintTarget);

            Console.WriteLine("Found {0} solutions", solutions.Count());

            foreach (var solution in solutions)
            {
                var solutionAsArray = solution.ToArray();
                var board = solver.PopulateBoardWithSolution(solutionAsArray);
                Console.WriteLine();
                boardPrinter.Print(board);
                Console.WriteLine();
                solver.WriteSolution(solutionAsArray);
            }

            solver.WritePropertyList("8x8_Pieces1.plist", pieces, solutions);
        }

        private static List<IEnumerable<int>> RemoveDuplicateSolutions(SolverUsingDlx solver, IEnumerable<IEnumerable<int>> solutions)
        {
            var result = new List<IEnumerable<int>>();
            var uniqueSolutionStrings = new List<string>();
            foreach (var solution in solutions)
            {
                var solutionList = solution.ToList();
                var solutionString = MakeSolutionString(solver, solutionList);
                if (uniqueSolutionStrings.Any(s => s == solutionString)) continue;
                result.Add(solutionList);
                uniqueSolutionStrings.Add(solutionString);
            }
            return result;
        }

        private static string MakeSolutionString(SolverUsingDlx solver, IEnumerable<int> solution)
        {
            var board = solver.PopulateBoardWithSolution(solution.ToArray());
            var pieceNames = new List<string>();
            for (var x = 0; x < board.BoardSize; x++)
            {
                for (var y = 0; y < board.BoardSize; y++)
                {
                    var piece = board.PieceAt(x, y);
                    var square = board.SquareAt(x, y);
                    var pieceName = piece.Name + ((square.Colour == Colour.Black) ? "b" : "w");
                    pieceNames.Add(pieceName);
                }
            }
            return string.Join("-", pieceNames);
        }
    }
}
