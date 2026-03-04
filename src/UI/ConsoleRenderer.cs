namespace PacMan
{
    public class ConsoleRenderer
    {
        private readonly Map _map;

        public ConsoleRenderer(Map map)
        {
            _map = map;
        }

        public void DrawMap()
        {
            Console.SetCursorPosition(0, 0);
            _map.Draw();
        }

        public void Draw(Entity entity) 
        {
            string tileAtOldPosition = _map.GetTileAt(entity.PreviousPositionY, entity.PreviousPositionX);

            Console.SetCursorPosition(entity.PreviousPositionX * 2, entity.PreviousPositionY);
            Console.ResetColor(); 
            Console.Write(tileAtOldPosition);
            
            Console.SetCursorPosition(entity.CurrentPositionX * 2, entity.CurrentPositionY);
            Console.ForegroundColor = entity.Color; 
            Console.Write(entity.Symbol);
            Console.ResetColor();
        }
    }
}