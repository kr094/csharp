using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGenerator
{
    class Walls
    {
        private bool leftWall;
        private bool rightWall;
        private bool topWall;
        private bool bottomWall;

        public Walls()
        {
            this.leftWall = true;
            this.rightWall = true;
            this.topWall = true;
            this.bottomWall = true;
        }

        public void knockdownWall(MazeWall wall)
        {
            switch (wall)
            {
                case MazeWall.LEFT:
                    this.leftWall = false;
                    break;
                case MazeWall.RIGHT:
                    this.rightWall = false;
                    break;
                case MazeWall.TOP:
                    this.topWall = false;
                    break;
                case MazeWall.BOTTOM:
                    this.bottomWall = false;
                    break;
            }
        }

        public bool isVoid()
        {
            return !(
                   this.rightWall
                || this.leftWall
                || this.topWall
                || this.bottomWall
                );
        }
    }
}
