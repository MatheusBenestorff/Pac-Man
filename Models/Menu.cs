namespace PacMan
{
    public class Menu
    {
        private string[] Options = { "Novo Jogo", "Continuar", "Sair" };
        private int SelectedOption = 0;

        public int Show()
        {
            Console.Clear();
            Console.CursorVisible = false;

            DrawMenuArt();

            while (true)
            {
                DrawMenuOptions();

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                //PROCESSAR A TECLA PRESSIONADA
                switch (keyInfo.Key)
                {
                    case ConsoleKey.DownArrow:
                        SelectedOption++;
                        if (SelectedOption >= Options.Length)
                        {
                            SelectedOption = 0;
                        }
                        break;

                    case ConsoleKey.UpArrow:
                        SelectedOption--;
                        if (SelectedOption < 0)
                        {
                            SelectedOption = Options.Length - 1;
                        }
                        break;

                    case ConsoleKey.Enter:
                        Console.Clear();
                        return SelectedOption;
                }
            }
        }

        public void DrawMenuArt()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(@"    _ __   __ _  ___ _ __ ___   __ _ _ __  ");
            Console.WriteLine(@"   | '_ \ / _` |/ __| '_ ` _ \ / _` | '_ \ ");
            Console.WriteLine(@"   | |_) | (_| | (__| | | | | | (_| | | | |");
            Console.WriteLine(@"   | .__/ \__,_|\___|_| |_| |_|\__,_|_| |_|");
            Console.WriteLine(@"   |_|                                    ");
            Console.ResetColor();

            Console.SetCursorPosition(5, 12);
            Console.ForegroundColor = ConsoleColor.White;
            // Console.Write("Use as setas (Cima/Baixo) e pressione Enter.");
        }
        public void DrawMenuOptions()
        {
            for (int i = 0; i < Options.Length; i++)
            {
                Console.SetCursorPosition(15, 7 + i);

                if (i == SelectedOption)
                {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.Write($"> {Options[i]} <");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write($"  {Options[i]}  ");
                }
                Console.ResetColor();
            }
        }
    }
}