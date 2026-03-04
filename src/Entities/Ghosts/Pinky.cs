namespace PacMan
{
    public class Pinky : Ghost
    {
        public Pinky(Map gameMap) : base(gameMap)
        {
            this.Color = ConsoleColor.Magenta;
        }
    }

}