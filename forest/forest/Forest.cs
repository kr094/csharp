using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace forest {
    class Forest {
        static int WIDTH = 70;
        static int HEIGHT = 70;
        static int BOUND_WIDTH = WIDTH - 1;
        static int BOUND_HEIGHT = HEIGHT - 1;
        static int SEARCH_RADIUS = 1;
        static int TICK_DELAY_MS = 60;
        static Dictionary<String, double> CHANCE = new Dictionary<String, double>(){
            {"FIRE", 0.0005},
            {"GROWTH", 0.01},
            {"SPAWN", 0.1},
        };

        enum CELL { EMPTY, TREE, HEATING, BURNING };
        private CELL[,] forest = new CELL[WIDTH, HEIGHT];
        private Random rand = new Random();

        public Forest() {
            Console.CursorVisible = false;
            CELL cell;
            for (int y = 0; y < HEIGHT; y++) {
                for (int x = 0; x < WIDTH; x++) {
                    cell = CELL.EMPTY;
                    if (this.chance("SPAWN")) {
                        cell = CELL.TREE;
                    }
                    this.forest[y, x] = cell;
                }
            }
        }

        public void run() {
            while (true) {
                this.update();
                this.print();
            }
        }

        public void update() {
            CELL cell;
            for (int y = 0; y < HEIGHT; y++) {
                for (int x = 0; x < WIDTH; x++) {
                    cell = this.forest[y, x];
                    switch (cell) {
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
            foreach (Tuple<int, int> pair in getNeighbors(y, x, SEARCH_RADIUS)) {
                if (forest[pair.Item1, pair.Item2] == CELL.BURNING) {
                    return true;
                }
            }
            return false;
        }

        private Tuple<int, int>[] getNeighbors(int srcY, int srcX, int radius) {
            if (radius > WIDTH || radius > HEIGHT) {
                throw new Exception("Radius is too large");
            }
            var neighbors = new List<Tuple<int, int>>();

            for (int y = srcY - radius; y <= srcY + radius; y++) {
                for (int x = srcX - radius; x <= srcX + radius; x++) {
                    if (y < 0 || x < 0 || y > BOUND_HEIGHT || x > BOUND_WIDTH) {
                        continue;
                    }
                    neighbors.Add(new Tuple<int, int>(y, x));
                }
            }
            
            return neighbors.ToArray();
        }

        private bool chance(string keyName) {
            return this.rand.NextDouble() < CHANCE[keyName];
        }

        public void print() {
            var buffer = new StringBuilder();
            char tree = ' ';
            for (int y = 0; y < HEIGHT; y++) {
                for (int x = 0; x < WIDTH; x++) {
                    switch (this.forest[y, x]) {
                        case CELL.EMPTY:
                            tree = ' ';
                            break;
                        case CELL.TREE:
                        case CELL.HEATING:
                            tree = '^';
                            break;
                        case CELL.BURNING:
                            tree = '*';
                            break;
                    }
                    buffer.Append(tree);
                }
                buffer.Append(Environment.NewLine);
            }
            Thread.Sleep(TICK_DELAY_MS);
            Console.SetCursorPosition(0, 0);
            Console.Write(buffer.ToString());
        }

    }
}
