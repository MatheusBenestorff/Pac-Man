namespace PacMan
{
    public class Entity
    {
        public int CurrentPositionX { get; set; } 
        public int CurrentPositionY { get; set; } 
        public int SpawnPositionX { get; set; }
        public int SpawnPositionY { get; set; }
        public EntityState State { get; set; }
        public char Symbol { get; set; }


        public enum EntityState
        {
            Normal,
            Vulnerable,
            Eaten
        }

        public virtual void Move()
        {
            
        }

    }
}