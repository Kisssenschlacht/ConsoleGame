namespace ConsoleGame.Tiles
{
    class Stone : MaterialTileBreakOutputItem
    {
        static readonly char[,] texture = Constants.FromString(new string[] { " # ", "## ", "###" });

        public Stone(Map map, Position position) : base(map, position) { }

        public override char[,] Texture => texture;
        public override IItem BreakOutputItem => new Items.Stone();
    }
}