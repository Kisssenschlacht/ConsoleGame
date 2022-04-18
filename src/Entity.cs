namespace ConsoleGame
{
    public enum EntityType : byte
    {
        Cow,
        Sheep
    }
    public struct Entity
    {
        public EntityType Type;
        public int Health;
        public Positions Pos;
        public Entity(EntityType type, int health, Positions pos)
        {
            Type = type;
            Health = health;
            Pos = pos;
        }
    }
}