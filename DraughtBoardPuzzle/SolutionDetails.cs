using System.Collections.Generic;

namespace DraughtBoardPuzzle
{
    public class SolutionDetails
    {
        public SolutionDetails(bool solved, int iterations, IEnumerable<RotatedPiece> solution)
        {
            Solution = solution;
            Iterations = iterations;
            Solved = solved;
        }

        public bool Solved { get; private set; }
        public int Iterations { get; private set; }
        public IEnumerable<RotatedPiece> Solution { get; private set; }
    }
}
