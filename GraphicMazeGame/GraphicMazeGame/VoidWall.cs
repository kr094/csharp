using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicMazeGame
{
    class VoidWall : MazeShape
    {
        public VoidWall(int x, int y, int width, int height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
            this.Bounds = new Rectangle(this.X, this.Y, this.Width, this.Height);
        }

        public override void draw(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color.LightGray), new Rectangle(this.X, this.Y, this.Width, this.Height));
        }
    }
}
