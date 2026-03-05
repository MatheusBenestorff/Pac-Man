using System;

namespace PacMan
{
    public class Pinky : Ghost
    {
        public Pinky(Map gameMap, PacMan pacman) : base(gameMap, pacman)
        {
            this.Color = ConsoleColor.Magenta;
            this.OriginalColor = this.Color;
        }

        public override void Move()
        {
            int targetX = _pacman.CurrentPositionX;
            int targetY = _pacman.CurrentPositionY;

            // Calculates the target 4 blocks ahead of Pac-Man
            switch (_pacman.CurrentDirection)
            {
                case Direction.Up:
                    targetY -= 4;
                    break;
                case Direction.Down:
                    targetY += 4;
                    break;
                case Direction.Left:
                    targetX -= 4;
                    break;
                case Direction.Right:
                    targetX += 4;
                    break;
            }

            MoveTowardsTarget(targetX, targetY);
        }
    }
}