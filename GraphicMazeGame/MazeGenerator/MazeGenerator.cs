using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGenerator
{
    public class MazeGenerator
    {
        private Cell[,] maze;
        private List<Cell> wallList;

        private int width;
        private int height;
        private int startX;
        private int startY;
        private int endX;
        private int endY;

        public MazeGenerator(int width, int height, int startX, int startY)
        {
            this.width = width;
            this.height = height;
            this.startX = startX;
            this.startY = startY;
            this.endX = 0;
            this.endY = 0;

            this.maze = new Cell[this.height, this.width];

            for(int i = 0; i < this.height; i++)
                for(int j = 0; j < this.width; j++)
                    this.maze[i, j] = new Cell(j, i, '|', false);

            this.wallList = new List<Cell>();
        }

        public string[] createMaze()
        {
            Random random = new Random();
            Cell currentCell = this.maze[this.startY, this.startX];
            this.addCellWallsToList(currentCell);

            while(wallList.Count > 0 && wallList.Count < 16000)
            {
                currentCell.InMaze = true;
                int randIndex = random.Next(wallList.Count);
                Cell randCell = this.wallList[randIndex];

                MazeWall wall = MazeWall.NONE;

                if (randCell.X > currentCell.X)
                    wall = MazeWall.RIGHT;
                else if (randCell.X < currentCell.X)
                    wall = MazeWall.LEFT;

                if (wall == MazeWall.NONE)
                {
                    if (randCell.Y > currentCell.Y)
                        wall = MazeWall.BOTTOM;
                    else if (randCell.Y < currentCell.Y)
                        wall = MazeWall.TOP;
                }

                Cell newCell = this.getOppositeCell(randCell, wall);
                                                
                if(newCell != null)
                {
                    if (!newCell.InMaze)
                    {
                        currentCell.Walls.knockdownWall(wall);
                        randCell.Walls.knockdownWall(getOppositeWall(wall));

                        this.maze[currentCell.Y, currentCell.X] = currentCell;
                        this.maze[randCell.Y, randCell.X] = randCell;

                        this.addCellWallsToList(randCell);
                        //newCell = new Cell(newCell.X, newCell.Y, '|', true);
                        //this.maze[newCell.Y, newCell.X] = newCell;
                    }
                    else
                    {
                        this.wallList.RemoveAt(randIndex);
                    }
                }
                else
                {
                    this.wallList.RemoveAt(randIndex);
                }
            }

            string[] mazeString = new string[this.height];

            for(int i = 0; i < this.height; i++)
            {
                string line = String.Empty;

                for(int j = 0; j < this.width; j++)
                {
                    Cell temp = this.maze[i, j];
                    if (temp.Walls.isVoid())
                    {
                        line += ' ';
                    }
                    else
                    {
                        line += '|';
                    }
                }
                mazeString[i] = line;
            }

            StringBuilder sb = new StringBuilder(mazeString[this.startY]);
            sb[this.startX] = '>';
            mazeString[this.startY] = sb.ToString();

            return mazeString;
        }

        private MazeWall getOppositeWall(MazeWall mw)
        {
            MazeWall temp = 0;
            switch (mw)
            {
                case MazeWall.RIGHT:
                    temp = MazeWall.LEFT;
                    break;
                case MazeWall.LEFT:
                    temp = MazeWall.RIGHT;
                    break;
                case MazeWall.TOP:
                    temp = MazeWall.BOTTOM;
                    break;
                case MazeWall.BOTTOM:
                    temp = MazeWall.TOP;
                    break;
            }
            return temp;
        }

        private Cell getOppositeCell(Cell c, MazeWall wall)
        {
            bool xMin = false;
            bool xMax = false;
            bool yMin = false;
            bool yMax = false;

            if (c.X == 0)
                xMin = true;
            if (c.X == this.width - 1)
                xMax = true;
            if (c.Y == 0)
                yMin = true;
            if (c.Y == this.height - 1)
                yMax = true;

            Cell temp = null;

            switch (wall)
            {
                case MazeWall.RIGHT:
                    if (!xMax)
                        temp = this.maze[c.Y, c.X + 1];
                    break;
                case MazeWall.LEFT:
                    if (!xMin)
                        temp = this.maze[c.Y, c.X - 1];
                    break;
                case MazeWall.BOTTOM:
                    if (!yMax)
                        temp = this.maze[c.Y + 1, c.X];
                    break;
                case MazeWall.TOP:
                    if (!yMin)
                        temp = this.maze[c.Y - 1, c.X];
                    break;
            }

            if (temp != null)
                return temp;

            return null;
        }

        private void addCellWallsToList(Cell c)
        {
            bool xMin = false;
            bool xMax = false;
            bool yMin = false;
            bool yMax = false;

            if (c.X == 0)
                xMin = true;
            if (c.X == this.width - 1)
                xMax = true;
            if (c.Y == 0)
                yMin = true;
            if (c.Y == this.height - 1)
                yMax = true;

            Cell temp = null;

            if(!xMin)
            {
                temp = this.maze[c.Y, c.X - 1];
                    this.wallList.Add(temp);
            }

            if(!xMax)
            {
                temp = this.maze[c.Y, c.X + 1];
                        this.wallList.Add(temp);
            }

            if(!yMin)
            {
                temp = this.maze[c.Y - 1, c.X];
                        this.wallList.Add(temp);
            }

            if(!yMax)
            {
                temp = this.maze[c.Y + 1, c.X];
                        this.wallList.Add(temp);
            }

            if (temp == null)
            {
                temp = this.maze[c.Y, c.X];
                this.wallList.Add(temp);
            }
        }

    }
}
