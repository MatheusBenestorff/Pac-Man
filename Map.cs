namespace PacMan
{
    public class Map
    {
        private const char WALL = '#';
        private const char POINT = '.';
        private const char EMPTY_SPACE = ' ';

        private readonly char[,] _grid;

        // --- Construtor ---
        public Map()
        {
            //Tamanho do mapa
            int height = 15;
            int width = 30;
            _grid = new char[height, width];

            //Preencher o grid
            for (int line = 0; line < height; line++)
            {
                for (int column = 0; column < width; column++)
                {
                    if (line == 0 || line == height - 1 || column == 0 || column == width - 1)
                    {
                        _grid[line, column] = WALL; 
                    }
                    else
                    {
                        _grid[line, column] = POINT; 
                    }
                }
            }
        }

        // --- Métodos ---
        
        //Desenhar o mapa na tela
        public void Draw()
        {
            for (int line = 0; line < _grid.GetLength(0); line++)
            {
                for (int column = 0; column < _grid.GetLength(1); column++)
                {
                    char element = _grid[line, column];
                    Console.Write(element);
                }
                Console.WriteLine();
            }
        }
    }
}