namespace DraughtBoardPuzzle
{
    public partial class Piece
    {
        public static Piece[] TestPieces
        {
            get
            {
                return CreatePieces(
                    new[]
                        {
                            // A
                            new[]
                                {
                                    "W   ",
                                    "BWBW"
                                },

                            // B
                            new[]
                                {
                                    "WBW",
                                    "B  "
                                },

                            // C
                            new[]
                                {
                                    "WB",
                                    "B "
                                },

                            // D
                            new[]
                                {
                                    " B",
                                    " W",
                                    "WB"
                                }
                        });
            }
        }

        public static readonly Piece TestPieceA = TestPieces[0];
        public static readonly Piece TestPieceB = TestPieces[1];
        public static readonly Piece TestPieceC = TestPieces[2];
        public static readonly Piece TestPieceD = TestPieces[3];
    }
}
