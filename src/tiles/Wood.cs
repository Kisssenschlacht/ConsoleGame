namespace ConsoleGame.Tiles
{
    class Wood : MaterialTileBreakOutputItem
    {
        static readonly char[,] texture = Constants.FromString(new string[] { "OOO", "OOO", "OOO" });

        public Wood(Map map, Position position) : base(map, position) { }

        public override char[,] Texture => texture;
        public override IItem BreakOutputItem => new Items.Wood();
    }
}