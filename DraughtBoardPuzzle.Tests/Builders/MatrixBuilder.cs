namespace DraughtBoardPuzzle.Tests.Builders
{
    static internal class MatrixBuilder
    {
        // http://janmagnet.files.wordpress.com/2008/07/decs-draft.pdf
        public static bool[,] Build()
        {
            return IntArrayToBoolArray(
                new[,]
                    {
                        { 1, 0, 0, 0 },
                        { 0, 1, 1, 0 },
                        { 1, 0, 0, 1 },
                        { 0, 0, 1, 1 },
                        { 0, 1, 0, 0 },
                        { 0, 0, 1, 0 }
                    });
        }

        private static bool[,] IntArrayToBoolArray(int[,] intArray)
        {
            var numRows = intArray.GetLength(0);
            var numCols = intArray.GetLength(1);
            var boolArray = new bool[numRows, numCols];
            for (var row = 0; row < numRows; row++)
            {
                for (var col = 0; col < numCols; col++)
                {
                    boolArray[row, col] = intArray[row, col] != 0;
                }
            }
            return boolArray;
        }
    }
}
