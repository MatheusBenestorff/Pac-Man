namespace PacMan
{
    public class PacMan : Entity
    {
        private Map _gameMap;

        private bool _isMouthOpen = true;

        public int Life { get; set; } = 7;
        public int Points { get; set; } = 0;

        public PacMan(Map gameMap)
        {
            this._gameMap = gameMap;

            this.SpawnPositionX = _gameMap.PlayerSpawnX;
            this.SpawnPositionY = _gameMap.PlayerSpawnY;

            this.CurrentPositionX = this.SpawnPositionX;
            this.CurrentPositionY = this.SpawnPositionY;
            this.Symbol = "C ";
            this.Color = ConsoleColor.Yellow;

            this.Sprite = new string[]
            {
                " CCCC ",
                "CCC   ",
                " CCCC "
            };
        }

        private void UpdateAnimation()
        {
            _isMouthOpen = !_isMouthOpen;

            if (!_isMouthOpen)
            {
                this.Sprite = new string[]
                {
                    " CCCC ",
                    "CCCCCC",
                    " CCCC "
                };
            }
            else
            {
                switch (CurrentDirection)
                {
                    case Direction.Right:
                        this.Sprite = new string[]
                        {
                            " CCCC ",
                            "CCC   ",
                            " CCCC "
                        };
                        break;
                    case Direction.Left:
                        this.Sprite = new string[]
                        {
                            " CCCC ",
                            "   CCC",
                            " CCCC "
                        };
                        break;
                    case Direction.Up:
                        this.Sprite = new string[]
                        {
                            "C    C",
                            "CCCCCC",
                            " CCCC "
                        };
                        break;
                    case Direction.Down:
                        this.Sprite = new string[]
                        {
                            " CCCC ",
                            "CCCCCC",
                            "C    C"
                        };
                        break;
                }
            }
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

                UpdateAnimation();
            }
            else
            {
                _isMouthOpen = false;
                UpdateAnimation();
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

        public void SetMap(Map newMap)
        {
            this._gameMap = newMap;
            this.SpawnPositionX = newMap.PlayerSpawnX;
            this.SpawnPositionY = newMap.PlayerSpawnY;
        }

    }
}