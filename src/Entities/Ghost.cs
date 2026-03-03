namespace PacMan
{
    public class Ghost : Entity
    {
        private readonly Map _gameMap;

        private static readonly Random _random = new Random();

        public Ghost(Map gameMap)
        {
            this._gameMap = gameMap;
            
            this.SpawnPositionX = _gameMap.EnemySpawnX;
            this.SpawnPositionY = _gameMap.EnemySpawnY;
            
            this.CurrentPositionX = this.SpawnPositionX;
            this.CurrentPositionY = this.SpawnPositionY;
            
            this.Symbol = "M"; 
            this.Color = ConsoleColor.Red;
            this.State = EntityState.Normal;

            this.CurrentDirection = ChooseRandomDirection();
        }

        public override void Move()
        {
            this.PreviousPositionX = this.CurrentPositionX;
            this.PreviousPositionY = this.CurrentPositionY;

            int nextX = CurrentPositionX;
            int nextY = CurrentPositionY;

            switch (CurrentDirection)
            {
                case Direction.Up: nextY--; break;
                case Direction.Down: nextY++; break;
                case Direction.Left: nextX--; break;
                case Direction.Right: nextX++; break;
            }

            if (!_gameMap.IsWall(nextY, nextX))
            {
                this.CurrentPositionX = nextX;
                this.CurrentPositionY = nextY;
            }
            else
            {
                this.CurrentDirection = ChooseRandomDirection();
            }
        }

        private Direction ChooseRandomDirection()
        {
            return (Direction)_random.Next(0, 4);
        }

        public void ResetPosition()
        {
            this.CurrentPositionX = this.SpawnPositionX;
            this.CurrentPositionY = this.SpawnPositionY;
        }
    }
}