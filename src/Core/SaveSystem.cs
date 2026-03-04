using System.Text.Json;

namespace PacMan
{
    public static class SaveSystem
    {
        private const string FILE_NAME = "savegame.json";

        public static void SaveGame(GameSaveData data)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };

            string jsonString = JsonSerializer.Serialize(data, options);

            File.WriteAllText(FILE_NAME, jsonString);
        }

        public static GameSaveData LoadGame()
        {
            if (!File.Exists(FILE_NAME))
            {
                return null;
            }

            string jsonString = File.ReadAllText(FILE_NAME);

            return JsonSerializer.Deserialize<GameSaveData>(jsonString);
        }

        public static bool SaveFileExists()
        {
            return File.Exists(FILE_NAME);
        }

        public static bool IsHighScore(int currentScore)
        {
            GameSaveData data = LoadGame();

            if (currentScore > data.HighScore) return true;
            else return false;
        }

        public static void DeleteSave()
        {
            if (File.Exists(FILE_NAME))
            {
                File.Delete(FILE_NAME);
            }
        }
    }
}

