namespace ConsoleGame.Tiles
{
    class Clay : MaterialTileBreakOutputItem
    {
        static readonly char[,] texture = Constants.FromString(new string[] { "++ ", "+++", "+++" });

        public Clay(Map map, Position position) : base(map, position) { }

        public override char[,] Texture => texture;
        public override IItem BreakOutputItem => new Items.Clay();
    }
}