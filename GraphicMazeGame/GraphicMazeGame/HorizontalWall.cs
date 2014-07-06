using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicMazeGame
{
    class HorizontalWall : MazeShape
    {
        public HorizontalWall(int x, int y, int width, int height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
            this.Bounds = new Rectangle(this.X, this.Y, this.Width, this.Height);
        }

        public override void draw(Graphics g)
        {
            //Pen pen = new Pen(Color.White);
            //g.DrawLine(pen, new Point(this.X, this.Y), new Point(this.X + this.Width, this.Y));

            //SolidBrush brush = new SolidBrush(Color.Azure);
            ////Rectangle wallRect = new Rectangle(this.X, this.Y - 2, this.Width, this.Height - 4);
            //Rectangle wallRect = new Rectangle(this.X, this.Y, this.Width, this.Height);
            //g.FillRectangle(brush, wallRect);

            g.FillRectangle(new SolidBrush(Color.Gray), new Rectangle(this.X, this.Y, this.Width, this.Height));
        }
    }
}
