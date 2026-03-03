namespace PacMan
{
    class Program
    {
        static void Main()
        {
            Console.Clear();

            Map map = new Map("maps/level1.pacmap");

            Menu menu = new Menu();

            PacMan pacman = new PacMan(map);

            Ghost ghost1 = new Ghost(map);

            ConsoleRenderer renderer = new ConsoleRenderer(map);

            Console.CursorVisible = false;
            int selectedOption = menu.Show();

            //GAME LOAD

            if (selectedOption == 0)
            {
                pacman.Life = 3;
                pacman.Points = 0;

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

                ghost1.Move();

                bool exactCollision = pacman.CurrentPositionX == ghost1.CurrentPositionX && 
                                    pacman.CurrentPositionY == ghost1.CurrentPositionY;

                bool intersectionCollision = (pacman.CurrentPositionX == ghost1.PreviousPositionX && 
                                        pacman.CurrentPositionY == ghost1.PreviousPositionY) &&
                                        (pacman.PreviousPositionX == ghost1.CurrentPositionX && 
                                        pacman.PreviousPositionY == ghost1.CurrentPositionY);

                if (exactCollision || intersectionCollision)
                {
                    pacman.LoseLife();
                    
                    ghost1.ResetPosition();

                    Console.Clear();
                    renderer.DrawMap(); 
                    Thread.Sleep(500);  

                    // GAME OVER?
                    if (pacman.Life <= 0)
                    {
                        isRunning = false;
                    }
                }

                if (isRunning) 
                {
                    renderer.Draw(pacman);
                    renderer.Draw(ghost1);
                }

                Thread.Sleep(200);
            }

            // --- FIM DO LOOP  ---
            Console.Clear();
            if (pacman.Life <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("=== GAME OVER ===");
                Console.ResetColor();
                Console.WriteLine($"Sua pontuação final foi: {pacman.Points}");
                Thread.Sleep(2000); 

                SaveSystem.DeleteSave();
            }
            else
            {
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
}
