namespace PacMan
{
    public class Ghost : Entity
    {
        private readonly Map _gameMap;

        public Ghost(Map gameMap)
        {
            this._gameMap = gameMap;
            
            this.SpawnPositionX = _gameMap.EnemySpawnX;
            this.SpawnPositionY = _gameMap.EnemySpawnY;
            
            this.CurrentPositionX = this.SpawnPositionX;
            this.CurrentPositionY = this.SpawnPositionY;
            
            this.Symbol = "M"; 
            this.State = EntityState.Normal;
        }

        public override void Move()
        {
        }
    }
}