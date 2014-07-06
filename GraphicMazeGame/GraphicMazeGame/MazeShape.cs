using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace GraphicMazeGame
{
    public abstract class MazeShape
    {
        private int x;
        private int y;
        private int width;
        private int height;
        private Rectangle bounds;

        public int X
        {
            get { return this.x; }
            set { this.x = value; }
        }

        public int Y
        {
            get { return this.y; }
            set { this.y = value; }
        }

        public int Width
        {
            get { return this.width; }
            set { this.width = value; }
        }

        public int Height
        {
            get { return this.height; }
            set { this.height = value; }
        }

        public Rectangle Bounds
        {
            get { return this.bounds; }
            set { this.bounds = value; }
        }

        public abstract void draw(Graphics g);
    }
}
