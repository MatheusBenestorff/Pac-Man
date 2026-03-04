namespace PacMan
{
    public class Inky : Ghost
    {
        public Inky(Map gameMap) : base(gameMap)
        {
            this.Color = ConsoleColor.Cyan;
        }
    }

}