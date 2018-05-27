namespace PlutoRover
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class RoverTest
    {
        [Test]
        public void NewRoverInstanceShouldSetItsPosition()
        {
            var rover = new Rover(0, 0, 'N');
            var expectedPosition = new Position(0, 0, 'N');
            Assert.That(rover.Position, Is.EqualTo(expectedPosition));
        }

        [Test]
        public void OrientationIsCaseInsensitive()
        {
            var rover = new Rover(0, 0, 'n');
            var expectedPosition = new Position(0, 0, 'n');
            Assert.That(rover.Position, Is.EqualTo(expectedPosition));

            rover = new Rover(0, 0, 's');
            expectedPosition = new Position(0, 0, 's');
            Assert.That(rover.Position, Is.EqualTo(expectedPosition));

            rover = new Rover(0, 0, 'e');
            expectedPosition = new Position(0, 0, 'e');
            Assert.That(rover.Position, Is.EqualTo(expectedPosition));

            rover = new Rover(0, 0, 'w');
            expectedPosition = new Position(0, 0, 'w');
            Assert.That(rover.Position, Is.EqualTo(expectedPosition));
        }

        [Test]
        public void ConstructingRoverWithUnrecognizedOrientationShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => new Rover(0, 0, 'x')).Message.Equals("Orientation 'x' is not valid.");
        }

        [Test]
        public void SingleCommandFShouldMoveRoverForwardByOneGridPoint()
        {
            var rover = new Rover(5, 5, 'N');
            rover.Move("F");
            var expectedPosition = new Position(5, 6, 'N');
            Assert.That(rover.Position, Is.EqualTo(expectedPosition));

            rover = new Rover(5, 5, 'S');
            rover.Move("F");
            expectedPosition = new Position(5, 4, 'S');
            Assert.That(rover.Position, Is.EqualTo(expectedPosition));

            rover = new Rover(5, 5, 'E');
            rover.Move("F");
            expectedPosition = new Position(6, 5, 'E');
            Assert.That(rover.Position, Is.EqualTo(expectedPosition));

            rover = new Rover(5, 5, 'W');
            rover.Move("F");
            expectedPosition = new Position(4, 5, 'W');
            Assert.That(rover.Position, Is.EqualTo(expectedPosition));
        }

        [Test]
        public void CommandIsCaseInsensitive()
        {
            var rover = new Rover(5, 5, 'N');
            rover.Move("F");
            var expectedPosition = new Position(5, 6, 'N');
            Assert.That(rover.Position, Is.EqualTo(expectedPosition));

            rover = new Rover(5, 5, 'N');
            rover.Move("f");
            expectedPosition = new Position(5, 6, 'N');
            Assert.That(rover.Position, Is.EqualTo(expectedPosition));
        }
    }
}
