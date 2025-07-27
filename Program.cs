namespace PacMan
{
    class Program
    {
        static void Main()
        {
            Console.Clear();
            //Instânciando o Mapa
            Map map = new Map();

            //Instânciando o PacMan e injetando o Mapa
            PacMan pacman = new PacMan(map);

            ConsoleRenderer renderer = new ConsoleRenderer(map);


            Console.CursorVisible = false;

            renderer.DrawMap(); 

            bool isRunning = true;

            // --- GAME LOOP ---
            while (isRunning)
            {
                //PROCESSAR INPUT
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    pacman.HandleInput(key.Key);
                }

                //ATUALIZAR ESTADO DO JOGO
                pacman.Move();


                //RENDERIZAR A TELA
                renderer.Draw(pacman);

                //CONTROLAR A VELOCIDADE DO JOGO
                Thread.Sleep(200);
            }

        }
    }
}
