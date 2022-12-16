namespace ConsoleGame
{
    abstract class Entity : IUpdatable, IHealth, IPosition, ITexture
    {
        public Map Map { get; init; }
        public abstract int Health { get; set; }
        public Position Position { get; set; }
        public abstract char[,] Texture { get; }
        public virtual void Update(TimeSpan elapsedTime) { }
        protected Entity(Map map, Position position)
        {
            Map = map;
            Position = position;
        }
    }
}