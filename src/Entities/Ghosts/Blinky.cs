namespace PacMan
{
    public class Blinky : Ghost
    {
        public Blinky(Map gameMap) : base(gameMap)
        {
            this.Color = ConsoleColor.Red;
        }
    }

}