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
        }

        [Test]
        public void InvalidCommand()
        {
            Assert.Throws<Exception>(() => InputParser.ParseCommand('x')).Message.Equals("Command 'x' is not valid.");
        }
    }
}
