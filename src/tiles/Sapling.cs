namespace ConsoleGame.Tiles
{
    class Sapling : StagedMaterialTileBreakOutputItem
    {
        static readonly char[][,] textures = new char[][,] {
            Constants.FromString(new string[] { "   ", " + ", "   " }),
            Constants.FromString(new string[] {" . ", "_", " , "}),
            Constants.FromString(new string[] {"   ", "/_\\", " | "})
        };
        protected override int TotalStages => 4;
        protected override void FinalStageReached() { Map.Tiles[Position.x, Position.y] = new Tiles.Tree(Map, Position); }
        protected override int GrowthTimeInMilliseconds(int stage) => 10000;
        public Sapling(Map map, Position position) : base(map, position)
        {
        }
        public override char[][,] Textures => textures;
        public override IItem BreakOutputItem => new Items.Sapling();
    }
}