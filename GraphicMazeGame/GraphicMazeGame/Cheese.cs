using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicMazeGame
{
    class Cheese : MazeShape
    {
        private Image cheeseImage;

        public Cheese(int x, int y, int width, int height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
            this.Bounds = new Rectangle(this.X, this.Y, this.Width, this.Height);
            this.cheeseImage = GraphicMazeGame.Properties.Resources.cheese;
        }

        public override void draw(System.Drawing.Graphics g)
        {
            g.DrawImage(this.cheeseImage, new Rectangle(this.X, this.Y, this.Width, this.Height));
        }
    }
}

