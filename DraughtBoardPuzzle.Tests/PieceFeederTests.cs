using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace DraughtBoardPuzzle.Tests
{
    // ReSharper disable InconsistentNaming

    [TestFixture]
    internal class PieceFeederTests
    {
        [Test]
        public void Permutations_GivenThatSomePiecesHaveBeenMadeAvailableViaTheConstructor_ReturnsThePiecesInTheInitialOrderAllWithOrientationNorth()
        {
            // Arrange
            var pieceFeeder = new PieceFeeder(Piece.TestPieceA, Piece.TestPieceB, Piece.TestPieceC, Piece.TestPieceD);

            // Act
            var rotatedPieces = pieceFeeder.Permutations.First().ToArray();

            // Assert
            Assert.That(rotatedPieces, Has.Length.EqualTo(4));
            Assert.That(rotatedPieces, Has.All.InstanceOf<RotatedPiece>());
            Assert.That(rotatedPieces[0].Piece, Is.SameAs(Piece.TestPieceA));
            Assert.That(rotatedPieces[1].Piece, Is.SameAs(Piece.TestPieceB));
            Assert.That(rotatedPieces[2].Piece, Is.SameAs(Piece.TestPieceC));
            Assert.That(rotatedPieces[3].Piece, Is.SameAs(Piece.TestPieceD));
            Assert.That(rotatedPieces, Has.All.Matches<RotatedPiece>(rp => rp.Orientation == Orientation.North));
        }

        [Test]
        public void Permutations_GivenThatTheFeederHasOnePiece_ReturnsNorthEastSouthWestOfTheSinglePieceThenStops()
        {
            // Arrange
            var pieceFeeder = new PieceFeeder(Piece.TestPieceA);

            // Act
            var permutations = pieceFeeder.Permutations.Take(4).ToArray();

            // Assert
            Assert.That(permutations, Has.Length.EqualTo(4));
            Assert.That(permutations, Has.All.Matches<IEnumerable<RotatedPiece>>(rps => rps.Count() == 1));
            Assert.That(permutations, Has.All.Matches<IEnumerable<RotatedPiece>>(rps => rps.First().Piece == Piece.TestPieceA));
            Assert.That(permutations[0].First().Orientation, Is.EqualTo(Orientation.North));
            Assert.That(permutations[1].First().Orientation, Is.EqualTo(Orientation.East));
            Assert.That(permutations[2].First().Orientation, Is.EqualTo(Orientation.South));
            Assert.That(permutations[3].First().Orientation, Is.EqualTo(Orientation.West));
        }

        [Test]
        public void Permutations_GivenThatTheFeederHasTwoPieces_ReturnsNorthEastSouthWestOfTheSecondPieceAsTheFirstFourPermutations()
        {
            // Arrange
            var pieceFeeder = new PieceFeeder(Piece.TestPieceA, Piece.TestPieceB);

            // Act
            var permutations = pieceFeeder.Permutations.Take(4).ToArray();

            // Assert
            Assert.That(permutations, Has.Length.EqualTo(4));
            Assert.That(permutations, Has.All.Matches<IEnumerable<RotatedPiece>>(rps => rps.Count() == 2));
            Assert.That(permutations[0].First().Orientation, Is.EqualTo(Orientation.North));
            Assert.That(permutations[1].First().Orientation, Is.EqualTo(Orientation.North));
            Assert.That(permutations[2].First().Orientation, Is.EqualTo(Orientation.North));
            Assert.That(permutations[3].First().Orientation, Is.EqualTo(Orientation.North));
            Assert.That(permutations[0].Last().Orientation, Is.EqualTo(Orientation.North));
            Assert.That(permutations[1].Last().Orientation, Is.EqualTo(Orientation.East));
            Assert.That(permutations[2].Last().Orientation, Is.EqualTo(Orientation.South));
            Assert.That(permutations[3].Last().Orientation, Is.EqualTo(Orientation.West));
        }

        [Test]
        public void Permutations_GivenThatTheFeederHasTwoPieces_ReturnsEOfFirstPieceAndNESWOfSecondPieceInTheSecondBatchOfFourPermutations()
        {
            // Arrange
            var pieceFeeder = new PieceFeeder(Piece.TestPieceA, Piece.TestPieceB);

            // Act
            var permutations = pieceFeeder.Permutations.Take(8).ToArray();

            // Assert
            Assert.That(permutations, Has.Length.EqualTo(8));
            Assert.That(permutations, Has.All.Matches<IEnumerable<RotatedPiece>>(rps => rps.Count() == 2));
            Assert.That(permutations[4].First().Orientation, Is.EqualTo(Orientation.East));
            Assert.That(permutations[5].First().Orientation, Is.EqualTo(Orientation.East));
            Assert.That(permutations[6].First().Orientation, Is.EqualTo(Orientation.East));
            Assert.That(permutations[7].First().Orientation, Is.EqualTo(Orientation.East));
            Assert.That(permutations[4].Last().Orientation, Is.EqualTo(Orientation.North));
            Assert.That(permutations[5].Last().Orientation, Is.EqualTo(Orientation.East));
            Assert.That(permutations[6].Last().Orientation, Is.EqualTo(Orientation.South));
            Assert.That(permutations[7].Last().Orientation, Is.EqualTo(Orientation.West));
        }
    }

    // ReSharper restore InconsistentNaming
}
