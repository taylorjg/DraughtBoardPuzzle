using System.Collections.Generic;
using System.Linq;

namespace DraughtBoardPuzzle
{
    public class PieceFeeder : IPieceFeeder
    {
        private class RotatedPieceHolder
        {
            public RotatedPieceHolder(Piece piece)
            {
                Piece = piece;
                Reset();
            }

            public Piece Piece { get; private set; }
            public Orientation Orientation { get; private set; }
            public int RotationNumber { get; private set; }

            public void Reset()
            {
                Orientation = Orientation.North;
                RotationNumber = 1;
            }

            public void Rotate()
            {
                Orientation = Orientation.NextOrientation();
                RotationNumber++;
            }
        }

        private readonly IEnumerable<RotatedPieceHolder> _pieces;

        public PieceFeeder(params Piece[] pieces)
        {
            _pieces = (from p in pieces select new RotatedPieceHolder(p)).ToArray();
        }

        public IEnumerable<Piece> Pieces
        {
            get
            {
                return from rph in _pieces select rph.Piece;
            }
        }

        public IEnumerable<IEnumerable<RotatedPiece>> Permutations
        {
            get
            {
                foreach (var orderPermutation in PermuteUtils.Permute(_pieces, _pieces.Count()))
                {
                    var orderPermutationAsAnArray = orderPermutation.ToArray();

                    foreach (var item in orderPermutationAsAnArray)
                        item.Reset();

                    for (; ; ) {
                        var nextOrientationPermutation = GetNextOrientationPermutation(orderPermutationAsAnArray);
                        if (nextOrientationPermutation == null)
                            break;
                        yield return nextOrientationPermutation;
                    }
                }
            }
        }

        private IEnumerable<RotatedPiece> GetNextOrientationPermutation(RotatedPieceHolder[] orderPermutation)
        {
            var lastPiece = orderPermutation.Last();

            if (lastPiece.RotationNumber > 4) {
                lastPiece.Reset();
                if (!RotateEarlierPieces(orderPermutation))
                    return null;
            }

            var rotatedPieces = (from rph in orderPermutation select new RotatedPiece(rph.Piece, rph.Orientation)).ToList();
            lastPiece.Rotate();

            return rotatedPieces;
        }

        private bool RotateEarlierPieces(RotatedPieceHolder[] orderPermutation)
        {
            int numPieces = orderPermutation.Length;

            if (numPieces <= 1)
                return false;

            var indexZeroHasBeenReset = false;

            for (int i = numPieces - 2; i >= 0; i--) {
                if (orderPermutation[i].RotationNumber < 4) {
                    orderPermutation[i].Rotate();
                    break;
                }

                orderPermutation[i].Reset();
                indexZeroHasBeenReset = i == 0;
            }

            return !indexZeroHasBeenReset;
        }
    }
}
