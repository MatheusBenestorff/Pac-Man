namespace PacMan
{
    public class Map
    {
        private const string WALL = "#";
        private const string POINT = ".";
        private const string EMPTY_SPACE = " ";
        public readonly int Height;
        public readonly int Width;

        private readonly string[,] _grid;

        public int PlayerSpawnX { get; private set; }
        public int PlayerSpawnY { get; private set; }

        public int EnemySpawnX { get; private set; }
        public int EnemySpawnY { get; private set; }

        public Map(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);

            // Define as dimensões dinamicamente baseadas no arquivo
            this.Height = lines.Length;
            this.Width = lines[0].Length; 
            
            _grid = new string[this.Height, this.Width];

            for (int line = 0; line < this.Height; line++)
            {
                for (int column = 0; column < this.Width; column++)
                {
                    char fileChar = lines[line][column];

                    switch (fileChar)
                    {
                        case 'H':
                            _grid[line, column] = WALL;
                            break;
                        case '.':
                            _grid[line, column] = POINT;
                            break;
                        case ' ':
                            _grid[line, column] = EMPTY_SPACE;
                            break;
                        
                        case '<':
                            this.PlayerSpawnX = column;
                            this.PlayerSpawnY = line;
                            _grid[line, column] = EMPTY_SPACE;
                            break;
                        
                        case 'o':
                            this.EnemySpawnX = column;
                            this.EnemySpawnY = line;
                            _grid[line, column] = EMPTY_SPACE;
                            break;
                        
                        default:
                            _grid[line, column] = EMPTY_SPACE; 
                            break;
                    }
                }
            }
        }

        //Retorna o caractere do mapa em uma coordenada específica.
        public string GetTileAt(int line, int column)
        {
            //Verificar se a coordenada estiver fora do mapa
            if (line < 0 || line >= this.Height || column < 0 || column >= this.Width)
            {
                return "  ";
            }

            //Retornar qual caracter está na coordenada solicitada
            return _grid[line, column];
        }

        public bool IsWall(int line, int column)
        {
            //Verificar se a coordenada estiver fora do mapa
            if (line < 0 || line >= Height || column < 0 || column >= Width)
            {
                return true;
            }
            return _grid[line, column] == WALL;
        }

        public void Draw()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            for (int line = 0; line < _grid.GetLength(0); line++)
            {
                for (int column = 0; column < _grid.GetLength(1); column++)
                {
                    string element = _grid[line, column];
                    Console.Write(element);
                }
                Console.WriteLine();
            }
        }
    }
}