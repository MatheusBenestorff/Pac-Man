namespace PacMan
{
    public class PacMan : Entity
    {
        private readonly Map _gameMap;

        public enum Direction { Up, Down, Left, Right, None }

        public int Life { get; set; } = 3;
        public int Points { get; set; } = 0;
        public Direction CurrentDirection { get; set; } = Direction.None;

        //Construtor
        public PacMan(Map gameMap)
        {
            this._gameMap = gameMap;
            this.SpawnPositionX = _gameMap.Width/2;
            this.SpawnPositionY = _gameMap.Height - 2;
            this.CurrentPositionX = this.SpawnPositionX;
            this.CurrentPositionY = this.SpawnPositionY;
            this.Symbol = 'C';
        }

        //Comportamento

        public override void Move()
        {
            //Guardando a posição atual
            this.PreviousPositionX = this.CurrentPositionX;
            this.PreviousPositionY = this.CurrentPositionY;

            int nextY = CurrentPositionY;
            int nextX = CurrentPositionX;

            switch (CurrentDirection)
            {
                case Direction.Up:
                    nextY--;
                    break;
                case Direction.Down:
                    nextY++;
                    break;
                case Direction.Right:
                    nextX++;
                    break;
                case Direction.Left:
                    nextX--;
                    break;
            }

            if (!_gameMap.IsWall(nextY, nextX))
            {
                this.CurrentPositionY = nextY;
                this.CurrentPositionX = nextX;
            }
        }
        public void HandleInput(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    CurrentDirection = Direction.Up;
                    break;
                case ConsoleKey.DownArrow:
                    CurrentDirection = Direction.Down;
                    break;
                case ConsoleKey.RightArrow:
                    CurrentDirection = Direction.Right;
                    break;
                case ConsoleKey.LeftArrow:
                    CurrentDirection = Direction.Left;
                    break;
            }
        }

        //Interação

        public void CollectPoint(int value)
        {
            this.Points += value;
        }

        public void LoseLife()
        {
            this.Life--;
            this.CurrentPositionX = this.SpawnPositionX;
            this.CurrentPositionY = this.SpawnPositionY;
        }

    }
}