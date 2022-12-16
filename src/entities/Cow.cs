namespace ConsoleGame.Entities
{
    class Cow : PeacefulMob
    {
        public Cow(Map map, Position position) : base(map, position) { }
        static readonly char[,] texture = Constants.FromString(new string[] { "   ", "@__", "| |" });
        public override char[,] Texture => texture;
        int _health = 6;
        public override int Health { get => _health; set => _health = value; }
    }
}