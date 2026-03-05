namespace PacMan
{
    public class Ghost : Entity
    {
        protected readonly Map _gameMap;
        protected readonly PacMan _pacman;

        private static readonly Random _random = new Random();

        public Ghost(Map gameMap, PacMan pacman)
        {
            this._gameMap = gameMap;
            this._pacman = pacman;
            
            this.SpawnPositionX = _gameMap.EnemySpawnX;
            this.SpawnPositionY = _gameMap.EnemySpawnY;
            
            this.CurrentPositionX = this.SpawnPositionX;
            this.CurrentPositionY = this.SpawnPositionY;
            
            this.Symbol = "M "; 
            this.State = EntityState.Normal;

            this.CurrentDirection = ChooseRandomDirection();
        }
        public override void Move()
        {
            MoveRandomly();
        }

        protected void MoveRandomly() 
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

        protected void MoveTowardsTarget(int targetX, int targetY)
        {
            this.PreviousPositionX = this.CurrentPositionX;
            this.PreviousPositionY = this.CurrentPositionY;

            int diffX = targetX - this.CurrentPositionX;
            int diffY = targetY - this.CurrentPositionY;

            // Pursuit Logic
            if (Math.Abs(diffX) > Math.Abs(diffY))
            {
                if (diffX > 0 && !_gameMap.IsWall(CurrentPositionY, CurrentPositionX + 1))
                {
                    this.CurrentPositionX++; return; 
                }
                else if (diffX < 0 && !_gameMap.IsWall(CurrentPositionY, CurrentPositionX - 1))
                {
                    this.CurrentPositionX--; return; 
                }
            }
            
            if (diffY > 0 && !_gameMap.IsWall(CurrentPositionY + 1, CurrentPositionX))
            {
                this.CurrentPositionY++; return; 
            }
            else if (diffY < 0 && !_gameMap.IsWall(CurrentPositionY - 1, CurrentPositionX))
            {
                this.CurrentPositionY--; return; 
            }

            MoveRandomly();
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