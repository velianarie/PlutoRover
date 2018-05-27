namespace PlutoRover
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;

    [TestFixture]
    public class RoverTest
    {
        private static IEnumerable<TestCaseData> OrientationCaseInsensitiveTestData
        {
            get
            {
                yield return new TestCaseData(new Rover(0, 0, 'n'), new Position(0, 0, 'n'));
                yield return new TestCaseData(new Rover(0, 0, 's'), new Position(0, 0, 's'));
                yield return new TestCaseData(new Rover(0, 0, 'e'), new Position(0, 0, 'e'));
                yield return new TestCaseData(new Rover(0, 0, 'w'), new Position(0, 0, 'w'));
            }
        }

        private static IEnumerable<TestCaseData> CommandCaseInsensitiveTestData
        {
            get
            {
                yield return new TestCaseData(new Rover(5, 5, 'N'), "F", new Position(5, 6, 'N'));
                yield return new TestCaseData(new Rover(5, 5, 'N'), "f", new Position(5, 6, 'N'));
            }
        }

        private static IEnumerable<TestCaseData> SingleCommandFTestData
        {
            get
            {
                yield return new TestCaseData(new Rover(5, 5, 'N'), new Position(5, 6, 'N'));
                yield return new TestCaseData(new Rover(5, 5, 'S'), new Position(5, 4, 'S'));
                yield return new TestCaseData(new Rover(5, 5, 'E'), new Position(6, 5, 'E'));
                yield return new TestCaseData(new Rover(5, 5, 'W'), new Position(4, 5, 'W'));
            }
        }

        private static IEnumerable<TestCaseData> SingleCommandBTestData
        {
            get
            {
                yield return new TestCaseData(new Rover(5, 5, 'N'), new Position(5, 4, 'N'));
                yield return new TestCaseData(new Rover(5, 5, 'S'), new Position(5, 6, 'S'));
                yield return new TestCaseData(new Rover(5, 5, 'E'), new Position(4, 5, 'E'));
                yield return new TestCaseData(new Rover(5, 5, 'W'), new Position(6, 5, 'W'));
            }
        }

        [Test]
        public void NewRoverInstanceShouldSetItsPosition()
        {
            var rover = new Rover(0, 0, 'N');
            var expectedPosition = new Position(0, 0, 'N');
            Assert.That(rover.Position, Is.EqualTo(expectedPosition));
        }

        [Test, TestCaseSource(nameof(OrientationCaseInsensitiveTestData))]
        public void OrientationIsCaseInsensitive(Rover rover, Position expectedPosition)
        {
            Assert.That(rover.Position, Is.EqualTo(expectedPosition));
        }

        [Test]
        public void ConstructingRoverWithUnrecognizedOrientationShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => new Rover(0, 0, 'x')).Message.Equals("Orientation 'x' is not valid.");
        }

        [Test, TestCaseSource(nameof(SingleCommandFTestData))]
        public void SingleCommandFShouldMoveRoverForwardByOneGridPoint(Rover rover, Position expectedPosition)
        {
            rover.Move("F");
            Assert.That(rover.Position, Is.EqualTo(expectedPosition));
        }

        [Test, TestCaseSource(nameof(SingleCommandBTestData))]
        public void SingleCommandBShouldMoveRoverBackwardByOneGridPoint(Rover rover, Position expectedPosition)
        {
            rover.Move("B");
            Assert.That(rover.Position, Is.EqualTo(expectedPosition));
        }

        [Test, TestCaseSource(nameof(CommandCaseInsensitiveTestData))]
        public void CommandIsCaseInsensitive(Rover rover, string commands, Position expectedPosition)
        {
            rover.Move(commands);
            Assert.That(rover.Position, Is.EqualTo(expectedPosition));
        }

        [Test]
        public void MovingRoverWithUnrecognizedCommandShouldThrowException()
        {
            var rover = new Rover(0, 0, 'N');
            Assert.Throws<Exception>(() => rover.Move("x")).Message.Equals("Command 'x' is not valid.");
        }
    }
}
