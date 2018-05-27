namespace PlutoRover
{
    using System;
    using System.Collections.Generic;

    public class Pluto
    {
        private readonly int width;
        private readonly int length;
        private readonly List<Tuple<int, int>> obstacles;

        public Pluto(int width, int length)
        {
            this.width = width;
            this.length = length;
            obstacles = new List<Tuple<int, int>>();
        }

        public int Width
        {
            get { return width; }
        }

        public int Length
        {
            get { return length; }
        }

        public List<Tuple<int, int>> Obstacles
        {
            get { return obstacles; }
        }

        public void AddObstacle(Tuple<int, int> obstacle)
        {
            if (obstacles.Contains(obstacle))
            {
                obstacles.Remove(obstacle);
            }

            obstacles.Add(obstacle);
        }
    }
}