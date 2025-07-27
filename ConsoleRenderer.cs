namespace PacMan
{
    public class ConsoleRenderer
    {
        private readonly Map _map;

        public ConsoleRenderer(Map map)
        {
            _map = map;
        }

        public void DrawMap()
        {
            Console.SetCursorPosition(0, 0);
            _map.Draw();
        }

        public void Draw(PacMan pacman)
        {
            char tileAtOldPosition = _map.GetTileAt(pacman.PreviousPositionY, pacman.PreviousPositionX);
            Console.SetCursorPosition(pacman.PreviousPositionX, pacman.PreviousPositionY);
            Console.Write(tileAtOldPosition);

            Console.SetCursorPosition(pacman.CurrentPositionX, pacman.CurrentPositionY);
            Console.Write(pacman.Symbol);
        }
    }
}