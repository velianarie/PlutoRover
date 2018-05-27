namespace PlutoRover
{
    using System;
    using System.Collections.Generic;

    public class Rover
    {
        private int x;
        private int y;
        private Orientation orientation;
        private Pluto pluto;
        private Tuple<int, int> detectedObstacle;

        public Rover(int x, int y, Orientation orientation)
        {
            this.x = x;
            this.y = y;
            this.orientation = orientation;
        }

        public Position Position
        {
            get { return new Position(x, y, orientation); }
        }

        public Tuple<int, int> DetectedObstacle
        {
            get { return detectedObstacle; }
        }

        public void Move(string commands)
        {
            foreach (var commandChar in commands)
            {
                var command = InputParser.ParseCommand(commandChar);
                Move(command, pluto);
            }
        }

        private void Move(Command command, Pluto pluto)
        {
            int candidateX = x;
            int candidateY = y;
            switch (command)
            {
                case Command.Forward:
                    if (orientation == Orientation.North) candidateY = candidateY + 1;
                    else if (orientation == Orientation.South) candidateY = candidateY - 1;
                    else if (orientation == Orientation.East) candidateX = candidateX + 1;
                    else candidateX = candidateX - 1;
                    break;
                case Command.Backward:
                    if (orientation == Orientation.North) candidateY = candidateY - 1;
                    else if (orientation == Orientation.South) candidateY = candidateY + 1;
                    else if (orientation == Orientation.East) candidateX = candidateX - 1;
                    else candidateX = candidateX + 1;
                    break;
                case Command.Left:
                    if (orientation == Orientation.North) orientation = Orientation.West;
                    else if (orientation == Orientation.South) orientation = Orientation.East;
                    else if (orientation == Orientation.East) orientation = Orientation.North;
                    else orientation = Orientation.South;
                    break;
                case Command.Right:
                    if (orientation == Orientation.North) orientation = Orientation.East;
                    else if (orientation == Orientation.South) orientation = Orientation.West;
                    else if (orientation == Orientation.East) orientation = Orientation.South;
                    else orientation = Orientation.North;
                    break;
                default:
                    throw new Exception($"Command '{command}' is not valid.");
            }

            bool isBlocked = false;
            if (pluto != null)
            {
                List<Tuple<int, int>> obstacles = pluto.Obstacles;
                for (int i = 0; i < obstacles.Count && !isBlocked; i++)
                {
                    Tuple<int, int> obstacle = obstacles[i];
                    int obstacleX = obstacle.Item1;
                    int obstacleY = obstacle.Item2;
                    if (obstacle.Item1 == candidateX && obstacle.Item2 == candidateY)
                    {
                        detectedObstacle = new Tuple<int, int>(obstacleX, obstacleY);
                        isBlocked = true;
                    }
                }

                int maxX = pluto.Width;
                int maxY = pluto.Length;
                if (candidateX > maxX) candidateX = candidateX - maxX - 1;
                if (candidateX < 0) candidateX = maxX + candidateX + 1;

                if (candidateY > maxY) candidateY = candidateY - maxY - 1;
                if (candidateY < 0) candidateY = maxY + candidateY + 1;
            }

            if (!isBlocked)
            {
                x = candidateX;
                y = candidateY;
            }
        }

        public void DeployTo(Pluto pluto)
        {
            this.pluto = pluto;
        }
    }
}