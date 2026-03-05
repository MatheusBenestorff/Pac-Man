namespace PacMan
{
    public class Inky : Ghost
    {
        private readonly Blinky _blinky;

        public Inky(Map gameMap, PacMan pacman, Blinky blinky) : base(gameMap, pacman)
        {
            this.Color = ConsoleColor.Cyan;
            this.OriginalColor = this.Color;

            this._blinky = blinky;
        }

        public override void Move()
        {
            int pivotX = _pacman.CurrentPositionX;
            int pivotY = _pacman.CurrentPositionY;

            switch (_pacman.CurrentDirection)
            {
                case Direction.Up: pivotY -= 2; break;
                case Direction.Down: pivotY += 2; break;
                case Direction.Left: pivotX -= 2; break;
                case Direction.Right: pivotX += 2; break;
            }

            // Calculate the vector from Blinky to that Pivot
            int diffX = pivotX - _blinky.CurrentPositionX;
            int diffY = pivotY - _blinky.CurrentPositionY;

            // Double the distance from the Pivot 
            int targetX = pivotX + diffX;
            int targetY = pivotY + diffY;

            MoveTowardsTarget(targetX, targetY);
        }
    }

}