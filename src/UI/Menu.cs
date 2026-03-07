using System;

namespace PacMan
{
    public class Menu
    {
        private string[] Options = { "Novo Jogo", "Continuar", "Sair" };
        private int SelectedOption = 0;

        private bool _saveExists;
        private int _baseStartY;

        public int Show()
        {
            _saveExists = SaveSystem.SaveFileExists();

            if (!_saveExists && SelectedOption == 1)
            {
                SelectedOption = 0;
            }

            Console.Clear();
            Console.CursorVisible = false;

            _baseStartY = Math.Max(2, (Console.WindowHeight - 24) / 2);

            DrawMenuArt();

            while (true)
            {
                DrawMenuOptions();

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.DownArrow:
                        do
                        {
                            SelectedOption++;
                            if (SelectedOption >= Options.Length) SelectedOption = 0;
                        }
                        while (SelectedOption == 1 && !_saveExists);
                        break;

                    case ConsoleKey.UpArrow:
                        do
                        {
                            SelectedOption--;
                            if (SelectedOption < 0) SelectedOption = Options.Length - 1;
                        }
                        while (SelectedOption == 1 && !_saveExists);
                        break;

                    case ConsoleKey.Enter:
                        Console.Clear();
                        return SelectedOption;
                }
            }
        }

        public void DrawMenuArt()
        {
            int currentY = _baseStartY;

            Console.ForegroundColor = ConsoleColor.Yellow;
            WriteCentered(@"██████╗  █████╗  ██████╗      ███╗   ███╗ █████╗ ███╗   ██╗", currentY++);
            WriteCentered(@"██╔══██╗██╔══██╗██╔════╝      ████╗ ████║██╔══██╗████╗  ██║", currentY++);
            WriteCentered(@"██████╔╝███████║██║           ██╔████╔██║███████║██╔██╗ ██║", currentY++);
            WriteCentered(@"██╔═══╝ ██╔══██║██║           ██║╚██╔╝██║██╔══██║██║╚██╗██║", currentY++);
            WriteCentered(@"██║     ██║  ██║╚██████╗      ██║ ╚═╝ ██║██║  ██║██║ ╚████║", currentY++);
            WriteCentered(@"╚═╝     ╚═╝  ╚═╝ ╚═════╝      ╚═╝     ╚═╝╚═╝  ╚═╝╚═╝  ╚═══╝", currentY++);

            currentY += 3; 

            int chaseSceneWidth = 66; 
            int startX = Math.Max(0, (Console.WindowWidth - chaseSceneWidth) / 2);

            Console.SetCursorPosition(startX, currentY);
            Console.ForegroundColor = ConsoleColor.Yellow; Console.Write(@"  CCCC                         ");
            Console.ForegroundColor = ConsoleColor.Red; Console.Write(@"/    \  ");
            Console.ForegroundColor = ConsoleColor.Magenta; Console.Write(@"/    \  ");
            Console.ForegroundColor = ConsoleColor.Cyan; Console.Write(@"/    \  ");
            Console.ForegroundColor = ConsoleColor.DarkYellow; Console.Write(@"/    \  ");

            currentY++;
            
            Console.SetCursorPosition(startX, currentY);
            Console.ForegroundColor = ConsoleColor.Yellow; Console.Write(@" CCC         .    .    .       ");
            Console.ForegroundColor = ConsoleColor.Red; Console.Write(@"|o o |  ");
            Console.ForegroundColor = ConsoleColor.Magenta; Console.Write(@"|o o |  ");
            Console.ForegroundColor = ConsoleColor.Cyan; Console.Write(@"|o o |  ");
            Console.ForegroundColor = ConsoleColor.DarkYellow; Console.Write(@"|o o |  ");

            currentY++;

            Console.SetCursorPosition(startX, currentY);
            Console.ForegroundColor = ConsoleColor.Yellow; Console.Write(@"  CCCC                         ");
            Console.ForegroundColor = ConsoleColor.Red; Console.Write(@"vv  vv  ");
            Console.ForegroundColor = ConsoleColor.Magenta; Console.Write(@"vv  vv  ");
            Console.ForegroundColor = ConsoleColor.Cyan; Console.Write(@"vv  vv  ");
            Console.ForegroundColor = ConsoleColor.DarkYellow; Console.Write(@"vv  vv  ");

        }

        public void DrawMenuOptions()
        {
            int optionsY = _baseStartY + 13; 

            for (int i = 0; i < Options.Length; i++)
            {
                string textToPrint = $"   {Options[i]}   ";
                if (i == SelectedOption) textToPrint = $" > {Options[i]} < ";

                int currentY = optionsY + (i * 2); 
                
                int centerX = (Console.WindowWidth / 2) - (textToPrint.Length / 2);
                Console.SetCursorPosition(centerX, currentY);

                if (i == 1 && !_saveExists)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }
                else if (i == SelectedOption)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Yellow;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }

                Console.Write(textToPrint);
                Console.ResetColor();
            }
        }

        private void WriteCentered(string text, int y)
        {
            int centerX = (Console.WindowWidth / 2) - (text.Length / 2);
            if (centerX < 0) centerX = 0; 
            
            Console.SetCursorPosition(centerX, y);
            Console.Write(text);
        }
    }
}