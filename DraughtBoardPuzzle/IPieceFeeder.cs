using System.Collections.Generic;

namespace DraughtBoardPuzzle
{
    public interface IPieceFeeder
    {
        IEnumerable<IEnumerable<RotatedPiece>> Permutations { get; }
        IEnumerable<Piece> Pieces { get; }
    }
}
