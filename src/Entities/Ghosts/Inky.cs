namespace PacMan
{
    public class Inky : Ghost
    {
        public Inky(Map gameMap, PacMan pacman) : base(gameMap, pacman)
        {
            this.Color = ConsoleColor.Cyan;
        }
    }

}