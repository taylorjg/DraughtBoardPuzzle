using System;
using System.Linq;
using NUnit.Framework;
using Moq;

// http://ayende.com/blog/1441/unable-to-obtain-public-key-for-strongnamekeypair

namespace DraughtBoardPuzzle.Tests
{
    // ReSharper disable InconsistentNaming

    [TestFixture]
    internal class SolverTests
    {
        private const int TestBoardSize = 4;

        [Test]
        public void SolvePuzzle_GivenASinglePermutationOfPiecesInTheCorrectOrderAndOrientation_SolvesThePuzzle()
        {
            // Arrange
            var board = new Board(TestBoardSize);
            var solver = new Solver();
            var mockPieceFeeder = new Mock<IPieceFeeder>();

            mockPieceFeeder.SetupGet(x => x.Permutations).Returns(
                new[]
                    {
                        new[]
                            {
                                new RotatedPiece(Piece.TestPieceA, Orientation.North),
                                new RotatedPiece(Piece.TestPieceC, Orientation.North),
                                new RotatedPiece(Piece.TestPieceD, Orientation.North),
                                new RotatedPiece(Piece.TestPieceB, Orientation.North)
                            }
                    });
            mockPieceFeeder.SetupGet(x => x.Pieces).Returns(new[] { Piece.TestPieceA, Piece.TestPieceB, Piece.TestPieceC, Piece.TestPieceD });

            // Act
            var actual = solver.SolvePuzzle(board, mockPieceFeeder.Object);

            // Assert
            Assert.That(actual.Solved, Is.True);
            Assert.That(actual.Iterations, Is.EqualTo(1));
            Assert.That(board.IsSolved(), Is.True);
        }

        [Test]
        public void SolvePuzzle_GivenASinglePermutationOfPiecesNotInTheCorrectOrderAndOrientation_DoesNotSolveThePuzzle()
        {
            // Arrange
            var board = new Board(TestBoardSize);
            var solver = new Solver();
            var mockPieceFeeder = new Mock<IPieceFeeder>();

            mockPieceFeeder.SetupGet(x => x.Permutations).Returns(
                new[]
                    {
                        new[]
                            {
                                new RotatedPiece(Piece.TestPieceA, Orientation.North),
                                new RotatedPiece(Piece.TestPieceB, Orientation.North),
                                new RotatedPiece(Piece.TestPieceC, Orientation.North),
                                new RotatedPiece(Piece.TestPieceD, Orientation.North)
                            }
                    });
            mockPieceFeeder.SetupGet(x => x.Pieces).Returns(new[] { Piece.TestPieceA, Piece.TestPieceB, Piece.TestPieceC, Piece.TestPieceD });

            // Act
            var actual = solver.SolvePuzzle(board, mockPieceFeeder.Object);

            // Assert
            Assert.That(actual.Solved, Is.False);
            Assert.That(actual.Iterations, Is.EqualTo(1));
            Assert.That(board.IsSolved(), Is.False);
        }

        [Test]
        public void SolvePuzzle_GivenAPieceFeederThatFeedsAWrongPermutationThenACorrectPermutation_SolvesThePuzzleOnTheSecondPermutation()
        {
            // Arrange
            var board = new Board(TestBoardSize);
            var solver = new Solver();
            var mockPieceFeeder = new Mock<IPieceFeeder>();

            mockPieceFeeder.SetupGet(x => x.Permutations).Returns(
                new[]
                    {
                        new[]
                            {
                                new RotatedPiece(Piece.TestPieceA, Orientation.North),
                                new RotatedPiece(Piece.TestPieceB, Orientation.North),
                                new RotatedPiece(Piece.TestPieceC, Orientation.North),
                                new RotatedPiece(Piece.TestPieceD, Orientation.North)
                            },
                        new[]
                            {
                                new RotatedPiece(Piece.TestPieceA, Orientation.North),
                                new RotatedPiece(Piece.TestPieceC, Orientation.North),
                                new RotatedPiece(Piece.TestPieceD, Orientation.North),
                                new RotatedPiece(Piece.TestPieceB, Orientation.North)
                            }
                    });
            mockPieceFeeder.SetupGet(x => x.Pieces).Returns(new[] {Piece.TestPieceA, Piece.TestPieceB, Piece.TestPieceC, Piece.TestPieceD});

            // Act
            var actual = solver.SolvePuzzle(board, mockPieceFeeder.Object);

            // Assert
            Assert.That(actual.Solved, Is.True);
            Assert.That(actual.Iterations, Is.EqualTo(2));
            Assert.That(board.IsSolved(), Is.True);
            mockPieceFeeder.Verify(x => x.Permutations, Times.Once());
        }

        [Test]
        public void SolvePuzzle_GivenAPieceFeederThatIncludesTheCorrectPermutation_ReturnsTheCorrectPermutation()
        {
            // Arrange
            var board = new Board(TestBoardSize);
            var solver = new Solver();
            var mockPieceFeeder = new Mock<IPieceFeeder>();

            mockPieceFeeder.SetupGet(x => x.Permutations).Returns(
                new[]
                    {
                        new[]
                            {
                                new RotatedPiece(Piece.TestPieceA, Orientation.North),
                                new RotatedPiece(Piece.TestPieceB, Orientation.North),
                                new RotatedPiece(Piece.TestPieceC, Orientation.North),
                                new RotatedPiece(Piece.TestPieceD, Orientation.North)
                            },
                        new[]
                            {
                                new RotatedPiece(Piece.TestPieceA, Orientation.North),
                                new RotatedPiece(Piece.TestPieceC, Orientation.North),
                                new RotatedPiece(Piece.TestPieceD, Orientation.North),
                                new RotatedPiece(Piece.TestPieceB, Orientation.North)
                            }
                    });
            mockPieceFeeder.SetupGet(x => x.Pieces).Returns(new[] { Piece.TestPieceA, Piece.TestPieceB, Piece.TestPieceC, Piece.TestPieceD });

            // Act
            var actual = solver.SolvePuzzle(board, mockPieceFeeder.Object);
            var solution = actual.Solution.ToArray();

            // Assert
            Assert.That(solution[0].Piece, Is.SameAs(Piece.TestPieceA));
            Assert.That(solution[0].Orientation, Is.EqualTo(Orientation.North));

            Assert.That(solution[1].Piece, Is.SameAs(Piece.TestPieceC));
            Assert.That(solution[1].Orientation, Is.EqualTo(Orientation.North));

            Assert.That(solution[2].Piece, Is.SameAs(Piece.TestPieceD));
            Assert.That(solution[2].Orientation, Is.EqualTo(Orientation.North));

            Assert.That(solution[3].Piece, Is.SameAs(Piece.TestPieceB));
            Assert.That(solution[3].Orientation, Is.EqualTo(Orientation.North));
        }

        [Test]
        public void SolvePuzzle_GivenInsaneData_ThrowsException()
        {
            // Arrange
            var board = new Board(TestBoardSize);
            var solver = new Solver();
            var pieceFeeder = new PieceFeeder(Piece.TestPieceA, Piece.TestPieceB, Piece.TestPieceC);

            // Act, Assert
            Assert.Throws<InvalidOperationException>(() => solver.SolvePuzzle(board, pieceFeeder));
        }

        [Test]
        public void SanityCheck_GivenSaneData_ReturnsTrue()
        {
            // Arrange
            var board = new Board(TestBoardSize);
            var solver = new Solver();
            var pieceFeeder = new PieceFeeder(Piece.TestPieceA, Piece.TestPieceB, Piece.TestPieceC, Piece.TestPieceD);

            // Act
            var actual = solver.SanityCheck(board, pieceFeeder);

            // Assert
            Assert.That(actual, Is.True);
        }

        [Test]
        public void SanityCheck_GivenInsaneDataDueToWrongNumberOfSquares_ReturnsFalse()
        {
            // Arrange
            var board = new Board(TestBoardSize);
            var solver = new Solver();
            var pieceFeeder = new PieceFeeder(Piece.TestPieceA, Piece.TestPieceB, Piece.TestPieceC);

            // Act
            var actual = solver.SanityCheck(board, pieceFeeder);

            // Assert
            Assert.That(actual, Is.False);
        }

        [Test]
        public void SanityCheck_GivenInsaneDataDueToWrongNumberOfBlackAndWhiteSquares_ReturnsFalse()
        {
            // Arrange
            var board = new Board(TestBoardSize);
            var solver = new Solver();
            var bogusPieceD = new Piece(
                new[]
                    {
                        //  B
                        //  W
                        // WW
                        new Square(0, 0, Colour.White),
                        new Square(1, 0, Colour.White),
                        new Square(1, 1, Colour.White),
                        new Square(1, 2, Colour.Black)
                    },
                'D');
            var pieceFeeder = new PieceFeeder(Piece.TestPieceA, Piece.TestPieceB, Piece.TestPieceC, bogusPieceD);

            // Act
            var actual = solver.SanityCheck(board, pieceFeeder);

            // Assert
            Assert.That(actual, Is.False);
        }
    }

    // ReSharper restore InconsistentNaming
}
