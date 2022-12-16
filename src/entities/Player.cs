namespace ConsoleGame.Entities
{
    class Player : InventoryEntity
    {
        static readonly char[,] texture = Constants.FromString(new string[] { " @ ", "_|_", "| |" });
        public Player(Map map, Position position) : base(map, position) { }
        public override char[,] Texture => texture;
        int _health = 10;
        public override int Health { get => _health; set => _health = value; }
    }
}