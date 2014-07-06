using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicMazeGame
{
    public partial class GraphicMazeForm : Form
    {
        private static string mazeFilePath = Path.GetFullPath("maze.txt");
        private Maze maze;
        private bool mazeLoadFailed = false;
        
        public GraphicMazeForm()
        {
            InitializeComponent();
            this.recreateMaze();
        }

        /// <summary>
        /// Calls the maze ctor and creates a new maze from file
        /// </summary>
        private void recreateMaze()
        {
            try
            {
                this.maze = new Maze(
                    this.DisplayRectangle.Width,
                    this.DisplayRectangle.Height,
                    this.DisplayRectangle,
                    mazeFilePath
                    );
            }
            catch (System.IO.FileNotFoundException)
            {
                this.mazeLoadFailed = true;
                MessageBox.Show("The maze file was not found." +
                    Environment.NewLine +
                    "Please ensure it is in the same folder as the binary that started the program.");
            }
        }

        /// <summary>
        /// Paint - method called after Invalidate() is called
        /// Checks if the game has ended then sends the
        /// graphics context into the appropriate maze method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GraphicMazeForm_Paint(object sender, PaintEventArgs e)
        {
            //Clean up if the maze load failed, don't try and load the maze, just exit
            if (!this.mazeLoadFailed)
            {
                if (this.maze.GameOver)
                {
                    this.maze.drawWin(e.Graphics);
                    this.recreateMaze();
                }
                else
                {
                    this.maze.draw(e.Graphics);
                }
            }
            else
            {
                this.Close();
            }
        }

        /// <summary>
        /// Resizes the maze dyanmically when the form does
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GraphicMazeForm_Resize(object sender, EventArgs e)
        {
            this.maze.setBounds(this.DisplayRectangle.Width, this.DisplayRectangle.Height, this.DisplayRectangle);
            this.maze.refreshMaze();
            Invalidate();
        }

        /// <summary>
        /// Main action listener to call the maze to move it's (internal) mouse
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GraphicMazeForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    this.maze.moveMouse(MouseMarker.LEFT);
                    break;
                case Keys.Up:
                    this.maze.moveMouse(MouseMarker.UP);
                    break;
                case Keys.Right:
                    this.maze.moveMouse(MouseMarker.RIGHT);
                    break;
                case Keys.Down:
                    this.maze.moveMouse(MouseMarker.DOWN);
                    break;
                case Keys.Q:
                    {
                        this.Close();
                    }break;
                case Keys.R:
                    {
                        this.recreateMaze();
                        Invalidate();                   
                    }break;
            }
            this.maze.refreshMaze();
            Invalidate();
        }
    }
}
