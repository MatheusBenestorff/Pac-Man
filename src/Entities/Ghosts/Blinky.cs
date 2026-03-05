namespace PacMan
{
    public class Blinky : Ghost
    {
        public Blinky(Map gameMap, PacMan pacman) : base(gameMap, pacman)
        {
            this.Color = ConsoleColor.Red;
        }

        public override void Move()
        {
            this.PreviousPositionX = this.CurrentPositionX;
            this.PreviousPositionY = this.CurrentPositionY;

            int targetX = _pacman.CurrentPositionX;
            int targetY = _pacman.CurrentPositionY;

            int diffX = targetX - this.CurrentPositionX;
            int diffY = targetY - this.CurrentPositionY;

            // Persecution logic
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

            base.Move(); 
        }
    }

}