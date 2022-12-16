namespace ConsoleGame.Tiles
{
    class Grass : StagedMaterialTileBreakOutputItem
    {
        static readonly char[][,] textures = new char[][,] {
            Constants.FromString(new string[] { "   ", " \" ", "   " }),
            Constants.FromString(new string[] {" \" ", "\"\"\"", " \" " }),
            Constants.FromString(new string[] {"\"\"\"", "\"\"\"", "\"\"\"" })
        };
        protected override int TotalStages => 3;
        protected override int GrowthTimeInMilliseconds(int _) => 10000;
        public Grass(Map map, Position position) : base(map, position)
        {
        }
        public override char[][,] Textures => textures;
        public override IItem BreakOutputItem => new Items.Sapling();
    }
}