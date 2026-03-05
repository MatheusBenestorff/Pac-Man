namespace PacMan
{
    public class ConsoleRenderer
    {
        private readonly Map _map;

        private const int SCALE_X = 6;
        private const int SCALE_Y = 3;

        public ConsoleRenderer(Map map)
        {
            _map = map;
        }

        public void DrawMap()
        {
            Console.SetCursorPosition(0, 0);

            for (int y = 0; y < _map.Height; y++)
            {
                for (int row = 0; row < SCALE_Y; row++)
                {
                    for (int x = 0; x < _map.Width; x++)
                    {
                        string tile = _map.GetTileAt(y, x);

                        if (tile == "##")
                        {
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.Write("######");
                        }
                        else if (tile == ". ")
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            if (row == 1) Console.Write("  .   ");
                            else Console.Write("      ");
                        }
                        else if (tile == "O ")
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            if (row == 1) Console.Write("  ()  ");
                            else Console.Write("      ");
                        }
                        else
                        {
                            Console.Write("      ");
                        }
                    }
                    Console.WriteLine();
                }
            }
            Console.ResetColor();
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
            int oldScreenX = entity.PreviousPositionX * SCALE_X;
            int oldScreenY = entity.PreviousPositionY * SCALE_Y;

            Console.ResetColor();
            string backgroundTile = _map.GetTileAt(entity.PreviousPositionY, entity.PreviousPositionX);

            for (int i = 0; i < SCALE_Y; i++)
            {
                Console.SetCursorPosition(oldScreenX, oldScreenY + i);

                if (backgroundTile == ". ")
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    if (i == 1) Console.Write("  .   ");
                    else Console.Write("      ");
                }
                else if (backgroundTile == "O ")
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    if (i == 1) Console.Write("  ()  ");
                    else Console.Write("      ");
                }
                else
                {
                    Console.Write("      ");
                }
            }

            int newScreenX = entity.CurrentPositionX * SCALE_X;
            int newScreenY = entity.CurrentPositionY * SCALE_Y;

            Console.ForegroundColor = entity.Color;

            for (int i = 0; i < entity.Sprite.Length; i++)
            {
                Console.SetCursorPosition(newScreenX, newScreenY + i);
                Console.Write(entity.Sprite[i]);
            }

            Console.ResetColor();
        }

        public void DrawHUD(PacMan pacman, int levelNumber)
        {
            Console.SetCursorPosition(0, (_map.Height * SCALE_Y) + 1);

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