namespace PlutoRover
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;

    [TestFixture]
    public class RoverTest
    {
        private static IEnumerable<TestCaseData> SingleCommandFTestData
        {
            get
            {
                yield return new TestCaseData(new Rover(5, 5, Orientation.North), new Position(5, 6, Orientation.North));
                yield return new TestCaseData(new Rover(5, 5, Orientation.South), new Position(5, 4, Orientation.South));
                yield return new TestCaseData(new Rover(5, 5, Orientation.East), new Position(6, 5, Orientation.East));
                yield return new TestCaseData(new Rover(5, 5, Orientation.West), new Position(4, 5, Orientation.West));
            }
        }

        private static IEnumerable<TestCaseData> SingleCommandBTestData
        {
            get
            {
                yield return new TestCaseData(new Rover(5, 5, Orientation.North), new Position(5, 4, Orientation.North));
                yield return new TestCaseData(new Rover(5, 5, Orientation.South), new Position(5, 6, Orientation.South));
                yield return new TestCaseData(new Rover(5, 5, Orientation.East), new Position(4, 5, Orientation.East));
                yield return new TestCaseData(new Rover(5, 5, Orientation.West), new Position(6, 5, Orientation.West));
            }
        }

        private static IEnumerable<TestCaseData> SingleCommandLTestData
        {
            get
            {
                yield return new TestCaseData(new Rover(5, 5, Orientation.North), new Position(5, 5, Orientation.West));
                yield return new TestCaseData(new Rover(5, 5, Orientation.South), new Position(5, 5, Orientation.East));
                yield return new TestCaseData(new Rover(5, 5, Orientation.East), new Position(5, 5, Orientation.North));
                yield return new TestCaseData(new Rover(5, 5, Orientation.West), new Position(5, 5, Orientation.South));
            }
        }

        private static IEnumerable<TestCaseData> WrappingTestData
        {
            get
            {
                var pluto = new Pluto(5, 5);
                yield return new TestCaseData(new Rover(5, 5, Orientation.North), pluto, "FFFFFFF", new Position(5, 0, Orientation.North));
                yield return new TestCaseData(new Rover(0, 0, Orientation.North), pluto, "BBBBBBB", new Position(0, 5, Orientation.North));

                yield return new TestCaseData(new Rover(5, 5, Orientation.South), pluto, "FFFFFFF", new Position(5, 4, Orientation.South));
                yield return new TestCaseData(new Rover(0, 0, Orientation.South), pluto, "BBBBBBB", new Position(0, 1, Orientation.South));

                yield return new TestCaseData(new Rover(5, 5, Orientation.East), pluto, "FFFFFFF", new Position(0, 5, Orientation.East));
                yield return new TestCaseData(new Rover(0, 0, Orientation.East), pluto, "BBBBBBB", new Position(5, 0, Orientation.East));

                yield return new TestCaseData(new Rover(5, 5, Orientation.West), pluto, "FFFFFFF", new Position(4, 5, Orientation.West));
                yield return new TestCaseData(new Rover(0, 0, Orientation.West), pluto, "BBBBBBB", new Position(1, 0, Orientation.West));
            }
        }

        private static IEnumerable<TestCaseData> SingleCommandRTestData
        {
            get
            {
                yield return new TestCaseData(new Rover(5, 5, Orientation.North), new Position(5, 5, Orientation.East));
                yield return new TestCaseData(new Rover(5, 5, Orientation.South), new Position(5, 5, Orientation.West));
                yield return new TestCaseData(new Rover(5, 5, Orientation.East), new Position(5, 5, Orientation.South));
                yield return new TestCaseData(new Rover(5, 5, Orientation.West), new Position(5, 5, Orientation.North));
            }
        }

        [Test]
        public void NewRoverInstanceShouldSetItsPosition()
        {
            var rover = new Rover(0, 0, Orientation.North);
            var expectedPosition = new Position(0, 0, Orientation.North);
            Assert.That(rover.Position, Is.EqualTo(expectedPosition));
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

        [Test]
        public void MovingRoverWithUnrecognizedCommandShouldThrowException()
        {
            var rover = new Rover(0, 0, Orientation.North);
            Assert.Throws<Exception>(() => rover.Move("x")).Message.Equals("Command 'x' is not valid.");
        }

        [Test, TestCaseSource(nameof(SingleCommandLTestData))]
        public void SingleCommandLShouldTurnRover90DegreesLeft(Rover rover, Position expectedPosition)
        {
            rover.Move("L");
            Assert.That(rover.Position, Is.EqualTo(expectedPosition));
        }

        [Test, TestCaseSource(nameof(SingleCommandRTestData))]
        public void SingleCommandRShouldTurnRover90DegreesRight(Rover rover, Position expectedPosition)
        {
            rover.Move("R");
            Assert.That(rover.Position, Is.EqualTo(expectedPosition));
        }

        [Test]
        public void ChainOfCommands()
        {
            var rover = new Rover(0, 0, Orientation.North);
            rover.Move("FFRFF");
            var expectedPosition = new Position(2, 2, Orientation.East);
            Assert.That(rover.Position, Is.EqualTo(expectedPosition));
        }

        [Test, TestCaseSource(nameof(WrappingTestData))]
        public void RoverInPlutoShouldBeAbleToMoveAroundThePlanetWithoutFallingOff(Rover rover, Pluto pluto, string commands, Position expectedPosition)
        {
            rover.DeployTo(pluto);
            rover.Move(commands);
            Assert.That(rover.Position, Is.EqualTo(expectedPosition));
        }

        [Test]
        public void RoverShouldDetectObstacleBeforeMoving()
        {
            var obstacle1 = new Tuple<int, int>(5, 3);
            var obstacle2 = new Tuple<int, int>(1, 1);

            var pluto = new Pluto(5, 5);
            pluto.AddObstacle(obstacle1);
            pluto.AddObstacle(obstacle2);

            var rover = new Rover(3, 3, Orientation.East);
            rover.DeployTo(pluto);
            rover.Move("FF");
            var expectedPosition = new Position(4, 3, Orientation.East);
            Assert.That(rover.Position, Is.EqualTo(expectedPosition));
            Assert.That(rover.DetectedObstacle, Is.EqualTo(obstacle1));

            rover = new Rover(5, 1, Orientation.East);
            rover.DeployTo(pluto);
            rover.Move("FF");
            expectedPosition = rover.Position;
            Assert.That(rover.Position, Is.EqualTo(expectedPosition));
            Assert.That(rover.DetectedObstacle, Is.EqualTo(obstacle2));
        }
    }
}
