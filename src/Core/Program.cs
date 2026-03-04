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

            List<Ghost> ghosts = new List<Ghost>
            {
                new Ghost(map) { Color = ConsoleColor.Red },         // Blinky
                new Ghost(map) { Color = ConsoleColor.Magenta },     // Pinky 
                new Ghost(map) { Color = ConsoleColor.Cyan },        // Inky
                new Ghost(map) { Color = ConsoleColor.DarkYellow }   // Clyde 
            };

            ConsoleRenderer renderer = new ConsoleRenderer(map);

            Console.CursorVisible = false;
            int selectedOption = menu.Show();

            //GAME LOAD

            if (selectedOption == 0) // Novo Jogo
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

                if (map.ConsumePoint(pacman.CurrentPositionY, pacman.CurrentPositionX))
                {
                    pacman.CollectPoint(10); 

                    if (map.RemainingPoints == 0)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("===========================");
                        Console.WriteLine("          YOU WON!         ");
                        Console.WriteLine("===========================");
                        Console.ResetColor();
                        Thread.Sleep(2000); 

                        isRunning = false; 
                        
                    }
                }

                foreach (Ghost ghost in ghosts)
                {
                    ghost.Move();
                }

                bool pacmanDied = false;

                foreach (Ghost ghost in ghosts)
                {
                    bool exactCollision = pacman.CurrentPositionX == ghost.CurrentPositionX && 
                                        pacman.CurrentPositionY == ghost.CurrentPositionY;

                    bool intersectionCollision = (pacman.CurrentPositionX == ghost.PreviousPositionX && 
                                                pacman.CurrentPositionY == ghost.PreviousPositionY) &&
                                                (pacman.PreviousPositionX == ghost.CurrentPositionX && 
                                                pacman.PreviousPositionY == ghost.CurrentPositionY);

                    if (exactCollision || intersectionCollision)
                    {
                        pacmanDied = true;
                        break; 
                    }
                
                }
                    if (pacmanDied)
                    {
                        pacman.LoseLife();
                        
                        foreach (Ghost ghost in ghosts)
                        {
                            ghost.ResetPosition();
                        }

                        Console.Clear();
                        renderer.DrawMap(); 
                        Thread.Sleep(500);  

                        if (pacman.Life <= 0)
                        {
                            isRunning = false;
                        }
                    }

                if (isRunning) 
                {
                    renderer.Draw(pacman);

                    foreach (Ghost ghost in ghosts)
                    {
                        renderer.Draw(ghost);
                    }

                    renderer.DrawHUD(pacman);
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
