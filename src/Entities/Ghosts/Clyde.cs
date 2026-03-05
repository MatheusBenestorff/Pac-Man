namespace PacMan
{
    public class Clyde : Ghost
    {
        public Clyde(Map gameMap, PacMan pacman) : base(gameMap, pacman)
        {
            this.Color = ConsoleColor.DarkYellow;
        }
    }

}