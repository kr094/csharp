using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace forest
{
    class Forest
    {
        static int WIDTH = 70;
        static int HEIGHT = 70;
        static int BOUND_WIDTH = WIDTH - 1;
        static int BOUND_HEIGHT = HEIGHT - 1;
        static Dictionary<String, double> CHANCE = new Dictionary<String, double>(){
            {"FIRE", 0.001},
            {"GROWTH", 0.01},
        };
        enum CELL { EMPTY, TREE, HEATING, BURNING };
        private CELL[,] forest = new CELL[WIDTH, HEIGHT];
        private Random rand;

        public Forest()
        {
            Console.CursorVisible = false;
            this.rand = new Random();
            CELL cell;
            for (int y = 0; y < HEIGHT; y++)
            {
                for (int x = 0; x < WIDTH; x++)
                {
                    cell = CELL.EMPTY;
                    if (this.chance("GROWTH")) {
                        cell = CELL.TREE;
                    }
                    this.forest[y, x] = cell;
                }
            }
        }

        public void run()
        {
            while (true)
            {
                this.tick();
            }
        }

        public void tick()
        {
            this.update();
            this.print();
        }

        public void update()
        {
            CELL cell;
            for (int y = 0; y < HEIGHT; y++)
            {
                for (int x = 0; x < WIDTH; x++)
                {
                    cell = this.forest[y, x];
                    switch (cell)
                    {
                        case CELL.EMPTY:
                            if (this.chance("GROWTH")) {
                                this.forest[y, x] = CELL.TREE;
                            }
                            break;
                        case CELL.TREE:
                            if (this.isNeighborBurning(y, x)) {
                                this.forest[y, x] = CELL.HEATING;
                            }

                            if (this.chance("FIRE")) {
                                this.forest[y, x] = CELL.BURNING;
                            }
                            break;
                        case CELL.HEATING:
                            if (this.isNeighborBurning(y, x)) {
                                this.forest[y, x] = CELL.BURNING;
                            }
                            break;
                        case CELL.BURNING:
                            this.forest[y, x] = CELL.EMPTY;
                            break;
                    }
                }
            }
        }

        private bool isNeighborBurning(int y, int x) {
            if (y < BOUND_HEIGHT && this.forest[y+1, x] == CELL.BURNING) {
                return true;
            } else if (y > 0 && this.forest[y-1, x] == CELL.BURNING) {
                return true;
            } else if (x < BOUND_WIDTH && this.forest[y, x+1] == CELL.BURNING) {
                return true;
            } else if (x > 0 && this.forest[y, x-1] == CELL.BURNING) {
                return true;
            } else if (y > 0 && x > 0 && this.forest[y-1, x-1] == CELL.BURNING) {
                return true;
            } else if (y > 0 && x < BOUND_WIDTH && this.forest[y-1, x+1] == CELL.BURNING) {
                return true;
            } else if (y < BOUND_HEIGHT && x > 0 && this.forest[y+1, x-1] == CELL.BURNING) {
                return true;
            } else if (y < BOUND_HEIGHT && x < BOUND_WIDTH && this.forest[y+1, x+1] == CELL.BURNING) {
                return true;
            } else {
                return false;
            }
        }

        private bool chance(string keyName) {
            return this.rand.NextDouble() < CHANCE[keyName];
        }

        public void print()
        {
            var buffer = new StringBuilder();
            char tree = ' ';
            for (int y = 0; y < HEIGHT; y++)
            {
                for (int x = 0; x < WIDTH; x++)
                {
                    switch (this.forest[y, x])
                    {
                        case CELL.EMPTY:
                            tree = ' ';
                            break;
                        case CELL.TREE:
                        case CELL.HEATING:
                            tree = '.';
                            break;
                        case CELL.BURNING:
                            tree = '*';
                            break;
                    }
                    buffer.Append(tree);
                }
                buffer.Append(Environment.NewLine);
            }
            Thread.Sleep(60);
            Console.SetCursorPosition(0, 0);
            Console.Write(buffer.ToString());
        }

    }
}
