using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGenerator
{
    class Cell
    {
        private int x;
        private int y;
        private char wall;
        private bool inMaze;
        private Walls walls;

        public Cell()
        {
            this.x = 0;
            this.y = 0;
            this.wall = '|';
        }

        public Cell(int x, int y, char wall, bool inMaze)
        {
            this.x = x;
            this.y = y;
            this.wall = wall;
            this.inMaze = inMaze;
            this.walls = new Walls();
        }

        public int X
        {
            get
            {
                return this.x;
            }
        }

        public int Y
        {
            get
            {
                return this.y;
            }
        }

        public char Wall
        {
            get
            {
                return this.wall;
            }
        }

        public bool InMaze
        {
            get
            {
                return this.inMaze;
            }
            set
            {
                this.inMaze = value;
            }
        }

        public Walls Walls
        {
            get
            {
                return this.walls;
            }
        }
    }
}
