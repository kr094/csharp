using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MazeGenerator;
using System.Threading;

namespace GraphicMazeGame
{
    class Maze
    {
        private const int MAZE_WIDTH = 10;
        private const int MAZE_HEIGHT = 10;
        private bool READ_MAZE_FILE = true;

        private bool gameOver = false;

        //graphical maze size
        private int formWidth;
        private int formHeight;
        private Rectangle formBounds;
        
        //graphical wall size
        private int wallWidth;
        private int wallHeight;

        //text maze dimensions
        private int mazeWidth;
        private int mazeHeight;

        //start/end locations in text array
        private int startX;
        private int startY;
        private int endX;
        private int endY;

        private string[] maze;
        private MazeShape[,] walls;

        private Mouse mouse;

        public bool GameOver
        {
            get
            {
                return this.gameOver;
            }
            set
            {
                this.gameOver = value;
            }
        }

        /// <summary>
        /// Public ctor for the maze
        /// </summary>
        /// <param name="width">Form.DisplayRectangle.Width</param>
        /// <param name="height">Form.DisplayRectangle.Height</param>
        /// <param name="bounds">Form.DisplayRectangle</param>
        /// <param name="mazeFilePath">@String for the maze file path</param>
        public Maze(int width, int height, Rectangle bounds, string mazeFilePath)
        {
            this.formWidth = width;
            this.formHeight = height;
            this.formBounds = bounds;

            //There should be a maze generator doing this :(
            if (this.maze == null)
            {
                if (READ_MAZE_FILE)
                {
                    if (File.Exists(mazeFilePath))
                    {
                        string[] tempMaze = System.IO.File.ReadAllLines(mazeFilePath);

                        this.mazeHeight = tempMaze.Length;
                        if (this.mazeHeight == 0)
                            throw new Exception("Maze is empty!");
                        this.mazeWidth = tempMaze[0].Length;

                        this.maze = tempMaze;
                    }
                    else throw new FileNotFoundException();
                }
                else
                {
                    //MazeGenerator.MazeGenerator mazeGen = new MazeGenerator.MazeGenerator(MAZE_WIDTH, MAZE_HEIGHT, 0, 0);
                    //this.maze = mazeGen.createMaze();
                    //this.mazeHeight = this.maze.Length;
                    //this.mazeWidth = this.maze[0].Length;
                }
            }

            //Create random placements in top left and bottom right for mouse/cheese respectively
            Random random = new Random();

            while (true)
            {
                this.startX = random.Next(0, this.mazeWidth / 2);
                this.startY = random.Next(0, this.mazeHeight / 2);
                if (this.maze[startY][startX] == ' ')
                    break;
            }

            //startX = 0;
            //startY = 1;

            //StringBuilder is used a lot as strings are immutable structures in C#
            StringBuilder sb = new StringBuilder(this.maze[startY]);
            sb[startX] = '>';
            this.maze[startY] = sb.ToString();

            while (true)
            {
                this.endX = random.Next(this.mazeWidth / 2, this.mazeWidth);
                this.endY = random.Next(this.mazeHeight / 2, this.mazeHeight);
                if (this.maze[endY][endX] == ' ')
                    break;
            }

            //endX = 3;
            //endY = 1;

            sb = new StringBuilder(this.maze[endY]);
            sb[endX] = 'E';
            this.maze[endY] = sb.ToString();

            //Call to build objects of the maze and place them in the walls[,] array
            this.refreshMaze();
        }

        public void setBounds(int width, int height, Rectangle bounds)
        {
            this.formWidth = width;
            this.formHeight = height;
            this.formBounds = bounds;
        }

        /// <summary>
        /// Draws win message on screen
        /// </summary>
        /// <param name="g"></param>
        public void drawWin(Graphics g)
        {
            Point rectPoint = new Point(this.formBounds.X + 10, this.formBounds.Y + 10);
            Size rectSize = new Size(450, 200);
            g.FillRectangle(new SolidBrush(Color.DarkGray), new Rectangle(rectPoint, rectSize));
            rectPoint = new Point(rectPoint.X + 30, rectPoint.Y + 30);
            g.DrawString("You've won, how flattering.", new Font("Arial", 24), new SolidBrush(Color.White), rectPoint);
        }

        /// <summary>
        /// Calls all the draw methods of the walls[,] array
        /// </summary>
        /// <param name="g"></param>
        public void draw(Graphics g)
        {
            for (int i = 0; i < this.mazeHeight; i++)
            {
                for (int j = 0; j < this.mazeWidth; j++)
                {
                    this.walls[i, j].draw(g);
                }
            }
        }

        /// <summary>
        /// Public method of maze to move the internal mouse
        /// An enum of directionality is used to seperate keyboard 
        /// </summary>
        /// <param name="newDirection"></param>
        public void moveMouse(MouseMarker newDirection)
        {
            //set newX and newY
            this.mouse.move(newDirection);

            if(this.mouse.getNewY() >= 0
            && this.mouse.getNewY() < this.mazeHeight
            && this.mouse.getNewX() >= 0
            && this.mouse.getNewX() < this.mazeWidth)
            {
                char mazeCell = this.maze[this.mouse.getNewY()][this.mouse.getNewX()];
                if (mazeCell == ' ')
                {
                    StringBuilder temp = new StringBuilder(this.maze[this.mouse.Y]);
                    temp[this.mouse.X] = ' ';
                    this.maze[this.mouse.Y] = temp.ToString();

                    //update X and Y internally
                    this.mouse.confirmMove();
                }
                else if (mazeCell == 'E')
                {
                    this.GameOver = true;
                }
                else
                {
                    //reset newX and newY
                    this.mouse.failedMove();
                }
            }
            else
            {
                this.mouse.failedMove();
            }

            this.refreshMouseDirection();
        }

        public void refreshMouseDirection()
        {
            //create a string builder for the mouse line in the array
            StringBuilder temp = new StringBuilder(this.maze[this.mouse.Y]);
            //replace the char at mouse position with its new direction char
            temp[this.mouse.X] = this.mouse.Marker;
            //replace the line in the array
            this.maze[this.mouse.Y] = temp.ToString();
        }

        public void refreshMaze()
        {
            this.walls = this.refreshMazeWalls(this.maze);
        }

        private MazeShape[,] refreshMazeWalls(string[] maze)
        {
            MazeShape[,] tempWalls = new MazeShape[this.mazeHeight, this.mazeWidth];

            string line;
            char c;

            this.wallWidth = this.formBounds.Width / this.mazeWidth;
            this.wallHeight = this.formBounds.Height / this.mazeHeight;

            //init X and Y locations for the grid
            //subtract width of graphic maze from width of form, then divide by two 
            int tempX = (this.formBounds.Width - (this.wallWidth * this.mazeWidth)) / 2;
            int tempY = (this.formBounds.Height - (this.wallHeight * this.mazeHeight)) / 2;

            //Read our private string[] and make a WallShape[,] array to draw
            for (int i = 0; i < this.mazeHeight; i++)
            {
                line = maze[i];
                for (int j = 0; j < this.mazeWidth; j++)
                {
                    c = line[j];

                    switch (c)
                    {
                        case '|':
                            tempWalls[i, j] = new VerticalWall(tempX, tempY, wallWidth, wallHeight);
                            break;
                        case '-':
                            tempWalls[i, j] = new HorizontalWall(tempX, tempY, wallWidth, wallHeight);
                            break;
                        case ' ':
                            tempWalls[i, j] = new VoidWall(tempX, tempY, wallWidth, wallHeight);
                            break;
                        case '>':
                        case '<':
                        case '^':
                        case 'd':
                            this.mouse = new Mouse(tempX, tempY, j, i, wallWidth, wallHeight, c);
                            tempWalls[i, j] = this.mouse;
                            break;
                        case 'E':
                            tempWalls[i, j] = new Cheese(tempX, tempY, wallWidth, wallHeight);
                            break;
                    }
                    //The next location is this location + width/height to allow for totally dynamic resizing, different mazes, etc
                    tempX += wallWidth;
                }
                tempX =  (this.formBounds.Width - (this.wallWidth * this.mazeWidth)) / 2;
                tempY += wallHeight;
            }

            return tempWalls;
        }

    }
}
