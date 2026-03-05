namespace PacMan
{
    public class Blinky : Ghost
    {
        public Blinky(Map gameMap, PacMan pacman) : base(gameMap, pacman)
        {
            this.Color = ConsoleColor.Red;
            this.OriginalColor = this.Color;
        }

        public override void Move()
        {
            MoveTowardsTarget(_pacman.CurrentPositionX, _pacman.CurrentPositionY);
        }
    }

}