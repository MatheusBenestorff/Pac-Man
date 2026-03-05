namespace PacMan
{
    public class Pinky : Ghost
    {
        public Pinky(Map gameMap, PacMan pacman) : base(gameMap, pacman)
        {
            this.Color = ConsoleColor.Magenta;
        }
    }

}