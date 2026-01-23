namespace PacMan
{
    public class GameSaveData
    {
        public int HighScore { get; set; }
        public int CurrentScore { get; set; }
        public int Lives { get; set; }
        public int Level { get; set; }

        public int PacManX { get; set; }
        public int PacManY { get; set; }

        public DateTime SaveDate { get; set; }
    }
}