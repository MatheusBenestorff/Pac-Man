using System;

namespace PacMan
{
    public static class GameScreens
    {
        public static void ShowGameOverScreen(int finalScore)
        {
            Console.Clear();
            int startY = Math.Max(2, (Console.WindowHeight - 15) / 2);

            Console.ForegroundColor = ConsoleColor.Red;
            WriteCentered(@"  ____    _    __  __ _____    ____ __     __ _____  ____  ", startY++);
            WriteCentered(@" / ___|  / \  |  \/  | ____|  / __ \\ \   / /| ____||  _ \ ", startY++);
            WriteCentered(@"| |  _  / _ \ | |\/| |  _|   | |  | |\ \ / / |  _|  | |_) |", startY++);
            WriteCentered(@"| |_| |/ ___ \| |  | | |___  | |__| | \ V /  | |___ |  _ < ", startY++);
            WriteCentered(@" \____/_/   \_\_|  |_|_____|  \____/   \_/   |_____||_| \_\", startY++);
            
            startY += 3;
            Console.ForegroundColor = ConsoleColor.White;
            WriteCentered($"Sua pontuação final foi: {finalScore}", startY++);
            
            startY += 3;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            WriteCentered("Pressione ENTER para voltar ao Menu...", startY++);
            
            Console.ResetColor();
            Console.ReadLine(); 
            Console.Clear();
        }

        public static void ShowWinScreen(int score, int level)
        {
            Console.Clear();
            int startY = Math.Max(2, (Console.WindowHeight - 15) / 2);

            Console.ForegroundColor = ConsoleColor.Green;
            WriteCentered(@"__   _____  _   _  __        _____  _   _ _ ", startY++);
            WriteCentered(@"\ \ / / _ \| | | | \ \      / / _ \| \ | | |", startY++);
            WriteCentered(@" \ V / | | | | | |  \ \ /\ / / | | |  \| | |", startY++);
            WriteCentered(@"  | || |_| | |_| |   \ V  V /| |_| | |\  |_|", startY++);
            WriteCentered(@"  |_| \___/ \___/     \_/\_/  \___/|_| \_(_)", startY++);
            
            startY += 3;
            Console.ForegroundColor = ConsoleColor.White;
            WriteCentered($"Fase {level} Concluída!", startY++);
            WriteCentered($"Pontuação Atual: {score}", startY++);
            
            startY += 3;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            WriteCentered("Pressione ENTER para iniciar a próxima fase...", startY++);
            
            Console.ResetColor();
            Console.ReadLine();
            Console.Clear();
        }

        public static void ShowGameCompletedScreen(int finalScore)
        {
            Console.Clear();
            int startY = Math.Max(2, (Console.WindowHeight - 15) / 2);

            Console.ForegroundColor = ConsoleColor.Cyan;
            WriteCentered(@" _____ _   _ _____   _____ _   _ ____  _ ", startY++);
            WriteCentered(@"|_   _| | | | ____| | ____| \ | |  _ \| |", startY++);
            WriteCentered(@"  | | | |_| |  _|   |  _| |  \| | | | | |", startY++);
            WriteCentered(@"  | | |  _  | |___  | |___| |\  | |_| |_|", startY++);
            WriteCentered(@"  |_| |_| |_|_____| |_____|_| \_|____/(_)", startY++);

            startY += 3;
            Console.ForegroundColor = ConsoleColor.Yellow;
            WriteCentered("PARABÉNS! VOCÊ ZEROU O JOGO!", startY++);
            
            Console.ForegroundColor = ConsoleColor.White;
            WriteCentered($"Pontuação Final: {finalScore}", startY + 1);

            startY += 4;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            WriteCentered("Pressione ENTER para voltar ao Menu...", startY++);
            
            Console.ResetColor();
            Console.ReadLine();
            Console.Clear();
        }

        private static void WriteCentered(string text, int y)
        {
            int centerX = (Console.WindowWidth / 2) - (text.Length / 2);
            if (centerX < 0) centerX = 0; 
            
            Console.SetCursorPosition(centerX, y);
            Console.Write(text);
        }
    }
}