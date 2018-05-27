namespace PlutoRover
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class InputParserTest
    {
        [Test]
        public void ParseCommand()
        {
            Assert.That(InputParser.ParseCommand('F'), Is.EqualTo(Command.Forward));
            Assert.That(InputParser.ParseCommand('f'), Is.EqualTo(Command.Forward));
            Assert.That(InputParser.ParseCommand('B'), Is.EqualTo(Command.Backward));
            Assert.That(InputParser.ParseCommand('b'), Is.EqualTo(Command.Backward));
            Assert.That(InputParser.ParseCommand('L'), Is.EqualTo(Command.Left));
            Assert.That(InputParser.ParseCommand('l'), Is.EqualTo(Command.Left));
            Assert.That(InputParser.ParseCommand('R'), Is.EqualTo(Command.Right));
            Assert.That(InputParser.ParseCommand('r'), Is.EqualTo(Command.Right));
        }

        [Test]
        public void InvalidCommand()
        {
            Assert.Throws<Exception>(() => InputParser.ParseCommand('x')).Message.Equals("Command 'x' is not valid.");
        }
    }
}
