namespace PacMan
{
    public class Clyde : Ghost
    {
        public Clyde(Map gameMap, PacMan pacman) : base(gameMap, pacman)
        {
            this.Color = ConsoleColor.DarkYellow;
            this.OriginalColor = this.Color;
        }

        public override void Move()
        {
            double distanceToPacman = Math.Sqrt(
                Math.Pow(_pacman.CurrentPositionX - this.CurrentPositionX, 2) +
                Math.Pow(_pacman.CurrentPositionY - this.CurrentPositionY, 2)
            );

            if (distanceToPacman > 8)
            {
                // It's far away: Pretend is Blinky and attack
                MoveTowardsTarget(_pacman.CurrentPositionX, _pacman.CurrentPositionY);
            }
            else
            {
                // It's close: Run to his safe corner
                int safeCornerX = 0;
                int safeCornerY = _gameMap.Height - 1;

                MoveTowardsTarget(safeCornerX, safeCornerY);
            }
        }
    }
}