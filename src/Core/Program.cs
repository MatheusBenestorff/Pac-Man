namespace PacMan
{
    class Program
    {
        static void Main()
        {
            Console.Clear();

            Map map = new Map("../Maps/level1.pacmap");

            Menu menu = new Menu();

            PacMan pacman = new PacMan(map);

            ConsoleRenderer renderer = new ConsoleRenderer(map);

            Console.CursorVisible = false;
            int selectedOption = menu.Show();

            //GAME LOAD

            if (selectedOption == 0)
            {

            }

            if (selectedOption == 1) // Continuar
            {

                GameSaveData data = SaveSystem.LoadGame();

                pacman.Points = data.CurrentScore;
                pacman.Life = data.Lives;

                // Restaurar posição
                if (!map.IsWall(data.PacManY, data.PacManX))
                {
                    pacman.CurrentPositionX = data.PacManX;
                    pacman.CurrentPositionY = data.PacManY;
                }
            }

            if (selectedOption == 2) return;

            renderer.DrawMap();
            bool isRunning = true;

            //GAME LOOP
            while (isRunning)
            {
                //PROCESSAR INPUT
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);

                    // Se apertar ESC, sai do jogo e salva
                    if (key.Key == ConsoleKey.Escape)
                    {
                        Console.Clear();
                        isRunning = false;
                    }
                    else
                    {
                        pacman.HandleInput(key.Key);
                    }
                }

                //ATUALIZAR ESTADO DO JOGO
                pacman.Move();

                //RENDERIZAR A TELA
                renderer.Draw(pacman);

                //FPS
                Thread.Sleep(100);
            }

            //GAME SAVE
            GameSaveData dataToSave = new GameSaveData
            {
                CurrentScore = pacman.Points,
                Lives = pacman.Life,
                PacManX = pacman.CurrentPositionX,
                PacManY = pacman.CurrentPositionY,
                SaveDate = DateTime.Now
            };

            SaveSystem.SaveGame(dataToSave);
        }
    }
}
