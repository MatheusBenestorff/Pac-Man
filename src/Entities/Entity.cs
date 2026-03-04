namespace PacMan
{
    public class Entity
    {
        public int CurrentPositionX { get; set; } 
        public int CurrentPositionY { get; set; } 
        public int PreviousPositionX  { get; set; } 
        public int PreviousPositionY  { get; set; } 
        public int SpawnPositionX { get; set; }
        public int SpawnPositionY { get; set; }
        public EntityState State { get; set; }
        public string Symbol { get; set; }
        public ConsoleColor Color { get; set; } = ConsoleColor.White;

        public enum Direction { Up, Down, Left, Right, None }
        public Direction CurrentDirection { get; set; } = Direction.None;


        public enum EntityState
        {
            Normal,
            Vulnerable,
            Eaten
        }

        public virtual void Move()
        {
            
        }

        public bool IsCollidingWith(Entity other)
        {
            bool exactCollision = this.CurrentPositionX == other.CurrentPositionX && 
                                this.CurrentPositionY == other.CurrentPositionY;

            bool intersectionCollision = (this.CurrentPositionX == other.PreviousPositionX && 
                                        this.CurrentPositionY == other.PreviousPositionY) &&
                                        (this.PreviousPositionX == other.CurrentPositionX && 
                                        this.PreviousPositionY == other.CurrentPositionY);

            return exactCollision || intersectionCollision;
        }

    }
}