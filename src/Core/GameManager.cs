namespace PacMan
{
    public class GameManager
    {
        private PacMan _pacman;
        private int _currentLevel;
        private bool _isGameRunning;

        private int _savedX = -1;
        private int _savedY = -1;

        public void Start()
        {
            Console.Clear();
            Console.CursorVisible = false;

            Menu menu = new Menu();
            int selectedOption = menu.Show();

            if (selectedOption == 2) return; // Sair

            InitializeGameState(selectedOption);

            _isGameRunning = true;

            while (_isGameRunning)
            {
                bool wonLevel = PlayLevel(_currentLevel);

                if (wonLevel)
                {
                    _currentLevel++; 

                }
                else
                {
                    _isGameRunning = false;
                }
            }
            

            HandleGameOverOrSave();
        }

        private void InitializeGameState(int selectedOption)
        {
            _currentLevel = 1; 

            _pacman = new PacMan(new Map("maps/level1.pacmap"));

            if (selectedOption == 0) // Novo Jogo
            {
                _pacman.Life = 3;
                _pacman.Points = 0;
            }
            else if (selectedOption == 1) // Continuar
            {
                GameSaveData data = SaveSystem.LoadGame();
                _pacman.Points = data.CurrentScore;
                _pacman.Life = data.Lives;
                _currentLevel = data.Level > 0 ? data.Level : 1; 

                _savedX = data.PacManX;
                _savedY = data.PacManY;
            }
        }

        // Roda UMA fase inteira e retorna TRUE se ele limpou o mapa, e FALSE se morreu/saiu
        private bool PlayLevel(int levelNumber)
        {
            string mapPath = $"maps/level{levelNumber}.pacmap";
            
            if (!File.Exists(mapPath))
            {
                Console.Clear();
                Console.WriteLine("Parabéns! Você zerou o jogo!");
                Thread.Sleep(3000);
                return false; 
            }

            Map map = new Map(mapPath);
            ConsoleRenderer renderer = new ConsoleRenderer(map);
            
            _pacman.SetMap(map); 
            if (_savedX != -1 && _savedY != -1)
            {
                _pacman.CurrentPositionX = _savedX;
                _pacman.CurrentPositionY = _savedY;
                
                _savedX = -1; 
                _savedY = -1;
            }
            else
            {
                _pacman.CurrentPositionX = map.PlayerSpawnX;
                _pacman.CurrentPositionY = map.PlayerSpawnY;
            }

            List<Ghost> ghosts = CreateGhosts(map);

            renderer.DrawMap();
            bool isLevelRunning = true;
            bool levelCleared = false;

            while (isLevelRunning)
            {
                //PROCESSAR INPUT
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);

                    // Se apertar ESC, sai do jogo e salva
                    if (key.Key == ConsoleKey.Escape)
                    {
                        Console.Clear();
                        isLevelRunning = false;
                    }
                    else
                    {
                        _pacman.HandleInput(key.Key);
                    }
                }

                //ATUALIZAR ESTADO DO JOGO
                _pacman.Move();

                if (map.ConsumePoint(_pacman.CurrentPositionY, _pacman.CurrentPositionX))
                {
                    _pacman.CollectPoint(10); 

                    if (map.RemainingPoints == 0)
                    {
                        renderer.ShowWinScreen();

                        levelCleared = true;
                        isLevelRunning = false; 
                        
                    }
                }

                foreach (Ghost ghost in ghosts)
                {
                    ghost.Move();
                }

                bool pacmanDied = false;

                foreach (Ghost ghost in ghosts)
                {
                    if (_pacman.IsCollidingWith(ghost))
                    {
                        pacmanDied = true;
                        break; 
                    }
                
                }

                if (pacmanDied)
                {
                    _pacman.LoseLife();
                    
                    foreach (Ghost ghost in ghosts)
                    {
                        ghost.ResetPosition();
                    }

                    Console.Clear();
                    renderer.DrawMap(); 
                    Thread.Sleep(500);  

                    if (_pacman.Life <= 0)
                    {
                        isLevelRunning = false;
                    }
                }

                if (isLevelRunning) 
                {
                    renderer.DrawGame(_pacman, ghosts, levelNumber); 
                }

                Thread.Sleep(200);
            }

            return levelCleared;
        }

        private void HandleGameOverOrSave()
        {
            Console.Clear();
            
            if (_pacman.Life <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("=== GAME OVER ===");
                Console.ResetColor();
                Console.WriteLine($"Sua pontuação final foi: {_pacman.Points}");
                Thread.Sleep(2000); 

                SaveSystem.DeleteSave();
            }
            else
            {
                //GAME SAVE
                GameSaveData dataToSave = new GameSaveData
                {
                    CurrentScore = _pacman.Points,
                    Lives = _pacman.Life,
                    PacManX = _pacman.CurrentPositionX,
                    PacManY = _pacman.CurrentPositionY,
                    SaveDate = DateTime.Now,
                    Level = _currentLevel
                };

                if (SaveSystem.IsHighScore(dataToSave.CurrentScore))
                {
                    dataToSave.HighScore = dataToSave.CurrentScore;
                }

                SaveSystem.SaveGame(dataToSave);
                
            }
        }

        private List<Ghost> CreateGhosts(Map map)
        {
            return new List<Ghost>
            {
                new Ghost(map) { Color = ConsoleColor.Red },
                new Ghost(map) { Color = ConsoleColor.Magenta },
                new Ghost(map) { Color = ConsoleColor.Cyan },
                new Ghost(map) { Color = ConsoleColor.DarkYellow }
            };
        }
    }
}