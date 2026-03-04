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

        public void DrawGame(PacMan pacman, List<Ghost> ghosts, int levelNumber)
        {
            DrawEntity(pacman);

            foreach (Ghost ghost in ghosts)
            {
                DrawEntity(ghost);
            }

            DrawHUD(pacman, levelNumber);
        }

        public void DrawEntity(Entity entity) 
        {
            string tileAtOldPosition = _map.GetTileAt(entity.PreviousPositionY, entity.PreviousPositionX);

            Console.SetCursorPosition(entity.PreviousPositionX * 2, entity.PreviousPositionY);
            Console.ResetColor(); 
            Console.Write(tileAtOldPosition);
            
            Console.SetCursorPosition(entity.CurrentPositionX * 2, entity.CurrentPositionY);
            Console.ForegroundColor = entity.Color; 
            Console.Write(entity.Symbol);
            Console.ResetColor();
        }

        public void DrawHUD(PacMan pacman, int levelNumber)
        {
            Console.SetCursorPosition(0, _map.Height + 1);
            
            Console.ForegroundColor = ConsoleColor.White;
            
            Console.Write($"Level: {levelNumber}  |  Lives: {pacman.Life}  |  Points: {pacman.Points}      ");
            
            Console.ResetColor();
        }

        public void ShowWinScreen()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("===========================");
            Console.WriteLine("          YOU WON!         ");
            Console.WriteLine("===========================");
            Console.ResetColor();
            Thread.Sleep(2000); 
        }
    }
}