namespace PacMan
{
    public class Ghost : Entity
    {
        protected readonly Map _gameMap;
        protected readonly PacMan _pacman;
        public ConsoleColor OriginalColor { get; protected set; }
        private static readonly Random _random = new Random();
        private int _respawnTimer = 0;
private const int RESPAWN_TIME = 25;

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

            this.Sprite = new string[]
            {
                @"/    \",
                @"| o o|",
                @"vv  vv"
            };
        }

        private void UpdateAnimation()
        {

            if (this.State == EntityState.Eaten)
            {
                this.Sprite = new string[]
                {
                    @"      ",
                    @" o  o ",
                    @"      "
                };
                return;
            }

            if (this.State == EntityState.Vulnerable)
            {
                this.Sprite = new string[]
                {
                    @"/----\",
                    @"|o  o|",
                    @"vv  vv"
                };
                return;
            }

            if(CurrentDirection == Direction.Left)
            {
                this.Sprite = new string[]
                {
                    @"/    \",
                    @"|o o |",
                    @"vv  vv"
                };
            }
            else
            {
                this.Sprite = new string[]
                {
                    @"/    \",
                    @"| o o|",
                    @"vv  vv"
                };  
            }
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

                UpdateAnimation();
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

            if (this.State == EntityState.Eaten)
            {
                _respawnTimer--;
                
                if (_respawnTimer <= 0)
                {
                    ResetPosition(); 
                    SetNormal();     
                }
                return;
            }

            if (this.State == EntityState.Vulnerable)
            {
                MoveRandomly();
                return;
            }

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

        public void SetVulnerable()
        {
            this.State = EntityState.Vulnerable;
            this.Color = ConsoleColor.Blue;
        }

        public void SetEaten()
        {
            this.State = EntityState.Eaten;
            this._respawnTimer = RESPAWN_TIME;
            this.Color = ConsoleColor.White;
            UpdateAnimation(); 
        }

        public void SetNormal()
        {
            this.State = EntityState.Normal;
            this.Color = this.OriginalColor;
        }
    }
}