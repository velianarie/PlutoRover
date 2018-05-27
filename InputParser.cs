namespace PlutoRover
{
    using System;

    public static class InputParser
    {
        public static Command ParseCommand(char input)
        {
            switch (input)
            {
                case 'F':
                case 'f':
                    return Command.Forward;
                case 'B':
                case 'b':
                    return Command.Backward;
                default:
                    throw new Exception($"Command '{input}' is not valid.");
            }
        }
    }
}