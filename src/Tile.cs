namespace ConsoleGame
{
    abstract class Tile : IBreakable, IPosition, IUpdatable, ITexture
    {
        public virtual bool IsObstacle => false;
        public Map Map { get; init; }
        Position position;
        public Position Position { get => position; set { Map.Tiles[value.x, value.y] = this; Map.Tiles[position.x, position.y] = null; position = value; } }
        public abstract char[,] Texture { get; }
        protected Tile(Map map, Position position)
        {
            Map = map;
            Position = position;
        }
        public abstract void Break(Entity entity);
        public virtual void Update(TimeSpan elapsedTime) { }
    }
}