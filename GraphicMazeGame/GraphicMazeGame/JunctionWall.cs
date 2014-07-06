using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicMazeGame
{
    class JunctionWall : WallShape
    {
        public JunctionWall(int x, int y, int width, int height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
            this.Bounds = new Rectangle(this.X, this.Y, this.Width, this.Height);
        }

        public override void draw(Graphics g)
        {
            Pen pen = new Pen(Color.White);
            g.DrawLine(pen, new Point(this.X, this.Y), new Point(this.X + this.Width, this.Y + this.Height));
        }
    }
}
