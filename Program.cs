namespace PacMan
{
    class Program
    {
        static void Main()
        {
            Console.Clear();

            Map map = new Map();

            Menu menu = new Menu();

            PacMan pacman = new PacMan(map);

            ConsoleRenderer renderer = new ConsoleRenderer(map);

            Console.CursorVisible = false;
            int selectedOption = menu.Show();

            if (selectedOption == 2) return;

            //GAME LOAD
            if (selectedOption == 1) // Continuar
            {
                if (SaveSystem.SaveFileExists())
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
                else
                {
                    // Se clicou em continuar mas não tem save, tratar como novo jogo

                }
            }

            renderer.DrawMap();
            bool isRunning = true;

            //GAME LOOP
            while (isRunning)
            {
                //PROCESSAR INPUT
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    pacman.HandleInput(key.Key);
                }

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);

                    // Se apertar ESC, sai do jogo e salva
                    if (key.Key == ConsoleKey.Escape)
                    {
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
