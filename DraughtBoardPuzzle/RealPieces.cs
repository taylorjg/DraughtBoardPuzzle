namespace DraughtBoardPuzzle
{
    public partial class Piece
    {
        public static Piece[] RealPieces
        {
            get
            {
                return CreatePieces(
                    new[]
                        {
                            // A
                            new[]
                                {
                                    "B ",
                                    "WB",
                                    "B ",
                                    "W "
                                },

                            // B
                            new[]
                                {
                                    "B  ",
                                    "WBW"
                                },

                            // C
                            new[]
                                {
                                    "W ",
                                    "BW"
                                },

                            // D
                            new[]
                                {
                                    " WB",
                                    " B ",
                                    "BW "
                                },

                            // E
                            new[]
                                {
                                    "W ",
                                    "BW",
                                    " B",
                                    " W"
                                },

                            // F
                            new[]
                                {
                                    "WB ",
                                    " W ",
                                    " BW"
                                },

                            // G
                            new[]
                                {
                                    "WB ",
                                    " WB"
                                },

                            // H
                            new[]
                                {
                                    "B ",
                                    "WB",
                                    "B "
                                },

                            // I
                            new[]
                                {
                                    "B ",
                                    "W ",
                                    "BW",
                                    "W "
                                },

                            // J
                            new[]
                                {
                                    " B",
                                    " W",
                                    " B",
                                    "BW"
                                },

                            // K
                            new[]
                                {
                                    "  W",
                                    " WB",
                                    "WB "
                                },

                            // L
                            new[]
                                {
                                    "B ",
                                    "W ",
                                    "BW"
                                },

                            // M
                            new[]
                                {
                                    " B",
                                    " W",
                                    "WB",
                                    "B "
                                },

                            // N
                            new[]
                                {
                                    "W ",
                                    "B ",
                                    "W ",
                                    "BW"
                                },
                        });
            }
        }
    }
}
