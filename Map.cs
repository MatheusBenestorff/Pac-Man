using System.Diagnostics.Contracts;

namespace PacMan
{
    public class Map
    {
        private const char WALL = '#';
        private const char POINT = '.';
        private const char EMPTY_SPACE = ' ';
        public readonly int Height;
        public readonly int Width;

        private readonly char[,] _grid;

        //Construtor
        public Map()
        {
            //Tamanho do mapa
            this.Height = 25;
            this.Width = 50;
            _grid = new char[this.Height, this.Width];

            //Preencher o grid
            for (int line = 0; line < this.Height; line++)
            {
                for (int column = 0; column < this.Width; column++)
                {
                    if (line == 0 || line == this.Height - 1 || column == 0 || column == this.Width - 1)
                    {
                        _grid[line, column] = WALL; 
                    }
                    else
                    {
                        _grid[line, column] = EMPTY_SPACE; 
                    }
                }
            }
        }

        //Métodos

        //Porteiro para avisar se a coordenada é uma parede
        public bool IsWall(int line, int column)
        {
            //Verificar se a coordenada estiver fora do mapa
            if (line < 0 || line >= Height || column < 0 || column >= Width)
            {
                return true;
            }
            return _grid[line, column] == WALL;
        }

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