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
    class Mouse : MazeShape
    {
        private Image mouseImage;
        private MouseMarker direction;
        private char marker;

        private int graphicX;
        private int graphicY;

        private int newX;
        private int newY;

        public int getNewX()
        {
            return this.newX;
        }

        public int getNewY()
        {
            return this.newY;
        }

        public Mouse(int graphicX, int graphicY, int x, int y, int width, int height, char marker)
        {
            this.graphicX = graphicX;
            this.graphicY = graphicY;

            this.X = x;
            this.Y = y;
            this.newX = this.X;
            this.newY = this.Y;
            this.Width = width;
            this.Height = height;
            this.Bounds = new Rectangle(this.graphicX, this.graphicY, this.Width, this.Height);
            this.marker = marker;
            this.direction = (MouseMarker)marker;
            this.mouseImage = GraphicMazeGame.Properties.Resources.mouseRight;
        }

        public void move(MouseMarker newDirection)
        {
            this.direction = newDirection;
            this.marker = (char) this.direction;

            switch (this.direction)
            {
                case MouseMarker.LEFT:
                    this.newX--;
                    break;
                case MouseMarker.UP:
                    this.newY--;
                    break;
                case MouseMarker.RIGHT:
                    this.newX++;
                    break;
                case MouseMarker.DOWN:
                    this.newY++;
                    break;
            }
        }

        public void confirmMove()
        {
            this.X = this.newX;
            this.Y = this.newY;
        }

        public void failedMove()
        {
            this.newX = this.X;
            this.newY = this.Y;
        }

        public char Marker
        {
            get
            {
                return this.marker;
            }
        }

        public override void draw(System.Drawing.Graphics g)
        {
            switch (this.direction)
            {
                case MouseMarker.LEFT:
                    this.mouseImage = GraphicMazeGame.Properties.Resources.mouseLeft;
                    break;
                case MouseMarker.UP:
                    this.mouseImage = GraphicMazeGame.Properties.Resources.mouseUp;
                    break;
                case MouseMarker.RIGHT:
                    this.mouseImage = GraphicMazeGame.Properties.Resources.mouseRight;
                    break;
                case MouseMarker.DOWN:
                    this.mouseImage = GraphicMazeGame.Properties.Resources.mouseDown;
                    break;
            }

            //g.FillRectangle(new SolidBrush(color), new Rectangle(this.graphicX, this.graphicY, this.Width, this.Height));
            g.DrawImage(this.mouseImage, new Rectangle(this.graphicX, this.graphicY, this.Width, this.Height));
        }
    }
}
